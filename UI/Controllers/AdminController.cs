using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly VacancyService _jobListingService;

        private readonly IConfiguration _configuration;


        public AdminController(ILogger<AdminController> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            VacancyService jobListingService,
            IConfiguration configuration)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _jobListingService = jobListingService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("makeadmin")]
        public async Task<ActionResult> MakeAdmin(string apiKey)
        {
            if (_configuration["SuperAdmin:ApiKey"] == apiKey)
            {
                var user = await _userManager.GetUserAsync(User);
                var roleExists = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExists)
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                user.Role = Role.Admin;
                await _userManager.AddToRoleAsync(user, "Admin");
                return Ok(await _userManager.GetUsersInRoleAsync("Admin"));
            }
            else return Unauthorized();
        }

        [HttpGet("changerole")]
        public async Task<ActionResult> ChangeRole(string apiKey)
        {
            if (_configuration["SuperAdmin:ApiKey"] == apiKey)
            {
                var user = await _userManager.GetUserAsync(User);
                user.Role = Role.Applicant;
                await _userManager.UpdateAsync(user);
                return Ok($"{user.FullName} is now {Role.Applicant}");
            }
            else return Unauthorized();
        }
    }
}
