namespace ProxyGas.WebApi.Contracts.Orders.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
}