// See https://aka.ms/new-console-template for more information
using ShoppingCart.Domain;

Cart cart = new Cart();

Product jeans = new Product("Jeans", 100.0m);
Product tShirts = new Product("T-Shirts", 150.0m);

cart.Add(jeans, 3);
cart.Add(tShirts, 1);

Console.WriteLine(cart.Print());
cart.Clear();
