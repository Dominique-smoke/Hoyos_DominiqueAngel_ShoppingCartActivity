# Hoyos_DominiqueAngel_ShoppingCartActivity
## **Description**

 This is a console-based Shopping Cart system using C# language. An object-oriented program which concepts includes, classes, objects, arrays and methods. In this project, the user can select desired products, validate inputs, manage remaining stocks, can generate receipts and access discount features.

## **What you will see in this project**

 It uses Product class to group related data
 Stores the store's products in a fixed array
 Uses int.TryParse() to handle non-numeric user inputs
 ID verifcation
 Calls a dedicated HasEnoughStock() method (To ensure that the customer wont order more items than the store currently have.)
 Zero-Stock handling
 Duplicate Merging 
 Real time Stock Deduction
 Cart Capacity Limit
 Display prices for specific products
 Detects if the total is 5,000 or more and applies a 10% discount automatically
 Provides Receipts
 Provided post-check out inventory

Flowchart
![Flowchart](https://github.com/user-attachments/assets/1d5719d6-743d-4d5a-886f-43e52d7f82a3)

## Ai usage
Used for understanding, debugging and guidance.

## Prompts used 
* help me understand this instruction so that I can create my own code about it
* How to aligned columns properly?
* How can I apply the 10% discount?
* My initial code wont because of null issue, why?
* If I use this to add product in the cart, will it scan for duplicates?
* What is padding? how can this help my code?


## Changes made
* Instead of using 
cart[cartItemCount] = new CartItem { ProductRef = Selected, Quantity = qty };
cartItemCount++;, 
I changed it to 
for (int i = 0; i < count; i++) {
    if (cart[i].ProductRef.Id == prod.Id) {
        cart[i].Quantity += qty; 
        return;

*  Stock Display 
 I added padding, from this:
 Console.WriteLine(Id + " " + Name + " " + RemainingStock);
 to this
 string stockDisplay = RemainingStock > 0 ? RemainingStock.ToString() : "OUT OF STOCK";
            Console.WriteLine($"{Id,-5} {Name,-20} {Price,-10:C} {stockDisplay,-15}"); // used Ai here


