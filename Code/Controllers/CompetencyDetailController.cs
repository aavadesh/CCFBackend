using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompetencyDetailController : ControllerBase
    {
        private readonly IService<CompetencyDetail, int> service;
        private readonly ICompetencyDetail _competencyDetail;

        public CompetencyDetailController(IService<CompetencyDetail, int> service,
            ICompetencyDetail competencyDetail)
        {
            this.service = service;
            this._competencyDetail = competencyDetail;
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

        [HttpGet("GetCompetencyNameByFrameworkID/{id}")]
        public async Task<IActionResult> GetCompetencyNameByFrameworkID(int id)
        {
            var res = await _competencyDetail.GetCompetencyNameByFrameworkID(id);
            return Ok(res);
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

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CompetencyDetail competencyDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await service.CreateAsync(competencyDetail);

            return Ok(competencyDetail);
        }
    }
}
