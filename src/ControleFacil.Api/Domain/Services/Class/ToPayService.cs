using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO.ToPayDTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ControleFacil.Api.Domain.Services.Class
{
    public class ToPayService : IService<ToPayRequestDTO, ToPayResponseDTO, Guid>
    {
        private readonly IToPayRepository _toPayRepository;

        private readonly IMapper _mapper;

        public ToPayService(IToPayRepository toPayRepository, IMapper mapper)
        {
            _toPayRepository = toPayRepository;
            _mapper = mapper;
        }

        public async Task<ToPayResponseDTO> Add(ToPayRequestDTO entity, Guid userId)
        {
            ToPay toPay = _mapper.Map<ToPay>(entity);

            toPay.RegisterDate = DateTime.Now;
            toPay.UserId = userId;

            toPay = await _toPayRepository.Add(toPay);

            return _mapper.Map<ToPayResponseDTO>(toPay);
        }

        public async Task<IEnumerable<ToPayResponseDTO>> GetAll(Guid userId)
        {
            var payments = await _toPayRepository.GetToPayByUserId(userId);

            return payments.Select(n => _mapper.Map<ToPayResponseDTO>(n));
        }

        public async Task<ToPayResponseDTO> GetById(Guid id, Guid userId)
        {
            ToPay toPay = await SearchByIdLinkedWithUser(id, userId);

            return _mapper.Map<ToPayResponseDTO>(toPay);
        }

        public async Task Inactivation(Guid id, Guid userId)
        {
            ToPay toPay = await SearchByIdLinkedWithUser(id, userId);

            await _toPayRepository.Delete(toPay);
        }

        public async Task<ToPayResponseDTO> UpdateById(Guid id, ToPayRequestDTO entity, Guid userId)
        {
            ToPay toPay = await SearchByIdLinkedWithUser(id, userId);

            var dataPay = _mapper.Map<ToPay>(entity);

            dataPay.UserId = toPay.UserId;
            dataPay.Id = toPay.Id;
            dataPay.RegisterDate = toPay.RegisterDate;

            toPay = await _toPayRepository.Update(toPay);

            return _mapper.Map<ToPayResponseDTO>(toPay);
        }

        private async Task<ToPay> SearchByIdLinkedWithUser(Guid id, Guid userId)
        {
            var toPay = await _toPayRepository.GetById(id);

            if(toPay is null || toPay.UserId != userId)
            {
                throw new Exception("Não foi possível encontrar esse lançamento e/ou usuário.");
            }

            return toPay;
        }
    }
}