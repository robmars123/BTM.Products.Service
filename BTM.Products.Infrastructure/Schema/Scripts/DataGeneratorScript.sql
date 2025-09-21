USE [BTM.Product.Database]
GO

-- Dynamic Product Records Generator
-- Generates random product data using SQL loops and randomization

DECLARE @Counter INT = 1;
DECLARE @MaxRecords INT = 50;
DECLARE @RandomName NVARCHAR(120);
DECLARE @RandomPrice DECIMAL(18,4);
DECLARE @RandomCategory NVARCHAR(50);
DECLARE @RandomBrand NVARCHAR(50);
DECLARE @RandomModel NVARCHAR(50);

-- Product categories for random selection
DECLARE @Categories TABLE (Id INT IDENTITY(1,1), Name NVARCHAR(50))
INSERT INTO @Categories (Name) VALUES 
('Smartphone'), ('Laptop'), ('Tablet'), ('Headphones'), ('Speaker'), 
('Monitor'), ('Keyboard'), ('Mouse'), ('Camera'), ('Smartwatch'),
('Gaming Console'), ('Smart TV'), ('Vacuum'), ('Blender'), ('Air Fryer'),
('Router'), ('Hard Drive'), ('Graphics Card'), ('Processor'), ('Motherboard');

-- Brands for random selection
DECLARE @Brands TABLE (Id INT IDENTITY(1,1), Name NVARCHAR(50))
INSERT INTO @Brands (Name) VALUES 
('Apple'), ('Samsung'), ('Sony'), ('Microsoft'), ('Google'), 
('Dell'), ('HP'), ('Lenovo'), ('ASUS'), ('Acer'),
('LG'), ('Canon'), ('Nikon'), ('Nintendo'), ('Xiaomi'),
('OnePlus'), ('Huawei'), ('Motorola'), ('Garmin'), ('Fitbit');

-- Model suffixes for variety
DECLARE @ModelSuffixes TABLE (Id INT IDENTITY(1,1), Name NVARCHAR(50))
INSERT INTO @ModelSuffixes (Name) VALUES 
('Pro Max'), ('Ultra'), ('Plus'), ('Mini'), ('SE'), 
('Advanced'), ('Professional'), ('Gaming Edition'), ('Lite'), ('Standard'),
('Premium'), ('Elite'), ('Executive'), ('Business'), ('Home'),
('Studio'), ('Creator'), ('Developer'), ('Essential'), ('Flagship');

-- Price ranges by category (Min, Max)
DECLARE @PriceRanges TABLE (Category NVARCHAR(50), MinPrice DECIMAL(18,4), MaxPrice DECIMAL(18,4))
INSERT INTO @PriceRanges VALUES 
('Smartphone', 199.99, 1599.99),
('Laptop', 499.99, 3999.99),
('Tablet', 149.99, 1299.99),
('Headphones', 29.99, 599.99),
('Speaker', 49.99, 899.99),
('Monitor', 199.99, 1999.99),
('Keyboard', 19.99, 299.99),
('Mouse', 9.99, 199.99),
('Camera', 299.99, 7999.99),
('Smartwatch', 99.99, 999.99),
('Gaming Console', 299.99, 699.99),
('Smart TV', 399.99, 4999.99),
('Vacuum', 79.99, 899.99),
('Blender', 39.99, 699.99),
('Air Fryer', 49.99, 399.99),
('Router', 29.99, 499.99),
('Hard Drive', 49.99, 999.99),
('Graphics Card', 199.99, 1999.99),
('Processor', 99.99, 899.99),
('Motherboard', 79.99, 799.99);

-- Clear existing data if needed (uncomment if you want fresh data)
-- DELETE FROM [dbo].[Product];

PRINT 'Starting dynamic product generation...';

