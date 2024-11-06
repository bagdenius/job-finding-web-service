using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace UI.Areas.Identity.Pages.Account
{
    public class AuthorizeModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AuthorizeModel> _logger;

        public AuthorizeModel(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AuthorizeModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Введіть ім'я та прізвище")]
            [DataType(DataType.Text, ErrorMessage = "Ім'я або прізвище було введено некоректно")]
            [Display(Name = "Ім'я та прізвище")]
            public string FullName { get; set; }

            //[Display(Name = "Організація/Інституція")]
            //[DataType(DataType.Text, ErrorMessage ="Організація була введена некоректно")]
            //public string Organisation { get; set; }

            [Required(ErrorMessage = "Введіть електронну адресу")]
            [EmailAddress(ErrorMessage = "Електронна адреса введена некоректно")]
            [Display(Name = "Електронна адреса")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Введіть пароль")]
            [MinLength(6, ErrorMessage = "Пароль не може бути менше 6 символів")]
            [MaxLength(100, ErrorMessage = "Пароль не може бути більше 100 символів")]
            [DataType(DataType.Password, ErrorMessage = "Пароль введено некоректно")]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повторіть пароль")]
            [Compare("Password", ErrorMessage = "Паролі повинні співпадати")]
            public string ConfirmPassword { get; set; }
        }


        public ActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userid = Guid.NewGuid().ToString();

            if (ModelState.IsValid)
            {
                // var userid = Guid.NewGuid().ToString();
                var user = new User
                {
                    Id = userid,
                    FullName = Input.FullName,
                    Role = Role.Applicant,
                    //Organisation = Input.Organisation,
                    UserName = Input.Email,
                    Email = Input.Email,
                    Resume = new Resume
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userid
                    }
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var roleExists = await _roleManager.RoleExistsAsync("Applicant");
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Applicant"));
                    }

                    await _userManager.AddToRoleAsync(user, "Applicant");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email,
                            "Підтвердження електронної адреси",
                            $"Будь ласка, підтвердіть вашу електронну адресу, для цього натисніть на <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>посилання</a>.");

                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        return LocalRedirect(Url.Content("~/"));
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}