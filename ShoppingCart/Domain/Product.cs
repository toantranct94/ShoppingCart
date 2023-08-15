using ShoppingCart.Domain.Common;

namespace ShoppingCart.Domain
{
    public class Product : Entity
    {
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }


        protected Product()
        {

        }

        public Product(string title, decimal price)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            Id = Guid.NewGuid();
            Title = title;
            Price = price;
        }
    }
}