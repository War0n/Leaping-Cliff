using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;

namespace Barometer_ASP_NET.Wrappers
{
    public class UserProjectWrapper
    {
        private int studentNumber;
        public string  CurrentProjectSummary { get; set; }
        public string  CurrentProjectName { get; set; }
        public string  CurrentProjectDate { get; set; }
        public int CurrentProjectGroupGrade { get; set; }
        public int CurrentProjectIndividualGrade { get; set; }
        public IQueryable<User> ProjectMembers { get; set; }
        public IQueryable<User> ProjectOwners { get; set; }
        public IQueryable<User> Tutors { get; set; }
        public IQueryable<Report> MyGrades { get; set; }
        private DAOProject project;
        private DAOStudent student;

        public UserProjectWrapper(int studentNumber)
        {
            this.studentNumber = studentNumber;
            student = DatabaseFactory.getInstance().getDAOStudent();
            project = DatabaseFactory.getInstance().getDAOProject();
            FillProjectMembers(student.getStudentGroup(studentNumber).First());
            FillTutors(student.getStudentGroup(studentNumber).First());
            FillProjectOwners(project.GetCurrentActiveProject(studentNumber).First());
            FillProjectDetails(project.GetCurrentActiveProject(studentNumber).First(), student.getStudentGroup(studentNumber).First());
            FillMyGrades(project.GetCurrentActiveProject(studentNumber).First());
        }

        private void FillProjectMembers(ProjectGroup projectGroup)
        {
            ProjectMembers = project.getUsersInGroup(projectGroup.id);
        }

        private void FillProjectOwners(Project p)
        {
            ProjectOwners = project.GetProjectOwners(p.id);
        }

        private void FillTutors(ProjectGroup projectGroup)
        {
            Tutors = project.GetTutor(projectGroup.id);
        }

        private void FillMyGrades(Project p)
        {
            MyGrades = student.getStudentGrades(studentNumber, p.id);
        }
         
        private void FillProjectDetails(Project p, ProjectGroup pg)
        {
            CurrentProjectSummary = p.description;
            CurrentProjectName = p.name;
            CurrentProjectDate = (Convert.ToDateTime(p.start_date).Date.ToString() + " tot " + Convert.ToDateTime(p.end_date).Date.ToString());
            CurrentProjectGroupGrade = student.getEndGradeGroup(studentNumber, pg.id);
            CurrentProjectIndividualGrade = student.getEndGradeIndividual(studentNumber, p.id);

        }
    }
}