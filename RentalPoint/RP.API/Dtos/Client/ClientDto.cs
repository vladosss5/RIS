namespace RP.API.Dtos.Client;

/// <summary>
/// DTO для отображения данных клиента
/// </summary>
public class ClientDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
}