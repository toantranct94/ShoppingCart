using ShoppingCart.Domain.Common;

namespace ShoppingCart.Domain
{
    public class CartItem : Entity
    {
        public Product Product { get; protected set; }
        public int Quantity { get; protected set; }
        public decimal UnitPrice => Product.Price;
        public decimal TotalPrice => Product.Price * Quantity;
        public decimal Discount { get; protected set; }
        public decimal TotalPriceAfterDisocunt => Product.Price * Quantity - Discount;

        public CartItem(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            Product = product;
            Quantity = quantity;
        }

        public void ApplyDiscount(decimal discount)
        {
            Discount = discount;
        }
    }
}