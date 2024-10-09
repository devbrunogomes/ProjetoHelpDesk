using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;

namespace SolutisHelpDesk.Profiles;

public class ChamadoProfile : Profile {

    public ChamadoProfile()
    {
      CreateMap<CreateChamadoDto, Chamado>();
      CreateMap<Chamado, ReadChamadoDto>();
      CreateMap<ResponderChamadoDto, Chamado>();
      CreateMap<FinalizarChamadoDto, Chamado>();
      CreateMap<ReatribuirChamadoDto, Chamado>();
    }
}
