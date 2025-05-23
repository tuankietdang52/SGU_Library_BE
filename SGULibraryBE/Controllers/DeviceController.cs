﻿using Microsoft.AspNetCore.Mvc;
using SGULibraryBE.DTOs.Requests;
using SGULibraryBE.Services;
using SGULibraryBE.Utilities;

namespace SGULibraryBE.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DeviceController(IDeviceService deviceService)
        {
            _service = deviceService;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceRequest request)
        {
            var response = await _service.Add(request);
            return this.Response(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] DeviceRequest request)
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
