using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OpenIddict.Abstractions;
using PreSizeMe.Controllers;
using PreSizeMe.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PreSizeMe.ApiController
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(
          UserManager<ApplicationUser> userManager,
          ApplicationDbContext context,
          SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: api/<controller>
        [HttpGet]
        [Authorize(AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
        //[EnableCors("CorsPolicy")]
        public async Task<Dictionary<string, string>> GetAsync()
        {
            var userData = new Dictionary<string, string>();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _context.Entry(user)
            .Reference(m => m.Measurement)
            .Load();

            var measurement = user.Measurement;
            _context.Entry(measurement)
            .Reference(g => g.Gender)
            .Load();

            var gender = measurement.Gender;

            if (User.HasClaim(OpenIddictConstants.Claims.Scope, CustomScopes.Name()))
            {
                userData.Add("Name", user.Name);
            }
            if (User.HasClaim(OpenIddictConstants.Claims.Scope, CustomScopes.Gender()))
            {
                userData.Add("Gender", gender.Sex);
            }
            if (User.HasClaim(OpenIddictConstants.Claims.Scope, OpenIddictConstants.Scopes.Email))
            {
                userData.Add("Email", user.Email);
                userData.Add("IsEmailConfirmed", user.EmailConfirmed.ToString());
            }
            if (User.HasClaim(OpenIddictConstants.Claims.Scope, CustomScopes.Measurements()))
            {
                var dataMeasurements = typeof(Measurement).GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
                foreach (var u in dataMeasurements)
                {
                    userData.Add(u.Name, u.GetValue(measurement)?.ToString() ?? "null");
                }
            }


            return userData;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
