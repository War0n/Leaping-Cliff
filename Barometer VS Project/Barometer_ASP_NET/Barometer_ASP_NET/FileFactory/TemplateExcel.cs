using BarometerDataAccesLayer.Database;
using BarometerDataAccesLayer;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.FileFactory
{
	public class TemplateExcel: IExcelDataTransfer
	{
        public TemplateExcel()
        {
            setTemplatePath("TemplateTemplate");
        }

        public override void Export(object identifier)
        {
            DAOTemplate templatedao = DatabaseFactory.getInstance().getDAOTemplate();
            IQueryable<BaroTemplate> templates = templatedao.getTemplate((int)identifier);
            BaroTemplate template = null;

            if (templates.Count() > 0)
            {
                template = templates.First();
                string newFileName = path + "Template-" + template.template_name + ".xlsx";
                CopyFile(TemplatePath, newFileName);
                document = SpreadsheetDocument.Open(newFileName, true);
                wbPart = document.WorkbookPart;
                Sheet sheet = wbPart.Workbook.Descendants<Sheet>().First();
                Worksheet ws = ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;

                string baroNameAdress = "B1";
                InsertCellInWorksheet(ws, baroNameAdress);
                UpdateValue("Blad1", baroNameAdress, template.template_name, 0, true);

                string ratingAdress = "D1";
                InsertCellInWorksheet(ws, ratingAdress);
                UpdateValue("Blad1", ratingAdress, template.rating_type.ToString(), 0, false);

                string anonymousAdress = "F1";
                InsertCellInWorksheet(ws, anonymousAdress);
                UpdateValue("Blad1", anonymousAdress, template.anonymous.ToString(), 0, false);

                int rowNumber = 3;
                foreach (BaroAspect aspect in template.BaroAspects)
                {
                    string idAdress = "A" + rowNumber;
                    InsertCellInWorksheet(ws, idAdress);
                    UpdateValue("Blad1", idAdress, aspect.id.ToString(), 0, false);

                    string parentIdAdress = "B" + rowNumber;
                    InsertCellInWorksheet(ws, idAdress);
                    UpdateValue("Blad1", parentIdAdress, aspect.BaroAspect1.id.ToString(), 0, false);

                    string headAspectAdress = "C" + rowNumber;
                    InsertCellInWorksheet(ws, headAspectAdress);
                    UpdateValue("Blad1", headAspectAdress, aspect.is_head_aspect.ToString(), 0, false);

                    string fillAdress = "D" + rowNumber;
                    InsertCellInWorksheet(ws, fillAdress);
                    UpdateValue("Blad1", fillAdress, aspect.can_be_filled.ToString(), 0, false);

                    string nameAdress = "E" + rowNumber;
                    InsertCellInWorksheet(ws, nameAdress);
                    UpdateValue("Blad1", nameAdress, aspect.name, 0, true);

                    string descrAdress = "F" + rowNumber;
                    InsertCellInWorksheet(ws, descrAdress);
                    UpdateValue("Blad1", descrAdress, aspect.desription, 0, true);
                    rowNumber++;
                }
            }
            else
            {
                //no templates found
                return; 
            }
        }

        public override void Import(Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}