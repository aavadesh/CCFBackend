using API.Models;
using API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hackathon.Controllers
{
    [Route("[controller]")] // -> Invoke method using its name

    [ApiController]
    [EnableCors("corspolicy")]
    public class InformationController : ControllerBase
    {
        private readonly IService<Information, int> service;

        public InformationController(IService<Information, int> service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
       {
            var res = await service.GetAsync();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var res = await service.GetAsync(id);
                if (res == null) throw new Exception("Record not found");
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(Information info)
        {
            if (ModelState.IsValid)
            {
                var res = await service.CreateAsync(info);
                return Ok(res);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Information info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await service.UpdateAsync(id, info);
                    return Ok(res);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var res = await service.DeleteAsync(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}