using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Profiles;

public class UsuarioProfle : Profile{

    public UsuarioProfle()
    {
      CreateMap<CreateUsuarioDto, Usuario>();
    }
}
