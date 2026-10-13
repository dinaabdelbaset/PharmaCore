#!/bin/bash
# Exit on error
set -e

echo "=== PharmaCore Web Deployment Script ==="

# 1. Directory Setup
DEPLOY_DIR="/var/www/PharmaCore.WEB"
if [ ! -d "$DEPLOY_DIR" ]; then
    echo "Creating deployment directory..."
    mkdir -p "$DEPLOY_DIR"
fi

cd "$DEPLOY_DIR"

# 2. Dependency installation
echo "Installing dependencies..."
npm install

# 3. Build the application
echo "Building the application..."
npm run build

# 4. PM2 Setup
echo "Setting up PM2 process..."
# Check if pm2 process already exists
if pm2 show pharmacore-web >/dev/null 2>&1; then
    echo "PM2 process exists, restarting..."
    pm2 restart pharmacore-web --update-env
else
    echo "Creating new PM2 process on port 3003..."
    pm2 start npm --name "pharmacore-web" -- run start -- -p 3003
fi

# Save PM2 list so it survives reboot
pm2 save

# 5. Nginx Configuration Setup
echo "Configuring Nginx..."
DOMAIN="${CUSTOM_DOMAIN:-pharmacore.local}"
NGINX_CONF="/etc/nginx/sites-available/$DOMAIN"
NGINX_LINK="/etc/nginx/sites-enabled/$DOMAIN"

cat << EOF > "$NGINX_CONF"
server {
    listen 80;
    listen [::]:80;
    server_name $DOMAIN www.$DOMAIN;
    return 301 https://\$host\$request_uri;
}

server {
    listen 443 ssl;
    listen [::]:443 ssl;
    server_name $DOMAIN www.$DOMAIN;

    ssl_certificate /etc/letsencrypt/live/$DOMAIN/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/$DOMAIN/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;

    location /api {
        proxy_pass http://127.0.0.1:5001;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    location / {
        proxy_pass http://localhost:3003;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
EOF

# Enable configuration if not already enabled
if [ ! -L "$NGINX_LINK" ]; then
    echo "Enabling Nginx site..."
    ln -s "$NGINX_CONF" "$NGINX_LINK"
fi

# Test Nginx and reload
echo "Testing Nginx config..."
nginx -t

echo "Reloading Nginx..."
systemctl reload nginx

echo "=== Deployment Completed Successfully ==="
