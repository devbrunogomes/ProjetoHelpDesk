using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Profiles;

public class RespostaProfile : Profile {

	public RespostaProfile() {
		CreateMap<CreateRespostaDto, Resposta>();
		CreateMap<Resposta, ReadRespostaDto>();
	}
}
