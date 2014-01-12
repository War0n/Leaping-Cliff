using BarometerDataAccesLayer;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.FileFactory
{
    public class GradeExcel : IExcelDataTransfer
    {
        public GradeExcel()
        {
            setTemplatePath("GradeTemplate");
        }

        int studentNumber;

        public override void Export(object studentNumberValue)
        {
            this.studentNumber = (int)studentNumberValue;
            Wrappers.UserDashboardWrapper userWrapper = new Wrappers.UserDashboardWrapper(studentNumber);
            Dictionary<string, int[]> gradesWithProjectname = userWrapper.Grades;
            string newFileName = path + studentNumber + ".xlsx";
            CopyFile(TemplatePath, newFileName);
            document = SpreadsheetDocument.Open(newFileName, true);
            wbPart = document.WorkbookPart;
            InsertValues(gradesWithProjectname);
        }

        private void InsertValues(Dictionary<string, int[]> grades)
        {
            Sheet sheet = wbPart.Workbook.Descendants<Sheet>().First();
            if (sheet != null)
            {
                Worksheet ws = ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;
                int rowCounter = 2;
                foreach (KeyValuePair<string, int[]> grade in grades)
                {
                    string studentAdress = "A" + rowCounter;
                    InsertCellInWorksheet(ws, studentAdress);
                    UpdateValue("Blad1", studentAdress,studentNumber.ToString(), 0, false);

                    string nameAdress = "B" + rowCounter;
                    InsertCellInWorksheet(ws, nameAdress);
                    UpdateValue("Blad1",nameAdress,grade.Key,0,true);

                    string indGradeAdress = "C" + rowCounter;
                    InsertCellInWorksheet(ws, indGradeAdress);
                    UpdateValue("Blad1",indGradeAdress,grade.Value[1].ToString(),0,false);

                    string groupGradeAdress = "D" + rowCounter;
                    InsertCellInWorksheet(ws, groupGradeAdress);
                    UpdateValue("Blad1",groupGradeAdress,grade.Value[0].ToString(),0,false);
                    rowCounter++;
                }
                document.Close();
            }
        }

        public override void Import(Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}