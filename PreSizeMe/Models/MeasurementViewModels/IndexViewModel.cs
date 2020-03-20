using PreSizeMe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreSizeMe.Models.MeasurementViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public Measurement Measurement { get; set; }

        public string StatusMessage { get; set; }
    }
}
