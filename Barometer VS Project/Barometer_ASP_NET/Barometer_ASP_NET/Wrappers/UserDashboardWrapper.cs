﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using Barometer_ASP_NET.Database;
using System.Data.Linq;

namespace Barometer_ASP_NET.Wrappers
{
    public class UserDashboardWrapper
    {
        private int studentNumber;
        public IQueryable<ProjectMember> ProjectMembers { get; set; }
        public IQueryable<Project> CurrentProject { get; set; }
        public IQueryable<User> CurrentProjectOwners { get; set; }
        public IQueryable<Project> AllProjects { get; set; }
        public Dictionary<string, int[]> Grades { get; set; }
<<<<<<< Updated upstream
        public IQueryable<User> CurrentProjectOwners { get; set; }
        public IQueryable<Project> AllProjects { get; set; }
        public Dictionary<string, int[]> Grades { get; set; }

        private DAOProject project;
        private DAOStudent student;
=======

        private DAOProject project;
        private DAOStudent student;

>>>>>>> Stashed changes

        public UserDashboardWrapper(int studentNumber)
        {
            this.studentNumber = studentNumber;
            student = DatabaseFactory.getInstance().getDAOStudent();
            project = DatabaseFactory.getInstance().getDAOProject();
            FillProjectMembers(student.getStudentGroup(studentNumber).First());
            FillCurrentProjectOwners(student.getStudentGroup(studentNumber).First());
            FillAllProjects();
            FillCurrentProject();
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
            CurrentProject = project.GetCurrentActiveProject(studentNumber);
        }
        private void FillAllProjects()
        {
            AllProjects = project.GetAllProjects(studentNumber);

        }

        private void FillAllIndividualGrades()
        {
            Grades = new Dictionary<string, int[]>();
<<<<<<< Updated upstream

            foreach (Project p in AllProjects)
            {
                int pid;
                pid = p.id;
                string pnaam;
                pnaam = p.name;
                int pcijfer;
                pcijfer = student.getReportResults(studentNumber, pid).Values.First<int>(); 
                int icijfer;
                icijfer = student.getEndGradeIndividual(studentNumber, pid);
                Grades.Add(pnaam, new int[2] { pcijfer, icijfer });
            }
=======
>>>>>>> Stashed changes

            foreach (Project p in AllProjects)
            {
                int pid;
                pid = p.id;
                string pnaam;
                pnaam = p.name;
                int pcijfer;
                pcijfer = student.getReportResults(studentNumber, pid).Values.First<int>(); 
                int icijfer;
                icijfer = student.getEndGradeIndividual(studentNumber, pid);
                Grades.Add(pnaam, new int[2] { pcijfer, icijfer });
            }

        }


    }
}