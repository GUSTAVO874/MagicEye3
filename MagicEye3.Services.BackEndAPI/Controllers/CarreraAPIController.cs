using AutoMapper;
using MagicEye3.Services.BackEndAPI.Data;
using MagicEye3.Services.BackEndAPI.Models;
using MagicEye3.Services.BackEndAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicEye3.Services.BackEndAPI.Controllers
{
    [Route("api/carrera")]
    [ApiController]
    [Authorize]
    public class CarreraAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public CarreraAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        [HttpPost("savecarrera")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> PostCarrera([FromBody] CarreraDto carreraDto)
        {
            //save de una sola entidad 
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    // Map DTOs to domain models
                    Carrera carrera= _mapper.Map<Carrera>(carreraDto);
                    _db.Carreras.Add(carrera);
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();
                    _response.Result = carreraDto;
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, revertimos todas las operaciones
                    await transaction.RollbackAsync();
                    _response.Message = ex.Message.ToString();
                    _response.IsSuccess = false;
                }
                return _response;
            }
        }
    }
}
