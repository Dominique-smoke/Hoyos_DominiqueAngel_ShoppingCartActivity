using Hoyos_ShoppingCartActivity;
using System.Runtime.InteropServices;
class CartItem
{
    public Product ProductRef { get; set; } = new Product();
    public int Quantity { get; set; }
    
    public double SubTotal => ProductRef.GetItemTotal(Quantity);
}


class Program
{
    static void Main(string[] args)
    {
        
        Product[] inventory =
        {
                new Product { Id = 1, Name = "Drawing Tablet", Price = 25000, RemainingStock = 7 },
                new Product { Id = 2, Name = "Nintendo Switch", Price = 21000, RemainingStock = 3 },
                new Product { Id = 3, Name = "PSP", Price = 16000, RemainingStock = 25 },
                new Product { Id = 4, Name = "XBOX", Price = 24000, RemainingStock = 2 },
                new Product { Id = 5, Name = "GameBoy", Price = 11000, RemainingStock = 5 }
            };

        // Fixed-size array 
        CartItem[] cart = new CartItem[5];
        int cartCount = 0;
        bool continueShopping = true;

        while (continueShopping)
        {
            Console.Clear();
            Console.WriteLine("=== WELCOME TO THE GEEK STORE ===");
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Price",-10} {"Stock",-15}");
            Console.WriteLine(new string('-', 55));

            foreach (var p in inventory) p.DisplayProduct();

            
            Console.Write("\nEnter Product ID to buy: ");
            if (!int.TryParse(Console.ReadLine(), out int prodId))
            {
                Console.WriteLine("Error: Please enter a numeric ID.");
                Wait(); continue;
            }

            
            var selected = inventory.FirstOrDefault(p => p.Id == prodId);
            if (selected == null)
            {
                Console.WriteLine("Error: Product not found!");
                Wait(); continue;
            }

             
            if (selected.RemainingStock <= 0)
            {
                Console.WriteLine("Error: This item is sold out!");
                Wait(); continue;
            }

            
            Console.Write($"Quantity for {selected.Name}: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Error: Invalid quantity.");
                Wait(); continue;
            }

            // Validate Stock Availability
            if (!selected.HasEnoughStock(qty))
            {
                Console.WriteLine("Error: Not enough stock available.");
                Wait(); continue;
            }

            //
            bool foundInCart = false;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].ProductRef.Id == selected.Id)
                {
                    cart[i].Quantity += qty;
                    selected.DeductStock(qty);
                    foundInCart = true;
                    Console.WriteLine("Success: Updated item in cart!");
                    break;
                }
            }

            if (!foundInCart)
            {
                if (cartCount < cart.Length)
                {
                    cart[cartCount] = new CartItem { ProductRef = selected, Quantity = qty };
                    selected.DeductStock(qty);
                    cartCount++;
                    Console.WriteLine("Success: Added to cart!");
                }
                else
                {
                    Console.WriteLine("Error: Your cart is full! (Max 5 unique items)");
                }
            }

            // (Y/N)
            Console.Write("\nContinue shopping? (Y/N): ");
            string choice = Console.ReadLine().ToUpper();
            if (choice != "Y")
            {
                continueShopping = false;
            }
        }

        // Receipt
        Console.Clear();
        Console.WriteLine("================ OFFICIAL RECEIPT ================");
        double grandTotal = 0;

        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{cart[i].ProductRef.Name,-20} x{cart[i].Quantity,-5} {cart[i].SubTotal,15:C}");
            grandTotal += cart[i].SubTotal;
        }

        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"SUBTOTAL: {grandTotal,37:C}");

        // 10% Discount
        if (grandTotal >= 5000)
        {
            double discount = grandTotal * 0.10;
            grandTotal -= discount;
            Console.WriteLine($"DISCOUNT (10%): -{discount,31:C}");
        }

        Console.WriteLine($"FINAL TOTAL: {grandTotal,34:C}");
        Console.WriteLine("==================================================");

        
        Console.WriteLine("\n--- Updated Inventory Levels ---");
        foreach (var p in inventory)
        {
            Console.WriteLine($"{p.Name,-20}: {p.RemainingStock} items remaining");
        }

        Console.WriteLine("\nThank you for shopping! Press any key to exit.");
        Console.ReadKey();
    }

    static void Wait()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

 