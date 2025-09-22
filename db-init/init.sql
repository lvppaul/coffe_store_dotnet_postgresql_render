-- Reset schema mỗi lần build lại
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

-- 1. Category
CREATE TABLE "Category" (
    "CategoryId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Description" VARCHAR(255)
);

-- 2. Product
CREATE TABLE "Product" (
    "ProductId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Price" NUMERIC(10,2) NOT NULL,
    "Description" VARCHAR(255),
    "CategoryId" INT NOT NULL,
    CONSTRAINT "FK_Product_Category" FOREIGN KEY ("CategoryId")
        REFERENCES "Category"("CategoryId")
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

-- 3. Menu
CREATE TABLE "Menu" (
    "MenuId" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "FromDate" DATE NOT NULL,
    "ToDate" DATE NOT NULL
);

-- 4. ProductInMenu
CREATE TABLE "ProductInMenu" (
    "ProductInMenuId" SERIAL PRIMARY KEY,
    "ProductId" INT NOT NULL,
    "MenuId" INT NOT NULL,
    "Quantity" INT NOT NULL CHECK ("Quantity" > 0),
    CONSTRAINT "FK_ProductInMenu_Product" FOREIGN KEY ("ProductId")
        REFERENCES "Product"("ProductId")
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT "FK_ProductInMenu_Menu" FOREIGN KEY ("MenuId")
        REFERENCES "Menu"("MenuId")
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT "UQ_Product_Menu" UNIQUE ("ProductId", "MenuId")
);

-- Insert sample data into Category
INSERT INTO "Category" ("Name", "Description") VALUES
('Coffee', 'Các loại cà phê'),
('Tea', 'Các loại trà'),
('Pastry', 'Bánh ngọt ăn kèm');

-- Insert sample data into Product
INSERT INTO "Product" ("Name", "Price", "Description", "CategoryId") VALUES
('Espresso', 30000, 'Cà phê đậm đặc, vị mạnh', 1),
('Cappuccino', 40000, 'Cà phê sữa bọt', 1),
('Latte', 45000, 'Cà phê sữa nóng', 1),
('Green Tea', 25000, 'Trà xanh truyền thống', 2),
('Black Tea', 20000, 'Trà đen nguyên chất', 2),
('Croissant', 35000, 'Bánh sừng bò bơ', 3),
('Muffin', 30000, 'Bánh muffin socola', 3);

-- Insert sample data into Menu
INSERT INTO "Menu" ("Name", "FromDate", "ToDate") VALUES
('Morning Menu', '2025-09-01', '2025-12-31'),
('Afternoon Menu', '2025-09-01', '2025-12-31');

-- Insert sample data into ProductInMenu
-- Morning Menu (MenuId = 1)
INSERT INTO "ProductInMenu" ("ProductId", "MenuId", "Quantity") VALUES
(1, 1, 50), -- Espresso
(2, 1, 50), -- Cappuccino
(4, 1, 40), -- Green Tea
(6, 1, 20); -- Croissant

-- Afternoon Menu (MenuId = 2)
INSERT INTO "ProductInMenu" ("ProductId", "MenuId", "Quantity") VALUES
(3, 2, 40), -- Latte
(5, 2, 30), -- Black Tea
(7, 2, 25); -- Muffin
