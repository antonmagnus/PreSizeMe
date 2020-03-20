using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSizeMe.Data
{
    public class Gender
    {
        public int Id { get; set; }
        [PersonalData]
        public String Sex { get; set; }
    }
}
