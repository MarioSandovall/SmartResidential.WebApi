using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Models.Residential;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/residential")]
    [Authorize(Roles = AuthorizationRoles.SuperAdmin)]
    public class ResidentialController : ControllerBase
    {
        private readonly IResidentialManager _manager;

        public ResidentialController(IResidentialManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Parameter parameter)
        {
            var residentials = await _manager.GetAllAsync(parameter);
            return Ok(residentials);
        }

        [HttpGet("{residentialId}")]
        public async Task<IActionResult> Get([FromRoute] int residentialId)
        {
            if (!await _manager.ExistsAsync(residentialId))
            {
                return NotFound();
            }

            var residential = await _manager.GetByIdAsync(residentialId);
            return Ok(residential);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ResidentialToAdd residential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _manager.AddAsync(residential);
            return NoContent();
        }

        [HttpPut("{residentialId}")]
        public async Task<IActionResult> Update([FromRoute] int residentialId, [FromBody] ResidentialToUpdate residential)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _manager.ExistsAsync(residentialId))
            {
                return NotFound();
            }

            await _manager.UpdateAsync(residential);
            return NoContent();
        }
    }
}
