using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using PreSizeMe.Data;
using PreSizeMe.Models.ClientViewModels;

namespace PreSizeMe.Controllers
{
    [Route("[controller]/[action]")]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOpenIddictApplicationManager _manager;
        private readonly UrlEncoder _urlEncoder;

        public ClientController(
          ApplicationDbContext context,
          IOpenIddictApplicationManager manager,
          UrlEncoder urlEncoder)
        {
            _context = context;
            _manager = manager;
            _urlEncoder = urlEncoder;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            String secretValue = "";
            String idValue;

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            // Buffer storage.
            byte[] data = new byte[8];
            rng.GetBytes(data);
            idValue = BitConverter.ToString(data, 0);
            for (int i = 0; i < 2; i++)
            {
                rng.GetBytes(data);
                secretValue += BitConverter.ToString(data, 0);
            }
            var model = new RegisterViewModel
            {
                ClientId = idValue,
                ClientSecret = secretValue,
                StatusMessage = StatusMessage

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (await _manager.FindByClientIdAsync(model.ClientId) == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = model.ClientId,
                    ClientSecret = model.ClientSecret,
                    DisplayName = model.DisplayName,
                    PostLogoutRedirectUris = { model.PostLogoutRedirectUris },
                    RedirectUris = { model.RedirectUris },
                    Permissions =
                            {
                                OpenIddictConstants.Permissions.Endpoints.Authorization,
                                OpenIddictConstants.Permissions.Endpoints.Logout,
                                OpenIddictConstants.Permissions.Endpoints.Token,
                                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                                OpenIddictConstants.Permissions.Scopes.Email,
                                CustomScopes.PermissionMeasurements(),
                                CustomScopes.PermissionGender(),
                                CustomScopes.PermissionName()
                            }
                };
                var setApplicationResult = _manager.CreateAsync(descriptor);
                if (!setApplicationResult.IsFaulted)
                {
                    StatusMessage = "Your application has been registered";
                    return RedirectToAction(nameof(Register));
                }

                ModelState.AddModelError(String.Empty, setApplicationResult.Exception.Message);

            }
            //if we get here something is wrong re displaying form.
            StatusMessage = "Error: Something went wrong";
            return RedirectToAction(nameof(Register));

        }
    }
}