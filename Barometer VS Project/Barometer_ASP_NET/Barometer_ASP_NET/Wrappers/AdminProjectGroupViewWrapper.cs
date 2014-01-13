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
        public List<User> GroupMemberUserList { get; set; }
        public IQueryable<User> TutorList { get; set; }
        public IQueryable<User> CurrentTutor { get; set; }
        public IQueryable<ProjectGroup> ProjectGroups { get; set; }


        public AdminProjectGroupViewWrapper(int groupid)
        {
            projectGroup = groupid;
            ProjectGroupMembers = projectDAO.getProjectGroupMembers(groupid);
            CurrentProjectOwners = projectDAO.GetProjectOwners(groupid);
            CurrentTutor = projectDAO.GetTutor(groupid);
            ProjectGroups = projectDAO.getProjectGroupsByProject(ProjectGroupMembers.First().ProjectGroup.project_id);
            GroupMemberUserList = new List<User>();
            foreach (ProjectMember member in ProjectGroupMembers)
            {
                GroupMemberUserList.Add(member.User);
            }
        }
    }
}