﻿using Entities;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public ResendEmailConfirmationModel(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Display(Name = "Електронна адреса")]
            [Required(ErrorMessage = "Введіть електронну адресу")]
            [EmailAddress(ErrorMessage = "Електронну адресу введено некоректно")]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Підтвердження електронної адреси",
                $"Будь ласка, підтвердіть вашу електронну адресу, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>натиснувши на посилання</a>.");
            ModelState.AddModelError(string.Empty, "Лист з підтвердженням надіслано. Перевірте вашу електронну скриньку.");
            return Page();
        }
    }
}
