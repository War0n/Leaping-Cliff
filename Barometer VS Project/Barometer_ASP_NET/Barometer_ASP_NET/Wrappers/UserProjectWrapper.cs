using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using Barometer_ASP_NET.Database;

namespace Barometer_ASP_NET.Wrappers
{
    public class UserProjectWrapper
    {
        private int studentNumber;
        public IQueryable<User> ProjectMembers { get; set; }
        public IQueryable<User> ProjectOwners { get; set; }
        public IQueryable<User> Tutors { get; set; }
        private DAOProject project;
        private DAOStudent student;

        public UserProjectWrapper(int studentNumber)
        {
            this.studentNumber = studentNumber;
            student = DatabaseFactory.getInstance().getDAOStudent();
            project = DatabaseFactory.getInstance().getDAOProject();
            FillProjectMembers(student.getStudentGroup(studentNumber).First());
            FillTutors(student.getStudentGroup(studentNumber).First());
            FillProjectOwners();
        }

        private void FillProjectMembers(ProjectGroup projectGroup)
        {
            ProjectMembers = project.getUsersInGroup(projectGroup.id);
        }

        private void FillProjectOwners()
        {
            ProjectOwners = project.GetProjectOwners(1);
        }

        private void FillTutors(ProjectGroup projectGroup)
        {
            Tutors = project.GetTutor(projectGroup.id);
        }
    }
}