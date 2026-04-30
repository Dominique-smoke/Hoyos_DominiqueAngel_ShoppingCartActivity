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

            string choice = Console.ReadLine()!;
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
        if (prod == null || prod.RemainingStock <= 0) { ShowError("Invalid product."); return; }

        Console.Write("Enter Quantity: ");
        if (int.TryParse(Console.ReadLine(), out int qty) && prod.HasEnoughStock(qty))
        {
            AddToCart(prod, qty);
        }
        else { ShowError("Invalid quantity or low stock."); }
    }
    static void CartManagementMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== MANAGE CART ===");
            if (cartCount == 0)
            { 
                Console.WriteLine("Cart is empty."); 
                Wait(); 
                return; 
            }

            for (int i = 0; i < cartCount; i++)
                Console.WriteLine($"{i + 1}. {cart[i].ProductRef.Name} (x{cart[i].Quantity}) - {cart[i].SubTotal:C}");

            Console.WriteLine("\n[R] Remove Item | [U] Update Qty | [C] Clear Cart | [K] Checkout | [B] Back"); // the remove, update, clear, checkout, back
            Console.Write("Option: ");
            string opt = Console.ReadLine() !.ToUpper();

            if (opt == "B") break;
            if (opt == "C") { ClearCart(); break; }
            if (opt == "K") { Checkout(); break; }

            if (opt == "R" || opt == "U")
            {
                Console.Write("Enter Item Number: ");
                if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > cartCount)
                { 
                    ShowError("Invalid selection"); 
                    continue; 
                }

                if (opt == "R") RemoveFromCart(idx - 1);
                if (opt == "U") UpdateQuantity(idx - 1);
            }
        }
    }
    static void AddToCart(Product p, int qty)
    {
        var existing = cart.Take(cartCount).FirstOrDefault(c => c.ProductRef.Id == p.Id);
        if (existing != null) { existing.Quantity += qty; }
        else if (cartCount < 5) 
        { cart[cartCount++] = new CartItem 

        { ProductRef = p, Quantity = qty }; 

        }
        else { ShowError("Cart is full!"); return; }
        p.DeductStock(qty);
        Console.WriteLine("Success!"); Wait();



    }
    static void RemoveFromCart(int index)
    {
        cart[index].ProductRef.Restock(cart[index].Quantity);
        for (int i = index; i < cartCount - 1; i++) cart[i] = cart[i + 1];
        cart[--cartCount] = null!;
        Console.WriteLine("Item removed."); Wait();
    }
    static void UpdateQuantity(int index)
    {
        Console.Write("Enter New Quantity: ");
        if (int.TryParse(Console.ReadLine(), out int newQty) && newQty > 0)
        {
            int diff = newQty - cart[index].Quantity;
            if (cart[index].ProductRef.HasEnoughStock(diff))
            {
                cart[index].ProductRef.DeductStock(diff);
                cart[index].Quantity = newQty;
                Console.WriteLine("Updated.");
            }
            else ShowError("Insufficient stock.");
        }
        Wait();
    }
    static void ClearCart()
    {
        for (int i = 0; i < cartCount; i++) cart[i].ProductRef.Restock(cart[i].Quantity);
        Array.Clear(cart, 0, cart.Length);
        cartCount = 0;
        Console.WriteLine("Cart cleared."); Wait();
    }
    static void Checkout()
    {
        double total = cart.Take(cartCount).Sum(c => c.SubTotal);
        if (total >= 5000) total *= 0.90;

        Console.WriteLine($"\nAmount Due: {total:C}");
        Console.Write("Enter Payment: ");
        if (double.TryParse(Console.ReadLine(), out double pay) && pay >= total)
        {
            string rNo = "REC-" + new Random().Next(10000, 99999);
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            orderHistory.Add($"{rNo} | {date} | Total: {total:C}");

            Console.WriteLine($"\nSuccess! Change: {(pay - total):C}");
            Console.WriteLine($"Receipt: {rNo} at {date}");
            Array.Clear(cart, 0, cart.Length);
            cartCount = 0;
        }
        else ShowError("Payment failed.");
        Wait();
    }
    static void ViewHistory()
    {
        Console.Clear();
        Console.WriteLine("=== ORDER HISTORY ===");
        if (orderHistory.Count == 0) 
        Console.WriteLine("No history.");
        else orderHistory.ForEach(Console.WriteLine);
        Wait();
    }

    static void ShowError(string m)
    { Console.ForegroundColor = ConsoleColor.Red; //highlight the text in red if error
        Console.WriteLine(m);
        Console.ResetColor(); // changes the text color to default
        Wait(); }
    static void Wait() 
    { Console.WriteLine("\nPress any key..."); 
        Console.ReadKey(); }
}






