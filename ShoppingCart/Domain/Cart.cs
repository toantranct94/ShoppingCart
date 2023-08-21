using System.Text;
using ShoppingCart.Domain.Common;

namespace ShoppingCart.Domain
{
    public class Cart : AggregateRoot
    {
        public IReadOnlyList<CartItem> Items => _items;
        private List<CartItem> _items { get; set; }

        public Cart()
        {
            Id = Guid.NewGuid();
            _items = new List<CartItem>();
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

            var item = _items.FirstOrDefault(
                x => x.Product.Id == product.Id);

            if (item != null)
            {
                _items.Remove(item);
            }

            item = new CartItem(product, quantity);

            _items.Add(item);
        }

        public virtual void Remove(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(Product));
            }

            var findItem = _items.FirstOrDefault(
                x => x.Product.Id == product.Id);

            if (findItem == null)
            {
                throw new ArgumentException(
                    $"{product.Title} was not found in your cart.");
            }

            _items.Remove(findItem);
        }

        public virtual void Clear()
        {
            _items.Clear();
        }

        public virtual decimal CalculateTotalPrice()
        {
            return _items.Sum(x => x.TotalPriceAfterDisocunt);
        }

        public int GetNumberOfProducts()
        {
            return _items.Count;
        }

        public string Print()
        {
            var builder = new StringBuilder();

            var headers = $"{"Title", -15} {"Quantity", 15} {"Unit Price", 15} {"Total Price", 15}";
            builder.AppendLine(headers);

            foreach (var cartItem in _items)
            {
                var formattedLine = $"{cartItem.Product.Title, -15} {cartItem.Quantity, 15} {cartItem.UnitPrice, 15} {cartItem.TotalPrice, 15}";
                builder.AppendLine(formattedLine);
            }

            builder.AppendLine();
            builder.AppendLine($"{"Total", -15} {"", 15} {"", 15} {CalculateTotalPrice(), 15}");

            return builder.ToString();
        }

        public void ApplyCoupon(Coupon coupon)
        {

            if (coupon == null)
            {
                throw new ArgumentNullException(nameof(Coupon));
            }

            switch(coupon.CouponType)
            {
                case CouponType.SingleProduct:
                    ApplySingleProductCoupon(coupon);
                    break;
                case CouponType.SetProduct:
                    ApplySetProductCoupon(coupon);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CouponType));
            }
        }

        private void ApplySingleProductCoupon(Coupon coupon)
        {
            foreach (var item in _items)
            {
                if (item.Quantity >= coupon.MinQuantity
                    && item.Product.Title.Equals(coupon.ProductTitle))
                {
                    decimal disocunt = GetDiscountByProduct(item);
                    item.ApplyDiscount(disocunt);
                }
            }
        }

        private void ApplySetProductCoupon(Coupon coupon)
        {
            var products = _items
                .GroupBy(c => c.Product.Title)
                .ToList();

            var productTitles = products.Select(c => c.Key).ToHashSet();

            if (!coupon.SetProductTitles.SetEquals(productTitles))
            {
                return;
            }

            foreach (var item in _items)
            {
                if (item.Quantity >= coupon.MinQuantity)
                {
                    decimal disocunt = GetDiscountBySet(item);
                    item.ApplyDiscount(disocunt);
                }
            }
        }

        private decimal GetDiscountBySet(CartItem cartItem)
        {
            switch (cartItem.Product.Title)
            {
                case "Jeans":
                    return cartItem.Quantity / 2 * 10;
                case "Shirt":
                    return cartItem.Quantity / 2 * 5;
                default:
                    return 0;
            }
        }

        private decimal GetDiscountByProduct(CartItem cartItem)
        {
            switch (cartItem.Product.Title)
            {
                case "Jeans":
                    return cartItem.Quantity / 3 * cartItem.UnitPrice;
                default:
                    return 0;
            }
        }
    }
}