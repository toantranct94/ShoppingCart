using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ShoppingCart.Domain.Common;

namespace ShoppingCart.Domain
{
    public class Cart : AggregateRoot
    {
        protected virtual List<CartItem> CartItems { get; set; }

        public Cart()
        {
            Id = Guid.NewGuid();
            CartItems = new List<CartItem>();
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
                

            var item = CartItems.FirstOrDefault(
                x => x.Product.Id == product.Id);

            if (item != null)
            {
                CartItems.Remove(item);
            }

            item = new CartItem(product, quantity);
            CartItems.Add(item);
        }

        public virtual void Remove(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(Product));
            }

            var findItem = CartItems.FirstOrDefault(
                x => x.Product.Id == product.Id);

            if (findItem == null)
            {
                throw new ArgumentException(
                    $"{product.Title} was not found in your cart.");
            }

            CartItems.Remove(findItem);
        }

        public virtual void Clear()
        {
            CartItems.Clear();
        }

        public virtual decimal CalculateTotalPrice()
        {
            return CartItems.Sum(x => x.TotalPrice);
        }

        public int GetNumberOfProducts()
        {
            return CartItems.Count;
        }

        public string Print()
        {
            var builder = new StringBuilder();

            foreach (var cartItem in CartItems)
            {
                var formattedLine = $"{cartItem.Product.Title,-20} {cartItem.Quantity,5} {cartItem.UnitPrice:F2} {cartItem.TotalPrice:F2}";
                builder.AppendLine(formattedLine);
            }

            builder.AppendLine();
            builder.AppendLine($"{"Total", -20}{" :", 5} {CalculateTotalPrice():F2} TL");

            return builder.ToString();
        }
    }
}