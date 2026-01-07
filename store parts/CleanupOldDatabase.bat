@echo off
REM Clean up old database copies from bin\Debug folder
REM Run this script AFTER closing Visual Studio

echo ====================================
echo Database Cleanup Script
echo ====================================
echo.
echo This script will remove old database copies from bin\Debug folder.
echo Make sure Visual Studio is CLOSED before continuing!
echo.
pause

set BIN_DEBUG_PATH="%~dp0store parts\bin\Debug"

if exist %BIN_DEBUG_PATH%\MainDB.mdf (
    echo Deleting MainDB.mdf from bin\Debug...
    del /F %BIN_DEBUG_PATH%\MainDB.mdf
    echo Done.
) else (
    echo MainDB.mdf not found in bin\Debug.
)

if exist %BIN_DEBUG_PATH%\MainDB_log.ldf (
    echo Deleting MainDB_log.ldf from bin\Debug...
    del /F %BIN_DEBUG_PATH%\MainDB_log.ldf
    echo Done.
) else (
    echo MainDB_log.ldf not found in bin\Debug.
)

echo.
echo ====================================
echo Cleanup complete!
echo ====================================
echo.
echo You can now open Visual Studio and run the application.
echo All data will be saved to the project folder database.
echo.
pause
