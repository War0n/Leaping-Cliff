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
            DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

              var report =
                from r in context.Reports
                join prd in context.ProjectReportDates on r.project_report_date_id equals prd.id
                join u in context.Users on r.reporter_id equals u.id
                where u.student_number == studentNumber && prd.project_id_int == projectId
                select r; 

            return report;
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