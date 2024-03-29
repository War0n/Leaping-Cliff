﻿using System;
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
        public int StudentNumber {get;set;}
        
        public IQueryable<ProjectMember> ProjectMembers { get; set; }

        public Project CurrentProject { get; set; }
        public ProjectReportDate NextReportDate { get; set; }

        public IQueryable<User> CurrentProjectOwners { get; set; }
        public User CurrentProjectTutor { get; set; }
        public IQueryable<Project> AllProjects { get; set; }
        public Dictionary<string, int[]> Grades { get; set; }

        private DAOProject project;
        private DAOStudent student;


        private DateTime _currentDate = DateTime.MinValue;
        public DateTime GetCurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }


        public UserDashboardWrapper(int studentNumber)
        {
            this.StudentNumber = studentNumber;
            student = DatabaseFactory.getInstance().getDAOStudent();
			project = DatabaseFactory.getInstance().getDAOProject();


            FillCurrentProject();
            if (HasProject)
            {
                FillNextReportDate();
                FillProjectMembers(student.getStudentGroup(studentNumber).First());
                FillCurrentProjectOwners(student.getStudentGroup(studentNumber).First());
                CurrentProjectTutor = ProjectMembers.FirstOrDefault().ProjectGroup.User;
            }


            try
            {
                FillAllProjects();
                FillAllIndividualGrades();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + " in " + ex.Source);
                // doe niks met error. (dit laten staan)
            }
            
        }

        private void FillNextReportDate()
        {
            NextReportDate = project.getNextReportDate(CurrentProject.id);
        }
        private void FillProjectMembers(ProjectGroup projectGroup)
        {
            ProjectMembers = project.getProjectGroupMembers(projectGroup.id);
        }
        private void FillCurrentProjectOwners(ProjectGroup projectGroup)
        {
            CurrentProjectOwners = project.GetProjectOwners(projectGroup.project_id);
        }
        private void FillCurrentProject()
        {
            CurrentProject = project.GetCurrentActiveProject(StudentNumber).FirstOrDefault();
            HasProject = (CurrentProject != null);
        }
        private void FillAllProjects()
        {
            AllProjects = project.GetAllProjects(StudentNumber);

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
                    icijfer = student.getEndGradeIndividual(StudentNumber, pid);
                    Grades.Add(pnaam, new int[2] { pcijfer, icijfer });
                }
                
            }
            int i = 0;
        }



        public bool HasProject { get; set; }
    }
}