using AutoMapper;
using SolutisHelpDesk.Data.DTOs;
using SolutisHelpDesk.Models;
using SolutisHelpDesk.Models.Enums;
using SolutisHelpDesk.Repositories;

namespace SolutisHelpDesk.Services;

public class AdministradorService {
	private IMapper _mapper;
	private AdministradorRepository _administradorRepository;

	public AdministradorService(IMapper mapper, AdministradorRepository administradorRepository) {
		_mapper = mapper;
		_administradorRepository = administradorRepository;
	}

	internal async Task<Administrador> RegistroAdmAsync(CreateAdministradorDto dto) {
		Administrador adm = _mapper.Map<Administrador>(dto);
		adm.Perfil = EnumPerfil.Administrador;

		await _administradorRepository.SalvarAdm(adm);
		return adm;
	}

	internal async Task<IEnumerable<ReadAdministradorDto>> GetAllAsync() {
		List<Administrador> listaAdms = await _administradorRepository.RecuperarAdministradoresAsync();
		return _mapper.Map<List<ReadAdministradorDto>>(listaAdms);
	}

	internal async Task<ReadAdministradorDto> GetByIdAsync(int id) {
		var adm = await _administradorRepository.RecuperarAdministradorPorIdAsync(id);
		return _mapper.Map<ReadAdministradorDto>(adm);
	}

	internal async Task<bool> DeleteAsync(int id) {
		var adm = await _administradorRepository.RecuperarAdministradorPorIdAsync(id);

		if (adm == null)
			return false;

		await _administradorRepository.DeleteAsync(adm);
		return true;
	}

	internal async Task<bool> UpdateAdministrador(int id, UpdateAdministradorDto admDto) {
		var admExistente = await _administradorRepository.RecuperarAdministradorPorIdAsync(id);

		if (admExistente == null) {
			return false;
		}

		_mapper.Map(admDto, admExistente);
		await _administradorRepository.AtualizarAdministradorAsync(admExistente);
		return true;
	}
}
