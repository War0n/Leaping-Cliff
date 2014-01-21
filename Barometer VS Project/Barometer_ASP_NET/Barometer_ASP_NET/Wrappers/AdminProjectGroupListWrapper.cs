using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;


namespace Barometer_ASP_NET.Wrappers
{
    public class AdminProjectGroupListWrapper
    {
        public IQueryable<ProjectGroup> ProjectGroups { get; set; }
        public DAOProject projectDAO = DatabaseFactory.getInstance().getDAOProject();

        public AdminProjectGroupListWrapper(int projectID)
        {
            ProjectGroups = projectDAO.getProjectGroupsByProject(projectID);
        }
    }
}