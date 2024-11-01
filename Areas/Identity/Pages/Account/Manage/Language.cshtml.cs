// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BaseWebApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseWebApplication.Areas.Identity.Pages.Account.Manage
{
    public class LanguageModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LanguageModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public string Username { get; set; }
        public string? SelectedLanguage { get; set; } = "en";
        public List<SelectListItem>? ListOfLanguages { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string? SelectedLanguage { get; set; }
            public bool IsSubmit { get; set; } = false;
            public List<SelectListItem>? ListOfLanguages { get; set; }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            SelectedLanguage = GetCultureFromCookie();
            Username = userName;
            ListOfLanguages = new List<SelectListItem>
                        {
                            new SelectListItem
                            {
                                Text = "English",
                                Value = "en"
                            },

                            new SelectListItem
                            {
                                Text = "Español",
                                Value = "es",
                            },
                        };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            HttpContext myContext = this.HttpContext;
            ChangeLanguageSetCookie(myContext, Input.SelectedLanguage);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private void ChangeLanguageSetCookie(HttpContext myContext, string? culture)
        {
            if (culture == null) { throw new Exception("culture == null"); };

            //this code sets .AspNetCore.Culture cookie
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.UtcNow.AddMonths(1);
            cookieOptions.IsEssential = true;

            myContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                cookieOptions
            );
        }

        private string GetCultureFromCookie()
        {
            HttpContext myContext = this.HttpContext;
            var cultureCookie = myContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

            if (cultureCookie != null)
            {
                var requestCulture = CookieRequestCultureProvider.ParseCookieValue(cultureCookie);
                return requestCulture.Cultures.FirstOrDefault().Value;
            }

            // Devuelve un valor por defecto si la cookie no existe
            return CultureInfo.CurrentCulture.Name;
        }
    }
}
