using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.Services;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Controllers
{
    [Route("api/account-violation")]
    [ApiController]
    public class AccountViolationController : ControllerBase
    {
        private readonly IAccountViolationService _service;

        public AccountViolationController(IAccountViolationService service)
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

        [HttpGet("violation/{id}")]
        public async Task<IActionResult> FindByViolationId(long id)
        {
            var response = await _service.FindByViolationId(id);
            return this.Response(response);
        }

        [HttpGet("account/{id}/check")]
        public async Task<IActionResult> IsAccountViolate(long id)
        {
            var response = await _service.IsAccountViolate(id);

            if (response.IsSuccess && response.Value is null) return NoContent();
            return this.Response(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            return this.Response(response);
        }
    }
}
