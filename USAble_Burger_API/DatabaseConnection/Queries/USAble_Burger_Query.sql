/*Database Creation*/
CREATE DATABASE USAble_Burger

/*Table Creation*/
CREATE TABLE Users (
    UserId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(20) NOT NULL,
    UserPassword VARCHAR(20) NOT NULL
);

CREATE TABLE Orders (
	OrderId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	OrderTotal FLOAT NOT NULL,
	OrderTaker INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	OrderItems VARCHAR(200) NOT NULL
);

CREATE TABLE Items (
	ItemId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ItemName VARCHAR(20) NOT NULL,
	ItemPrice FLOAT NOT NULL
);

CREATE TABLE Discounts (
	DiscountId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	DiscountName VARCHAR(20) NOT NULL,
	DiscountPercentage FLOAT NOT NULL
);

/*Insert Data*/

INSERT INTO Items (ItemName, ItemPrice)
VALUES 
('Hamburger', 5.99), 
('Corndog', 1.99), 
('Fries', 1.99), 
('Chips', 0.99);

/*This is not how account creation would work in reality. 
Especially with explicitly stated passwords.*/
INSERT INTO Users (UserName, UserPassword)
VALUES 
('Tammy', 'password123'), 
('Ken', 'kenough1990');

INSERT INTO Discounts (DiscountName, DiscountPercentage)
VALUES 
('Military', 0.10), 
('Employee', 0.15);

/*For Table Deletion*/
DROP TABLE Orders;
DROP TABLE Users;
DROP TABLE Items;
DROP TABLE Discounts;