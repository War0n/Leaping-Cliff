using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;

namespace Barometer_ASP_NET.Models
{
    public class BaroGradeViewModel
    {
        public BaroAspect Aspect { get; set; }
        public Report Report { get; set; }
    }
}