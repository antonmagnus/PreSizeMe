using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreSizeMe.Models.ClientViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(20)]
        public String ClientId { get; set; }
        [Required]
        [MinLength(35)]
        public String ClientSecret { get; set; }
        [Required]
        [StringLength (25)]
        public String DisplayName { get; set; }
        [Url]
        public Uri PostLogoutRedirectUris { get; set; }
        [Required]
        [Url]
        public Uri RedirectUris { get; set; }
        public string StatusMessage { get; set; }
    }
}
