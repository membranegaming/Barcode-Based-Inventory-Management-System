I developed this barcode based inventory management system for my father's textile factory in C#, i made this in Visual Studio, Windows Forms .net Framework, it has all the steps, it can print and config the barcode printer, it's dimension, it contains indeginous database, person, item, machine, party name master list for all these, it has history for quantity where the part was used, when and who too the part for usage, it also shows about the data in stats form in dashboard
**COMPLETE STEP BY STEP INSTRUCTIONS FOR SETTING UP THIS SOFTWARE**
# Complete Step-by-Step Guide: Deploying Store Parts with Docker Database

**Version: 2.1 - Docker Edition**  
**Last Updated: December 2024**

---

## TABLE OF CONTENTS

1. OVERVIEW - Application Features
2. **PHASE 1 - Docker Database Setup (NEW)**
3. PHASE 2 - Build the Application
4. PHASE 3 - Deploy to Client Computers
5. PHASE 4 - Verification & Testing
6. PHASE 5 - Backup & Maintenance
7. PHASE 6 - Troubleshooting Guide
8. PHASE 7 - Advanced Configuration
9. PHASE 8 - User Management

---

## 1. OVERVIEW - APPLICATION FEATURES

Store Parts Management is a comprehensive inventory management system for tracking machinery parts with the following features:

**CORE FEATURES:**
- Parts Entry (Inward) - Record new parts received
- Parts Usage Tracking - Track usage with full history
- Barcode Printing - Print barcode labels for parts (TSC/TSPL printers)
- Dashboard & Reports - Analytics and usage reports
- Multi-user Support - Admin and regular users
- Database Configuration - Easy setup for local or network databases

**KEY IMPROVEMENTS IN VERSION 2.1:**
- **Docker-based SQL Server deployment** - Easy setup, portable, consistent
- No complex SQL Server installation required
- Simple container management
- Easy backup and restore with Docker volumes
- All previous 2.0 features maintained

---

## PHASE 1: DOCKER DATABASE SETUP (NEW)

### Step 1.1: Install Docker Desktop

**For Windows:**

1. **Download Docker Desktop:**
   - Go to: https://www.docker.com/products/docker-desktop
   - Click "Download for Windows"
   - Run the installer (Docker Desktop Installer.exe)

2. **Installation Options:**
   - Check "Use WSL 2 instead of Hyper-V" (recommended for Windows 10/11)
   - Follow the installation wizard
   - Restart your computer when prompted

3. **Start Docker Desktop:**
   - Launch Docker Desktop from Start Menu
   - Wait for Docker to start (whale icon in system tray)
   - You may need to accept the service agreement

4. **Verify Installation:**
   ```cmd
   docker --version
   docker compose version
   ```
   You should see version numbers for both commands.

---

### Step 1.2: Create Docker Configuration Files

**Create a project folder on your server:**
```cmd
mkdir C:\StoreParts-Docker
cd C:\StoreParts-Docker
```

**Create `docker-compose.yml` file:**

Save this as `C:\StoreParts-Docker\docker-compose.yml`:

```yaml
version: '3.8'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: storeparts-sqlserver
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Password123
      - MSSQL_PID=Express
    ports:
      - "1433:1433"
    volumes:
      - sqlserver-data:/var/opt/mssql
      # Note: See explanation below about init scripts — the official mssql image
      # does not automatically execute scripts from /docker-entrypoint-initdb.d.
    restart: unless-stopped
    healthcheck:
      test: /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -Q "SELECT 1"
      interval: 10s
      timeout: 3s
      retries: 10
      start_period: 10s

volumes:
  sqlserver-data:
    driver: local
```

Important note about `init-scripts` and the MSSQL image

- The official Microsoft SQL Server Linux container image does NOT behave like some other database images (MySQL/Postgres) that auto-run scripts found in `/docker-entrypoint-initdb.d` at container start. Mounting a folder into `/docker-entrypoint-initdb.d` is harmless, but the SQL files will not be executed automatically.
- Recommended approach: keep your initialization scripts in a host folder (for example `C:\StoreParts-Docker\init-scripts`) and run them explicitly using `sqlcmd` after the container is up and healthy (see examples below). You can also copy files into the container and execute them there.

