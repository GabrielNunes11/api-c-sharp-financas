using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication(UserLoginRequestDTO userLoginRequest)
        {
            try
            {
                return Ok(await _userService.AuthUserLogin(userLoginRequest));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUser(UserRequestDTO userRequest)
        {
            try
            {
                return Created("", await _userService.Add(userRequest, Guid.Empty));
            }
            catch(AuthenticationException ex)
            {
                return Unauthorized(new { statusCode = 401, message = ex.Message });
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetAll(Guid.Empty));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                return Ok(await _userService.GetById(Guid.Empty, userId));
            }
            catch
            {
                return NotFound("Usuário não encontrado.");
            }
        }

        [HttpPut]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid userId, UserRequestDTO userRequest)
        {
            try
            {
                return Created("", await _userService.UpdateById(Guid.Empty, userRequest, userId));
            }
            catch
            {
                return NotFound("Usuário não encontrado.");
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                await _userService.Inactivation(Guid.Empty, userId);

                return Ok("Usuário excluído com sucesso!");
            }
            catch
            {
                return NotFound("Usuário não encontrado.");
            }
        }
    }
}