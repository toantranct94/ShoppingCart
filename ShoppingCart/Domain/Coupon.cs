using ShoppingCart.Domain.Common;

namespace ShoppingCart.Domain
{
    public class Coupon : Entity
    {
        public decimal MinQuantity { get; protected set; }
        public string ProductTitle { get; set; }
        public HashSet<string> SetProductTitles { get; set; }
        public CouponType CouponType { get; protected set; }

        public Coupon(decimal minQuantity, string productTitle, CouponType couponType)
        {
            if (minQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minQuantity));
            }

            MinQuantity = minQuantity;
            ProductTitle = productTitle;
            CouponType = couponType;
        }

        public Coupon(decimal minQuantity, HashSet<string> setProductTitles, CouponType couponType)
        {
            if (minQuantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minQuantity));
            }

            MinQuantity = minQuantity;
            SetProductTitles = setProductTitles;
            CouponType = couponType;
        }
    }
}