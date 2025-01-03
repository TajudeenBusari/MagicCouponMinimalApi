namespace MagicVilaCouponApi.model.dto;

public class UpdateCouponRequestDto
{
    
    public string Name { get; set; }
    public int Percentage { get; set; }
    public DateTime? LastUpdated { get; set; } = DateTime.Now;
    
    
}