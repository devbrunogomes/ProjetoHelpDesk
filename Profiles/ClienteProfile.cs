using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Profiles;

public class ClienteProfile : Profile {

	public ClienteProfile() {
		CreateMap<CreateClienteDto, Cliente>();
		CreateMap<Cliente, ReadClienteDto>()
			.ForMember(clienteDto => clienteDto.Chamados, 
			opt => opt.MapFrom(cliente => cliente.Chamados));
		CreateMap<UpdateClienteDto, Cliente>();
	}
}
