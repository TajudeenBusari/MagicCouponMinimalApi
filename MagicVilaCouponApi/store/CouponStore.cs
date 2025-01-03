using MagicVilaCouponApi.model;

namespace MagicVilaCouponApi.store;

public static class CouponStore
{
    public static List<Coupon> getCoupons()
    {
        return new List<Coupon>()
        {
            new Coupon()
            {
                Id = 1,
                Name = "Coupon 1",
                Percentage = 10,
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now,
                IsActive = true
            },
            new Coupon()
            {
                Id = 2,
                Name = "Coupon 2",
                Percentage = 20,
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now,
                IsActive = true
            },
            new Coupon()
            {
                Id = 3,
                Name = "Coupon 3",
                Percentage = 30,
                CreatedAt = DateTime.Now,
                LastUpdated = DateTime.Now,
                IsActive = true
            }
        };
    }
}