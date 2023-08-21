using ShoppingCart.Domain;

namespace ShoppingCart.Test;

[TestClass]
public class ShoppingCartTests
{
    [TestMethod]
    public void ShoppingCart_AddProduct_ShouldIncreaseItemCount()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;
        Product product = new Product("Test Product", 10.00m);

        // Act
        cart.Add(product, quantity);

        // Assert
        Assert.AreEqual(1, cart.GetNumberOfProducts());
    }

    [TestMethod]
    public void ShoppingCart_AddExistProduct_ShouldUpdateQuantity()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;
        int newQuantity = 2;
        Product product = new Product("Test Product", 10.00m);

        // Act
        cart.Add(product, quantity);
        cart.Add(product, newQuantity);
        var actualQuantity = cart.Items.First(c => c.Product.Id == product.Id).Quantity;

        // Assert
        Assert.AreEqual(newQuantity, actualQuantity);
    }


    [TestMethod]
    public void ShoppingCart_RemoveProduct_ShouldDecreaseItemCount()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;
        Product product1 = new Product("Test Product 1", 10.00m);
        Product product2 = new Product("Test Product 2", 20.00m);
        Product product3 = new Product("Test Product 3", 30.00m);

        // Act
        cart.Add(product1, quantity);
        cart.Add(product2, quantity);
        cart.Add(product3, quantity);

        cart.Remove(product1);

        // Assert
        Assert.AreEqual(2, cart.GetNumberOfProducts());
    }

    [TestMethod]
    public void ShoppingCart_AddProduct_NegativeQuantity_ArgumentOutOfRangeExceptionThrown()
    {
        // Arrange
        var cart = new Cart();
        var product = new Product("Test Product", 10.0m);
        int quantity = -1;

        // Act & Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => cart.Add(product, quantity));
    }

    [TestMethod]
    public void ShoppingCart_AddNullProduct_ArgumentNullExceptionThrown()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => cart.Add(null, quantity));
    }


    [TestMethod]
    public void ShoppingCart_CalculateTotalPrice_ShouldReturnCorrectTotal()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;
        Product product1 = new Product("Test Product 1", 10.00m);
        Product product2 = new Product("Test Product 2", 20.00m);

        // Act
        cart.Add(product1, quantity);
        cart.Add(product2, quantity);

        decimal total = cart.CalculateTotalPrice();

        // Assert
        Assert.AreEqual(30.00m, total);
    }


    [TestMethod]
    public void ShoppingCart_RemoveProduct_ShouldRecalculateTotalPrice()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;
        Product product1 = new Product("Test Product 1", 10.00m);
        Product product2 = new Product("Test Product 2", 20.00m);

        // Act
        cart.Add(product1, quantity);
        cart.Add(product2, quantity);

        cart.Remove(product1);
        decimal total = cart.CalculateTotalPrice();

        // Assert
        Assert.AreEqual(20.00m, total);
    }

    [TestMethod]
    public void ShoppingCart_RemoveAllProduct_ShouldBeEmpty()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;
        Product product1 = new Product("Test Product 1", 10.00m);
        Product product2 = new Product("Test Product 2", 20.00m);

        // Act
        cart.Add(product1, quantity);
        cart.Add(product2, quantity);

        cart.Clear();

        // Assert
        Assert.AreEqual(0, cart.GetNumberOfProducts());
    }

    [TestMethod]
    public void ShoppingCart_RemoveProduct_ProductNotFound_ArgumentExceptionThrown()
    {
        // Arrange
        var cart = new Cart();
        var product = new Product("Test Product", 10.0m);

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => cart.Remove(product));
    }

    [TestMethod]
    public void ShoppingCart_RemoveNullProduct_ArgumentNullExceptionThrown()
    {
        // Arrange
        Cart cart = new Cart();

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => cart.Remove(null));
    }

    [TestMethod]
    public void ShoppingCart_Print_ShouldReturnString()
    {
        // Arrange
        Cart cart = new Cart();
        int quantity = 1;
        Product product = new Product("Test Product", 10.00m);

        // Act
        cart.Add(product, quantity);
        var actualOutput = cart.Print();

        // Assert
        Assert.IsInstanceOfType(actualOutput, typeof(string));
    }
}
