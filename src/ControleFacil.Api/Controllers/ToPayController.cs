using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO;
using ControleFacil.Api.DTO.ToPayDTO;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("to-pay")]
    public class ToPayController : BaseController
    {
        private readonly IService<ToPayRequestDTO, ToPayResponseDTO, Guid> _toPayService;

        public ToPayController(IService<ToPayRequestDTO, ToPayResponseDTO, Guid> toPayService)
        {
            _toPayService = toPayService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPay(ToPayRequestDTO toPayRequest)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Created("", await _toPayService.Add(toPayRequest, userId));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ReturnModelBadRequestException(ex));
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ReturnModelUnathorizedException(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllToPays()
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Ok(await _toPayService.GetAll(userId));
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(ReturnModelUnathorizedException(ex));
            }
            catch(NotFoundException ex)
            {
                return NotFound(ReturnModelNotFoundException(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{toPayId}")]
        [Authorize]
        public async Task<IActionResult> GetByToPayId(Guid toPayId)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Ok(await _toPayService.GetById(toPayId, userId));
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ReturnModelUnathorizedException(ex));
            }
            catch(NotFoundException ex)
            {
                return NotFound(ReturnModelNotFoundException(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{toPayId}")]
        [Authorize]
        public async Task<IActionResult> UpdateToPay(Guid toPayId, ToPayRequestDTO toPayRequest)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Created("", await _toPayService.UpdateById(toPayId, toPayRequest, userId));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ReturnModelBadRequestException(ex));
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ReturnModelUnathorizedException(ex));
            }
            catch(NotFoundException ex)
            {
                return NotFound(ReturnModelNotFoundException(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{toPayId}")]
        [Authorize]
        public async Task<IActionResult> DeleteToPay(Guid toPayId)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                await _toPayService.Inactivation(toPayId, userId);

                return Ok("Lançamento excluído com sucesso!");
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ReturnModelUnathorizedException(ex));
            }
            catch(NotFoundException ex)
            {
                return NotFound(ReturnModelNotFoundException(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}