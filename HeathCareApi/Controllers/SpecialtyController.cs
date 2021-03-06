using HealthCareApi.Authorization;
using HealthCareApi.Dto.Specialty;
using HealthCareApi.Entities;
using HealthCareApi.Enuns;
using HealthCareApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialtyController : ControllerBase
    {
        private readonly ISpecialtyService _service;

        public SpecialtyController(ISpecialtyService service)
        {
            _service = service;
        }

        [Authorize(TypeUser.Doctor)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SpecialtyRequest specialtyRequest) => Ok(await _service.Create(specialtyRequest));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [Authorize(TypeUser.Doctor)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] SpecialtyRequest specialtyIn, int id)
        {
            await _service.Update(specialtyIn, id);
            return NoContent();
        }


        [Authorize(TypeUser.Doctor)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
  } 