**Create `init-scripts` folder (optional - for storing scripts to run manually):**
```cmd
mkdir C:\StoreParts-Docker\init-scripts
```

**Create database initialization script (example):**

Save this as `C:\StoreParts-Docker\init-scripts\01-create-database.sql` (this file is provided as an example and should be run manually using `sqlcmd` after the container is ready):

```sql
-- Wait for SQL Server to be ready
WAITFOR DELAY '00:00:05';
GO

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MainDB')
BEGIN
    CREATE DATABASE MainDB;
    PRINT 'Database MainDB created.';
END
GO

USE MainDB;
GO

-- Table: machinery_item_inward_unit2 (Main Data)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'machinery_item_inward_unit2')
BEGIN
    CREATE TABLE machinery_item_inward_unit2 (
        id INT IDENTITY(1,1) PRIMARY KEY,
        challan_no NVARCHAR(50) NOT NULL,
        date DATE NOT NULL DEFAULT GETDATE(),
        party_name NVARCHAR(100) NOT NULL,
        item NVARCHAR(100) NOT NULL,
        qty INT NOT NULL DEFAULT 0,
        unit NVARCHAR(20) NULL DEFAULT 'pcs',
        used_qty INT NOT NULL DEFAULT 0,
        reference_bill_no NVARCHAR(50) NULL,
        bill_date DATE NULL
    );
    PRINT 'Table machinery_item_inward_unit2 created.';
END
GO

-- Add unit column if not exists
IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'unit' AND Object_ID = Object_ID(N'machinery_item_inward_unit2'))
BEGIN
    ALTER TABLE machinery_item_inward_unit2 ADD unit NVARCHAR(20) NULL DEFAULT 'pcs';
    PRINT 'Column unit added.';
END
GO

-- Table: PartsUsageHistory
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PartsUsageHistory')
BEGIN
    CREATE TABLE PartsUsageHistory (
        id INT IDENTITY(1,1) PRIMARY KEY,
        inward_item_id INT NOT NULL,
        quantity_used INT NOT NULL DEFAULT 1,
        used_in_machine NVARCHAR(100) NOT NULL,
        taken_by NVARCHAR(100) NOT NULL,
        usage_date DATETIME NOT NULL DEFAULT GETDATE(),
        remarks NVARCHAR(500) NULL,
        created_date DATETIME NOT NULL DEFAULT GETDATE()
    );
    CREATE INDEX IX_PartsUsageHistory_InwardItemId ON PartsUsageHistory(inward_item_id);
    CREATE INDEX IX_PartsUsageHistory_UsageDate ON PartsUsageHistory(usage_date);
    CREATE INDEX IX_PartsUsageHistory_Machine ON PartsUsageHistory(used_in_machine);
    PRINT 'Table PartsUsageHistory created.';
END
GO

-- Table: PartyMaster
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PartyMaster')
BEGIN
    CREATE TABLE PartyMaster (
        id INT IDENTITY(1,1) PRIMARY KEY,
        party_name NVARCHAR(100) NOT NULL,
        address NVARCHAR(255) NULL,
        contact NVARCHAR(50) NULL,
        is_active BIT NOT NULL DEFAULT 1,
        created_date DATETIME DEFAULT GETDATE()
    );
    INSERT INTO PartyMaster (party_name, is_active) VALUES ('Default Party', 1);
    PRINT 'Table PartyMaster created.';
END
GO

-- Table: ItemMaster
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ItemMaster')
BEGIN
    CREATE TABLE ItemMaster (
        id INT IDENTITY(1,1) PRIMARY KEY,
        item_name NVARCHAR(100) NOT NULL,
        description NVARCHAR(255) NULL,
        unit NVARCHAR(20) NULL DEFAULT 'PCS',
        is_active BIT NOT NULL DEFAULT 1,
        created_date DATETIME DEFAULT GETDATE()
    );
    INSERT INTO ItemMaster (item_name, is_active) VALUES ('Default Item', 1);
    PRINT 'Table ItemMaster created.';
END
GO

-- Table: MachineMaster
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MachineMaster')
BEGIN
    CREATE TABLE MachineMaster (
        id INT IDENTITY(1,1) PRIMARY KEY,
        machine_name NVARCHAR(100) NOT NULL,
        location NVARCHAR(100) NULL,
        is_active BIT NOT NULL DEFAULT 1,
        created_date DATETIME DEFAULT GETDATE()
    );
    PRINT 'Table MachineMaster created.';
END
GO

-- Table: PersonMaster
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonMaster')
BEGIN
    CREATE TABLE PersonMaster (
        id INT IDENTITY(1,1) PRIMARY KEY,
        person_name NVARCHAR(100) NOT NULL,
        department NVARCHAR(100) NULL,
        is_active BIT NOT NULL DEFAULT 1,
        created_date DATETIME DEFAULT GETDATE()
    );
    PRINT 'Table PersonMaster created.';
END
GO

-- View: Parts with Usage Summary
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_PartsWithUsageSummary')
    DROP VIEW vw_PartsWithUsageSummary;
GO
CREATE VIEW vw_PartsWithUsageSummary AS
SELECT 
    p.id, p.challan_no, p.date, p.party_name, p.item, p.qty, p.unit,
    ISNULL((SELECT SUM(quantity_used) FROM PartsUsageHistory WHERE inward_item_id = p.id), 0) as used_qty,
    p.qty - ISNULL((SELECT SUM(quantity_used) FROM PartsUsageHistory WHERE inward_item_id = p.id), 0) as remaining_qty,
    p.reference_bill_no, p.bill_date,
    (SELECT TOP 1 used_in_machine FROM PartsUsageHistory WHERE inward_item_id = p.id ORDER BY usage_date DESC) as last_used_in_machine,
    (SELECT TOP 1 taken_by FROM PartsUsageHistory WHERE inward_item_id = p.id ORDER BY usage_date DESC) as last_taken_by,
    (SELECT TOP 1 usage_date FROM PartsUsageHistory WHERE inward_item_id = p.id ORDER BY usage_date DESC) as last_usage_date,
    (SELECT COUNT(*) FROM PartsUsageHistory WHERE inward_item_id = p.id) as usage_event_count
FROM machinery_item_inward_unit2 p;
GO

PRINT 'Database initialization complete!';
GO
```

