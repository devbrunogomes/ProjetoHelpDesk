using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Profiles;

public class AdministradorProfile : Profile {
	public AdministradorProfile() {
		CreateMap<CreateAdministradorDto, Administrador>();
		CreateMap<Administrador, ReadAdministradorDto>();
		CreateMap<UpdateAdministradorDto, Administrador>();
		CreateMap<CreateAdministradorDto, CreateUsuarioDto>();
	}
}
