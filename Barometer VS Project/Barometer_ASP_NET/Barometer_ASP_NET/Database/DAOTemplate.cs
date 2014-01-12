using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.Database
{
    public class DAOTemplate 
    {
        /// <summary>
        /// Get all grades belonging to a student in a project
        /// </summary>
        /// <param name="studentNumber">The student you want to query</param>
        /// <param name="projectId">The project of which you like to see the grades</param>
        /// <returns></returns>
        public IQueryable<BaroTemplate> getTemplate(int templateNumber)
        {
            if (templateNumber > 0)
            {
                DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();

                var template =
                    from bt in context.BaroTemplates
                    join ba in context.BaroAspects on bt.id equals ba.baro_template_id
                    where bt.id == templateNumber
                    select bt;
                return template;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}