using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDataAccesLayer;
using System.Data.SqlClient;
using System.Data;
using System.Data;

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
            if (studentNumber > 0 && projectId > 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var report =
                  from r in context.Reports
                  join prd in context.ProjectReportDates on r.project_report_date_id equals prd.id
                  join u in context.Users on r.reporter_id equals u.id
                  where u.student_number == studentNumber && prd.project_id_int == projectId
                  select r;
                if (report.ToList().Count > 0)
                {
                    return report;
                }
                else
                {
                    throw new DataException("No data found for valid argument");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }




        /// <summary>
        /// Get the projectgroup(s) the student belongs to
        /// </summary>
        /// <param name="studentNumber">The student you want to query</param>
        /// <returns></returns>
        public IQueryable<ProjectGroup> getStudentGroup(int studentNumber)
        {
            if (studentNumber > 0)
            {
                DatabaseFactory factory = DatabaseFactory.getInstance();
                DatabaseClassesDataContext context = factory.getDataContext();
                var projectGroup =
                    from pg in context.ProjectGroups
                    join pm in context.ProjectMembers on pg equals pm.ProjectGroup
                    join u in context.Users on pm.student_user_id equals u.id
                    where u.student_number == studentNumber
                    select pg;
                if (projectGroup.ToList().Count > 0)
                {
                    return projectGroup;
                }
                else
                {
                    throw new DataException("No data found for valid argument");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get the results of the student of a single project
        /// </summary>
        /// <param name="studentNumber">The student and the project you want to querry</param>
        /// <returns></returns>
        public IQueryable<ProjectGroup> getReportResults(int projectID)
        {
            if (projectID >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var results =
                    from pg in context.ProjectGroups
                    join pm in context.ProjectMembers on pg.id equals pm.project_group_id
                    join u in context.Users on pm.student_user_id equals u.id
                    join p in context.Projects on pg.project_id equals p.id
                    where p.id == projectID
                    && p.status_name == "Closed"
                    select pg;
                if (results.ToList().Count > 0)
                {
                    return results;
                }
                else
                {
                    throw new DataException("No data found for valid argument");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

        }

        /// <summary>
        /// Get the endgrade of a project of an indivdual student
        /// </summary>
        /// <param name="studentNumber">The student you want the grade from</param>
        /// <returns>Returns the final grade as an integer</returns>
        public int getEndGradeIndividual(int studentNumber, int projectId)
        {
            if (studentNumber > 0 && projectId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var endGrade =
                    (from u in context.Users
                     join pm in context.ProjectMembers on u.id equals pm.student_user_id
                     join pg in context.ProjectGroups on pm.project_group_id equals pg.id
                     where u.student_number == studentNumber && pg.project_id == projectId
                     select pm.end_grade).SingleOrDefault();
               if (endGrade != null)
               {
                   return (int)endGrade;
               }
               else
               {
                   throw new DataException("No data found for valid argument");
               }
           }
           else
           {
                throw new ArgumentOutOfRangeException();
            }

        }

        /// <summary>
        /// Get the end grade of a group
        /// </summary>
        /// <param name="studentNumber", "GroupId">The student you want the grade from, the group he belonged to during the project</param>
        /// <returns>Returns the final group grade as an integer</returns>
        public int getEndGradeGroup(int studentNumber, int groupId)
        {
            if (studentNumber > 0 && groupId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var endGroupGrade =
                    (from u in context.Users
                     join pm in context.ProjectMembers on u.id equals pm.student_user_id
                     join pg in context.ProjectGroups on pm.project_group_id equals pg.project_id
                     join p in context.Projects on pg.project_id equals p.id
                     where u.student_number == studentNumber && pg.id == groupId
                     select pg.group_end_grade).SingleOrDefault();
               if (endGroupGrade != null)
               {
                   return (int)endGroupGrade;
               }
               else
               {
                   throw new DataException("No data found for valid argument");
               }
           }
           else
           {
                throw new ArgumentOutOfRangeException();
            }
        }

        public Dictionary<string, string> getName(int studentNumber)
        {
            if (studentNumber > 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
                Dictionary<string, string> fullName = new Dictionary<string, string>();

                var name =
                    from u in context.Users
                    where u.student_number == studentNumber
                    select new
                    {
                        u.firstname,
                        u.lastname
                    };

                foreach (var v in name)
                {
                    fullName.Add(v.firstname, v.lastname);
                }
               if (fullName.Count > 0)
               {
                   return fullName;
               }
               else
               {
                   throw new DataException("No data found for valid argument");
               }
               }
               else
               {
                   throw new DataException("No data found for valid argument");
               }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public User getStudentInfo(int studentNumber)
        {
            DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            var student =
                from u in context.Users
                where u.student_number == studentNumber
                select u;
            return student.FirstOrDefault();
        }
    }
}