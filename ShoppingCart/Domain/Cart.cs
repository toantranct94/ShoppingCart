using System.Text;
using ShoppingCart.Domain.Common;

namespace ShoppingCart.Domain
{
    public class Cart : AggregateRoot
    {
        public IReadOnlyList<CartItem> CartItems => _cartItems;

        private List<CartItem> _cartItems;

        public Cart()
        {
            Id = Guid.NewGuid();
            _cartItems = new List<CartItem>();
        }

        public virtual void Add(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(Product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            var item = _cartItems.FirstOrDefault(
                x => x.Product.Id == product.Id);

            if (item != null)
            {
                _cartItems.Remove(item);
            }

            item = new CartItem(product, quantity);
            _cartItems.Add(item);
        }

        public virtual void Remove(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(Product));
            }

            var findItem = _cartItems.FirstOrDefault(
                x => x.Product.Id == product.Id);

            if (findItem == null)
            {
                throw new ArgumentException(
                    $"{product.Title} was not found in your cart.");
            }

            _cartItems.Remove(findItem);
        }

        public virtual void Clear()
        {
            _cartItems.Clear();
        }

        public virtual decimal CalculateTotalPrice()
        {
            return _cartItems.Sum(x => x.TotalPrice);
        }

        public int GetNumberOfProducts()
        {
            return _cartItems.Count;
        }

        public string Print()
        {
            var builder = new StringBuilder();

            var headers = $"{"Title", -15} {"Quantity", 15} {"Unit Price", 15} {"Total Price", 15}";
            builder.AppendLine(headers);

            foreach (var cartItem in _cartItems)
            {
                var formattedLine = $"{cartItem.Product.Title, -15} {cartItem.Quantity, 15} {cartItem.UnitPrice, 15} {cartItem.TotalPrice, 15}";
                builder.AppendLine(formattedLine);
            }

            builder.AppendLine();
            builder.AppendLine($"{"Total", -15} {"", 15} {"", 15} {CalculateTotalPrice(), 15}");

            return builder.ToString();
        }
    }
}