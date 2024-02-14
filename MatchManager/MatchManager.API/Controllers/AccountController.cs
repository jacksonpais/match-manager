using MatchManager.Core;
using MatchManager.Core.Services.Account.Interface;
using MatchManager.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MatchManager.API.Controllers
{
    [Route("api/account")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServiceAsync _userService;
        private readonly ILogger<AccountController> _logger;
        public IDataProtectionProvider _iDataProtectionProvider;
        protected Response _response;

        public AccountController(IAccountServiceAsync userService, ILogger<AccountController> logger, IDataProtectionProvider iDataProtectionProvider)
        {
            _logger = logger;
            _userService = userService;
            _iDataProtectionProvider = iDataProtectionProvider;
            _response = new Response();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> Register([FromBody] RegisterRequestDTO registerDto)
        {
            try
            {
                bool userExists = await _userService.IsUserPresent(registerDto.Email);
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
            }
            catch
            {
            }
            return Ok(_response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> Login([FromBody] LoginRequestDTO loginDto)
        {
            bool userExists = await _userService.IsUserPresent(loginDto.Email);
            if (!userExists)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Entered Email is Invalid");
                return BadRequest(_response);
            }
            var loginResponse = await _userService.Login(loginDto);
            if (loginResponse.IsSuccess == false)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = Enumerable.Concat(_response.ErrorMessages, loginResponse.ErrorMessages).ToList();
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse.Result;
            return Ok(_response);
        }

        [AllowAnonymous]
        [HttpGet("registration/verify")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Response>> VerifyAccount(string key, string hashtoken)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(hashtoken))
            {
                var loginResponse = await _userService.VerifyAccount(new VerifyAccountDTO()
                {
                    Key = key,
                    HashToken = hashtoken
                });
                if (loginResponse.IsSuccess == false)
                {
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = Enumerable.Concat(_response.ErrorMessages, loginResponse.ErrorMessages).ToList();
                    return StatusCode(StatusCodes.Status500InternalServerError, _response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = loginResponse.Result;
                return Ok(_response);
            }
            else
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Key or token is Invalid");
                return BadRequest(_response);
            }
        }
    }
}
