# Store Parts - Database Setup Guide

## Overview

Your Store Parts application requires a SQL Server database. You have several deployment options depending on your needs.

---

## Option 1: Centralized Database (Recommended for Multiple Users)

**Best for:** Office environment where multiple computers need to access the same data.

### Setup Steps:

#### On the Server Machine:
1. **Install SQL Server** (any edition):
   - SQL Server Express (Free): https://www.microsoft.com/en-us/sql-server/sql-server-downloads
   - Download "Express" edition
   - During installation, note the instance name (default: `SQLEXPRESS`)

2. **Enable Network Access:**
   - Open **SQL Server Configuration Manager**
   - Enable **TCP/IP** under SQL Server Network Configuration
   - Set TCP Port to **1433** (or remember your custom port)
   - Restart SQL Server service

3. **Configure Firewall:**
   - Open Windows Firewall
   - Add inbound rule for port **1433** (TCP)
   - Add inbound rule for SQL Server Browser (UDP 1434)

4. **Create the Database:**
   - Open **SQL Server Management Studio (SSMS)**
   - Connect to your server
   - Run the `DatabaseSetup.sql` script included with the application
   - Or let the application create it automatically

#### On Each Client Machine:
1. Install the Store Parts application
2. On first run, click **"Configure Database"**
3. Enter connection details:
   - **Server Name:** `SERVER_NAME\SQLEXPRESS` (replace SERVER_NAME with actual server name/IP)
   - **Database Name:** `MainDB`
   - **Authentication:** Windows Authentication (recommended) or SQL Server Authentication
4. Click **"Test Connection"** then **"Save & Connect"**

### Connection String Examples:

**Windows Authentication (Recommended):**
```
Server=192.168.1.100\SQLEXPRESS;Database=MainDB;Integrated Security=True;
```

**SQL Server Authentication:**
```
Server=192.168.1.100\SQLEXPRESS;Database=MainDB;User Id=sa;Password=YourPassword;
```

---

## Option 2: Local Database (Single User / Standalone)

**Best for:** Single computer use, offline operation, or testing.

### Setup Steps:

1. **Install SQL Server Express** on the local machine:
   - Download from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
   - Choose "Express" edition (free)
   - Use default instance name: `SQLEXPRESS`

2. **Run the Application:**
   - Start Store Parts
   - Click **"Configure Database"**
   - Enter:
     - **Server Name:** `.\SQLEXPRESS` or `localhost\SQLEXPRESS`
     - **Database Name:** `MainDB`
     - **Authentication:** Windows Authentication
   - Check **"Create database if not exists"**
   - Click **"Save & Connect"**

3. **Database will be created automatically** with all required tables.

---

## Option 3: SQL Server LocalDB (Simplest)

**Best for:** Development, single user, no SQL Server installation needed.

LocalDB is included with Visual Studio and some SQL Server installations.

### Connection String for LocalDB:
```
Server=(localdb)\MSSQLLocalDB;Database=MainDB;Integrated Security=True;
```

### To Use:
1. Click **"Configure Database"**
2. Enter Server: `(localdb)\MSSQLLocalDB`
3. Enter Database: `MainDB`
4. Check **"Create database if not exists"**
5. Click **"Save & Connect"**

---

## Database Tables

The application will automatically create these tables:

| Table Name | Purpose |
|------------|---------|
| `machinery_item_inward_unit2` | Main data table for parts entries |
| `PartyMaster` | Supplier/Party information |
| `ItemMaster` | Item/Part types |
| `MachineMaster` | Machine information |
| `PersonMaster` | Employee/Person information |

---

## Troubleshooting

### "Cannot connect to server"
- Verify SQL Server is running (check Windows Services)
- Verify server name is correct
- Check firewall settings
- Try using IP address instead of server name

### "Login failed"
- Verify username and password
- For Windows Authentication, ensure user has database access
- Check SQL Server is configured for the authentication mode

### "Database does not exist"
- Check "Create database if not exists" option
- Or run `DatabaseSetup.sql` manually in SSMS

### "Network path not found"
- Verify server is reachable (try `ping SERVER_NAME`)
- Check SQL Server Browser service is running
- Verify TCP/IP is enabled in SQL Server Configuration

---

## Backup and Restore

### To Backup:
```sql
BACKUP DATABASE MainDB TO DISK = 'C:\Backup\MainDB.bak'
```

### To Restore:
```sql
RESTORE DATABASE MainDB FROM DISK = 'C:\Backup\MainDB.bak'
```

---

## For Installer Distribution

When distributing with an installer:

1. **Include** `DatabaseSetup.sql` with the installer
2. **First Run:** Application will prompt for database configuration
3. **Options for Users:**
   - Connect to existing SQL Server
   - Install SQL Server Express locally
   - Use LocalDB (if available)

The application handles database initialization automatically when configured.
