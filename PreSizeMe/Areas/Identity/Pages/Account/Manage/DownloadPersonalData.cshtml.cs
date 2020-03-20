using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PreSizeMe.Data;

namespace PreSizeMe.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly ApplicationDbContext _context;

        public DownloadPersonalDataModel(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ILogger<DownloadPersonalDataModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            _context.Entry(user)
            .Reference(m => m.Measurement)
            .Load();
            var measurement = user.Measurement;
            _context.Entry(measurement)
            .Reference(g => g.Gender)
            .Load();
            var gender = measurement.Gender;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }



            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));


            // Only include personal data for download

            var personalData = new Dictionary<string, string>();

            var personalDataMeasurements = typeof(Measurement).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataMeasurements)
            {
                personalData.Add(p.Name, p.GetValue(measurement)?.ToString() ?? "null");
            }

            personalData.Add("Gender", gender.Sex);

            var personalDataProps = typeof(ApplicationUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }
    }
}
