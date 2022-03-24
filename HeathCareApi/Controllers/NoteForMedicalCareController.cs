using HealthCareApi.Dto.User;
using HealthCareApi.Entities;
using HealthCareApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteForMedicalCareController : ControllerBase
    {
        private readonly INoteForMedicalCareService _service;

        public NoteForMedicalCareController(INoteForMedicalCareService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteForMedicalCareRequest noteRequest) => Ok(await _service.Create(noteRequest));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] NoteForMedicalCareRequest noteRequest, int id)
        {
            await _service.Update(noteRequest, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
  } 