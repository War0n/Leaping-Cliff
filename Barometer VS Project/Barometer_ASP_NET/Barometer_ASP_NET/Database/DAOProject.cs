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
            if (projectID >= 0)
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
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get all students belonging to the projectgroup
        /// </summary>
        /// <param name="projectGroupId">The projectgroup you want to query</param>
        /// <returns>All Group members belonging to a group in a project</returns>
        public IQueryable<ProjectGroup> getProjectGroupsByProject(int projectId)
        {
            DatabaseFactory factory = DatabaseFactory.getInstance();
            DatabaseClassesDataContext context = factory.getDataContext();
            var projectGroups =
                from pg in context.ProjectGroups
                join p in context.Projects on pg.project_id equals p.id
                where p.id == projectId
                select pg;
            return projectGroups;
        }
         
        /// <summary>
        /// Get all students belonging to the projectgroup
        /// </summary>
        /// <param name="projectGroupId">The projectgroup you want to query</param>
        /// <returns>All Group members belonging to a group in a project</returns>
        public IQueryable<ProjectMember> getProjectGroupMembers(int projectGroupId)
        {
            if (projectGroupId >= 0)
            {
                DatabaseFactory factory = DatabaseFactory.getInstance();
                DatabaseClassesDataContext context = factory.getDataContext();
                var projectMembers =
                    from pm in context.ProjectMembers
                    join pg in context.ProjectGroups on pm.ProjectGroup equals pg
                    select pm;
                return projectMembers;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// Get all tutors belonging to the projectgroup
        /// </summary>
        public Dictionary<string, string> getTutors(int projectId)
        {
            if (projectId >= 0)
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
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }


        /// <summary>
        /// Get all the student names from a single group
        /// </summary>
        /// <param name="groupId">The group you want to query</param>
        /// <returns>All the names of the students in a dictionary </returns>
        public Dictionary<string, string> getNamesOfGroupMembers(int groupId)
        {
            if (groupId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
                Dictionary<string, string> nameDictionary = new Dictionary<string, string>();

                var users =
                    from pm in context.ProjectMembers
                    join u in context.Users on pm.student_user_id equals u.id
                    where pm.project_group_id == groupId
                    select new
                    {
                        u.firstname,
                        u.lastname
                    };

                foreach (var f in users)
                {
                    nameDictionary.Add(f.firstname, f.lastname);
                }

                return nameDictionary;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get the project name
        /// </summary>
        /// <param name="groupId">The project you want to query</param>
        /// <returns>The name of the project </returns>
        public string getProjectName(int projectId)
        {
            if (projectId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var projectName =
                    (from p in context.Projects
                     where p.id == projectId
                     select p.name).SingleOrDefault();

                return (string)projectName;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get the start and the end date of a project
        /// </summary>
        /// <param name="groupId">The project you want to query</param>
        /// <returns>The start and end date of a project in string format </returns>
        public Dictionary<string, string> getStartAndEndDate(int projectId)
        {
            if (projectId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
                Dictionary<string, string> startAndEndDate = new Dictionary<string, string>();

                var projectDate =
                    from p in context.Projects
                    where p.id == projectId
                    select new
                    {
                        p.start_date,
                        p.end_date
                    };

                foreach (var p in projectDate)
                {
                    startAndEndDate.Add(p.start_date.ToString(), p.end_date.ToString());
                }
                return startAndEndDate;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get the summary of a project
        /// </summary>
        /// <param name="groupId">The project you want to query</param>
        /// <returns>The summary of a project</returns>
        public string getProjectSummary(int projectId)
        {
            if (projectId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var projectSummary =
                    (from p in context.Projects
                     where p.id == projectId
                     select p.description).SingleOrDefault();

                return (string)projectSummary;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get the names of the teachers who own a particular project
        /// </summary>
        /// <param name="groupId">The project you want to query</param>
        /// <returns>Returns a dictionary with the names of the teachers </returns>
        public Dictionary<string, string> projectTeachers(int projectId)
        {
            if (projectId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
                Dictionary<string, string> teachers = new Dictionary<string, string>();

                var teacher =
                    from p in context.Projects
                    join po in context.ProjectOwners on p.id equals po.project_id
                    join u in context.Users on po.user_id equals u.id
                    where p.id == projectId
                    select new
                    {
                        u.firstname,
                        u.lastname
                    };

                foreach (var t in teacher)
                {
                    teachers.Add(t.firstname, t.lastname);
                }

                return teachers;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get all the users of a project who belong to a particular group
        /// </summary>
        /// <param name="groupId">The project and group you want to query</param>
        /// <returns>Returns user objects </returns>
        /// 
        public IQueryable<User> getUsersInGroup(int groupId)
        {
            if (groupId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var users =
                    from u in context.Users
                    join pm in context.ProjectMembers on u.id equals pm.student_user_id
                    where pm.project_group_id == groupId
                    select u;

                return users;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get all the the project owners of a project
        /// </summary>
        /// <param name="groupId">The project you want to query</param>
        /// <returns>Returns the owners of a project</returns>

        public IQueryable<User> GetProjectOwners(int projectId)
        {
            if (projectId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var projectOwners =
                    from u in context.Users
                    join po in context.ProjectOwners on u.id equals po.user_id
                    where po.project_id == projectId
                    select u;

                return projectOwners;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

        }

        /// <summary>
        /// Get the tutors of a group
        /// </summary>
        /// <param name="groupId">The group you want to query</param>
        /// <returns>Returns tutors of a group </returns>
        public IQueryable<User> GetTutor(int groupId)
        {
            if (groupId >= 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var tutor =
                    from u in context.Users
                    join pg in context.ProjectGroups on u.id equals pg.tutor_user_id
                    where pg.id == groupId
                    select u;

                return tutor;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets the ID of an active project 
        /// </summary>
        /// <param name="studentNumber">The project of a student</param>
        /// <returns>Returns an ID from an active project </returns>
        public IQueryable<Project> GetCurrentActiveProject(int studentNumber)
        {
            if (studentNumber > 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var activeProject =
                    from p in context.Projects
                    join pg in context.ProjectGroups on p.id equals pg.project_id
                    join pm in context.ProjectMembers on pg.id equals pm.project_group_id
                    join u in context.Users on pm.student_user_id equals u.id
                    where u.student_number == studentNumber && p.status_name.Equals("Active")
                    select p;

                return activeProject;
            }
             else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        
        public IQueryable<Project> GetProjectsByOwner(int ownerId)
        {
            DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            var projects =
                from p in context.Projects
                join po in context.ProjectOwners on p.id equals po.project_id
                join usr in context.Users on po.user_id equals usr.id
                where usr.student_number == ownerId
                select p;
            return projects;
        }
        /// <summary>
        /// Gets all projects where a student participates
        /// </summary>
        /// <param name="studentNumber">The project of a student</param>
        /// <returns>Returns all projects where a student took part of. </returns>
        public IQueryable<Project> GetAllProjects(int studentNumber)
        {
            DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

            var AllProjects =
                from p in context.Projects
                join pg in context.ProjectGroups on p.id equals pg.project_id
                join pm in context.ProjectMembers on pg.id equals pm.project_group_id
                join u in context.Users on pm.student_user_id equals u.id
                where u.student_number == studentNumber
                select p;

            return AllProjects;

        }
    }
}
