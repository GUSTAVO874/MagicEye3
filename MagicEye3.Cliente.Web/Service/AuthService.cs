﻿using MagicEye3.Cliente.Web.Models;
using MagicEye3.Cliente.Web.Service.IService;
using MagicEye3.Cliente.Web.Utility;

namespace MagicEye3.Cliente.Web.Service
{//
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "api/auth/login"
            }, withBearer: false);
            
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "api/auth/register"
            }, withBearer: false);
            //return await _baseService.SendAsync(new RequestDto()
            //{
            //    ApiType = SD.ApiType.POST,
            //    Data = registrationRequestDto,
            //    Url = SD.AuthAPIBase + "/api/auth/register"
            //});
        }
    }
}