-- Main loop to generate records
WHILE @Counter <= @MaxRecords
BEGIN
    -- Get random category
    SELECT TOP 1 @RandomCategory = Name 
    FROM @Categories 
    ORDER BY NEWID();
    
    -- Get random brand
    SELECT TOP 1 @RandomBrand = Name 
    FROM @Brands 
    ORDER BY NEWID();
    
    -- Get random model suffix
    SELECT TOP 1 @RandomModel = Name 
    FROM @ModelSuffixes 
    ORDER BY NEWID();
    
    -- Generate random price based on category
    SELECT 
        @RandomPrice = ROUND(
            MinPrice + (RAND() * (MaxPrice - MinPrice)), 2
        )
    FROM @PriceRanges 
    WHERE Category = @RandomCategory;
    
    -- Build product name
    SET @RandomName = @RandomBrand + ' ' + @RandomCategory + ' ' + @RandomModel;
    
    -- Add some variation to names
    IF @Counter % 3 = 0
        SET @RandomName = @RandomName + ' v' + CAST((@Counter % 5 + 1) AS NVARCHAR(2));
    
    IF @Counter % 7 = 0
        SET @RandomName = @RandomName + ' (2024 Edition)';
        
    -- Insert the record
    INSERT INTO [dbo].[Product] ([Id], [Name], [UnitPrice], [IsDeleted])
    VALUES (
        NEWID(), 
        @RandomName, 
        @RandomPrice, 
        CASE 
            WHEN @Counter % 20 = 0 THEN 1  -- 5% chance of being deleted
            ELSE 0 
        END
    );
    
    -- Progress indicator
    IF @Counter % 10 = 0
        PRINT 'Generated ' + CAST(@Counter AS NVARCHAR(10)) + ' products...';
    
    SET @Counter = @Counter + 1;
END;

PRINT 'Product generation completed!';

-- Generate summary statistics
SELECT 
    'Generation Summary' as ReportType,
    COUNT(*) as TotalRecords,
    SUM(CASE WHEN IsDeleted = 0 THEN 1 ELSE 0 END) as ActiveRecords,
    SUM(CASE WHEN IsDeleted = 1 THEN 1 ELSE 0 END) as DeletedRecords,
    MIN(UnitPrice) as MinPrice,
    MAX(UnitPrice) as MaxPrice,
    AVG(UnitPrice) as AvgPrice
FROM [dbo].[Product];

-- Show sample of generated products
SELECT TOP 15
    LEFT(CAST(Id AS NVARCHAR(50)), 8) + '...' as ShortId,
    Name,
    FORMAT(UnitPrice, 'C') as FormattedPrice,
    CASE WHEN IsDeleted = 1 THEN 'Deleted' ELSE 'Active' END as Status
FROM [dbo].[Product]
ORDER BY UnitPrice DESC;

-- Optional: Generate additional batch with different logic
-- Uncomment below to generate another 25 records with different naming pattern

/*
PRINT 'Generating additional specialized products...';

DECLARE @SpecialProducts TABLE (Name NVARCHAR(120), Price DECIMAL(18,4))
INSERT INTO @SpecialProducts VALUES 
('Enterprise Database Server Pro', 25999.99),
('AI Development Workstation Ultra', 15999.99),
('Quantum Computing Simulator', 99999.99),
('Blockchain Mining Rig Professional', 8999.99),
('Cloud Infrastructure Manager', 5999.99);

DECLARE @SpecialCounter INT = 1;
WHILE @SpecialCounter <= 25
BEGIN
    IF @SpecialCounter <= 5
    BEGIN
        -- Insert from predefined special products
        SELECT TOP 1 @RandomName = Name, @RandomPrice = Price
        FROM @SpecialProducts
        ORDER BY NEWID();
    END
    ELSE
    BEGIN
        -- Generate tech-focused names
        SET @RandomName = 'Tech Product ' + 
                         CAST(@SpecialCounter AS NVARCHAR(5)) + ' ' +
                         CASE (@SpecialCounter % 4)
                             WHEN 0 THEN 'Enterprise Edition'
                             WHEN 1 THEN 'Developer Kit'
                             WHEN 2 THEN 'Business Suite'
                             ELSE 'Professional Tools'
                         END;
        
        SET @RandomPrice = ROUND(100 + (RAND() * 9900), 2);
    END;
    
    INSERT INTO [dbo].[Product] ([Id], [Name], [UnitPrice], [IsDeleted])
    VALUES (NEWID(), @RandomName, @RandomPrice, 0);
    
    SET @SpecialCounter = @SpecialCounter + 1;
END;

PRINT 'Specialized products generated!';
*/

-- Final verification
SELECT 
    'Final Count' as ReportType,
    COUNT(*) as TotalProducts,
    COUNT(DISTINCT LEFT(Name, CHARINDEX(' ', Name + ' ') - 1)) as UniqueBrands,
    AVG(LEN(Name)) as AvgNameLength
FROM [dbo].[Product];

GO