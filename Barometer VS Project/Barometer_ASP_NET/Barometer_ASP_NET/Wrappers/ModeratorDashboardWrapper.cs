using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;
using System.Data.Linq;
namespace Barometer_ASP_NET.Wrappers
{

    public class ModeratorDashboardWrapper
    {

        private DAOProject projectDAO = new DAOProject();
        public IEnumerable<Project> OwnProjects { get; set; }
        public Dictionary<string, IEnumerable<User>> UsersInGroups { get; set; }

        public ModeratorDashboardWrapper(int adminId)
        {
            OwnProjects = projectDAO.GetProjectsByOwner(adminId);
            FillUsersInGroups();
        }

        private void FillUsersInGroups()
        {
            List<ProjectGroup> projectGroups = new List<ProjectGroup>();
            UsersInGroups = new Dictionary<string, IEnumerable<User>>();
            foreach (Project project in OwnProjects)
            {
                projectGroups.AddRange(projectDAO.getProjectGroupsByProject(project.id));
            }
            foreach (ProjectGroup projectGroup in projectGroups)
            {
                UsersInGroups.Add(projectGroup.group_code, projectDAO.getUsersInGroup(projectGroup.id));
            }
        }

    }
}