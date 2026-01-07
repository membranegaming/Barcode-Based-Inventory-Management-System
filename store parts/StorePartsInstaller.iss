; ============================================
; Store Parts Management - Installer Script
; ============================================
; INSTRUCTIONS:
; 1. Install Inno Setup from https://jrsoftware.org/isdl.php
; 2. Build your project in Release mode in Visual Studio
; 3. Open this file in Inno Setup
; 4. Press Ctrl+F9 to compile the installer
; ============================================

#define MyAppName "Store Parts Management"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Your Company Name"
#define MyAppExeName "store parts.exe"

; IMPORTANT: Update this path to match your actual Release folder
; Use absolute path or relative path from where this .iss file is located
#define SourcePath "C:\Users\Vardh\Desktop\EUP\store parts\store parts\bin\Release"

; Logo/Icon paths - Update these with your actual file paths
#define AppIcon "logo.ico"
#define WizardImageFile "wizard-logo.bmp"
#define WizardSmallImageFile "wizard-logo-small.bmp"

[Setup]
AppId={{8A2B3C4D-5E6F-7890-ABCD-EF1234567890}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\Store Parts
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=Installer_Output
OutputBaseFilename=StorePartsSetup_v{#MyAppVersion}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}
MinVersion=6.1sp1
; Logo and icon settings
SetupIconFile={#AppIcon}
WizardImageFile={#WizardImageFile}
WizardSmallImageFile={#WizardSmallImageFile}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkedonce

[Files]
; Application files
Source: "{#SourcePath}\store parts.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourcePath}\*.dll"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "{#SourcePath}\*.config"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "{#SourcePath}\*.xml"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

; Logo/Icon file (will be copied to installation folder)
Source: "{#AppIcon}"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

; Documentation
Source: "DATABASE_SETUP_GUIDE.md"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "DatabaseSetup.sql"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Uninstall {#MyAppName}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch Store Parts"; Flags: nowait postinstall skipifsilent

[Code]
// Check for .NET Framework 4.7.2
function IsDotNetDetected(): Boolean;
var
    release: Cardinal;
begin
    Result := False;
    if RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', release) then
    begin
        Result := (release >= 461808); // 461808 = .NET Framework 4.7.2
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
               'After installing .NET Framework, run this setup again.', mbError, MB_OK);
        Result := False;
    end;
end;

[Messages]
WelcomeLabel2=This will install [name/ver] on your computer.%n%nIMPORTANT: After installation, you will need to configure the database connection to connect to your SQL Server.%n%nClick Next to continue.