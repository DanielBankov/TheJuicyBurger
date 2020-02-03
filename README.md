# Project - TheJuicyBurger

## Type - Web app

## Description

This is a basic idea for Burger shop project which 
offers burgers by choice from the menu. Guest Users can register
and login to their accounts.
Regular Users can view and order products with quantity.
Regular Users can creating a contract with a restaurant partner.
Regular Users can see their receipt history.
The project also supports Administration. 
Administrators have all rights that regular User has.
Administrators can also Promote and Demote Users.
Administrators can also add, edit or delete products to / from the shop. 
Administrators can also add, edit and delete restaurant partner.
Restaurant partner companies provide their products to the Users.
Restaurant partner companies are just data entities moderated by an Administrator.

## Entities

### User
  - Id (string)
  - Username (string)
  - Password (string)
  - Email (string)
  - Full Name (string)
  - Phone Number (string)
  - Reviews (list of Review)
### Product
  - Id (string)
  - Name (string)
  - In Stock - (bool)
  - Weight - (double)
  - Price (decimal)
  - Carbohydrates (double)
  - Fat (double)
  - Proteins (double)
  - Type (enum) (Classic/Crunchy/Buffalo etc . . .)
  - Image - (string)
  - Total Calories (double)
  - Ingredients (list of Ingredient)
  - Reviews (list of Review)
  - Orders (list of Orders)
### Order
  - Id (string)
  - IssuedOn (dateTime)
  - Quantity (int)
  - Status (Pending, Approver, Delivered)
  - Issuer (User)
  - Dasher (User)
  - Product (list of Products)
 ### Ingredient
  - Id (string)
  - Name (string)
  - Weight (double)
  - Carbohydrates (double)
  - Fat (double)
  - Proteins (double)
  - Price (decimal)
  - Products (list of Product)
### Restaurant
  - Id (string)
  - Full Name (string)
  - Phone Number (string)
  - Company (string)
  - Location (string)
  - VAT Number (string)
### Restaurant Contracts
 - Id (string)
 - Issued On (DateTime)
 - Expires On (DateTime)
 - Price per Month (decimal)
 - Restaurant (Restaurant)
 - Contractor (User)
### Review
 - Id (string)
 - Rating (int)
 - Title (string)
 - Description (string)
 - Product (Product)
 - User (User)
### Dasher
 - Id (string)
 - Name (string)
 - Raiting (int)
 - Orders (list of Order)
### Order Product (mapping table)
### Product Ingredient (mapping table)
### User Review (mapping table)
