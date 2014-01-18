using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Models
{
    public class AddAspectsViewModel
    {
        public int reporter_id { get; set; }
        public int subject_id { get; set; }
        public int project_report_date_id { get; set; }
        public int[] aspect { get; set; }
    }
}