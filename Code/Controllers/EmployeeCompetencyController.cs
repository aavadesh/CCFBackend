using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCompetencyController : ControllerBase
    {
        private readonly IService<EmployeeCompetency, int> service;
        private readonly IEmployeeCompetency _employeeCompetency;

        public EmployeeCompetencyController(IService<EmployeeCompetency, int> service,
            IEmployeeCompetency employeeCompetency)
        {
            this.service = service;
            this._employeeCompetency = employeeCompetency;
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

        [HttpGet("{id}/{employeeID}")]
        public async Task<IActionResult> GetAsync(int id, int employeeID)
        {
            try
            {
                var res = await _employeeCompetency.GetAsync(id, employeeID);
                if (res == null) throw new Exception("Record not found");
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] EmployeeCompetency employeeCompetency)
        {
            var files = employeeCompetency.Files;
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (files.Any(f => f.Length == 0))
            {
                return BadRequest();
            }
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                employeeCompetency.FileName = fileName;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await service.CreateAsync(employeeCompetency);

            return Ok(employeeCompetency);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeCompetency employeeCompetency)
        {
            await service.UpdateAsync(employeeCompetency.EmployeeCompetencyID, employeeCompetency);

            return Ok(employeeCompetency);
        }
    }
}
