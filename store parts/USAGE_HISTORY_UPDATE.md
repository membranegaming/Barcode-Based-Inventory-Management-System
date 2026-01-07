# Parts Usage History - Database Update

## Overview

This update fixes the bug where only the latest parts usage was saved. Now, **every usage event** is recorded with:
- **Date and Time** when the parts were used
- **Machine/Venue** where parts were used
- **Person** who took the parts
- **Quantity** used in each event
- **Optional Remarks**

## Database Changes

### Removed Redundant Columns

The following columns have been **removed from the main table** (`machinery_item_inward_unit2`):
- `used_in_machine` - Was redundant, only stored last usage
- `taken_by` - Was redundant, only stored last usage

**Reason**: These columns were misleading because they only stored the LAST usage event. If a part was used 5 times by different people on different machines, only the last event was visible.

### New Table: `PartsUsageHistory`

A new table stores ALL usage records with complete history:

```sql
CREATE TABLE PartsUsageHistory (
    id INT IDENTITY(1,1) PRIMARY KEY,
    inward_item_id INT NOT NULL,          -- Links to machinery_item_inward_unit2.id
    quantity_used INT NOT NULL,            -- How many were used in this event
    used_in_machine NVARCHAR(100) NOT NULL, -- Machine where parts were used
    taken_by NVARCHAR(100) NOT NULL,       -- Person who took the parts
    usage_date DATETIME NOT NULL,          -- When the parts were used
    remarks NVARCHAR(500) NULL,            -- Optional notes
    created_date DATETIME NOT NULL
);
```

### Main Table Structure (Updated)

The `machinery_item_inward_unit2` table now contains ONLY inward (receiving) data:

```sql
CREATE TABLE machinery_item_inward_unit2 (
    id INT IDENTITY(1,1) PRIMARY KEY,
    challan_no NVARCHAR(50) NOT NULL,
    date DATE NOT NULL,
    party_name NVARCHAR(100) NOT NULL,
    item NVARCHAR(100) NOT NULL,
    qty INT NOT NULL,
    used_qty INT NOT NULL DEFAULT 0,      -- Kept for quick lookups, synced from history
    reference_bill_no NVARCHAR(50) NULL,
    bill_date DATE NULL
);
```

### How It Works

1. **Recording Usage**: When you record parts usage in Parts Usage form, a new row is inserted into `PartsUsageHistory`
2. **Calculating Used Qty**: The `used_qty` is calculated as `SUM(quantity_used)` from all history records for that item
3. **Complete History**: Every usage event is preserved with full details

## Application Changes

### Parts Usage Form (`PartsUsageForm`)

**Features:**
- **Usage Date/Time Picker**: Select when the parts were used (defaults to current date/time)
- **Machine Selection**: Select from MachineMaster dropdown
- **Person Selection**: Select from PersonMaster dropdown  
- **Remarks Field**: Add optional notes for each usage
- **Usage History Grid**: Shows all historical usage records for the selected part
- **Export History**: Export usage history to CSV

**How to Use:**
1. Select a part from the top grid (or scan barcode)
2. Fill in Machine Name, Taken By, and Quantity
3. Set the Usage Date (defaults to now)
4. Add optional remarks
5. Click "Record Usage"
6. The history grid below shows all usage events for that part

### Admin Edit Form

**Changes:**
- Removed Machine and Taken By fields (they're no longer in main table)
- Used Qty field is now read-only (calculated from history)
- Added note explaining usage info is in Parts Usage form
- Delete now also cleans up associated usage history

### Dashboard

**Updated Reports:**
- All reports pull data from `PartsUsageHistory` for accuracy
- "Usage History" tab shows all usage events across all parts
- Filter by date range, machine, or person
- Export any report to CSV

### Main Form (Form1)

- No changes to user interface
- Entry form only captures inward (receiving) data
- No longer inserts empty machine/person values

## Running the Database Update

### Option 1: Automatic
Run the updated `DatabaseSetup.sql` script:
1. Open SQL Server Management Studio
2. Connect to your database  
3. Open and execute `DatabaseSetup.sql`

The script will:
- Drop `used_in_machine` column if it exists
- Drop `taken_by` column if it exists
- Create `PartsUsageHistory` table if it doesn't exist
- Update stored procedures and views

### Option 2: Manual Migration
If you have existing data to preserve:

```sql
-- 1. First ensure PartsUsageHistory table exists and has all data

-- 2. Drop the redundant columns
ALTER TABLE machinery_item_inward_unit2 DROP COLUMN used_in_machine;
ALTER TABLE machinery_item_inward_unit2 DROP COLUMN taken_by;
```

## Benefits of This Update

1. **Complete History**: Track every single usage event with date, location, and person
2. **No Data Loss**: Every usage is preserved, not just the last one
3. **Clean Data Model**: Main table contains only inward data, usage in separate table
4. **Better Reporting**: Generate reports by date range, machine, or person
5. **Audit Trail**: Know exactly when and where each item was used
6. **Export Capabilities**: Export history to CSV for external analysis
7. **Reduced Redundancy**: No more duplicate/misleading data

## Example Scenarios

### Before (Bug with old columns)
- Part A received: 100 pcs
- Used 20 pcs in Machine X by John on Jan 1
- Used 30 pcs in Machine Y by Jane on Jan 15
- **Result**: Main table shows "Machine Y, Jane, 50 used" - lost the Jan 1 event!

### After (Fixed with PartsUsageHistory)
- Part A received: 100 pcs
- PartsUsageHistory shows:
  | Date | Qty | Machine | Person |
  |------|-----|---------|--------|
  | Jan 1 | 20 | Machine X | John |
  | Jan 15 | 30 | Machine Y | Jane |
- **Total Used**: 50 pcs (calculated from history)
- **Complete audit trail preserved!**

## Troubleshooting

### "Column 'used_in_machine' does not exist"
This is expected after the update. The column has been intentionally removed.
Update your queries to use `PartsUsageHistory` table instead.

### "Used qty doesn't match history"
The `used_qty` in main table is synced when usage is recorded.
To fix any discrepancies, run:
```sql
UPDATE machinery_item_inward_unit2
SET used_qty = ISNULL((SELECT SUM(quantity_used) FROM PartsUsageHistory WHERE inward_item_id = machinery_item_inward_unit2.id), 0)
```

### View for backward compatibility
Use this view if you need the "last usage" info:
```sql
SELECT * FROM vw_PartsWithUsageSummary
```
This view includes `last_used_in_machine` and `last_taken_by` from history.

### Performance
Indexes are created on:
- `inward_item_id` - for fast lookups by part
- `usage_date` - for date-based queries
- `used_in_machine` - for machine-based reports
