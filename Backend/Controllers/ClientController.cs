using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchClients([FromQuery] string searchTerm)
        {
            var clients = await _clientRepository.SearchClientsAsync(searchTerm);
            return Ok(clients);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterClient(ClientDto clientDto)
        {
            await _clientRepository.RegisterClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = clientDto.Email }, clientDto);  // Assuming Email as ID for simplicity
        }
    }
}
