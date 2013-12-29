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
       public Dictionary<int, int> getReportResults(int studentNumber, int projectID)
        {
            
          DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            Dictionary<int, int> grades = new Dictionary<int, int>();

           var progress =
                from u in context.Users
                join pm in context.ProjectMembers on u.id equals pm.student_user_id
                join pg in context.ProjectGroups on pm.project_group_id equals pg.id
                join p in context.Projects on pg.project_id equals p.id
                join r in context.Reports on u.id equals r.subject_id
                where u.student_number == studentNumber
                && p.id == projectID
                && p.status_name.Equals("Done")
                select new
                {
                   r.project_report_date_id, r.grade
                };

           foreach(var s in progress)
           {
               grades.Add((int)s.project_report_date_id, (int)s.grade);
           }
            return grades;
       }

       /// <summary>
       /// Get the endgrade of a project of an indivdual student
       /// </summary>
       /// <param name="studentNumber">The student you want the grade from</param>
       /// <returns>Returns the final grade as an integer</returns>
       public int getEndGradeIndividual(int studentNumber, int projectId)
       {
           DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

           var endGrade =
               (from u in context.Users
               join pm in context.ProjectMembers on u.id equals pm.student_user_id
               join pg in context.ProjectGroups on pm.project_group_id equals pg.id
               where u.student_number == studentNumber && pg.project_id == projectId          
               select pm.end_grade).SingleOrDefault();

          return (int)endGrade; 
           
       }

       /// <summary>
       /// Get the end grade of a group
       /// </summary>
       /// <param name="studentNumber", "GroupId">The student you want the grade from, the group he belonged to during the project</param>
       /// <returns>Returns the final group grade as an integer</returns>
       public int getEndGradeGroup(int studentNumber, int groupId)
       {
           DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

           var endGroupGrade =
               (from u in context.Users
               join pm in context.ProjectMembers on u.id equals pm.student_user_id
               join pg in context.ProjectGroups on pm.project_group_id equals pg.project_id
               join p in context.Projects on pg.project_id equals p.id
               where u.student_number == studentNumber && pg.id == groupId
               select pg.group_end_grade).SingleOrDefault();

           return (int)endGroupGrade;
       }

       public Dictionary<string, string> getName(int studentNumber)
       {
         DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
         Dictionary<string, string> fullName = new Dictionary<string, string>();

         var name =
             from u in context.Users
             where u.student_number == studentNumber
             select new
             {
                 u.firstname, u.lastname
             };

         foreach (var v in name)
         {
             fullName.Add(v.firstname, v.lastname);
         }

         return fullName;
       }
    }
}