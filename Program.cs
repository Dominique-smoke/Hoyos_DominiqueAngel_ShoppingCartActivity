using Hoyos_ShoppingCartActivity;
using System.Runtime.InteropServices;
class CartItem
{
    public Product ProductRef { get; set; } = new Product();
    public int Quantity { get; set; }
    public double SubTotal => ProductRef.Price * Quantity;
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

        List<CartItem> cart = new List<CartItem>(); // Lists are easier to manage than fixed arrays

        while (true)
        {
           
            Console.WriteLine("=== WELCOME TO THE GEEK STORE ===");
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Price",-10} {"Stock",-15}");
            Console.WriteLine(new string('-', 55));

            foreach (var p in inventory) p.DisplayProduct();

            Console.Write("\nEnter Product ID (0 to checkout): ");
            if (!int.TryParse(Console.ReadLine(), out int prodId) || prodId == 0) break;

            var selected = inventory.FirstOrDefault(p => p.Id == prodId);

            if (selected == null)
            {
                Console.WriteLine("Error: Product not found!");
            }
            else if (selected.RemainingStock <= 0)
            {
                Console.WriteLine("Error: This item is sold out!");
            }
            else
            {
                Console.Write($"Quantity for {selected.Name}: ");
                if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0)
                {
                    if (selected.HasEnoughStock(qty))
                    {
                        AddToCart(cart, selected, qty);
                        selected.DeductStock(qty);
                        Console.WriteLine("Success: Added to cart!");
                    }
                    else
                    {
                        Console.WriteLine("Error: Not enough stock!");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid quantity!");
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        ShowReceipt(cart);
    }

    static void AddToCart(List<CartItem> cart, Product prod, int qty)
    {
        var existing = cart.FirstOrDefault(c => c.ProductRef.Id == prod.Id);
        if (existing != null)
            existing.Quantity += qty;
        else
            cart.Add(new CartItem { ProductRef = prod, Quantity = qty });
    }

    static void ShowReceipt(List<CartItem> cart)
    {
        Console.Clear();
        Console.WriteLine("=== FINAL RECEIPT ===");
        foreach (var item in cart)
        {
            Console.WriteLine($"{item.ProductRef.Name,-20} x{item.Quantity,-5} {item.SubTotal,10:C}");
        }
        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"GRAND TOTAL: {cart.Sum(c => c.SubTotal),26:C}");
        Console.WriteLine("\nThank you for shopping!");
        Console.ReadKey();
    }
}


