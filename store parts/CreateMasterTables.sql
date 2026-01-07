-- Create Master Tables for Party Names and Items

-- Create PartyMaster table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PartyMaster]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[PartyMaster](
        [id] [int] IDENTITY(1,1) NOT NULL,
        [party_name] [varchar](255) NOT NULL,
        [contact_person] [varchar](255) NULL,
        [phone] [varchar](50) NULL,
        [address] [varchar](500) NULL,
        [is_active] [bit] NOT NULL DEFAULT 1,
        CONSTRAINT [PK_PartyMaster] PRIMARY KEY CLUSTERED ([id] ASC)
    )
END
GO

-- Create ItemMaster table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemMaster]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ItemMaster](
        [id] [int] IDENTITY(1,1) NOT NULL,
        [item_name] [varchar](255) NOT NULL,
        [item_code] [varchar](50) NULL,
        [description] [varchar](500) NULL,
        [unit] [varchar](50) NULL,
        [is_active] [bit] NOT NULL DEFAULT 1,
        CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED ([id] ASC)
    )
END
GO

PRINT 'Master tables created successfully!'
