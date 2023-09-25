using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleFacil.Api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected Guid GetIdSignedUser()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Guid.TryParse(id, out Guid userId);

            return userId;
        }

        protected ModelErrorDTO ReturnModelBadRequestException(Exception ex)
        {
            return new ModelErrorDTO
            {
                StatusCode = 400,
                Title = "Bad Request",
                Message = ex.Message,
                DateTimeError = DateTime.Now
            };
        }

        protected ModelErrorDTO ReturnModelUnathorizedException(Exception ex)
        {
            return new ModelErrorDTO
            {
                StatusCode = 401,
                Title = "Unathorized",
                Message = ex.Message,
                DateTimeError = DateTime.Now
            };
        }

        protected ModelErrorDTO ReturnModelNotFoundException(Exception ex)
        {
            return new ModelErrorDTO
            {
                StatusCode = 404,
                Title = "NotFound",
                Message = ex.Message,
                DateTimeError = DateTime.Now
            };
        }
    }
}