﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using Barometer_ASP_NET.Database;

namespace Barometer_ASP_NET.Wrappers
{
    public class BaroWidgetWrapper
    {
        public User Subject { get; set; }
        public User Reporter { get; set; }

        public Project Project { get; set; }
        public List<ProjectReportDate> ReportDates { get; set; }
        public List<BaroAspect> HeadAspects { get; set; }
        public List<BaroAspect> ParentAspects { get; set; }
        public List<BaroAspect> SubAspects { get; set; }
    }
}