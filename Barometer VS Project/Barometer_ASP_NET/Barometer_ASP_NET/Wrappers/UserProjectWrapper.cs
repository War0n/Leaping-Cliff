using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;
using System.Diagnostics;

namespace Barometer_ASP_NET.Wrappers
{
    public class UserProjectWrapper
    {
        private int studentNumber;
        private int projectId;
        public int StudentNumber { get; set; }
        public int CurrentProjectId { get; set; }
        public string  CurrentProjectSummary { get; set; }
        public string  CurrentProjectName { get; set; }
        public string  CurrentProjectDate { get; set; }
        public int CurrentProjectGroupGrade { get; set; }
        public int CurrentProjectIndividualGrade { get; set; }
        public IQueryable<User> ProjectMembers { get; set; }
        public IQueryable<User> ProjectOwners { get; set; }
        public IQueryable<User> Tutors { get; set; }
        public IQueryable<Report> MyGrades { get; set; }
        public IQueryable<BaroAspect> SubAspects { get; set; }
        public IQueryable<BaroAspect> SubSubAspects { get; set; }
        public Dictionary<ProjectReportDate, Dictionary<Report, int>> SecondDictionary { get; set; }
        public Dictionary<Report, int> ThirdDictionary { get; set; }
        public Dictionary<BaroAspect, Dictionary<ProjectReportDate, Dictionary<Report, int>>> Grades { get; set; }
        private DAOProject project;
        private DAOStudent student;

        public UserProjectWrapper(int studentNumber)
        {
            this.projectId = 1; //IMPORTANT, set this value to pId
            this.studentNumber = studentNumber;
            StudentNumber = studentNumber;
            student = DatabaseFactory.getInstance().getDAOStudent();
            project = DatabaseFactory.getInstance().getDAOProject();
            FillProjectMembers(student.getStudentGroup(studentNumber).First());
            FillTutors(student.getStudentGroup(studentNumber).First());
            FillProjectOwners(project.GetProject(this.studentNumber, projectId).FirstOrDefault());
            FillProjectDetails(project.GetProject(this.studentNumber, projectId).FirstOrDefault(), student.getStudentGroup(studentNumber).First());
            FillMyGrades(project.GetProject(this.studentNumber, projectId).FirstOrDefault());
            FillGrades(project.GetProject(this.studentNumber, projectId).FirstOrDefault());
            FillSubAspects(project.GetProject(this.studentNumber, projectId).FirstOrDefault());
            FillSubSubAspects(project.GetProject(this.studentNumber, projectId).FirstOrDefault());
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

        private void FillSubAspects(Project p)
        {
            SubAspects = project.GetSubAspects(p.id);
        }

        private void FillSubSubAspects(Project p)
        {
            SubSubAspects = project.GetSubSubAspects(p.id);
        }
         
        private void FillProjectDetails(Project p, ProjectGroup pg)
        {
            CurrentProjectId = p.id;
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

            foreach (var v in project.GetBaroAspect(project.GetProject(this.studentNumber, projectId).FirstOrDefault().id))
            {
                Grades.Add(v, FillSecondDictionary());
            }
        }

        private Dictionary<ProjectReportDate, Dictionary<Report, int>> FillSecondDictionary()
        {
            SecondDictionary = new Dictionary<ProjectReportDate, Dictionary<Report, int>>();

            foreach (var v in project.GetProjectReportDate(project.GetProject(this.studentNumber, projectId).FirstOrDefault().id))
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

            foreach (var v in project.GetReports(project.GetProject(this.studentNumber, projectId).FirstOrDefault().id, this.studentNumber))
            {
                ThirdDictionary.Add(v, (int)v.grade);
            }

            return ThirdDictionary;
        }
    }
}