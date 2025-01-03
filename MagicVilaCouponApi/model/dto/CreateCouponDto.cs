namespace MagicVilaCouponApi.model.dto;

public class CreateCouponDto
{
    public string Name { get; set; }
    public int Percentage { get; set; }
    public bool IsActive { get; set; } = true;
}