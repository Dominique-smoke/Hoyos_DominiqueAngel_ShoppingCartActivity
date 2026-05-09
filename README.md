# Hoyos_DominiqueAngel_ShoppingCartActivity
## **Description**

 The store initializes an inventory array containing gadgets.
 Users can browse the entire list, search for specific names using keywords, 
 or filter products by their specific category. The user can manage the cart where they can search products, 
 view history, filter by category and checkout products.

## **Part 2 features**

Product Discovery: Users can browse the catalog, search for specific products by name, or filter items by category using LINQ queries.

Cart Management: The system supports adding items to a cart, updating quantities, removing items, or clearing the entire cart with automatic stock restoration.

Checkout: The program calculates a 10% discount for orders totaling PHP 5,000 or more and enforces a payment validation loop to ensure the user provides a sufficient numeric amount.

Receipts & History: Upon successful payment, it generates a formatted receipt with a unique number and timestamp, then logs the transaction into a permanent order history.

Inventory Alerts: After every checkout, the system identifies and displays a "Low Stock Alert" for any products with 5 or fewer items remaining.

Flowchart for Part 1
![Flowchart](https://github.com/user-attachments/assets/1d5719d6-743d-4d5a-886f-43e52d7f82a3)

## Ai usage in part 2
Used to undertand how to implement:
* Cart Management View, update quantity, remove item, clear cart 
* Payment 
* Receipt Details
* Low Stock Alert
* Order History 
Because compared to part 1, part 2 is more complicated and advance. 
Especially with viewing history, product search, filter categories, and manage cart.

## Prompts used in Part 2
* in C#, How can I update my checkout method to get reciept number and date
* in C# how can I higlight a text?
* What does query mean in C#
* Why is my category showing as error
* is it right to use ShowError instead of just Error?
* Is there a shorter way to add the remove, update, clear, checkout, back?
* Did I put something to make my code crash? (debugging)



## Changes made in Part 1
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
## Changes made in Part 2
The comments addressed in part 1:
GetItemTotal: I added this to the Product class. Now, the CartItem.SubTotal property specifically calls ProductRef.GetItemTotal(Quantity) instead of doing its own math.

Fixed-Size Array: I replaced the List with CartItem[] cart = new CartItem[5]. I added a cartCount variable to keep track of how many slots are used. When cartCount hits 5, the program blocks new unique items.

Y/N: Instead of using "0" to exit, I implemented the if (choice != "Y") continueShopping = false; logic. This ensures the loop only ends when the user is actually finished.

Discount Logic: Added a block at the end that checks if the grandTotal >= 5000. If it is, it calculates a 10% discount and subtracts it before printing the final total.

Post-Checkout Inventory: At the very end of the program, it loops through the inventory array one last time to show the user (and the teacher) that the RemainingStock has actually decreased based on the purchases.


## Screenshots or sample output of the program
![] (<img width="579" height="399" alt="Image" src="https://github.com/user-attachments/assets/5a7ba9d7-5620-478b-92ee-783b6db8e386" />)

(<img width="1123" height="353" alt="Image" src="https://github.com/user-attachments/assets/28e99a9d-571e-495a-bf2b-3927bac82d21" />)

(<img width="1128" height="445" alt="Image" src="https://github.com/user-attachments/assets/ad64c06e-d827-4437-98bb-fe6840675b09" />)

(<img width="1118" height="371" alt="Image" src="https://github.com/user-attachments/assets/4a3da222-6871-420e-bb3d-1203e21b1637" />)

(<img width="520" height="267" alt="Image" src="https://github.com/user-attachments/assets/7b786a66-7d75-47c9-bc12-e506d1ab46bb" />)

(<img width="887" height="338" alt="Image" src="https://github.com/user-attachments/assets/55ab2a0b-32e8-49a5-9f51-e68e2202ba40" />)

(<img width="993" height="395" alt="Image" src="https://github.com/user-attachments/assets/33609534-2b30-434e-9675-9997fe2bdac4" />)

(<img width="1093" height="414" alt="Image" src="https://github.com/user-attachments/assets/e2055e15-ec99-4f46-a123-df79e1ce4620" />)

(<img width="707" height="670" alt="Image" src="https://github.com/user-attachments/assets/5bcf89f4-ce60-44bc-921a-4a8b2bae4a4b" />)

(<img width="968" height="253" alt="Image" src="https://github.com/user-attachments/assets/140e1c5d-44fd-4d67-96aa-3982aabb2dca" />)

(<img width="1031" height="374" alt="Image" src="https://github.com/user-attachments/assets/ba0125fa-b4a1-4f3a-a98c-f124bf1dd332" />)


