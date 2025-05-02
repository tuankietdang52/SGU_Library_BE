using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.Services;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            var response = await _service.FindById(id);
            return this.Response(response);
        }

        [HttpGet("account/{id}")]
        public async Task<IActionResult> FindByAccountId(long id)
        {
            var response = await _service.FindByAccountId(id);
            return this.Response(response);
        }

        [HttpGet("device/{id}")]
        public async Task<IActionResult> FindByDeviceId(long id)
        {
            var response = await _service.FindByDeviceId(id);
            return this.Response(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            return this.Response(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationRequest request)
        {
            var response = await _service.Add(request);
            return this.Response(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ReservationRequest request)
        {
            var response = await _service.Update(id, request);
            return this.Response(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _service.Delete(id);
            return this.Response(response);
        }
    }
}
