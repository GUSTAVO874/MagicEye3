﻿using MagicEye2.Services.AuthAPI.Models.Dto;
using MagicEye2.Services.AuthAPI.Models.Dto;
using MagicEye2.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MagicEye2.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        //private readonly IMessageBus _messageBus;
        //private readonly IConfiguration _configuration;
        protected ResponseDto _response;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            //_configuration = configuration;
            //_messageBus = messageBus;
            _response = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {

            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            //await _messageBus.PublishMessage(model.Email, _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));
            return Ok(_response);
        }
        //[HttpPost("register")]//de chatgpt el del curso es el código de arriba
        //public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = string.Join(" | ", ModelState.Values
        //                                    .SelectMany(v => v.Errors)
        //                                    .Select(e => e.ErrorMessage));
        //        return BadRequest(_response);
        //    }

        //    var errorMessage = await _authService.Register(model);
        //    if (!string.IsNullOrEmpty(errorMessage))
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = errorMessage;
        //        return BadRequest(_response);
        //    }

        //    return Ok(_response);
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }
    }
}
