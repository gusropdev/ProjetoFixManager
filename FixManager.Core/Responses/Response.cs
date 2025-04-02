using System.Text.Json.Serialization;

namespace FixManager.Core.Responses;

public class Response<TData>
{
    [JsonConstructor]
    public Response() => _code = Configuration.DefaultStatusCode;

    public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null, List<string>? errors = null)
    {
        Data = data;
        Message = message;
        _code = code;
        Errors = errors;
    }
    
    private readonly int _code;
    
    public TData? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; }
    
    [JsonIgnore]
    public bool IsSuccess
        => _code is >= 200 and <= 299;

}