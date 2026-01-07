-- Create MachineMaster table
CREATE TABLE MachineMaster (
    id INT PRIMARY KEY IDENTITY(1,1),
    machine_name VARCHAR(255) NOT NULL,
    machine_code VARCHAR(50) NULL,
    location VARCHAR(255) NULL,
    description VARCHAR(500) NULL,
    is_active BIT NOT NULL DEFAULT 1
);

-- Create PersonMaster table
CREATE TABLE PersonMaster (
    id INT PRIMARY KEY IDENTITY(1,1),
    person_name VARCHAR(255) NOT NULL,
    employee_id VARCHAR(50) NULL,
    department VARCHAR(100) NULL,
    contact_number VARCHAR(50) NULL,
    is_active BIT NOT NULL DEFAULT 1
);

-- Insert some default data
INSERT INTO MachineMaster (machine_name, machine_code, location, is_active) 
VALUES 
    ('CNC Machine 1', 'CNC-01', 'Shop Floor A', 1),
    ('Lathe Machine', 'LTH-01', 'Shop Floor B', 1),
    ('Milling Machine', 'MIL-01', 'Shop Floor A', 1);

INSERT INTO PersonMaster (person_name, employee_id, department, is_active)
VALUES
    ('John Doe', 'EMP001', 'Production', 1),
    ('Jane Smith', 'EMP002', 'Maintenance', 1),
    ('Bob Johnson', 'EMP003', 'Production', 1);
