using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using System.Data.SqlClient;

namespace Barometer_ASP_NET.Database
{
    public class DAOStudent
    {
        //Dient als voorbeeld! Moet nog gevuld worden
        /// <summary>
        /// Get all grades belonging to a student in a project
        /// </summary>
        /// <param name="studentNumber">The student you want to query</param>
        /// <param name="projectId">The project of which you like to see the grades</param>
        /// <returns></returns>
        public IQueryable<Report> getStudentGrades(int studentNumber, int projectId)
        {
            return null;
        }


        /// <summary>
        /// Get the projectgroup(s) the student belongs to
        /// </summary>
        /// <param name="studentNumber">The student you want to query</param>
        /// <returns></returns>
        public IQueryable<ProjectGroup> getStudentGroup(int studentNumber)
        {
            DatabaseFactory factory = DatabaseFactory.getInstance();
            DatabaseClassesDataContext context = factory.getDataContext();
            var projectGroup =
                from pg in context.ProjectGroups
                join pm in context.ProjectMembers on pg equals pm.ProjectGroup
                join u in context.Users on pm.student_user_id equals u.id
                where u.student_number == studentNumber
                select pg;
            return projectGroup;
        }
    }
}