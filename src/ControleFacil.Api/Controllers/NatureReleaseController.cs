using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO.NatureReleaseDTO;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("nature-releases")]
    public class NatureReleaseController : BaseController
    {
        private readonly IService<NatureReleaseRequestDTO, NatureReleaseResponseDTO, Guid> _natureReleaseService;

        public NatureReleaseController(IService<NatureReleaseRequestDTO, NatureReleaseResponseDTO, Guid> natureReleaseService)
        {
            _natureReleaseService = natureReleaseService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNatureRelease(NatureReleaseRequestDTO natureReleaseRequest)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Created("", await _natureReleaseService.Add(natureReleaseRequest, userId));
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
        public async Task<IActionResult> GetAllNatureReleases()
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Ok(await _natureReleaseService.GetAll(userId));
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
        [Route("{natureReleaseId}")]
        [Authorize]
        public async Task<IActionResult> GetByNatureReleaseId(Guid natureReleaseId)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Ok(await _natureReleaseService.GetById(natureReleaseId, userId));
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
        [Route("{natureReleaseId}")]
        [Authorize]
        public async Task<IActionResult> UpdateNatureRelease(Guid natureReleaseId, NatureReleaseRequestDTO natureReleaseRequest)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                return Created("", await _natureReleaseService.UpdateById(natureReleaseId, natureReleaseRequest, userId));
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
        [Route("{natureReleaseId}")]
        [Authorize]
        public async Task<IActionResult> DeleteNatureRelease(Guid natureReleaseId)
        {
            try
            {
                Guid userId = GetIdSignedUser();
                await _natureReleaseService.Inactivation(natureReleaseId, userId);

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