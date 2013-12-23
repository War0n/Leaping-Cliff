using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Barometer_ASP_NET.Database
{
    public class DAOProject
    {
        /// <summary>
        /// Get all students belonging to the project
        /// </summary>
        /// <param name="projectID">The project you want to query</param>
        /// <returns></returns>
        public IQueryable<User> getStudents(int projectID)
        {
            DatabaseFactory factory = DatabaseFactory.getInstance();
            DatabaseClassesDataContext context = factory.getDataContext();
            var students =
                from u in context.Users
                join pm in context.ProjectMembers on u.ProjectGroups.First() equals pm.ProjectGroup
                where pm.ProjectGroup.Project.id == projectID
                select u;
            return students;
        }

        /// <summary>
        /// Get all students belonging to the projectgroup
        /// </summary>
        /// <param name="projectGroupId">The projectgroup you want to query</param>
        /// <returns>All Group members belonging to a group in a project</returns>
        public IQueryable<ProjectMember> getProjectGroupMembers(int projectGroupId)
        {
            DatabaseFactory factory = DatabaseFactory.getInstance();
            DatabaseClassesDataContext context = factory.getDataContext();
            var projectMembers =
                from pm in context.ProjectMembers
                join pg in context.ProjectGroups on pm.ProjectGroup equals pg
                select pm;
            return projectMembers;
        }
        /// <summary>
        /// Get all tutors belonging to the projectgroup
        /// </summary>
        public Dictionary<string, string> getTutors(int projectId)
        {
            DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            Dictionary<string, string> nameDictionary = new Dictionary<string, string>();
             var fullName =
                from pg in context.ProjectGroups
                join u in context.Users on pg.tutor_user_id equals u.id
                where projectId == pg.project_id
                select new
                {
                    u.firstname,
                    u.lastname
                };
             foreach (var f in fullName)
             {
                 nameDictionary.Add(f.firstname, f.lastname);
             }

            return nameDictionary;
        }


        /// <summary>
        /// Get all the student names from a single group
        /// </summary>
        /// <param name="groupId">The group you want to query</param>
        /// <returns>All the names of the students in a dictionary </returns>
        public Dictionary<string, string> getNamesOfGroupMembers(int groupId)
        {
            DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            Dictionary<string, string> nameDictionary = new Dictionary<string, string>();

            var users =
                from pm in context.ProjectMembers
                join u in context.Users on pm.student_user_id equals u.id
                where pm.project_group_id == groupId
                select new
                {
                    u.firstname, u.lastname
                };

            foreach (var f in users)
            {
                nameDictionary.Add(f.firstname, f.lastname);
            }

            return nameDictionary;
        }
    }
}
