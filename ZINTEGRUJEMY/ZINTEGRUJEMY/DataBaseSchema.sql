CREATE TABLE Products (
    ID INTEGER PRIMARY KEY UNIQUE NOT NULL,
    SKU TEXT UNIQUE NOT NULL,
    Name TEXT,
    EAN TEXT NOT NULL,
    ProducerName TEXT NOT NULL,
    Category TEXT NOT NULL,
    IsWire INTEGER NOT NULL,
    Available INTEGER NOT NULL,
    IsVendor INTEGER NOT NULL,
    DefaultImage TEXT
);

CREATE TABLE Inventories (
    ProductID INTEGER UNIQUE NOT NULL,
    SKU TEXT UNIQUE NOT NULL,
    Unit TEXT NOT NULL,
    Quantity INTEGER NOT NULL,
    Manufacturer TEXT,
    Shipping TEXT NOT NULL,
    ShippingCost REAL,
    PRIMARY KEY (ProductID, SKU),
    FOREIGN KEY (ProductID) REFERENCES Products(ID)
);

CREATE TABLE Prices (
    ID TEXT PRIMARY KEY UNIQUE NOT NULL,
    SKU TEXT UNIQUE NOT NULL,
    NettPrice REAL NOT NULL,
    NettPriceAfterDiscount REAL NOT NULL,
    VatRate REAL NOT NULL,
    NettPriceAfterDiscountForLogisticUnit REAL NOT NULL,
    FOREIGN KEY (SKU) REFERENCES Products(SKU)
);
