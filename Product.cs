using System;
using System.Collections.Generic;
using System.Text;

namespace Hoyos_ShoppingCartActivity
{
    internal class Product
       
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int RemainingStock { get; set; }

        public void DisplayProduct()
        {
            string stockInfo = RemainingStock > 0 ? RemainingStock.ToString() : "NO STOCK LEFT!!!";

            Console.WriteLine($"{Id,-5} {Name, -20} {Price, -10:C} {stockInfo, -15}");

        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool HasEnoughStock (int quantity)
        {  
            return RemainingStock >= quantity;
        }
        public void DeductStock(int quantity)
        {
           RemainingStock -= quantity;
        }
    
       


    }
    class CartItem
    {
        public Product ProductRef { get; set; }
        public int Quantity { get; set; }
        public double SubTotal => ProductRef.Price * Quantity;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Product[] inventory = new Product[]
            {
                new Product { Id = 1, Name = "Drawing_Tablet", Price = 25000, RemainingStock = 7 },
                new Product { Id = 2, Name = "Nintendo", Price = 21000, RemainingStock = 3 },
                new Product { Id = 3, Name = "PSP", Price = 16000, RemainingStock = 25 },
                new Product { Id = 4, Name = "XBOX", Price = 24000, RemainingStock = 2 },
                new Product { Id = 5, Name = "GameBoy", Price = 11000, RemainingStock = 5 },


            };

            CartItem[] cart = new CartItem[5];
            int cartItemCount = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===WELCOME DEAR CUSTOMER!!====");
                Console.WriteLine($"{"ID",-5} {"Name",-20} {"Price",-10} {"Stock",-15}"); // added after the debugging
                Console.WriteLine("----------------------------------------------------------"); // added after the debugging
                foreach (var p in inventory) { p.DisplayProduct(); }

                Console.WriteLine("\nEnter the Product Id you want to buy (or 0 to checkout):");
                if (!int.TryParse(Console.ReadLine(), out int prodId))
                {
                    Console.WriteLine("Invalid Input, press any key to continue");
                    Console.ReadKey();
                    continue;

                }
                if (prodId == 0) break;

                Product? Selected = Array.Find(inventory, p => p.Id == prodId);

                if (Selected == null)
                {
                    Console.WriteLine("Invalid product number!");
                }
                else if (Selected.RemainingStock == 0)
                {
                    Console.WriteLine("This Item is out of stock");
                }
                else
                {
                    Console.WriteLine($"Enter quantity for {Selected.Name}:");
                    if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                    {
                        Console.WriteLine("Invalid Inventory");
                    }
                    else if (!Selected.HasEnoughStock(qty))
                    {
                        Console.WriteLine("Not enough Stock available");
                    }
                    else
                    {
                        ProcessCartEntry(cart, ref cartItemCount, Selected, qty);
                        Selected.DeductStock(qty);
                        Console.WriteLine("Item added to cart");
                    }


                }
                Console.Write("\nAdd more Items? (Y/N): ");
                string? input = Console.ReadLine();
                if (!string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase)) break;
            }
        }
        static void ProcessCartEntry(CartItem[] cart, ref int count, Product prod, int qty)
        {
            for (int i = 0; i < count; i++)
            {
                if (cart[i].ProductRef.Id == prod.Id)
                {
                    cart[i].Quantity += qty;
                    return;
                }

            }
            if (count < cart.Length)
            {
                cart[count] = new CartItem { ProductRef = prod, Quantity = qty };
                count++;
            }
            else
            {
                Console.WriteLine("Cart is Full");
            }
        
        }
        static void ShowReceipt(CartItem[] cart, int count)
        {
            Console.Clear();
            Console.WriteLine("=== YOUR RECEIPT ===");
            double grandTotal = 0;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{cart[i].ProductRef.Name,-20} x{cart[i].Quantity} - {cart[i].SubTotal:C}");
                grandTotal += cart[i].SubTotal;
            }
            Console.WriteLine("--------------------");
            Console.WriteLine($"TOTAL: {grandTotal:C}");
            Console.WriteLine("Thank you for shopping!");
            Console.ReadKey();
        }
    }
}


