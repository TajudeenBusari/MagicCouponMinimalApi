using MagicVilaCouponApi.model;
using MagicVilaCouponApi.model.dto;

namespace MagicVilaCouponApi.mapper;

public static class CouponMapper
{
    public static CouponDto mapFromCouponToCouponDto(Coupon coupon)
    {
        return new CouponDto()
        {
            Id = coupon.Id,
            Name = coupon.Name,
            Percentage = coupon.Percentage,
            CreatedAt = coupon.CreatedAt,
            IsActive = coupon.IsActive
        };
    }
    
    public static List<CouponDto> mapFromCouponsToCouponDtos(List<Coupon> coupons)
    {
        return coupons.Select(coupon => mapFromCouponToCouponDto(coupon)).ToList();
    }
    
    public static Coupon mapFromCreateCouponDtoToCoupon(CreateCouponDto createCouponDto)
    {
        return new Coupon()
        {
            Name = createCouponDto.Name,
            Percentage = createCouponDto.Percentage,
            IsActive = createCouponDto.IsActive
        };
    }
    
    public static Coupon mapFromUpdateCouponRequestDtoToCoupon(UpdateCouponRequestDto updateCouponRequestDto)
    {
        return new Coupon()
        {
            Name = updateCouponRequestDto.Name,
            Percentage = updateCouponRequestDto.Percentage,
            
        };
    }

    
}