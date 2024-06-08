using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client> GetClientByIdAsync(int id);
        Task<IEnumerable<Client>> SearchClientsAsync(string searchTerm);
        Task RegisterClientAsync(ClientDto clientDto);
    }
}
