using FluentValidation;
using MagicVilaCouponApi.model.dto;

namespace MagicVilaCouponApi.Validations;

public class CouponUpdateValidation: AbstractValidator<UpdateCouponRequestDto>
{
    public CouponUpdateValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Percentage).InclusiveBetween(1, 100).WithMessage("Percentage must be between 1 and 100");
        
    }
}