using Core.DBModels;
using Data;
using RP.Application.Interfaces;

namespace RP.Application.Services;

/// <summary>
/// Сервис для работы с клиентами (работает только с доменными моделями)
/// </summary>
public class ClientService : IClientService
{
    private readonly DataContext _context;

    public ClientService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    /// <param name="client">Доменная модель клиента</param>
    /// <returns>Созданный клиент</returns>
    public async Task<Client> CreateClientAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public async Task<Client?> GetClientByIdAsync(string id)
    {
        return await _context.Clients.FindAsync(id);
    }
}