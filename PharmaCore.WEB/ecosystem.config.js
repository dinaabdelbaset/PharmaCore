module.exports = {
  apps: [
    {
      name: "pharmacore-web",
      script: "node_modules/next/dist/bin/next",
      args: "start -p 3003",
      instances: "max",
      exec_mode: "cluster",
      env: {
        NODE_ENV: "production"
      }
    }
  ]
};
