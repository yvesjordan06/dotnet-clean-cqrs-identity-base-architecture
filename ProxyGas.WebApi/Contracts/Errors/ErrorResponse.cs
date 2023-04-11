namespace ProxyGas.WebApi.Contracts.Errors;

internal class ErrorResponse
{
   

    public string Message { get; set; } = "An error occurred";
    public int StatusCode { get; set; } = 500;
    public string StatusPhrase { get; set; } = "Internal Server Error";
    public ICollection<string> Errors { get; set; } = new List<string>();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
}