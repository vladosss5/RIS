namespace RIS.Core.Models;

public class Client : BaseIdEntity
{
    public string FName { get; set; } = null!;
    public string SName { get; set; } = null!;
    public string? LName { get; set; }
    public string Phone { get; set; } = null!;
    public List<Rental> Rentals { get; set; } = new();
}
