using Microsoft.AspNetCore.Mvc;
using API.Repository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutRepository _aboutRepo;

        public AboutController(IAboutRepository aboutRepo)
        {
            _aboutRepo = aboutRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var company = await _aboutRepo.GetCompanyInfoAsync();
            var team = await _aboutRepo.GetTeamMembersAsync();
            return Ok(new { company, team });
        }
    }
}
