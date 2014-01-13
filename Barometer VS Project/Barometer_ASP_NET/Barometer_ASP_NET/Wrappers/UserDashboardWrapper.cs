using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;
using System.Data.Linq;

namespace Barometer_ASP_NET.Wrappers
{
    public class UserDashboardWrapper
    {
        private int studentNumber;
        public IQueryable<ProjectMember> ProjectMembers { get; set; }
        public Project CurrentProject { get; set; }
        public IQueryable<User> CurrentProjectOwners { get; set; }
        public IQueryable<Project> AllProjects { get; set; }
        public Dictionary<string, int[]> Grades { get; set; }

        private DAOProject project;
        private DAOStudent student; 



        public UserDashboardWrapper(int studentNumber)
        {
            this.studentNumber = studentNumber;
            student = DatabaseFactory.getInstance().getDAOStudent();
            project = DatabaseFactory.getInstance().getDAOProject();


            FillCurrentProject();
            if (HasProject)
            {
                FillProjectMembers(student.getStudentGroup(studentNumber).First());
                FillCurrentProjectOwners(student.getStudentGroup(studentNumber).First());
            }
            
            FillAllProjects();
            
            FillAllIndividualGrades();
        }

        private void FillProjectMembers(ProjectGroup projectGroup)
        {
            ProjectMembers = project.getProjectGroupMembers(projectGroup.id);
        }
        private void FillCurrentProjectOwners(ProjectGroup projectGroup)
        {
            CurrentProjectOwners = project.GetProjectOwners(projectGroup.id);
        }
        private void FillCurrentProject()
        {
            CurrentProject = project.GetCurrentActiveProject(studentNumber).FirstOrDefault();
            HasProject = !(CurrentProject == null);
        }
        private void FillAllProjects()
        {
            AllProjects = project.GetAllProjects(studentNumber);

        }

        private void FillAllIndividualGrades()
        {
            Grades = new Dictionary<string, int[]>();

            foreach (Project p in AllProjects)
            {
                if (student.getReportResults(p.id).Count() > 0)
                {
                    int pid;
                    pid = p.id;
                    string pnaam;
                    pnaam = p.name;
                    int pcijfer;
                    pcijfer = (int)student.getReportResults(pid).First().group_end_grade;
                    int icijfer;
                    icijfer = student.getEndGradeIndividual(studentNumber, pid);
                    Grades.Add(pnaam, new int[2] { pcijfer, icijfer });
                }
                
            }

        }



        public bool HasProject { get; set; }
    }
}