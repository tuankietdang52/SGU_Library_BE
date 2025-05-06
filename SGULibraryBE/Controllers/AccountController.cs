using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.DTOs.Responses;
using SGULibraryBE.Services;
using SGULibraryBE.Services.Implementations;
using SGULibraryBE.Utilities;
using SGULibraryBE.Utilities.ResultHandler;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            var response = await _service.FindById(id);
            return this.Response(response);
        }

        [HttpGet("send")]
        public async Task<IActionResult> Send([FromQuery] string email)
        {

            var result = await _service.SendMail(email);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }
        [HttpGet("verify")]
        public async Task<IActionResult> Send([FromQuery] string email,
                                                [FromQuery] string otp)
        {

            var result = await _service.VerifyOtp(email, otp);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            return this.Response(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountRequest request)
        {
            var response = await _service.Add(request);
            return this.Response(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] AccountRequest request)
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
