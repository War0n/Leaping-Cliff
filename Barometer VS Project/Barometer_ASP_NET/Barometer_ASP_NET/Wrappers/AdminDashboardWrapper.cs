﻿using BarometerDataAccesLayer.Database;
using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Wrappers
{
    public class AdminDashboardWrapper
    {
        private DAOProject projectDAO = DatabaseFactory.getInstance().getDAOProject();
        public IEnumerable<Project> OwnProjects { get; set; }
        public Dictionary<string, IEnumerable<User>> UsersInGroups { get; set; }

        public AdminDashboardWrapper(int adminId)
        {
			OwnProjects = projectDAO.getAllProjects();
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
                if (!UsersInGroups.ContainsKey(projectGroup.group_code))
                {
                    UsersInGroups.Add(projectGroup.group_code, projectDAO.getUsersInGroup(projectGroup.id));
                }
            }
        }

    }
}