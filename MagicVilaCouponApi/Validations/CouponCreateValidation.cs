using FluentValidation;
using MagicVilaCouponApi.model.dto;

namespace MagicVilaCouponApi.Validations;

public class CouponCreateValidation: AbstractValidator<CreateCouponDto>
{
    public CouponCreateValidation()
    {
        RuleFor(model => model.Name).NotEmpty();
        RuleFor(model => model.Percentage).InclusiveBetween(1, 100);
    }

}

/***
 * This file is used to validate the create coupon request.
 * By default, model validation is not enabled with Minimal api,
 * so we have to manually validate the request.
 * Add the dependency FluentValidation.DependencyInjectionExtensions to the project.
 */
