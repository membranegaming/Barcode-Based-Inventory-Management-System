-- Add used_qty column to machinery_item_inward_unit2 table
-- Run this script in your MainDB.mdf database to add quantity tracking support
-- This script is safe to run multiple times - it checks if the column exists first

-- Step 1: Add used_qty column if it doesn't exist
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'machinery_item_inward_unit2' 
    AND COLUMN_NAME = 'used_qty'
)
BEGIN
    ALTER TABLE machinery_item_inward_unit2 
    ADD used_qty INT NOT NULL DEFAULT 0;
    
    PRINT 'Column used_qty added successfully.';
END
ELSE
BEGIN
    PRINT 'Column used_qty already exists.';
END

-- Step 2: Add party_outward_challan_no column if it doesn't exist (optional field)
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'machinery_item_inward_unit2' 
    AND COLUMN_NAME = 'party_outward_challan_no'
)
BEGIN
    ALTER TABLE machinery_item_inward_unit2 
    ADD party_outward_challan_no VARCHAR(50) NULL DEFAULT '';
    
    PRINT 'Column party_outward_challan_no added successfully.';
END
ELSE
BEGIN
    PRINT 'Column party_outward_challan_no already exists.';
END

-- Step 3: Update any NULL used_qty values to 0
UPDATE machinery_item_inward_unit2 
SET used_qty = 0 
WHERE used_qty IS NULL;

PRINT 'Updated NULL used_qty values to 0.';

-- Step 4: Verify the table structure
SELECT 
    COLUMN_NAME, 
    IS_NULLABLE, 
    DATA_TYPE, 
    CHARACTER_MAXIMUM_LENGTH,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'machinery_item_inward_unit2'
ORDER BY ORDINAL_POSITION;

-- Step 5: Show sample data with used_qty
SELECT TOP 10 
    id, 
    challan_no, 
    item, 
    qty, 
    used_qty, 
    (qty - ISNULL(used_qty, 0)) as remaining_qty,
    used_in_machine, 
    taken_by
FROM machinery_item_inward_unit2
ORDER BY id DESC;
