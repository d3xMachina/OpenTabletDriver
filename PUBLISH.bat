@echo off
set "options=--nologo --configuration Release -p:PublishSingleFile=true --self-contained false"
set "runtime=win10-x64"
set "framework=net6.0"
set "frameworkwindows=net6.0-windows"

dotnet publish OpenTabletDriver.Daemon %options% --runtime %runtime% --framework %frameworkwindows% -o build/%runtime%
dotnet publish OpenTabletDriver.Console %options% --runtime %runtime% --framework %framework% -o build/%runtime%
dotnet publish OpenTabletDriver.UX.Wpf %options% --runtime %runtime% --framework %frameworkwindows% -o build/%runtime%
rem robocopy "OpenTabletDriver.Configurations/" "build/%runtime%/Configurations/" /s /e >nul
pause

