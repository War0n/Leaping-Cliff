using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;

namespace Barometer_ASP_NET.Wrappers
{
    public class BaroReportWrapper
    {
        public int ReporterId { get; set; }
        public Project Project { get; set; }
        public ProjectReportDate ReportDate { get; set; }
        public List<User> Members { get; set; }
        public List<BaroAspect> Aspects { get; set; }
    }
}