using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreSizeMe.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        [StringLength(25)]
        public String Name { get; set; }
        [Required]
        public Measurement Measurement { get; set; }
        [ForeignKey("Measurement")]
        public int MeasurementId { get; set; }
    }
}
