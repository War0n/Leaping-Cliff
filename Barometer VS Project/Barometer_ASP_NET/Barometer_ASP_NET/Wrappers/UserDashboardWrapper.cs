using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using Barometer_ASP_NET.Database;

namespace Barometer_ASP_NET.Wrappers
{
    public class UserDashboardWrapper
    {
        private int studentNumber;
        public IQueryable<ProjectMember> ProjectMembers { get; set; }
        private DAOProject project;
        private DAOStudent student;

        public UserDashboardWrapper(int studentNumber)
        {
            this.studentNumber = studentNumber;
            student = DatabaseFactory.getInstance().getDAOStudent();
            project = DatabaseFactory.getInstance().getDAOProject();
            FillProjectMembers(student.getStudentGroup(studentNumber).First());
        }

        private void FillProjectMembers(ProjectGroup projectGroup)
        {
            ProjectMembers = project.getProjectGroupMembers(projectGroup.id);
        }


    }
}