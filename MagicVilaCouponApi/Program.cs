using System.Net;
using FluentValidation;
using MagicVilaCouponApi.mapper;
using MagicVilaCouponApi.model;
using MagicVilaCouponApi.model.dto;
using MagicVilaCouponApi.store;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
} 


app.MapGet("api/v1/coupon", (ILogger<Program> _logger) =>
{
    var response = new ApiResponse();
    _logger.Log(LogLevel.Information, "Get All Coupons successfully");
    response.Result = CouponStore.getCoupons();
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    return Results.Ok(response);

}).WithName("GetAllCoupons").Produces<ApiResponse>(200);

app.MapGet("api/v1/coupon/{id}", (int id) =>
{
    
    var coupon = CouponStore.getCoupons().FirstOrDefault(c => c.Id == id);
    if (coupon == null)
    {
        return Results.NotFound();
    }
    var response = new ApiResponse();
    response.Result = coupon;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    return Results.Ok(response);
}).WithName("GetCouponById").Produces<ApiResponse>(200);

app.MapPost("api/v1/coupon", async (IValidator <CreateCouponDto> _validation, [FromBody]CreateCouponDto createCouponDto) =>
{
    var response = new ApiResponse(){IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};
    var validationResult = await _validation.ValidateAsync(createCouponDto);
    if (!validationResult.IsValid)
    {
        response.Errors.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
  

    if (CouponStore.getCoupons().FirstOrDefault(u => u.Name.ToLower() == createCouponDto.Name.ToLower()) != null)
    {
        response.Errors.Add("Coupon with the same name already exists");
        return Results.BadRequest(response);
    }
    
    //map to coupon
    /*var coupon = new Coupon()
    {
        Name = createCouponDto.Name,
        Percentage = createCouponDto.Percentage,
        IsActive = createCouponDto.IsActive
    };*/
    var coupon = CouponMapper.mapFromCreateCouponDtoToCoupon(createCouponDto);
    
    //get all coupons from the store
    var coupons = CouponStore.getCoupons();
    //get the max id from the list
    var maxId = coupons.Max(c => c.Id);
    //assign the max id + 1 to the new coupon
    coupon.Id = maxId + 1;
    //add the new coupon to the list
    coupons.Add(coupon);
    
    //map to couponDto
    /*var couponDto = new CouponDto()
    {
        Id = coupon.Id,
        Name = coupon.Name,
        Percentage = coupon.Percentage,
        IsActive = coupon.IsActive,
        CreatedAt = coupon.CreatedAt
    };*/
    var couponDto = CouponMapper.mapFromCouponToCouponDto(coupon);
    response.Result = couponDto;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.Created;
    return Results.Ok(response);
    //return Results.CreatedAtRoute("GetCouponById", new {id = couponDto.Id}, response);

    //return Results.Created($"api/v1/coupon/{coupon.Id}", coupon);
    //return Results.CreatedAtRoute("GetCouponById", new {id = couponDto.Id}, couponDto);

}).WithName("AddCoupon").Produces<ApiResponse>(201).Produces(400);

app.MapPut("api/v1/coupon/{id:int}", async (IValidator<UpdateCouponRequestDto> _validation, [FromRoute]int id, [FromBody] UpdateCouponRequestDto UpdateCouponRequestDto) =>
{
    var response = new ApiResponse(){IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};
    var validationResult = await _validation.ValidateAsync(UpdateCouponRequestDto);
    if (!validationResult.IsValid)
    {
        response.Errors.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    
    var couponFromStore = CouponStore.getCoupons().FirstOrDefault(c => c.Id == id);
    if (couponFromStore == null)
    {
        return Results.NotFound();
    }
    
    //update
    couponFromStore.Name = UpdateCouponRequestDto.Name;
    couponFromStore.Percentage = UpdateCouponRequestDto.Percentage;
    couponFromStore.LastUpdated = UpdateCouponRequestDto.LastUpdated;
    
    
    
    //map to couponDto
    /*var couponDto = new CouponDto()
    {
        Id = coupon.Id,
        Name = coupon.Name,
        Percentage = coupon.Percentage,
        IsActive = coupon.IsActive,
        CreatedAt = coupon.CreatedAt
    };*/
    var couponDto = CouponMapper.mapFromCouponToCouponDto(couponFromStore);
    response.Result = couponDto;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    return Results.Ok(response);
}).WithName("UpdateCoupon").Produces<ApiResponse>(200).Produces(400);
    
app.MapDelete("api/v1/coupon/{id:int}", (int id) =>
{
    var response = new ApiResponse(){IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};
    var coupon = CouponStore.getCoupons().FirstOrDefault(c => c.Id == id);
    if (coupon == null)
    {
        return Results.NotFound("Invalid Coupon Id");
    }
    CouponStore.getCoupons().Remove(coupon);
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    return Results.Ok(response);
}).WithName("DeleteCoupon").Produces<ApiResponse>(200);
    
app.UseHttpsRedirection();
app.Run();

