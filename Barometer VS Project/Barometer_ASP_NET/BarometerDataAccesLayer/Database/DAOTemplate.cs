﻿using BarometerDataAccesLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BarometerDataAccesLayer.Database
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
                if (template.ToList().Count > 0)
                {
                    return template;
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
    }
}