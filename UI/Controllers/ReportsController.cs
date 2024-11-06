using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Abstract;
using System.Data;

namespace UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private const Role employer = Role.Employer;
        private readonly ILogger<ReportsController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IVacancyService _jobListingService;
        private readonly IApplicationService _jobApplicationService;

        public ReportsController(ILogger<ReportsController> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IVacancyService jobListingService,
            IApplicationService jobApplicationService)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _jobListingService = jobListingService;
            _jobApplicationService = jobApplicationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("jobs")]
        public async Task<ActionResult> GetJobs()
        {
            var jobs = await _jobListingService.GetStrings();
            if (jobs == null)
                return NoContent();
            jobs = jobs.Prepend("Id, Employer Id, Job Title, Job Sector, Listing Date, Job Location");
            return Ok(String.Join("\n", jobs));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("applications")]
        public async Task<ActionResult> GetApplications()
        {
            var applications = await _jobApplicationService.GetStringsAsync();
            if (applications == null)
                return NoContent();
            applications = applications.Prepend("Id, Applicant Id, Name, Date, Approval, College, Job, Listing date, EmployerId");
            return Ok(String.Join("\n", applications));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("applicants")]
        public ActionResult GetApplicants()
        {
            var users = from user in _userManager.Users.Include(x => x.Resume)
                        where user.Role == Role.Applicant
                        select $"{user.Id}," +
                               $"{user.FullName}," +
                               $"{user.Email}," +
                               $"{user.EmailConfirmed}," +
                               $"{user.PhoneNumber}," +
                               $"{user.PhoneNumberConfirmed}," +
                               $"{user.Resume.LinkedIn}";

            if (users == null)
                return NoContent();
            users = users.Prepend("Id, Name, Email, Email Confirmed, Phone Number, Phone Number Confirmed, Linkedin");
            return Ok(String.Join("\n", users));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("employers")]
        public ActionResult GetEmployers()
        {
            var users = from user in _userManager.Users.Include(x => x.Resume)
                        where user.Role == employer
                        select $"{user.Id}," +
                               $"{user.FullName}," +
                               $"{user.Email}," +
                               $"{user.EmailConfirmed}," +
                               $"{user.PhoneNumber}," +
                               $"{user.PhoneNumberConfirmed}," +
                               $"{user.Organisation}," +
                               $"{user.IsVerified}," +
                               $"{user.Resume.LinkedIn}";
            if (users == null)
                return NoContent();
            users = users.Prepend("Id, Name, Email, Email Confirmed, Phone Number, Phone Number Confirmed, Organisation, Organisation Confirmed, Linkedin");
            return Ok(String.Join("\n", users));
        }
    }
}