**Create setup helper script (updated):**

Save this as `C:\StoreParts-Docker\setup.bat` (this script starts the container and then runs the init script explicitly using `sqlcmd`):

```batch
@echo off
echo ============================================
echo Store Parts Docker Database Setup
echo ============================================
echo.

REM Check if Docker is running
docker info >nul 2>&1
if errorlevel 1 (
    echo ERROR: Docker is not running!
    echo Please start Docker Desktop and try again.
    pause
    exit /b 1
)

echo Docker is running...
echo.

REM Stop and remove existing container if it exists
echo Stopping existing container (if any)...
docker-compose down
echo.

REM Start the database
echo Starting SQL Server container...
docker-compose up -d
echo.

REM Wait for SQL Server to be ready
echo Waiting for SQL Server to start (this may take 30-60 seconds)...
timeout /t 30 /nobreak >nul

echo Checking health status...
REM Wait until the healthcheck passes (or increase timeout as needed)
for /l %%i in (1,1,30) do (
  docker inspect --format='{{json .State.Health.Status}}' storeparts-sqlserver | find "healthy" >nul 2>&1 && goto :healthy || (
    timeout /t 2 >nul
  )
)
:healthy

echo Running database initialization script (if present)...
if exist "C:\StoreParts-Docker\init-scripts\01-create-database.sql" (
  docker cp "C:\StoreParts-Docker\init-scripts\01-create-database.sql" storeparts-sqlserver:/tmp/01-create-database.sql
  docker exec -i storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -i /tmp/01-create-database.sql
) else (
  echo No init script found at C:\StoreParts-Docker\init-scripts\01-create-database.sql — skipping.
)

echo.
echo ============================================
echo Setup Complete!
echo ============================================
echo.
echo Database Server: localhost,1433
echo Database Name: MainDB
echo Username: sa
echo Password: YourStrong@Password123
echo.
echo For network access from other computers, use:
echo Server: YOUR-SERVER-IP,1433
echo.

REM Get server IP
for /f "tokens=2 delims=:" %%a in ('ipconfig ^| findstr /c:"IPv4 Address"') do (
    set IP=%%a
    goto :display_ip
)

:display_ip
echo Your server IP address: %IP%
echo Connection string for clients: %IP%,1433
echo.

echo To stop the database: docker-compose down
echo To restart the database: docker-compose up -d
echo To view logs: docker-compose logs -f
echo.
pause
```

