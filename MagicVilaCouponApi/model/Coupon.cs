namespace MagicVilaCouponApi.model;

public class Coupon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Percentage { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastUpdated { get; set; }
    public bool IsActive { get; set; } = true;
}