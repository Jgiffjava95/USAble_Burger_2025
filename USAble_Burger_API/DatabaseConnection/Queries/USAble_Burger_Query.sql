/*Database Creation*/
CREATE DATABASE USAble_Burger

/*Table Creation*/
CREATE TABLE Users (
    UserId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(20) NOT NULL,
    UserPassword VARCHAR(20) NOT NULL,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL
);

CREATE TABLE Items (
	ItemId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ItemName VARCHAR(20) NOT NULL,
	ItemPrice FLOAT NOT NULL,
	ItemType INT NOT NULL FOREIGN KEY REFERENCES ItemTypes(TypeId)
);

CREATE TABLE ItemTypes (
	TypeId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	TypeName VARCHAR(20) NOT NULL
);

CREATE TABLE Discounts (
	DiscountId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	DiscountName VARCHAR(20) NOT NULL,
	DiscountPercentage FLOAT NOT NULL
);

CREATE TABLE TaxType (
	TaxId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	TaxName VARCHAR(20) NOT NULL,
	TaxPercentage FLOAT NOT NULL
);

CREATE TABLE Orders (
	OrderId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	OrderSubTotal FLOAT NOT NULL,
	OrderDiscountAmount FLOAT NOT NULL,
	OrderPreTaxTotal FLOAT NOT NULL,
	OrderTaxAmount FLOAT NOT NULL,
	OrderTotal FLOAT NOT NULL,
	OrderDate Date NOT NULL,
	OrderTaker INT NOT NULL FOREIGN KEY REFERENCES Users(UserId)
);

CREATE TABLE OrderItems (
	OrderItemId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	OrderItemName VARCHAR(20) NOT NULL,
	OrderItemQuan Int NOT NULL,
	ItemId INT NOT NULL FOREIGN KEY REFERENCES Items(ItemId),
	OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderId)
);

/*Insert Data*/

INSERT INTO ItemTypes (TypeName)
VALUES 
('Main'), 
('Side'), 
('Drink'), 
('Other');

INSERT INTO Items (ItemName, ItemPrice, ItemType)
VALUES 
('Hamburger', 5.99, 1), 
('Corndog', 1.99, 1), 
('Fries', 1.99, 2), 
('Chips', 0.99, 2),
('Soda', 0.99, 3),
('Water', 0.99, 3),
('Tea', 0.99, 3),
('Ketchup Packet', 0.25, 4),
('Mustard Packet', 0.25, 4);

/*This is not how account creation would work in reality. 
Especially with explicitly stated passwords.*/
INSERT INTO Users (UserName, UserPassword, FirstName, LastName)
VALUES 
('Tammy4', 'password123', 'Tammy', 'Person'), 
('Ken9', 'kenough1990', 'Ken', 'Dude');

INSERT INTO Discounts (DiscountName, DiscountPercentage)
VALUES 
('Military', 0.10), 
('Employee', 0.15);

INSERT INTO TaxType (TaxName, TaxPercentage)
VALUES 
('City', 0.04), 
('State', 0.065);
