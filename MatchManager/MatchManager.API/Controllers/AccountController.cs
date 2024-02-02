using MatchManager.Core;
using MatchManager.Core.Services.Account.Interface;
using MatchManager.Domain.Entities.Account;
using MatchManager.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> Register([FromBody] RegisterRequestDTO registerDto)
        {
            bool userExists = _userService.IsUserPresent(registerDto.Email);
            if (userExists)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }
            if (!string.Equals(registerDto.Password, registerDto.ConfirmPassword,
                StringComparison.Ordinal))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Confirm password does not match");
                return BadRequest(_response);
            }

            var registerResponse = await _userService.Register(registerDto);
            if (registerResponse.IsSuccess == false)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = Enumerable.Concat(_response.ErrorMessages, registerResponse.ErrorMessages).ToList();
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = registerResponse.Result;
            return Ok(_response);
        }
    }
}
