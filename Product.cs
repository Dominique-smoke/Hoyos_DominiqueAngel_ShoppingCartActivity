using System;
using System.Collections.Generic;
using System.Linq; 

namespace Hoyos_ShoppingCartActivity
{
    internal class Product
    {
        
        private int _id;
        private string _name = string.Empty;
        private string _category = string.Empty;
        private double _price;
        private int _remainingStock;

        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                
                if (value < 0) _price = 0;
                else _price = value;
            }
        }

        public int RemainingStock
        {
            get { return _remainingStock; }
            set
            {
                
                if (value < 0) _remainingStock = 0;
                else _remainingStock = value;
            }
        }

        public void DisplayProduct()
        {
            string stockDisplay = RemainingStock > 0 ? RemainingStock.ToString() : "OUT OF STOCK";
            string alert = (RemainingStock > 0 && RemainingStock <= 3) ? "LOW STOCK!!!" : "";
            Console.WriteLine($"{Id,-5} {Name,-20} {Category,-15} {Price,-10:C} {stockDisplay,-10} {alert}");
        }

        public double GetItemTotal(int quantity) => Price * quantity;
        public bool HasEnoughStock(int quantity) => RemainingStock >= quantity;
        public void DeductStock(int quantity) => RemainingStock -= quantity;
        public void Restock(int qty) => RemainingStock += qty;
    }
}



    

    