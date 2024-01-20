/***************************************************
Script: joins.sql
Author: Brian Minaji
Date: July 2022
Description: Create some tables to demonstrate joins
****************************************************/

-- Setting NOCOUNT ON suppresses completion messages for each INSERT
SET NOCOUNT ON

-- Make the master database the current database
USE master

-- If database joins exists, drop it
IF EXISTS (SELECT  * FROM sysdatabases WHERE name = 'joins')
BEGIN
  ALTER DATABASE joins SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE joins;
END
GO

-- Create the joins database
CREATE DATABASE joins;
GO

-- Make the joins database the current database
USE joins;

-- Create products table
CREATE TABLE products (
  product_id INT PRIMARY KEY, 
  product_description VARCHAR(25));

-- Create sales table
CREATE TABLE sales (
  product_id INT PRIMARY KEY, 
  sold INT);

-- Create purchases table
CREATE TABLE purchases (
  product_id INT PRIMARY KEY, 
  purchased INT);

-- Create employees table
CREATE TABLE employees (
  employee_id INT PRIMARY KEY, 
  first_name VARCHAR(10), 
  last_name VARCHAR(10), 
  department VARCHAR(10), 
  title VARCHAR(15), 
  supervisor INT,
  salary MONEY);

-- Create company_stats table
CREATE TABLE company_stats (
  number_of_employees INT NOT NULL);
GO

-- Insert products records
INSERT INTO products VALUES(100, 'Super Outfit');
INSERT INTO products VALUES(101, 'Giant Rubber Band');
INSERT INTO products VALUES(102, 'Dehydrated Boulders');
INSERT INTO products VALUES(103, 'Rocket Sled');
INSERT INTO products VALUES(104, 'Invisible Paint');
INSERT INTO products VALUES(105, 'Anvils');
INSERT INTO products VALUES(106, 'Instant Water');

-- Insert sales records
INSERT INTO sales VALUES(100, 5);
INSERT INTO sales VALUES(101, 6);
INSERT INTO sales VALUES(103, 8);
INSERT INTO sales VALUES(104, 5);
INSERT INTO sales VALUES(106, 7);
GO

-- Insert purchases records
INSERT INTO purchases VALUES(100, 15);
INSERT INTO purchases VALUES(102, 25);
INSERT INTO purchases VALUES(103, 10);
INSERT INTO purchases VALUES(106, 10);
GO

-- Insert employees records
INSERT INTO employees VALUES(101, 'Malcolm', 'Reynolds', 'Management', 'President', NULL, 1000);
INSERT INTO employees VALUES(102, 'Zoe', 'Washburne', 'Sales', 'VP of Sales', 101, 750);
INSERT INTO employees VALUES(103, 'Jayne', 'Cobb', 'Sales', 'Sales Rep', 102, 500);
INSERT INTO employees VALUES(104, 'Hoban', 'Washburne', 'Finance', 'VP of Finance', 101, 750);
INSERT INTO employees VALUES(105, 'Kaylee', 'Frye', 'Finance', 'AP Clerk', 104, 400);
INSERT INTO employees VALUES(106, 'Inara', 'Serra', 'Finance', 'AR Clerk', 104, 400);
INSERT INTO employees VALUES(107, 'Simon', 'Tam', 'Sales', 'Sales Rep', 102, 500);
INSERT INTO employees VALUES(108, 'Derrial', 'Book', 'Sales', 'Sales Rep',  102, 500);
INSERT INTO employees VALUES(109, 'River', 'Tam', 'Finance', 'GL Clerk', 104, 400);
GO

-- Insert company_stats records
INSERT INTO company_stats VALUES(9);
GO

-- Verify inserts
CREATE TABLE verify (
  table_name varchar(30), 
  actual INT, 
  expected INT);
GO

INSERT INTO verify VALUES('products', (SELECT COUNT(*) FROM products), 7);
INSERT INTO verify VALUES('sales', (SELECT COUNT(*) FROM sales), 5);
INSERT INTO verify VALUES('purchases', (SELECT COUNT(*) FROM purchases), 4);
INSERT INTO verify VALUES('employees', (SELECT COUNT(*) FROM employees), 9);
INSERT INTO verify VALUES('company_stats', (SELECT COUNT(*) FROM company_stats), 1);
PRINT 'Verification';
SELECT table_name, actual, expected, expected - actual discrepancy FROM verify;
DROP TABLE verify;
GO