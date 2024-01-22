using MatchManager.Core;
using MatchManager.Core.Services.Account.Interface;
using MatchManager.Domain.Entities.Account;
using MatchManager.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MatchManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServiceAsync _userService;
        private readonly ILogger<AccountController> _logger;
        protected Response _response;

        public AccountController(IAccountServiceAsync userService, ILogger<AccountController> logger)
        {
            _logger = logger;
            _userService = userService;
            _response = new Response();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Response>> Register([FromBody] RegisterDTO registerDto)
        {
            bool userExists = _userService.IsUserPresent(registerDto.Email);
            if (userExists)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userService.Register(registerDto);
            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}
