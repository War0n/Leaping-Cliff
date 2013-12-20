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

        /// <summary>
        /// Get the results of the student of a single project
        /// </summary>
        /// <param name="studentNumber">The student and the project you want to querry</param>
        /// <returns></returns>
        //public IQueryable<Project> getProgress(int studentNumber, int projectID)
        //{
            
        //    DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

        //    var progress =
        //        from u in context.Users
        //        join pm in context.ProjectMembers on u.id equals pm.student_user_id
        //        join pg in context.ProjectGroups on pm.project_group_id equals pg.id
        //        join p in context.Projects on pg.project_id equals p.id
        //        where u.student_number == studentNumber
        //        && p.id == projectID
        //        && p.status_name.Equals("Done")
        //        select pm.end_grade;


        //    return progress;
        //}
    }
}