using Core.DBModels;
using RP.API.Dtos.Client;

namespace RP.API.Mapping;

public class ClientsMapper
{
    /// <summary>
    /// Преобразовать ClientCreateDto в доменную модель Client
    /// </summary>
    public Client MapToDomain(ClientCreateDto dto)
    {
        return new Client
        {
            FName = dto.FName,
            SName = dto.SName,
            LName = dto.LName,
            PhoneNumber = dto.PhoneNumber
        };
    }

    /// <summary>
    /// Преобразовать доменную модель Client в ClientDto
    /// </summary>
    public ClientDto MapToDto(Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            FullName = $"{client.SName} {client.FName} {client.LName}".Trim(),
            PhoneNumber = client.PhoneNumber
        };
    }
}