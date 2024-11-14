using SolutisHelpDesk.Data.DTOs;

namespace SolutisHelpDesk.Services.Interfaces;

public interface IClienteService {
	Task<bool> RegistroClienteAsync(CreateClienteDto dto);
	Task<IEnumerable<ReadClienteDto>> GetAllAsync();
	Task<ReadClienteDto> GetByIdAsync(int id);
	Task<ReadClienteDto> GetByUsernameAsync(string username);
	Task<bool> DeleteAsync(int id);
	Task<bool> UpdateCliente(int id, UpdateClienteDto clienteDto);
}
