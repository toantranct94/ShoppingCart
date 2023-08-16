using ShoppingCart.Domain;

namespace ShoppingCart.Test;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void Product_ShouldHaveCorrectTitleAndPrice()
    {
        // Arrange
        string title = "Test Product";
        decimal price = 10.0m;

        // Act
        Product product = new Product(title, price);

        // Assert
        Assert.AreEqual(title, product.Title);
        Assert.AreEqual(price, product.Price);
    }

    [TestMethod]
    public void Product_NullTitle_ArgumentNullExceptionThrown()
    {
        // Arrange
        string title = null;
        decimal price = 10.0m;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => new Product(title, price));
    }

    [TestMethod]
    public void Product_NegativePrice_ArgumentOutOfRangeExceptionThrown()
    {
        // Arrange
        string title = "Test Product";
        decimal price = -10.0m;

        // Act & Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Product(title, price));
    }
}