using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Profiles;

public class ChamadoProfile : Profile {

	public ChamadoProfile() {
		CreateMap<CreateChamadoDto, Chamado>();
		CreateMap<Chamado, ReadChamadoDto>()
			.ForMember(dto => dto.Respostas, opt => opt.MapFrom(src => src.Resposta));
		CreateMap<FinalizarChamadoDto, Chamado>();
		CreateMap<ReatribuirChamadoDto, Chamado>();
		CreateMap<AlterarPrioridadeChamadoDto, Chamado>();
	}
}
