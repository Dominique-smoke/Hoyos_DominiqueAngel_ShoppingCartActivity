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
    static Product[] inventory = {
        new Product { Id = 1, Name = "Drawing Tablet", Price = 25000, RemainingStock = 7 },
        new Product { Id = 2, Name = "Nintendo Switch", Price = 21000, RemainingStock = 3 },
        new Product { Id = 3, Name = "PSP", Price = 16000, RemainingStock = 25 },
        new Product { Id = 4, Name = "XBOX", Price = 24000, RemainingStock = 2 },
        new Product { Id = 5, Name = "GameBoy", Price = 11000, RemainingStock = 5 }
    };
    static CartItem[] cart = new CartItem[5];
    static int cartCount = 0;
    static List<string> orderHistory = new List<string>();
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== WELCOME TO THE GEEK STORE ===");
            Console.WriteLine("1. Shop (Browse Products)");
            Console.WriteLine("2. View/Manage Cart");
            Console.WriteLine("3. Order History");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": ShopMenu(); break;
                case "2": CartManagementMenu(); break;
                case "3": ViewHistory(); break;
                case "4": return;
            }
        }
    }
    static void ShopMenu()
    {
        Console.Clear();
        Console.WriteLine("=== BROWSE PRODUCTS ===");
        foreach (var p in inventory) p.DisplayProduct();

        Console.Write("\nEnter ID to add (or 0 to go back): ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) return;

        var prod = inventory.FirstOrDefault(p => p.Id == id);
        if (prod == null || prod.RemainingStock <= 0) { Error("Invalid product."); return; }

        Console.Write("Enter Quantity: ");
        if (int.TryParse(Console.ReadLine(), out int qty) && prod.HasEnoughStock(qty))
        {
            AddToCart(prod, qty);
        }
        else { Error("Invalid quantity or low stock."); }
    }
    static void CartManagementMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== MANAGE CART ===");
            if (cartCount == 0) { Console.WriteLine("Cart is empty."); Wait(); return; }

            for (int i = 0; i < cartCount; i++)
                Console.WriteLine($"{i + 1}. {cart[i].ProductRef.Name} (x{cart[i].Quantity}) - {cart[i].SubTotal:C}");

            Console.WriteLine("\n[R] Remove | [U] Update Qty | [C] Clear | [K] Checkout | [B] Back"); // the remove, update, clear, checkout, back
            Console.Write("Option: ");
            string opt = Console.ReadLine().ToUpper();

            if (opt == "B") break;
            if (opt == "C") { ClearCart(); break; }
            if (opt == "K") { Checkout(); break; }

            if (opt == "R" || opt == "U")
            {
                Console.Write("Enter Item Number: ");
                int idx = int.Parse(Console.ReadLine()) - 1;
                if (idx < 0 || idx >= cartCount) { Error("Invalid index."); continue; }

                if (opt == "R") RemoveFromCart(idx);
                if (opt == "U") UpdateQuantity(idx);
            }
        }
    }
    static void AddToCart(Product p, int qty)
    {
       

    }





            