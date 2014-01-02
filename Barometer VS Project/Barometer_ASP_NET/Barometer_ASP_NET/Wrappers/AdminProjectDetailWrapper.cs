using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using Barometer_ASP_NET.Database;

namespace Barometer_ASP_NET.Wrappers
{
    public class AdminProjectDetailWrapper
    {
        private int         projectNumber;
        public string       ProjectName { get; set; }
        public string       ProjectDescription { get; set; }
        public DateTime     BeginDate { get; set; }
        public DateTime     EndDate { get; set; }
        public int CurrentProjectIndividualGrade { get; set; }
        public IQueryable<User> ProjectMembers { get; set; }
        public IQueryable<User> ProjectOwners { get; set; }
        public IQueryable<User> Tutors { get; set; }
        private DAOProject project;

        public AdminProjectDetailWrapper(int projectNumber)
        {
            this.projectNumber = projectNumber;
            project = DatabaseFactory.getInstance().getDAOProject();
        }
    }
}