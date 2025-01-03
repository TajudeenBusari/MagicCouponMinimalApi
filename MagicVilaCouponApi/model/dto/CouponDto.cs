namespace MagicVilaCouponApi.model.dto;

public class CouponDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Percentage { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    
    public bool IsActive { get; set; } = true;
}