@echo off
REM ============================================
REM Store Parts Database Backup Script
REM ============================================
REM Run this script daily to backup your database
REM Schedule using Windows Task Scheduler
REM ============================================

setlocal

REM Configuration - MODIFY THESE VALUES
set SERVER_NAME=.\SQLEXPRESS
set DATABASE_NAME=MainDB
set BACKUP_FOLDER=C:\DatabaseBackups\StoreParts
set DAYS_TO_KEEP=30

REM Create backup folder if it doesn't exist
if not exist "%BACKUP_FOLDER%" mkdir "%BACKUP_FOLDER%"

REM Generate timestamp for filename
set TIMESTAMP=%date:~-4%%date:~4,2%%date:~7,2%_%time:~0,2%%time:~3,2%%time:~6,2%
set TIMESTAMP=%TIMESTAMP: =0%

REM Set backup filename
set BACKUP_FILE=%BACKUP_FOLDER%\%DATABASE_NAME%_%TIMESTAMP%.bak

echo ============================================
echo Store Parts Database Backup
echo ============================================
echo Date/Time: %date% %time%
echo Server: %SERVER_NAME%
echo Database: %DATABASE_NAME%
echo Backup File: %BACKUP_FILE%
echo ============================================
echo.

REM Perform backup using sqlcmd
echo Creating backup...
sqlcmd -S %SERVER_NAME% -E -Q "BACKUP DATABASE [%DATABASE_NAME%] TO DISK='%BACKUP_FILE%' WITH FORMAT, INIT, NAME='%DATABASE_NAME% Full Backup', SKIP, NOREWIND, NOUNLOAD, STATS=10"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Backup completed successfully!
    echo File: %BACKUP_FILE%
    
    REM Get file size
    for %%A in ("%BACKUP_FILE%") do echo Size: %%~zA bytes
) else (
    echo.
    echo ERROR: Backup failed! Error code: %ERRORLEVEL%
    echo Please check SQL Server is running and accessible.
)

echo.
echo ============================================
echo Cleaning up old backups (older than %DAYS_TO_KEEP% days)...
echo ============================================

REM Delete backups older than specified days
forfiles /p "%BACKUP_FOLDER%" /s /m *.bak /d -%DAYS_TO_KEEP% /c "cmd /c del @path && echo Deleted: @file" 2>nul

echo.
echo Backup process complete.
echo ============================================

REM Log to file
echo %date% %time% - Backup completed: %BACKUP_FILE% >> "%BACKUP_FOLDER%\backup_log.txt"

endlocal
pause