Notes about `sqlcmd` and tools:
- The `healthcheck` and `setup.bat` call `/opt/mssql-tools/bin/sqlcmd`. Some minimal images may not include `mssql-tools`. If `sqlcmd` is missing, you can either use a helper container that has the tools installed or install `mssql-tools` inside the container (not recommended for production). Example alternative: use a small container with `mssql-tools` to run scripts against the running SQL Server container.

---

### Step 1.3: Start the Database

1. **Run the setup script:**
   ```cmd
   cd C:\StoreParts-Docker
   setup.bat
   ```

2. **Wait for completion:**
   - Script will start SQL Server container
   - Initialize the database by explicit `sqlcmd` execution
   - Display connection information

3. **Verify container is running:**
   ```cmd
   docker ps
   ```
   You should see `storeparts-sqlserver` in the list.

---

### Step 1.4: Configure Windows Firewall

**Allow Docker to accept network connections:**

1. **Open Windows Firewall:**
   ```cmd
   firewall.cpl
   ```

2. **Create inbound rule:**
   - Click "Advanced settings"
   - Click "Inbound Rules" → "New Rule..."
   - Select "Port" → Next
   - TCP, Specific ports: 1433 → Next
   - Allow the connection → Next
   - Check all profiles → Next
   - Name: "Docker SQL Server" → Finish

---

### Step 1.5: Get Server Connection Information

**Your connection details:**

| Setting | Value |
|---------|-------|
| **Local Server** | `localhost,1433` |
| **Network Server** | `YOUR-SERVER-IP,1433` |
| **Database** | `MainDB` |
| **Username** | `sa` |
| **Password** | `YourStrong@Password123` |

**To find your server IP:**
```cmd
ipconfig
```
Look for "IPv4 Address" (e.g., 192.168.1.100)

**Connection string example:**
```
Server=192.168.1.100,1433;Database=MainDB;User Id=sa;Password=YourStrong@Password123;
```

---

### Step 1.6: Docker Management Commands

**Essential Docker commands:**

```batch
# Start the database
cd C:\StoreParts-Docker
docker-compose up -d

# Stop the database
docker-compose down

# View logs
docker-compose logs -f sqlserver

# Restart the database
docker-compose restart

# Check status
docker-compose ps

# Access SQL command line
docker exec -it storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123

# View resource usage
docker stats storeparts-sqlserver
```

---

## PHASE 2: BUILD THE APPLICATION

### Step 2.1: Update Connection String in Application

The application reads its default connection string from the application settings. The exact setting name used by the project is:

- `MainDBConnectionString` (application scope)

User-level override keys (used by the app when the user configures the database at runtime):

- `CustomConnectionString` (string, user scope)
- `UseCustomConnection` (bool, user scope)

When deploying with Docker SQL Server, update the `MainDBConnectionString` to point to the Docker server. You can either edit `Properties\Settings.settings` (or the generated `App.config`/`.config` file) before building, or let users configure a custom connection using the app UI which sets `CustomConnectionString` and `UseCustomConnection`.

Example `App.config` connectionStrings entry (if you prefer to put the connection string into `connectionStrings`):

