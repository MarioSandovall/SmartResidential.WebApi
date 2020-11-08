using Business.Interfaces;
using Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/residential/status")]
    [Authorize(Roles = AuthorizationRoles.SuperAdminOrAdmin)]
    public class ResidentialStatusController : ControllerBase
    {
        private readonly IResidentialStatusManager _manager;
        public ResidentialStatusController(IResidentialStatusManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var status = await _manager.GetAllAsync();
            return Ok(new { status });
        }
    }
}