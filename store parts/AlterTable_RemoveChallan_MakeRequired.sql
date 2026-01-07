-- Alter machinery_item_inward_unit2 table to remove party_outward_challan_no and make all fields required

-- Step 1: Remove the party_outward_challan_no column
ALTER TABLE machinery_item_inward_unit2 DROP COLUMN party_outward_challan_no;

-- Step 2: Make all remaining fields NOT NULL (except id which is auto-increment PK)
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN challan_no VARCHAR(20) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN date DATE NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN party_name VARCHAR(255) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN item VARCHAR(255) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN qty INT NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN reference_bill_no VARCHAR(50) NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN bill_date DATE NOT NULL;
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN used_in_machine VARCHAR(255) NULL; -- Keep nullable for parts not yet used
ALTER TABLE machinery_item_inward_unit2 ALTER COLUMN taken_by VARCHAR(255) NULL; -- Keep nullable for parts not yet used

-- Note: used_in_machine and taken_by are kept nullable because parts may not be used immediately
-- They will be populated when parts are recorded as used in the PartsUsageForm