```xml
<connectionStrings>
  <add name="MainDBConnectionString"
       connectionString="Server=localhost,1433;Database=MainDB;User Id=sa;Password=YourStrong@Password123;TrustServerCertificate=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Or edit `Properties\Settings.settings` to change the `Value` for `MainDBConnectionString` to the same connection string.

If you prefer runtime configuration, the application uses these settings:
- To use a custom connection immediately after user input, the app will set `CustomConnectionString` and `UseCustomConnection = true` (this is handled by `DatabaseHelper.SaveConnectionString()` in code). If you need to revert to the default embedded connection string, clear `UseCustomConnection` or call `DatabaseHelper.ResetToDefaultConnection()`.

**Note:** The project code uses `Properties.Settings.Default.MainDBConnectionString` as the default connection string and falls back to `CustomConnectionString` when `UseCustomConnection` is true. Make sure you update the `MainDBConnectionString` setting or use the application's database configuration dialog to save a `CustomConnectionString`.

---

### Step 2.2: Build Release Version

Follow the same process as the original guide:

1. Open Visual Studio
2. Open the solution
3. Set build to Release
4. Build → Build Solution
5. Verify output in `bin\Release\`

---

### Step 2.3: Create Installer

Same as original guide - use Inno Setup with the existing script.

---

## PHASE 3: DEPLOY TO CLIENT COMPUTERS

### Step 3.1: Prerequisites

- Windows 7 SP1 or later
- .NET Framework 4.7.2 or later
- Network connectivity to server
- **No Docker required on client machines**

---

### Step 3.2: Install Application

Same as original guide - run the installer on each client.

---

### Step 3.3: Configure Database Connection

**When configuring the database:**

1. Click "Configure Database" button

2. Enter details:
   | Field | Value |
   |-------|-------|
   | Server Name | `SERVER_IP,1433` (e.g., `192.168.1.100,1433`) |
   | Database Name | `MainDB` |
   | Authentication | **SQL Server Authentication** |
   | Username | `sa` |
   | Password | `YourStrong@Password123` |

3. **Important:** Check "Trust Server Certificate" if option is available

4. Click "Test Connection"

5. Click "Save and Connect"

The application will save the connection string into `CustomConnectionString` and set `UseCustomConnection` to `true` so future runs use the user-provided connection.

---

## PHASE 5: BACKUP & MAINTENANCE (DOCKER VERSION)

### Step 5.1: Automated Backup with Docker

**Create backup script:**

Save as `C:\StoreParts-Docker\backup.bat`:

```batch
@echo off
setlocal

set BACKUP_DIR=C:\DatabaseBackups\StoreParts-Docker
set TIMESTAMP=%date:~-4%%date:~4,2%%date:~7,2%_%time:~0,2%%time:~3,2%%time:~6,2%
set TIMESTAMP=%TIMESTAMP: =0%
set BACKUP_FILE=%BACKUP_DIR%\MainDB_%TIMESTAMP%.bak

if not exist "%BACKUP_DIR%" mkdir "%BACKUP_DIR%"

REM Ensure container backup folder exists
docker exec storeparts-sqlserver mkdir -p /var/opt/mssql/backup

echo Creating backup...
docker exec storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -Q "BACKUP DATABASE [MainDB] TO DISK='/var/opt/mssql/backup/MainDB_%TIMESTAMP%.bak' WITH FORMAT, INIT"

echo Copying backup to host...
docker cp storeparts-sqlserver:/var/opt/mssql/backup/MainDB_%TIMESTAMP%.bak "%BACKUP_FILE%"

echo Backup complete: %BACKUP_FILE%
echo %date% %time% - Backup: %BACKUP_FILE% >> "%BACKUP_DIR%\backup_log.txt"

pause
```

---

### Step 5.2: Volume Backup (Complete Docker State)

**Backup entire Docker volume:**

```batch
@echo off
set BACKUP_DIR=C:\DatabaseBackups\StoreParts-Docker-Volumes
set TIMESTAMP=%date:~-4%%date:~4,2%%date:~7,2%_%time:~0,2%%time:~3,2%%time:~6,2%
set TIMESTAMP=%TIMESTAMP: =0%

if not exist "%BACKUP_DIR%" mkdir "%BACKUP_DIR%"

echo Stopping container...
docker-compose down

echo Backing up volume...
docker run --rm -v storeparts-docker_sqlserver-data:/source -v %BACKUP_DIR%:/backup alpine tar czf /backup/volume-backup-%TIMESTAMP%.tar.gz -C /source .

echo Starting container...
docker-compose up -d

echo Volume backup complete!
pause
```

---

### Step 5.3: Restore from Backup

**Restore database backup:**

```batch
@echo off
set /p BACKUP_FILE="Enter full path to backup file: "

echo Copying backup to container...
docker cp "%BACKUP_FILE%" storeparts-sqlserver:/var/opt/mssql/backup/restore.bak

echo Restoring database...
docker exec storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -Q "RESTORE DATABASE [MainDB] FROM DISK='/var/opt/mssql/backup/restore.bak' WITH REPLACE"

