namespace RP.API.Dtos.Order;

public class OrderCompletionResultDto
{
    public string OrderId { get; set; }
    public decimal TotalHours { get; set; }
    public decimal TotalPrice { get; set; }
}