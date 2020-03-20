using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreSizeMe.Data
{
    public class Measurement
    {

        public int Id { get; set; }
        [PersonalData]
        [Range(0, 100, ErrorMessage = "Head circumrence must be between 0 to 100")]
        public int Head { get; set; }
        [PersonalData]
        [Range(0, 100, ErrorMessage = "Neck circumrence must be between 0 to 100")]
        public int Neck { get; set; }
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Sleeve length must be between 0 to 200")]
        public int Sleeve { get; set; }
        [Display(Name = "Upperbody length")]
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Upperbody length must be between 0 to 200")]
        public int Upperbody { get; set; }
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Chest circumrence must be between 0 to 200")]
        public int Chest { get; set; }
        [Display(Name = "Under chest")]
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Under chest circumrence must be between 0 to 200")]
        public int BelowChest { get; set; }
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Waist circumrence must be between 0 to 200")]
        public int Waist { get; set; }
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Hip circumrence must be between 0 to 200")]
        public int Hips { get; set; }
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Thigh circumrence must be between 0 to 200")]
        public int Thigh { get; set; }
        [PersonalData]
        [Range(0, 200, ErrorMessage = "Inseam length must be between 0 to 200")]
        public int Inseam { get; set; }
        [PersonalData]
        [Range(0, 300, ErrorMessage = "Hight must be between 0 to 300")]
        public int Height { get; set; }
        [Display(Name = "Foot length")]
        [PersonalData]
        [Range(0, 100, ErrorMessage = "Foot length must be between 0 to 100")]
        public int Feet { get; set; }
        [PersonalData]
        [Range(0, 100, ErrorMessage = "Hand circumrence must be between 0 to 100")]
        public int Hand { get; set; }
        public Gender Gender { get; set; }
        [ForeignKey("Gender")]
        public int GenderId { get; set; }

    }
}

