
using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Repositories;

namespace SolutisHelpDesk.Services;

public class ChamadoService {
	private IMapper _mapper;
	private ChamadoRepository _chamadoRepository;

	public ChamadoService(IMapper mapper, ChamadoRepository chamadoRepository) {
		_mapper = mapper;
		_chamadoRepository = chamadoRepository;
	}


	internal async Task<Chamado> RegistroChamadaAsync(CreateChamadoDto chamadoDto) {
		Chamado chamado = _mapper.Map<Chamado>(chamadoDto);

		await _chamadoRepository.SalvarChamado(chamado);
		return chamado;
	}
	internal async Task<IEnumerable<ReadChamadoDto>> GetAllAsync() {
		List<Chamado> listaChamado = await _chamadoRepository.RecuperarChamadosAsync();
		return _mapper.Map<List<ReadChamadoDto>>(listaChamado);
	}
}
