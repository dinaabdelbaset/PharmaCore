@echo off
title PharmaCore 1-Click Runner
echo ========================================================
echo              PharmaCore Local Ecosystem
echo ========================================================
echo.
echo [1/2] Starting PharmaCore Backend API on http://localhost:5117 ...
start "PharmaCore API" cmd /k "cd /d %~dp0PharmaCore.API && dotnet run --launch-profile http"

echo [2/2] Starting PharmaCore Web Dashboard on http://localhost:3000 ...
start "PharmaCore Web" cmd /k "cd /d %~dp0PharmaCore.WEB && npm run dev"

echo.
echo ========================================================
echo All services launched!
echo API: http://localhost:5117/swagger
echo Web: http://localhost:3000
echo ========================================================
pause
