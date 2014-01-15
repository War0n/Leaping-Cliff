using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;

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
        public IQueryable<ProjectGroup> Groups { get; set; }
        private DAOProject project;

        public AdminProjectDetailWrapper(int projectNumber)
        {
            this.projectNumber = projectNumber;
            project = DatabaseFactory.getInstance().getDAOProject();
            ProjectName = project.getProjectName(projectNumber);
            ProjectDescription = project.getProjectSummary(projectNumber);
            GetDates();
            //CurrentProjectIndividualGrade = mist nog, weet niet wat het is en is geen query voor
            ProjectMembers = project.getStudents(projectNumber);
            ProjectOwners = project.GetProjectOwners(projectNumber);
            // Tutors = mist nog, tutors kan alleen per groep, weet niet of je wat aan een array hebt
            Groups = project.getProjectGroupsByProject(projectNumber);
        }

        public void AddGroup(ProjectGroup group)
        {
            project.AddGroup(group);
        }

        public IQueryable<ProjectMember> GetGroupMembers(ProjectGroup group)
        {
            return project.getProjectGroupMembers(group.id);
        }

        public IQueryable<User> GetTutor(ProjectGroup group)
        {
            return project.GetTutor(group.id);
        }

        private void GetDates()
        {
            foreach (KeyValuePair<string, string> pair in project.getStartAndEndDate(projectNumber))
            {
                BeginDate = DateTime.Parse(pair.Key);
                EndDate = DateTime.Parse(pair.Value);
            }
        }
    }
}