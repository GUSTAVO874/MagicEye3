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
        [HttpPost("savecarrera")]//deprecated
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
        [HttpPost("savecarrera2")] //save carrera-período-ciclo-parcial
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> PostCarrera2([FromBody] CarreraPerCicParcDto carreraPerCicParcDto)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    // Map DTOs to domain models
                    Carrera carrera = _mapper.Map<Carrera>(carreraPerCicParcDto.Carrera);
                    Periodo periodo = _mapper.Map<Periodo>(carreraPerCicParcDto.Periodo);

                    _db.Carreras.Add(carrera);
                    _db.Periodos.Add(periodo);
                    await _db.SaveChangesAsync();

                    // create carrera periodo link
                    CarreraPeriodo carreraPeriodo = new CarreraPeriodo
                    {
                        CarreraId = carrera.CarreraId,
                        PeriodoId = periodo.PeriodoId
                    };
                    _db.CarreraPeriodos.Add(carreraPeriodo);
                    await _db.SaveChangesAsync();

                    // Mapear Ciclo
                    Ciclo ciclo = _mapper.Map<Ciclo>(carreraPerCicParcDto.Ciclo);
                    ciclo.PeriodoId = periodo.PeriodoId;
                    _db.Ciclos.Add(ciclo);
                    await _db.SaveChangesAsync();

                    // Mapear Parcial
                    Parcial parcial = _mapper.Map<Parcial>(carreraPerCicParcDto.Parcial);
                    parcial.CicloId = ciclo.CicloId;
                    _db.Parciales.Add(parcial);
                    await _db.SaveChangesAsync();

                    // Confirmar la transacción (solo UNA vez)
                    await transaction.CommitAsync();

                    // Retornar el DTO (o uno actualizado con los IDs generados)
                    _response.Result = carreraPerCicParcDto;
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, revertimos todas las operaciones
                    await transaction.RollbackAsync();
                    _response.Message = ex.Message;
                    _response.IsSuccess = false;
                }
                return _response;
            }
        }

    }
}
