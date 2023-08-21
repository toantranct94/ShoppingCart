using ShoppingCart.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = CreateHostBuilder(args).Build();

IHostBuilder CreateHostBuilder(string[] strings)
{
    return Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
    {
        services.AddScoped<Cart>();
    });
}

var services = host.Services;

var cart = services.GetRequiredService<Cart>();

Product jeans = new Product("Jeans", 100.0m);
Product tShirts = new Product("T-Shirts", 150.0m);

cart.Add(jeans, 3);
cart.Add(tShirts, 1);

Console.WriteLine(cart.Print());
cart.Clear();