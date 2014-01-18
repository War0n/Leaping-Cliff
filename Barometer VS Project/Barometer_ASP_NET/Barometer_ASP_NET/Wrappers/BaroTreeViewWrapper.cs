using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;

namespace Barometer_ASP_NET.Wrappers
{
    public class BaroTreeViewWrapper
    {
        public BaroTemplate Template { get; set; }
        public List<BaroAspect> HeadAspect { get; set; }
    }
}