using System.Net;

namespace MagicVilaCouponApi.model;

public class ApiResponse
{
    
    public bool IsSuccess { get; set; }
    public object Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}