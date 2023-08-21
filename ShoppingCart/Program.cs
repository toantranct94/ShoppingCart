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

Product jeans = new Product("Jeans", 20.00m);
Product shirt = new Product("Shirt", 10.00m);
HashSet<string> productTitles = new HashSet<string>() { "Jeans", "Shirt" };
Coupon coupon = new Coupon(2, productTitles, CouponType.SetProduct);
int quantity = 2;

cart.Add(jeans, quantity);
cart.Add(shirt, quantity);
cart.ApplyCoupon(coupon);

Console.WriteLine(cart.Print());
cart.Clear();