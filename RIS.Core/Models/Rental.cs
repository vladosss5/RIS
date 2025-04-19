namespace RIS.Core.Models;

public class Rental : BaseIdEntity
{
    public string ClientId { get; set; }
    public Client Client { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal TotalCost { get; set; }
    public string DepositInfo { get; set; }
    public List<RentalItem> RentalItems { get; set; } = new();
}