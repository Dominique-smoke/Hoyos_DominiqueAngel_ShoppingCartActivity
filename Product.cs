using System;
using System.Collections.Generic;
using System.Linq; // Added for easier array manipulation

namespace Hoyos_ShoppingCartActivity
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int RemainingStock { get; set; }

        public void DisplayProduct()
        {
            string stockDisplay = RemainingStock > 0 ? RemainingStock.ToString() : "OUT OF STOCK";
            Console.WriteLine($"{Id,-5} {Name,-20} {Price,-10:C} {stockDisplay,-15}"); // used Ai here
        }

        public bool HasEnoughStock(int quantity) => RemainingStock >= quantity;

        public void DeductStock(int quantity) => RemainingStock -= quantity;
    }

}

    

    