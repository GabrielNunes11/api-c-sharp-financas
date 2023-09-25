using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO.NatureReleaseDTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ControleFacil.Api.Domain.Services.Class
{
    public class NatureReleaseService : IService<NatureReleaseRequestDTO, NatureReleaseResponseDTO, Guid>
    {
        private readonly INatureReleaseRepository _natureReleaseRepository;

        private readonly IMapper _mapper;

        public NatureReleaseService(INatureReleaseRepository natureReleaseRepository, IMapper mapper)
        {
            _natureReleaseRepository = natureReleaseRepository;
            _mapper = mapper;
        }

        public async Task<NatureReleaseResponseDTO> Add(NatureReleaseRequestDTO entity, Guid userId)
        {
            NatureRelease natureRelease = _mapper.Map<NatureRelease>(entity);

            natureRelease.RegisterDate = DateTime.Now;
            natureRelease.UserId = userId;

            natureRelease = await _natureReleaseRepository.Add(natureRelease);

            return _mapper.Map<NatureReleaseResponseDTO>(natureRelease);
        }

        public async Task<IEnumerable<NatureReleaseResponseDTO>> GetAll(Guid userId)
        {
            var naturesRelease = await _natureReleaseRepository.GetReleaseByUserId(userId);

            return naturesRelease.Select(n => _mapper.Map<NatureReleaseResponseDTO>(n));
        }

        public async Task<NatureReleaseResponseDTO> GetById(Guid id, Guid userId)
        {
            NatureRelease natureRelease = await SearchByIdLinkedWithUser(id, userId);

            return _mapper.Map<NatureReleaseResponseDTO>(natureRelease);
        }

        public async Task Inactivation(Guid id, Guid userId)
        {
            NatureRelease natureRelease = await SearchByIdLinkedWithUser(id, userId);

            await _natureReleaseRepository.Delete(natureRelease);
        }

        public async Task<NatureReleaseResponseDTO> UpdateById(Guid id, NatureReleaseRequestDTO entity, Guid userId)
        {
            NatureRelease natureRelease = await SearchByIdLinkedWithUser(id, userId);

            natureRelease.Description = entity.Description;
            natureRelease.Details = entity.Details;

            natureRelease = await _natureReleaseRepository.Update(natureRelease);

            return _mapper.Map<NatureReleaseResponseDTO>(natureRelease);
        }

        private async Task<NatureRelease> SearchByIdLinkedWithUser(Guid id, Guid userId)
        {
            var natureRelease = await _natureReleaseRepository.GetById(id);

            if (natureRelease is null || natureRelease.UserId != userId)
            {
                throw new Exception("Não foi possível encontrar esse lançamento e/ou usuário.");
            }

            return natureRelease;
        }
    }
}