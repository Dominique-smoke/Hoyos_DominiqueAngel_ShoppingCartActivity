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
            string alert = (RemainingStock > 0 && RemainingStock <= 3) ? "LOW STOCK!!!" : ""; // req 4
            Console.WriteLine($"{Id,-5} {Name,-20} {Price,-10:C} {stockDisplay,-10} {alert}");
        }

        public double GetItemTotal(int quantity) => Price * quantity; //
        public bool HasEnoughStock(int quantity) => RemainingStock >= quantity;

        public void DeductStock(int quantity) => RemainingStock -= quantity;
        public void Restock(int qty) => RemainingStock += qty;
    }

}

    

    