using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.Services;

namespace SGULibraryBE.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountRequest request)
        {
            var response = await _service.Add(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] AccountRequest request)
        {
            var response = await _service.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _service.Delete(id);
            return NoContent();
        }
    }
}
