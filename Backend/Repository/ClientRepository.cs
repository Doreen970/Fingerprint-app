using Backend.Data;
using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> SearchClientsAsync(string searchTerm)
        {
            return await _context.Clients
                                 .Where(c => c.Email.Contains(searchTerm))
                                 .ToListAsync();
        }

        public async Task RegisterClientAsync(ClientDto clientDto)
        {
            var client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                PhoneNumber = clientDto.PhoneNumber,
                IDPassport = clientDto.IDPassport,
                BankAccountNumber = clientDto.BankAccountNumber,
                DateOfBirth = clientDto.DateOfBirth,
                FingerprintData = clientDto.FingerprintData
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
    }
}
