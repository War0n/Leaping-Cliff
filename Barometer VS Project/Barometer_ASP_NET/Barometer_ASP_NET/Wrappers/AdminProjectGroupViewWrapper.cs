using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using BarometerDataAccesLayer.Database;


namespace Barometer_ASP_NET.Wrappers
{
    public class AdminProjectGroupViewWrapper
    {
        private DAOProject projectDAO = new DAOProject();
        private DAOStudent studentDAO = new DAOStudent();
        public int projectID { get; set; }
        public int projectGroup { get; set; }
        public IQueryable<ProjectMember> ProjectGroupMembers { get; set; }
        public IQueryable<User> CurrentProjectOwners { get; set; }
        public IQueryable<User> GroupMemberUserList { get; set; }
        public IQueryable<User> TutorList { get; set; }
        public IQueryable<User> CurrentTutor { get; set; }
        public IQueryable<ProjectGroup> ProjectGroups { get; set; }


        public AdminProjectGroupViewWrapper(int groupid)
        {
            ProjectGroupMembers = projectDAO.getProjectGroupMembers(groupid);
            CurrentProjectOwners = projectDAO.GetProjectOwners(groupid);
            int projectID = ProjectGroupMembers.First().project_group_id;
            CurrentTutor = projectDAO.GetTutor(groupid);
            ProjectGroups = projectDAO.getProjectGroupsByProject(projectID);
        }
    }
}