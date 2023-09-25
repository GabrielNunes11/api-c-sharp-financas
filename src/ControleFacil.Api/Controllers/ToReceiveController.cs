using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO.ToReceiveDTO;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("to-receive")]
    public class ToReceiveController : BaseController
    {
        private readonly IService<ToReceiveRequestDTO, ToReceiveResponseDTO, Guid> _toReceiveService;

        public ToReceiveController(IService<ToReceiveRequestDTO, ToReceiveResponseDTO, Guid> toReceiveService)
        {
            _toReceiveService = toReceiveService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToReceive(ToReceiveRequestDTO toReceiveRequest)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Created("", await _toReceiveService.Add(toReceiveRequest, userId));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ReturnModelBadRequestException(ex));
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(ReturnModelUnathorizedException(ex));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllToReceives()
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Ok(await _toReceiveService.GetAll(userId));
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

        [HttpGet]
        [Route("{toReceiveId}")]
        [Authorize]
        public async Task<IActionResult> GetByToReceiveId(Guid toReceiveId)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Ok(await _toReceiveService.GetById(toReceiveId, userId));
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
        [Route("{toReceiveId}")]
        [Authorize]
        public async Task<IActionResult> UpdateToReceive(Guid toReceiveId, ToReceiveRequestDTO toReceiveRequest)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Created("", await _toReceiveService.UpdateById(toReceiveId, toReceiveRequest, userId));
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
        [Route("{toReceiveId}")]
        [Authorize]
        public async Task<IActionResult> DeleteToReceive(Guid toReceiveId)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                await _toReceiveService.Inactivation(toReceiveId, userId);

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