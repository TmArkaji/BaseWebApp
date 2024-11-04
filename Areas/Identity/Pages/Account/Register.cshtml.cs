// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BaseWebApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using BaseWebApplication.Configurations.Cryptography;
using BaseWebApplication.Configurations;
using BaseWebApplication.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BaseWebApplication.Interfaces;

namespace BaseWebApplication.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration Configuration;
        private readonly ICryptoParamsProtector _protector;
        private readonly IAppUserConfigRepository _appUserConfigRepository;


        public RegisterModel(
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IAppUserConfigRepository appUserConfigRepository,
            IConfiguration configuration,
            ICryptoParamsProtector protector)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _appUserConfigRepository = appUserConfigRepository;
            Configuration = configuration;
            _protector = protector;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = nameof(Resource.AppUser_Email), ResourceType = typeof(Resource))]
            [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
            [Display(Name = nameof(Resource.AppUser_FirstName), ResourceType = typeof(Resource))]
            public string FirstName { get; set; }

            [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
            [Display(Name = nameof(Resource.AppUser_MiddleName), ResourceType = typeof(Resource))]
            public string MiddleName { get; set; }

            [Required]
            [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
            [Display(Name = nameof(Resource.AppUser_LastName), ResourceType = typeof(Resource))]
            public string LastName { get; set; }

            [StringLength(100, ErrorMessageResourceName = nameof(Resource.General_StringLength_Error), ErrorMessageResourceType = typeof(Resource), MinimumLength = 1)]
            [Display(Name = nameof(Resource.AppUser_MiddleLastName), ResourceType = typeof(Resource))]
            public string MiddleLastName { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            String Password = "9oQW1wSzY5bFV3RVZ1dTAxQ0VubXJhOFVqaDF0SHpkUmNrU0wybnFOS3plUk9sUk1TSEl.";
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                user.FirstName = Input.FirstName;
                user.MiddleName = Input.MiddleName;
                user.LastName = Input.LastName;
                user.MiddleLastName = Input.MiddleLastName;
                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, Constants.GESTOR_ROLE);

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);


                    string htmlMessage = System.IO.File.ReadAllText(string.Format(Configuration["AppKeys:HtmlTemplates"] ?? Constants.DEFAULT_PATH, "WelcomeEmail.html"));
                    htmlMessage = htmlMessage.Replace($"_{nameof(user.FirstName)}", user.FirstName);
                    htmlMessage = htmlMessage.Replace($"_{nameof(user.LastName)}", user.LastName);
                    htmlMessage = htmlMessage.Replace($"_{nameof(user.Email)}", user.Email);
                    var URL = $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>";
                    htmlMessage = htmlMessage.Replace($"_clickHere", URL);

                    await _emailSender.SendEmailAsync(Input.Email, Resource.General_ConfirmYourEmail, htmlMessage);

                    var routeID = _appUserConfigRepository.CreateEmptyConfig(userId).Result;
                    var encriptedID = _protector.EncryptParamDictionary(
                        new Dictionary<string, string> {
                            {"ID", routeID.ToString() }
                        }
                    );
                    return RedirectToAction("Index", "AppUserConfig");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private AppUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AppUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<AppUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AppUser>)_userStore;
        }
    }
}
