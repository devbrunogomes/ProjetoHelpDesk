﻿using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models.Enums;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Repositories;
using AutoMapper;

namespace SolutisHelpDesk.Services;

public class TecnicoService {
	private IMapper _mapper;
	private TecnicoRepository _tecnicoRepository;

	public TecnicoService(IMapper mapper, TecnicoRepository tecnicoRepository) {
		_mapper = mapper;
		_tecnicoRepository = tecnicoRepository;
	}


	internal async Task<Tecnico> RegistroTecnicoAsync(CreateTecnicoDto dto) {
		Tecnico tecnico = _mapper.Map<Tecnico>(dto);
		tecnico.Perfil = EnumPerfil.Tecnico;

		await _tecnicoRepository.SalvarTecnico(tecnico);
		return tecnico;
	}
	internal async Task<IEnumerable<ReadTecnicoDto>> GetAllAsync() {
		List<Tecnico> listaTecnicos = await _tecnicoRepository.RecuperarTecnicosAsync();
		return _mapper.Map<List<ReadTecnicoDto>>(listaTecnicos);
	}

	internal async Task<ReadTecnicoDto> GetByIdAsync(int id) {
		var tecnico = await _tecnicoRepository.RecuperarTecnicoPorIdAsync(id);
		return _mapper.Map<ReadTecnicoDto>(tecnico);
	}

	internal async Task<bool> DeleteAsync(int id) {
		var tecnico = await _tecnicoRepository.RecuperarTecnicoPorIdAsync(id);

		if (tecnico == null) {
			return false;
		}

		await _tecnicoRepository.DeleteAsync(tecnico);
		return true;
	}

	internal async Task<bool> UpdateTecnico(int id, UpdateTecnicoDto tecnicoDto) {
		var tecnicoExistente = await _tecnicoRepository.RecuperarTecnicoPorIdAsync(id);

		if (tecnicoExistente == null) {
			return false;
		}

		_mapper.Map(tecnicoDto, tecnicoExistente);
		await _tecnicoRepository.AtualizarTecnicoAsync(tecnicoExistente);
		return true;
	}
}