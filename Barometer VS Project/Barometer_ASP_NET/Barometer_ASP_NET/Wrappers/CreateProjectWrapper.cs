using Barometer_ASP_NET.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Wrappers
{
	public class CreateProjectWrapper
	{
        DAOProject project = Database.DatabaseFactory.getInstance().getDAOProject();
        public IQueryable<BarometerDataAccesLayer.BaroTemplate> Templates { get; set; }
        public BarometerDataAccesLayer.Project FormProject { get; set; }

        public CreateProjectWrapper()
        {
            Templates = project.GetAllTemplates();
        }
	}
}