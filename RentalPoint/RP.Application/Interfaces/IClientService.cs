using Core.DBModels;

namespace RP.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с клиентами
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Создать нового клиента
    /// </summary>
    Task<Client> CreateClientAsync(Client client);

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    Task<Client?> GetClientByIdAsync(string id);
}