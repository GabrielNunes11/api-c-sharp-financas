using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO.ToReceiveDTO;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ControleFacil.Api.Domain.Services.Class
{
    public class ToReceiveService : IService<ToReceiveRequestDTO, ToReceiveResponseDTO, Guid>
    {
        private readonly IToReceiveRepository _toReceiveRepository;

        private readonly IMapper _mapper;

        public ToReceiveService(IToReceiveRepository toReceiveRepository, IMapper mapper)
        {
            _toReceiveRepository = toReceiveRepository;
            _mapper = mapper;
        }

        public async Task<ToReceiveResponseDTO> Add(ToReceiveRequestDTO entity, Guid userId)
        {
            ValidateTransaction(entity);

            ToReceive toReceive = _mapper.Map<ToReceive>(entity);

            toReceive.RegisterDate = DateTime.Now;
            toReceive.UserId = userId;

            toReceive = await _toReceiveRepository.Add(toReceive);

            return _mapper.Map<ToReceiveResponseDTO>(toReceive);
        }

        public async Task<IEnumerable<ToReceiveResponseDTO>> GetAll(Guid userId)
        {
            var receivings = await _toReceiveRepository.GetToReceiveById(userId);

            return receivings.Select(n => _mapper.Map<ToReceiveResponseDTO>(n));
        }

        public async Task<ToReceiveResponseDTO> GetById(Guid id, Guid userId)
        {
            ToReceive toReceive = await SearchByIdLinkedWithUser(id, userId);

            return _mapper.Map<ToReceiveResponseDTO>(toReceive);
        }

        public async Task Inactivation(Guid id, Guid userId)
        {
            ToReceive toReceive = await SearchByIdLinkedWithUser(id, userId);

            await _toReceiveRepository.Delete(toReceive);
        }

        public async Task<ToReceiveResponseDTO> UpdateById(Guid id, ToReceiveRequestDTO entity, Guid userId)
        {
            ValidateTransaction(entity);

            ToReceive toReceive = await SearchByIdLinkedWithUser(id, userId);

            var dataReceive = _mapper.Map<ToReceive>(entity);

            dataReceive.UserId = toReceive.UserId;
            dataReceive.Id = toReceive.Id;
            dataReceive.RegisterDate = toReceive.RegisterDate;

            dataReceive = await _toReceiveRepository.Update(dataReceive);

            return _mapper.Map<ToReceiveResponseDTO>(dataReceive);
        }

        private async Task<ToReceive> SearchByIdLinkedWithUser(Guid id, Guid userId)
        {
            var toReceive = await _toReceiveRepository.GetById(id);

            if (toReceive is null || toReceive.UserId != userId)
            {
                throw new Exception("Não foi possível encontrar esse lançamento e/ou usuário.");
            }

            return toReceive;
        }

        private void ValidateTransaction(ToReceiveRequestDTO entity)
        {
            if(entity.OriginalValue < 0 || entity.ReceivedValue < 0)
            {
                throw new BadRequestException("Os campos não podem receber valores negativos.");
            }
        }
    }
}