echo Restore complete!
pause
```

---

### Step 5.4: Database Maintenance

**Run monthly maintenance:**

Save as `C:\StoreParts-Docker\maintenance.sql`:

```sql
USE MainDB;
GO

EXEC sp_updatestats;
PRINT 'Statistics updated.';
GO

ALTER INDEX ALL ON machinery_item_inward_unit2 REBUILD;
ALTER INDEX ALL ON PartsUsageHistory REBUILD;
PRINT 'Indexes rebuilt.';
GO

UPDATE machinery_item_inward_unit2
SET used_qty = ISNULL((SELECT SUM(quantity_used) FROM PartsUsageHistory WHERE inward_item_id = machinery_item_inward_unit2.id), 0);
PRINT 'Used quantities synced.';
GO
```

**Run maintenance:**
```cmd
docker exec -i storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -i /docker-entrypoint-initdb.d/maintenance.sql
```

---

## PHASE 6: TROUBLESHOOTING (DOCKER EDITION)

### Issue 6.1: Docker Container Won't Start

**Check Docker status:**
```cmd
docker ps -a
docker logs storeparts-sqlserver
```

**Common fixes:**
1. Restart Docker Desktop
2. Check if port 1433 is already in use:
   ```cmd
   netstat -ano | findstr :1433
   ```
3. Remove and recreate container:
   ```cmd
   docker-compose down -v
   docker-compose up -d
   ```

---

### Issue 6.2: Cannot Connect from Client

**Verify container is accessible:**
```cmd
# On server
docker exec storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -Q "SELECT @@VERSION"

# From client
telnet SERVER_IP 1433
```

**Check firewall** - ensure port 1433 is open

---

### Issue 6.3: Database Not Initialized

**Manually run init script:**
```cmd
docker exec -i storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -i /docker-entrypoint-initdb.d/01-create-database.sql
```

---

### Issue 6.4: Container Uses Too Much Memory

**Set memory limits in `docker-compose.yml`:**
```yaml
services:
  sqlserver:
    # ... existing config ...
    deploy:
      resources:
        limits:
          memory: 2G
        reservations:
          memory: 1G
```

---

## ADVANTAGES OF DOCKER DEPLOYMENT

✅ **Easier Setup:** No complex SQL Server installation  
✅ **Portability:** Move database to new server by copying files  
✅ **Isolation:** SQL Server runs in container, doesn't affect host  
✅ **Consistency:** Same environment across all deployments  
✅ **Easy Backup:** Simple volume snapshots  
✅ **Resource Control:** Limit memory and CPU usage  
✅ **Quick Recovery:** Rebuild container from image instantly  
✅ **Version Control:** Lock to specific SQL Server version  

---

## APPENDIX A: Quick Reference Card

**Server Information:**
- Server IP: ________________
- Database Port: 1433
- Database Name: MainDB
- SA Password: YourStrong@Password123

**Docker Commands:**
```batch
# Start database
cd C:\StoreParts-Docker
docker-compose up -d

# Stop database
docker-compose down

# View logs
docker-compose logs -f

# Backup
backup.bat

# Maintenance
docker exec -i storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -i /docker-entrypoint-initdb.d/maintenance.sql
```

**Connection String Template:**
```
Server={SERVER_IP},1433;Database=MainDB;User Id=sa;Password=YourStrong@Password123;TrustServerCertificate=True;
```

---

## APPENDIX B: Migration from Traditional SQL Server

**If you have existing SQL Server installation:**

1. **Backup existing database:**
   ```sql
   BACKUP DATABASE [MainDB] TO DISK='C:\Temp\MainDB.bak'
   ```

2. **Copy backup to Docker folder:**
   ```cmd
   copy C:\Temp\MainDB.bak C:\StoreParts-Docker\init-scripts\
   ```

3. **Restore in Docker:**
   ```cmd
   docker cp C:\StoreParts-Docker\init-scripts\MainDB.bak storeparts-sqlserver:/var/opt/mssql/backup/
   
   docker exec storeparts-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Password123 -Q "RESTORE DATABASE [MainDB] FROM DISK='/var/opt/mssql/backup/MainDB.bak' WITH REPLACE"
   ```

---

**Documentation Version: 2.1 - Docker Edition**  
**Last Updated: December 2024**

**END OF GUIDE**
