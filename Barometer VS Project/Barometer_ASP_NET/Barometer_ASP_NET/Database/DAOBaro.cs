using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Database
{

    public class DAOBaro
    {
        private DatabaseClassesDataContext _db = DatabaseFactory.getInstance().getDataContext();


        public Project getProject(int project_id)
        {
            var project = (from p in _db.Projects
                           where (p.id == project_id)
                           select p).FirstOrDefault();
            return project;
        }



        /*
        /// <summary>
        /// Verkrijg de hoofd aspecten van een specifiek project
        /// </summary>
        /// <param name="project_id">project om te zien</param>
        /// <returns></returns>
        public IEnumerable<BaroAspect> getHeadAspects(int project_id)
        {
            if (project_id == 0)
                throw new ArgumentNullException("Geef project id mee");

            IEnumerable<BaroAspect> headAspects =
                (from ba in db.BaroAspects
                 join bt in db.BaroTemplates on ba.baro_template_id equals bt.id
                 join p in db.Projects on bt.id equals p.baro_template_id
                 where (p.id == project_id && ba.is_head_aspect == 1)
                 select ba).ToList();

            return headAspects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent_id"></param>
        /// <returns></returns>
        public IQueryable<Barometer_ASP_NET.Models.BaroGradeViewModel> getReports(int project_id, int reporter_id, int subject_id, int parent_id, int project_report_date_id)
        {
            var model = (from a in db.BaroAspects
                         from r in db.Reports
                        where (r.baro_aspect_id == a.id && a.parent_id == parent_id && r.reporter_id == reporter_id && r.subject_id == subject_id)
                         select new Barometer_ASP_NET.Models.BaroGradeViewModel
                         {
                            Aspect = a,
                            Report = r
                        });
            return model;


           /* var model =
                _db.Query<Restaurant>()
                   .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                   .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                   .Select(r => new RestaurantListViewModel
                   {
                       Id = r.Id,
                       Name = r.Name,
                       City = r.City,
                       Country = r.Country,
                       CountOfReviews = r.Reviews.Count()
                   }).ToPagedList(page, 10);*/


            /*var reports =
                (from r in db.Reports
                 where (r.reporter_id == reporter_id && 
                        r.subject_id == subject_id && 
                        r.ProjectReportDate.id == project_report_date_id &&
                        r.ProjectReportDate.project_id_int == project_id)
                 select r);
            //return reports;
        }

        /// <summary>
        /// Verkrijg de beoordelingsmomenten voor een specifiek project
        /// </summary>
        /// <param name="project_id">project</param>
        /// <returns></returns>
        public IEnumerable<ProjectReportDate> getReportDates(int project_id)
        {
            if (project_id == 0)
                throw new ArgumentNullException("Geef project id mee");

            IEnumerable<ProjectReportDate> reportDates =
                (from pr in db.ProjectReportDates
                 where (pr.Project.id == project_id)
                 select pr).ToList();

            return reportDates;
        }

        /// <summary>
        /// Verkrijg een project
        /// </summary>
        /// <param name="project_id"></param>
        /// <returns></returns>
        public Project getProject(int project_id)
        {
            if (project_id == 0)
                throw new ArgumentException("Geef project id mee");

            return db.Projects.Where(r => r.id == project_id).FirstOrDefault();
        }

        /// <summary>
        /// Verkrijg een user. Deze method zal vast wel een duplicate zijn
        /// maar denk doe gewoon gek
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public User getUser(int user_id)
        {
            if (user_id == 0)
                throw new ArgumentException("Geef user id mee");

            return db.Users.Where(r => r.id == user_id).FirstOrDefault();
        }*/
    }
}