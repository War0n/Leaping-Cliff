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
    }
}
