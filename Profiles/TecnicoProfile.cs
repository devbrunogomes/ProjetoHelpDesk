using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Profiles;

public class TecnicoProfile : Profile {

    public TecnicoProfile()
    {
      CreateMap<CreateTecnicoDto, Tecnico>();
      CreateMap<Tecnico, ReadTecnicoDto>();
      CreateMap<UpdateTecnicoDto, Tecnico>();
      
    }
}
