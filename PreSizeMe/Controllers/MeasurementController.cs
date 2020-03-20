using PreSizeMe.Data;
using PreSizeMe.Models;
using PreSizeMe.Areas;
using PreSizeMe.Models.MeasurementViewModels;
//using PreSizeMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace PreSizeMe.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class MeasurementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
       // private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        public MeasurementController(
          UserManager<ApplicationUser> userManager,
          ApplicationDbContext context,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
         // ILogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _emailSender = emailSender;
            //  _logger = logger;
            _urlEncoder = urlEncoder;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            _context.Entry(user)
            .Reference(m => m.Measurement)
            .Load();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Name = user.Name,
                Measurement = user.Measurement,
                StatusMessage = StatusMessage
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            _context.Entry(user)
            .Reference(m => m.Measurement)
            .Load();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var chest = model.Measurement.Chest;
            if (user.Measurement.Chest != chest)
            {
                user.Measurement.Chest = chest;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {

                    throw new ApplicationException($"Unexpected error occurred setting chest size for user with ID '{user.Id}'.");
                }
            }
            var Neck = model.Measurement.Neck;
            if (user.Measurement.Neck != Neck)
            {
                user.Measurement.Neck = Neck;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting neck size for user with ID '{user.Id}'.");
                }
            }
            var Sleeve = model.Measurement.Sleeve;
            if (user.Measurement.Sleeve != Sleeve)
            {
                user.Measurement.Sleeve = Sleeve;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting sleeve size for user with ID '{user.Id}'.");
                }
            }
            var UpperBody = model.Measurement.Upperbody;
            if (user.Measurement.Upperbody != UpperBody)
            {
                user.Measurement.Upperbody = UpperBody;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting upperbody size for user with ID '{user.Id}'.");
                }
            }
            var Hips = model.Measurement.Hips;
            if (user.Measurement.Hips != Hips)
            {
                user.Measurement.Hips = Hips;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting hip size for user with ID '{user.Id}'.");
                }
            }
            var Inseam = model.Measurement.Inseam;
            if (user.Measurement.Inseam != Inseam)
            {
                user.Measurement.Inseam = Inseam;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting inseam size for user with ID '{user.Id}'.");
                }
            }
            var Height = model.Measurement.Height;
            if (user.Measurement.Height != Height)
            {
                user.Measurement.Height = Height;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting height for user with ID '{user.Id}'.");
                }
            }
            var Waist = model.Measurement.Waist;
            if (user.Measurement.Waist != Waist)
            {
                user.Measurement.Waist = Waist;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting waist size for user with ID '{user.Id}'.");
                }
            }
            var Thigh = model.Measurement.Thigh;
            if (user.Measurement.Thigh != Thigh)
            {
                user.Measurement.Thigh = Thigh;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting thigh size for user with ID '{user.Id}'.");
                }
            }
            var Feet = model.Measurement.Feet;
            if (user.Measurement.Feet != Feet)
            {
                user.Measurement.Feet = Feet;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting foot size for user with ID '{user.Id}'.");
                }
            }
            var Head = model.Measurement.Head;
            if (user.Measurement.Head != Head)
            {
                user.Measurement.Head = Head;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting foot size for user with ID '{user.Id}'.");
                }
            }
            var Hand = model.Measurement.Hand;
            if (user.Measurement.Hand != Hand)
            {
                user.Measurement.Hand = Hand;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting foot size for user with ID '{user.Id}'.");
                }
            }
            var BelowChest = model.Measurement.BelowChest;
            if (user.Measurement.BelowChest != BelowChest)
            {
                user.Measurement.BelowChest = BelowChest;
                var setChestResult = await _userManager.UpdateAsync(user);
                if (!setChestResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting foot size for user with ID '{user.Id}'.");
                }
            }
            StatusMessage = "Your measurements has been updated";
            return RedirectToAction(nameof(Index));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}