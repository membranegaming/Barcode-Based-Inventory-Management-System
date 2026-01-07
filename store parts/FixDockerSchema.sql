-- ============================================
-- Complete Schema Fix Script for Docker Database
-- ============================================
-- Run this script to add ALL missing columns
-- to match the application's expected schema
-- ============================================

USE MainDB;
GO

PRINT 'Starting schema fix...';
GO

-- ============================================
-- Fix PartyMaster table
-- ============================================
PRINT 'Fixing PartyMaster table...';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PartyMaster' AND COLUMN_NAME = 'contact_person')
    ALTER TABLE PartyMaster ADD contact_person VARCHAR(255) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PartyMaster' AND COLUMN_NAME = 'phone')
    ALTER TABLE PartyMaster ADD phone VARCHAR(50) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PartyMaster' AND COLUMN_NAME = 'address')
    ALTER TABLE PartyMaster ADD address VARCHAR(500) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PartyMaster' AND COLUMN_NAME = 'is_active')
    ALTER TABLE PartyMaster ADD is_active BIT NOT NULL DEFAULT 1;
GO

-- ============================================
-- Fix ItemMaster table
-- ============================================
PRINT 'Fixing ItemMaster table...';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ItemMaster' AND COLUMN_NAME = 'item_code')
    ALTER TABLE ItemMaster ADD item_code VARCHAR(50) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ItemMaster' AND COLUMN_NAME = 'description')
    ALTER TABLE ItemMaster ADD description VARCHAR(500) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ItemMaster' AND COLUMN_NAME = 'unit')
    ALTER TABLE ItemMaster ADD unit VARCHAR(50) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ItemMaster' AND COLUMN_NAME = 'is_active')
    ALTER TABLE ItemMaster ADD is_active BIT NOT NULL DEFAULT 1;
GO

-- ============================================
-- Fix MachineMaster table
-- ============================================
PRINT 'Fixing MachineMaster table...';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MachineMaster' AND COLUMN_NAME = 'machine_code')
    ALTER TABLE MachineMaster ADD machine_code VARCHAR(50) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MachineMaster' AND COLUMN_NAME = 'location')
    ALTER TABLE MachineMaster ADD location VARCHAR(100) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MachineMaster' AND COLUMN_NAME = 'description')
    ALTER TABLE MachineMaster ADD description VARCHAR(500) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MachineMaster' AND COLUMN_NAME = 'is_active')
    ALTER TABLE MachineMaster ADD is_active BIT NOT NULL DEFAULT 1;
GO

-- ============================================
-- Fix PersonMaster table
-- ============================================
PRINT 'Fixing PersonMaster table...';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonMaster' AND COLUMN_NAME = 'employee_id')
    ALTER TABLE PersonMaster ADD employee_id VARCHAR(50) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonMaster' AND COLUMN_NAME = 'department')
    ALTER TABLE PersonMaster ADD department VARCHAR(100) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonMaster' AND COLUMN_NAME = 'contact_number')
    ALTER TABLE PersonMaster ADD contact_number VARCHAR(50) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonMaster' AND COLUMN_NAME = 'is_active')
    ALTER TABLE PersonMaster ADD is_active BIT NOT NULL DEFAULT 1;
GO

-- ============================================
-- Fix machinery_item_inward_unit2 table
-- ============================================
PRINT 'Fixing machinery_item_inward_unit2 table...';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'machinery_item_inward_unit2' AND COLUMN_NAME = 'unit')
    ALTER TABLE machinery_item_inward_unit2 ADD unit VARCHAR(50) NULL DEFAULT 'pcs';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'machinery_item_inward_unit2' AND COLUMN_NAME = 'party_outward_challan_no')
    ALTER TABLE machinery_item_inward_unit2 ADD party_outward_challan_no VARCHAR(50) NULL;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'machinery_item_inward_unit2' AND COLUMN_NAME = 'used_qty')
    ALTER TABLE machinery_item_inward_unit2 ADD used_qty INT NOT NULL DEFAULT 0;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'machinery_item_inward_unit2' AND COLUMN_NAME = 'used_in_machine')
    ALTER TABLE machinery_item_inward_unit2 ADD used_in_machine VARCHAR(100) NULL DEFAULT '';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'machinery_item_inward_unit2' AND COLUMN_NAME = 'taken_by')
    ALTER TABLE machinery_item_inward_unit2 ADD taken_by VARCHAR(100) NULL DEFAULT '';
GO

-- ============================================
-- Verify all columns
-- ============================================
PRINT '';
PRINT '============================================';
PRINT 'Schema fix complete!';
PRINT '============================================';
PRINT '';
PRINT 'PartyMaster columns:';
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PartyMaster';
PRINT '';
PRINT 'ItemMaster columns:';
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ItemMaster';
PRINT '';
PRINT 'MachineMaster columns:';
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MachineMaster';
PRINT '';
PRINT 'PersonMaster columns:';
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PersonMaster';
GO
