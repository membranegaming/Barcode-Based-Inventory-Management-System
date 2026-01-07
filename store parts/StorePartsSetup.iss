; Store Parts Management System - Inno Setup Installer Script
; ============================================================
; Version: 2.2.0
; Last Updated: December 2024
;
; To use this script:
; 1. Download Inno Setup from https://jrsoftware.org/isdl.php
; 2. Build your project in Release mode first
; 3. Open this file in Inno Setup Compiler
; 4. Press F9 to compile the installer
; ============================================================

#define MyAppName "Store Parts Management"
#define MyAppVersion "2.2.0"
#define MyAppPublisher "Vardh Chhajer"
#define MyAppURL "https://vardh.com"
#define MyAppExeName "store parts.exe"
#define MyAppDescription "Inventory management system for machinery parts with barcode printing and usage tracking"

[Setup]
; Application information
AppId={{8A2B3C4D-5E6F-7890-ABCD-EF1234567890}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
AppComments={#MyAppDescription}

; Installation settings
DefaultDirName={autopf}\Store Parts
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
AllowNoIcons=yes

; Output settings
OutputDir=InstallerOutput
OutputBaseFilename=StorePartsSetup_v{#MyAppVersion}

; Compression
Compression=lzma2/ultra64
SolidCompression=yes

; Visual settings
WizardStyle=modern
; Uncomment these if you have icon files:
; SetupIconFile=Resources\logo.ico
; WizardImageFile=Resources\wizard-logo.bmp
; WizardSmallImageFile=Resources\wizard-logo-small.bmp

; Privileges (change to 'lowest' for per-user install)
PrivilegesRequired=admin

; Uninstaller
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}

; Minimum Windows version (Windows 7 SP1 or later for .NET 4.7.2)
MinVersion=6.1sp1

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce

[Files]
; Main application files - from Release folder
Source: "bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "bin\Release\*.config"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "bin\Release\*.xml"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

; SQL Scripts for database setup
Source: "DatabaseSetup.sql"; DestDir: "{app}\SQL"; Flags: ignoreversion
Source: "FixDockerSchema.sql"; DestDir: "{app}\SQL"; Flags: ignoreversion skipifsourcedoesntexist

; Documentation files
Source: "Steps.txt"; DestDir: "{app}\Documentation"; Flags: ignoreversion
Source: "DATABASE_SETUP_GUIDE.md"; DestDir: "{app}\Documentation"; Flags: ignoreversion skipifsourcedoesntexist
Source: "USAGE_HISTORY_UPDATE.md"; DestDir: "{app}\Documentation"; Flags: ignoreversion skipifsourcedoesntexist
Source: "INSTALLER_GUIDE.md"; DestDir: "{app}\Documentation"; Flags: ignoreversion skipifsourcedoesntexist

[Dirs]
; Create directories
Name: "{app}\Documentation"
Name: "{app}\SQL"

[Icons]
; Start Menu shortcuts
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Comment: "Launch {#MyAppName}"
Name: "{group}\Documentation"; Filename: "{app}\Documentation"; Comment: "View Documentation"
Name: "{group}\SQL Scripts"; Filename: "{app}\SQL"; Comment: "Database Setup Scripts"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

; Desktop shortcut
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon; Comment: "Launch {#MyAppName}"

[Run]
; Option to run application after installation
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
// Check for .NET Framework 4.7.2
function IsDotNetDetected(): Boolean;
var
    release: Cardinal;
begin
    Result := False;
    if RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', release) then
    begin
        // 461808 = .NET Framework 4.7.2
        Result := (release >= 461808);
    end;
end;

function InitializeSetup(): Boolean;
begin
    Result := True;
    
    if not IsDotNetDetected() then
    begin
        MsgBox('This application requires .NET Framework 4.7.2 or later.' + #13#10 + #13#10 +
               'Please download and install it from:' + #13#10 +
               'https://dotnet.microsoft.com/download/dotnet-framework/net472' + #13#10 + #13#10 +
               'Setup will now exit.', mbError, MB_OK);
        Result := False;
    end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
    if CurStep = ssPostInstall then
    begin
        // Post-installation tasks can be added here
    end;
end;

[Registry]
; Application registry entries
Root: HKLM; Subkey: "SOFTWARE\{#MyAppPublisher}\{#MyAppName}"; ValueType: string; ValueName: "InstallPath"; ValueData: "{app}"; Flags: uninsdeletekey
Root: HKLM; Subkey: "SOFTWARE\{#MyAppPublisher}\{#MyAppName}"; ValueType: string; ValueName: "Version"; ValueData: "{#MyAppVersion}"

[Messages]
WelcomeLabel2=This will install [name/ver] on your computer.%n%nStore Parts Management v2.2 includes:%n- Parts inventory tracking%n- Usage history with full audit trail%n- Barcode label printing%n- Dashboard with analytics%n- Configurable database connection%n- Docker SQL Server support%n%nAfter installation, click "Configure Database" to connect to your SQL Server.%n%nClick Next to continue.
FinishedLabel=Setup has finished installing [name] on your computer.%n%nIMPORTANT NEXT STEPS:%n1. Click "Configure Database" on login screen%n2. Enter your SQL Server connection details%n3. Run DatabaseSetup.sql on your server (see SQL folder)%n%nDefault login: admin / admin

[UninstallDelete]
; Clean up files created during use
Type: filesandordirs; Name: "{app}\Documentation"
Type: filesandordirs; Name: "{app}\SQL"
