# Store Parts Application - Installer Creation Guide

## Option 1: Visual Studio Installer Project (Recommended for Enterprise)

### Step 1: Install Extension
1. Open Visual Studio
2. Go to **Extensions** ? **Manage Extensions**
3. Search for "**Microsoft Visual Studio Installer Projects**"
4. Download and install (requires VS restart)

### Step 2: Create Setup Project
1. Right-click on Solution in Solution Explorer
2. Select **Add** ? **New Project**
3. Search for "**Setup Project**"
4. Name it "StorePartsSetup" and click Create

### Step 3: Configure Setup Project
1. Right-click on the Setup Project ? **View** ? **File System**
2. Right-click "Application Folder" ? **Add** ? **Project Output**
3. Select "store parts" project and "Primary output"
4. Click OK

### Step 4: Add Shortcuts
1. In File System view, right-click "User's Desktop" ? **Create New Shortcut**
2. Navigate to Application Folder ? Primary output from store parts
3. Rename shortcut to "Store Parts"
4. Repeat for "User's Programs Menu"

### Step 5: Configure Properties
Select the Setup Project and set in Properties window:
- **ProductName**: Store Parts Management
- **Manufacturer**: Your Company Name
- **Version**: 1.0.0
- **Author**: Your Name

### Step 6: Add Prerequisites
1. Right-click Setup Project ? **Properties**
2. Click **Prerequisites**
3. Check:
   - ? .NET Framework 4.7.2
   - ? SQL Server Express (if needed)
4. Select "Download from same location as my application"

### Step 7: Build Installer
1. Set configuration to **Release**
2. Right-click Setup Project ? **Build**
3. Find installer in: `StorePartsSetup\Release\StorePartsSetup.msi`

---

## Option 2: Inno Setup (Free, Professional)

### Step 1: Download Inno Setup
Download from: https://jrsoftware.org/isdl.php

### Step 2: Create Script
Save this as `StorePartsSetup.iss`:

```iss
; Store Parts Installer Script
[Setup]
AppName=Store Parts Management
AppVersion=1.0.0
AppPublisher=Your Company Name
DefaultDirName={autopf}\Store Parts
DefaultGroupName=Store Parts
OutputDir=Output
OutputBaseFilename=StorePartsSetup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern

[Files]
; Main application files - adjust path as needed
Source: "bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\Store Parts"; Filename: "{app}\store parts.exe"
Name: "{group}\Uninstall Store Parts"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Store Parts"; Filename: "{app}\store parts.exe"

[Run]
Filename: "{app}\store parts.exe"; Description: "Launch Store Parts"; Flags: postinstall nowait skipifsilent
```

### Step 3: Build
1. Open the .iss file in Inno Setup
2. Press F9 or click Build ? Compile
3. Installer will be created in Output folder

---

## Option 3: WiX Toolset (Advanced, MSI)

### Step 1: Install WiX
Download from: https://wixtoolset.org/releases/

### Step 2: Install VS Extension
Install "WiX Toolset Visual Studio Extension"

### Step 3: Add WiX Project
1. Add New Project ? "Setup Project for WiX"
2. Configure Product.wxs file

---

## Option 4: NSIS (Nullsoft Scriptable Install System)

Download from: https://nsis.sourceforge.io/

### Basic Script:
```nsis
!include "MUI2.nsh"

Name "Store Parts Management"
OutFile "StorePartsSetup.exe"
InstallDir "$PROGRAMFILES\Store Parts"

!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES

Section "Install"
    SetOutPath "$INSTDIR"
    File /r "bin\Release\*.*"
    CreateShortcut "$DESKTOP\Store Parts.lnk" "$INSTDIR\store parts.exe"
    CreateDirectory "$SMPROGRAMS\Store Parts"
    CreateShortcut "$SMPROGRAMS\Store Parts\Store Parts.lnk" "$INSTDIR\store parts.exe"
SectionEnd
```

---

## Quick Distribution (No Installer)

### Create a Portable ZIP Package:

1. Build your project in **Release** mode
2. Navigate to `bin\Release` folder
3. Copy ALL files to a new folder
4. Include these additional files:
   - README.txt (instructions)
   - Database setup script (if needed)
5. Zip the folder
6. Distribute the ZIP file

### Required Files in Release Folder:
```
Store Parts/
??? store parts.exe          (main application)
??? store parts.exe.config   (configuration)
??? *.dll                    (any dependencies)
??? README.txt               (instructions)
```

---

## Prerequisites for Target Machines

Your installer should include or check for:

1. **.NET Framework 4.7.2**
   - Download: https://dotnet.microsoft.com/download/dotnet-framework/net472
   - Most Windows 10/11 machines have this pre-installed

2. **SQL Server** (if using local database)
   - SQL Server Express (free)
   - Or configure connection string for remote server

3. **Printer Drivers** (for barcode printing)
   - TSC printer drivers if using TSPL

---

## Recommended Approach

For your **Store Parts** application, I recommend:

### For Small Distribution (< 10 machines):
? Use **Option 4: Portable ZIP** - Simple, no installer needed

### For Medium Distribution (10-50 machines):
? Use **Option 2: Inno Setup** - Professional look, free, easy to update

### For Enterprise Distribution (50+ machines):
? Use **Option 1: VS Installer Project** - MSI format, Group Policy deployment

---

## Building Release Version

Before creating any installer:

1. **Change Build Configuration to Release:**
   - Build ? Configuration Manager ? Set to "Release"

2. **Build the Solution:**
   - Build ? Build Solution (Ctrl+Shift+B)

3. **Verify Output:**
   - Check `store parts\bin\Release\` folder
   - Ensure all DLLs and config files are present

4. **Test the Release Build:**
   - Run the .exe from Release folder
   - Verify all features work correctly
