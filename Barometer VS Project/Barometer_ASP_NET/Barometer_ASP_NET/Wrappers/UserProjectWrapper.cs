using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using Barometer_ASP_NET.Database;
using System.Diagnostics;

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
        public Dictionary<ProjectReportDate, Dictionary<Report, int>> SecondDictionary { get; set; }
        public Dictionary<Report, int> ThirdDictionary { get; set; }
        public Dictionary<BaroAspect, Dictionary<ProjectReportDate, Dictionary<Report, int>>> Grades { get; set; }
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
            FillGrades(project.GetCurrentActiveProject(this.studentNumber).FirstOrDefault());
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

        private void FillGrades(Project p)
        {
            project = DatabaseFactory.getInstance().getDAOProject();

            Grades = new Dictionary<BaroAspect, Dictionary<ProjectReportDate, Dictionary<Report, int>>>();

            foreach (var v in project.GetBaroAspect(project.GetCurrentActiveProject(this.studentNumber).FirstOrDefault().id))
            {
                Grades.Add(v, FillSecondDictionary());
            }
        }

        private Dictionary<ProjectReportDate, Dictionary<Report, int>> FillSecondDictionary()
        {
            SecondDictionary = new Dictionary<ProjectReportDate, Dictionary<Report, int>>();

            foreach (var v in project.GetProjectReportDate(project.GetCurrentActiveProject(this.studentNumber).FirstOrDefault().id))
            {
                SecondDictionary.Add(v, FillThirdDictionary());
            }

            foreach (KeyValuePair<ProjectReportDate, Dictionary<Report, int>> s in SecondDictionary)
            {
                for (int i = 0; i < ThirdDictionary.Count; i++)
                {
                    Debug.WriteLine(s.Key.week_label + " " + s.Value.Keys.ElementAt(i).grade);
                }
            }

            return SecondDictionary;
        }

        private Dictionary<Report, int> FillThirdDictionary()
        {
            ThirdDictionary = new Dictionary<Report, int>();

            foreach (var v in project.GetSubAspects(project.GetCurrentActiveProject(this.studentNumber).FirstOrDefault().id, this.studentNumber))
            {
                ThirdDictionary.Add(v, (int)v.grade);
            }

            return ThirdDictionary;
        }
    }
}