using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.Services;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Controllers
{
    [Route("api/violations")]
    [ApiController]
    public class ViolationController : ControllerBase
    {
        private readonly IViolationService _service;
        
        public ViolationController(IViolationService violationService)
        {
            _service = violationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            var response = await _service.FindById(id);
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
