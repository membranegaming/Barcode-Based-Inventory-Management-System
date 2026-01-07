using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace store_parts
{
    /// <summary>
    /// Helper class for database setup and connection management
    /// </summary>
    public static class DatabaseHelper
    {
        private static string _connectionString;

        /// <summary>
        /// Gets the current connection string (custom if set, otherwise default)
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    // Check if custom connection is configured
                    if (Properties.Settings.Default.UseCustomConnection && 
                        !string.IsNullOrEmpty(Properties.Settings.Default.CustomConnectionString))
                    {
                        _connectionString = Properties.Settings.Default.CustomConnectionString;
                    }
                    else
                    {
                        _connectionString = Properties.Settings.Default.MainDBConnectionString;
                    }
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Saves a custom connection string to settings
        /// </summary>
        public static void SaveConnectionString(string connectionString)
        {
            Properties.Settings.Default.CustomConnectionString = connectionString;
            Properties.Settings.Default.UseCustomConnection = true;
            Properties.Settings.Default.Save();
            _connectionString = connectionString;
        }

        /// <summary>
        /// Resets to use the default connection string
        /// </summary>
        public static void ResetToDefaultConnection()
        {
            Properties.Settings.Default.UseCustomConnection = false;
            Properties.Settings.Default.Save();
            _connectionString = null; // Force reload
        }

        /// <summary>
        /// Tests the database connection
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tests connection with a specific connection string
        /// </summary>
        public static bool TestConnection(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes the database - creates tables if they don't exist
        /// </summary>
        public static bool InitializeDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // Create tables if not exist
                    CreateTablesIfNotExist(conn);

                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database initialization error: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Creates all required tables if they don't exist
        /// </summary>
        private static void CreateTablesIfNotExist(SqlConnection conn)
        {
            // Create machinery_item_inward_unit2 table
            string createMainTable = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'machinery_item_inward_unit2')
                BEGIN
                    CREATE TABLE machinery_item_inward_unit2 (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        challan_no NVARCHAR(50) NOT NULL,
                        date DATE NOT NULL DEFAULT GETDATE(),
                        party_name NVARCHAR(100) NOT NULL,
                        item NVARCHAR(100) NOT NULL,
                        qty INT NOT NULL DEFAULT 0,
                        used_qty INT NOT NULL DEFAULT 0,
                        reference_bill_no NVARCHAR(50) NULL,
                        bill_date DATE NULL,
                        used_in_machine NVARCHAR(100) NULL DEFAULT '',
                        taken_by NVARCHAR(100) NULL DEFAULT ''
                    );
                END";
            ExecuteNonQuery(conn, createMainTable);

            // Create PartyMaster table
            string createPartyTable = @"
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
                END";
            ExecuteNonQuery(conn, createPartyTable);

            // Create ItemMaster table
            string createItemTable = @"
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
                END";
            ExecuteNonQuery(conn, createItemTable);

            // Create MachineMaster table
            string createMachineTable = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MachineMaster')
                BEGIN
                    CREATE TABLE MachineMaster (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        machine_name NVARCHAR(100) NOT NULL,
                        location NVARCHAR(100) NULL,
                        is_active BIT NOT NULL DEFAULT 1,
                        created_date DATETIME DEFAULT GETDATE()
                    );
                END";
            ExecuteNonQuery(conn, createMachineTable);

            // Create PersonMaster table
            string createPersonTable = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PersonMaster')
                BEGIN
                    CREATE TABLE PersonMaster (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        person_name NVARCHAR(100) NOT NULL,
                        department NVARCHAR(100) NULL,
                        is_active BIT NOT NULL DEFAULT 1,
                        created_date DATETIME DEFAULT GETDATE()
                    );
                END";
            ExecuteNonQuery(conn, createPersonTable);

            // Add used_qty column if it doesn't exist (for older databases)
            string addUsedQtyColumn = @"
                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('machinery_item_inward_unit2') AND name = 'used_qty')
                BEGIN
                    ALTER TABLE machinery_item_inward_unit2 ADD used_qty INT NOT NULL DEFAULT 0;
                END";
            ExecuteNonQuery(conn, addUsedQtyColumn);
        }

        private static void ExecuteNonQuery(SqlConnection conn, string sql)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SQL Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Builds a connection string from parameters
        /// </summary>
        public static string BuildConnectionString(string server, string database, bool useWindowsAuth, string username = "", string password = "")
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.InitialCatalog = database;

            if (useWindowsAuth)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = username;
                builder.Password = password;
            }

            builder.ConnectTimeout = 10;
            builder.TrustServerCertificate = true;

            return builder.ConnectionString;
        }

        /// <summary>
        /// Configures a TableAdapter's connection to use the custom database connection.
        /// This is needed because auto-generated TableAdapters hardcode the connection string.
        /// </summary>
        /// <param name="tableAdapter">The TableAdapter to configure</param>
        public static void ConfigureTableAdapterConnection(object tableAdapter)
        {
            if (tableAdapter == null) return;

            try
            {
                // Use reflection to set the Connection property
                var connectionProperty = tableAdapter.GetType().GetProperty("Connection",
                    System.Reflection.BindingFlags.NonPublic | 
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.Public);

                if (connectionProperty != null)
                {
                    var connection = new SqlConnection(ConnectionString);
                    connectionProperty.SetValue(tableAdapter, connection);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ConfigureTableAdapterConnection error: " + ex.Message);
            }
        }
    }
}
