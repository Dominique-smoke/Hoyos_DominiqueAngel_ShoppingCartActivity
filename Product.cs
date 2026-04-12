using System;
using System.Collections.Generic;
using System.Text;

namespace Hoyos_ShoppingCartActivity
{
    internal class Product
       
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
            {
                new Product { Id = 1, Name = "Drawing_Tablet", Price = 25000, RemainingStock = 7 };
                new Product { Id = 2, Name = "Nintendo", Price = 21000, RemainingStock = 3 };
                new Product { Id = 3, Name = "PSP", Price = 16000, RemainingStock = 25 };
                new Product { Id = 4, Name = "XBOX", Price = 24000, RemainingStock = 2 };
                new Product { Id = 5, Name = "GameBoy", Price = 11000, RemainingStock = 5 };


            };

            CartItem[] cart = new CartItem[5];
            int cartItemCount = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===WELCOME DEAR CUSTOMER!!====");
                foreach (var p in inventory) { p.DisplayProduct(); }

                Console.WriteLine("\nEnter the Product Id you want to buy:");
                if (!int.TryParse(Console.ReadLine(), out int prodId))
                {
                    Console.WriteLine("Invalid Input, press any key to continue");
                    Console.ReadKey();
                    continue;

                }
                if (prodId == 0) break;

                Product Selected = Array.Find(inventor, p => p.Id == prodId);

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
                    if (!int.TryParse(Console.ReadLine(), out int qty) || qty == 0)
                    {
                        Console.WriteLine("Invalid Inventory");
                    }
                    else if (!selected.HasEnoughStock(qty))
                    {
                        Console.WriteLine("Not enough Stock available");
                    }
                    else
                    {
                        ProcessCartEntry(cart, ref cartItemCount, selected, qty);
                        Selected.DeductStock(qty);
                        Console.WriteLine("Item added to cart");
                    }


                }
                Console.Write("\nAdd more Items? (Y/N): ");
                if (Console.ReadLine().ToUpper() != "Y") break;
            }
            ShowReceipt(cart, cartItemCount);


        }
    }

}
