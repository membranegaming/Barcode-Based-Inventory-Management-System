@echo off
REM ============================================
REM Store Parts - Build and Package Script
REM ============================================
REM This script builds the project in Release mode
REM and creates a distributable package
REM ============================================

echo ============================================
echo Store Parts - Build and Package Script
echo ============================================
echo.

REM Set paths - ADJUST THESE IF NEEDED
set PROJECT_DIR=%~dp0store parts
set SOLUTION_FILE=%PROJECT_DIR%\store parts.sln
set OUTPUT_DIR=%~dp0Distributable
set RELEASE_DIR=%PROJECT_DIR%\bin\Release

REM Check if MSBuild is available
where msbuild >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo MSBuild not found in PATH.
    echo.
    echo Please run this script from:
    echo   - Developer Command Prompt for Visual Studio
    echo   - Or add MSBuild to your PATH
    echo.
    echo Alternatively, build manually in Visual Studio:
    echo   1. Open the solution in Visual Studio
    echo   2. Set Configuration to "Release"
    echo   3. Build ^> Build Solution
    echo.
    pause
    exit /b 1
)

echo Step 1: Cleaning previous build...
if exist "%RELEASE_DIR%" rd /s /q "%RELEASE_DIR%"
if exist "%OUTPUT_DIR%" rd /s /q "%OUTPUT_DIR%"

echo Step 2: Building Release configuration...
msbuild "%SOLUTION_FILE%" /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild /v:minimal

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo BUILD FAILED! Please check for errors above.
    pause
    exit /b 1
)

echo.
echo Step 3: Creating distributable package...

REM Create output directory
mkdir "%OUTPUT_DIR%"
mkdir "%OUTPUT_DIR%\Store Parts"

REM Copy release files
echo Copying application files...
xcopy "%RELEASE_DIR%\*.exe" "%OUTPUT_DIR%\Store Parts\" /Y
xcopy "%RELEASE_DIR%\*.dll" "%OUTPUT_DIR%\Store Parts\" /Y
xcopy "%RELEASE_DIR%\*.config" "%OUTPUT_DIR%\Store Parts\" /Y
if exist "%RELEASE_DIR%\*.xml" xcopy "%RELEASE_DIR%\*.xml" "%OUTPUT_DIR%\Store Parts\" /Y

REM Create README file
echo Creating README...
(
echo ============================================
echo Store Parts Management System
echo Version 1.0.0
echo ============================================
echo.
echo INSTALLATION INSTRUCTIONS:
echo.
echo 1. Ensure .NET Framework 4.7.2 is installed
echo    Download from: https://dotnet.microsoft.com/download/dotnet-framework/net472
echo.
echo 2. Copy the entire "Store Parts" folder to your desired location
echo    Recommended: C:\Program Files\Store Parts\
echo.
echo 3. Create a shortcut to "store parts.exe" on your desktop
echo.
echo 4. Configure your database connection if needed
echo.
echo DEFAULT LOGIN:
echo    Username: admin
echo    Password: admin
echo.
echo For support, contact: support@yourcompany.com
echo ============================================
) > "%OUTPUT_DIR%\Store Parts\README.txt"

REM Create ZIP file if 7-Zip is available
where 7z >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo Creating ZIP archive...
    cd "%OUTPUT_DIR%"
    7z a -tzip "StorePartsPortable_v1.0.0.zip" "Store Parts\*" -r
    echo.
    echo ZIP file created: %OUTPUT_DIR%\StorePartsPortable_v1.0.0.zip
) else (
    echo.
    echo Note: 7-Zip not found. Please manually zip the folder or install 7-Zip.
)

echo.
echo ============================================
echo BUILD COMPLETE!
echo ============================================
echo.
echo Output location: %OUTPUT_DIR%
echo.
echo Files created:
dir /b "%OUTPUT_DIR%\Store Parts\"
echo.
echo Next steps:
echo   1. Test the application from: %OUTPUT_DIR%\Store Parts\store parts.exe
echo   2. Create installer using Inno Setup with: StorePartsSetup.iss
echo   3. Or distribute the "Store Parts" folder as a portable app
echo.
pause
