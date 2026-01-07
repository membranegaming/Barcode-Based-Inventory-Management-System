-- Remove party_outward_challan_no column and make all fields required
-- Run this script in your MainDB.mdf database

-- Step 1: Drop the party_outward_challan_no column
ALTER TABLE machinery_item_inward_unit2 DROP COLUMN party_outward_challan_no;

-- Step 2: Make all remaining fields NOT NULL
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN challan_no VARCHAR(20) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN date DATE NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN party_name VARCHAR(255) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN item VARCHAR(255) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN qty INT NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN reference_bill_no VARCHAR(50) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN bill_date DATE NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN used_in_machine VARCHAR(255) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN taken_by VARCHAR(255) NOT NULL;

-- Step 3: Verify the changes
SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'machinery_item_inward_unit2'
ORDER BY ORDINAL_POSITION;