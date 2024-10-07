﻿
using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;
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

	internal async Task<ReadChamadoDto> GetByIdAsync(int id) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(id);
		return _mapper.Map<ReadChamadoDto>(chamado);
	}

	internal async Task<bool> RegistrarRespostaTecnicaAsync(ResponderChamadoDto dto) {
		var chamado = await _chamadoRepository.RecuperarChamadoPorIdAsync(dto.ChamadoId);

		if (chamado == null) {
			return false;
		}

		chamado.Status = EnumStatus.EmAndamento;
		_mapper.Map(dto, chamado);
		await _chamadoRepository.RegistrarRespostaAsync(chamado);
			return true;

	}
}
