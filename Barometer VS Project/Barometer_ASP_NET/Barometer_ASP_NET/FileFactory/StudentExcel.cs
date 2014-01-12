using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barometer_ASP_NET.FileFactory
{
    public class StudentExcel : IExcelDataTransfer
    {
        public StudentExcel()
        {
            setTemplatePath("StudentTemplate");
            string newFileName = path + "Students" + DateTime.Now.ToShortDateString() + ".xlsx";
            CopyFile(TemplatePath, newFileName);
            document = SpreadsheetDocument.Open(newFileName, true);
            wbPart = document.WorkbookPart;
        }

        public override void Export(object students)
        {
            Sheet sheet = wbPart.Workbook.Descendants<Sheet>().First();
            if (sheet != null)
            {
                Database.DAOStudent studentdao = Database.DatabaseFactory.getInstance().getDAOStudent();
                Worksheet ws = ((WorksheetPart)(wbPart.GetPartById(sheet.Id))).Worksheet;
                int rowNumber = 2;
                foreach (int studentnumber in (int[])students)
                {
                    BarometerDataAccesLayer.User student = studentdao.getStudentInfo(studentnumber);
                    if (student != null)
                    {
                        string numberAdress = "A" + rowNumber;
                        InsertCellInWorksheet(ws, numberAdress);
                        UpdateValue("Blad1", numberAdress, student.student_number.ToString(), 0, false);

                        string firstNameAdress = "B" + rowNumber;
                        InsertCellInWorksheet(ws, firstNameAdress);
                        UpdateValue("Blad1", firstNameAdress, student.firstname, 0, true);

                        string lastNameAdress = "C" + rowNumber;
                        InsertCellInWorksheet(ws, lastNameAdress);
                        UpdateValue("Blad1", lastNameAdress, student.lastname, 0, true);

                        string emailAdress = "D" + rowNumber;
                        InsertCellInWorksheet(ws, emailAdress);
                        UpdateValue("Blad1", emailAdress, student.email, 0, true);

                    }
                    else
                    {
                        string numberAdress = "A" + rowNumber;
                        InsertCellInWorksheet(ws, numberAdress);
                        UpdateValue("Blad1", numberAdress, studentnumber.ToString(), 0, false);

                        string lastNameAdress = "C" + rowNumber;
                        InsertCellInWorksheet(ws, lastNameAdress);
                        UpdateValue("Blad1", lastNameAdress, "Bestaat niet!", 0, true);
                    }
                    rowNumber++;
                }
                document.Close();
            }
        }

        public override void Import(System.IO.Stream fileStream)
        {
            using (SpreadsheetDocument spreadsheetDocument =
                SpreadsheetDocument.Open(fileStream, false))
            {
                WorkbookPart workBookPart = spreadsheetDocument.WorkbookPart;

                foreach (Sheet s in workBookPart.Workbook.Descendants<Sheet>())
                {
                    WorksheetPart wsPart = workBookPart.GetPartById(s.Id) as WorksheetPart;
                    System.Diagnostics.Debug.WriteLine("Worksheet {1}:{2} - id({0}) {3}", s.Id, s.SheetId, s.Name,
                        wsPart == null ? "NOT FOUND!" : "found.");

                    if (wsPart == null)
                    {
                        continue;
                    }

                    Row[] rows = wsPart.Worksheet.Descendants<Row>().ToArray();

                    Dictionary<int, object[]> rowData = new Dictionary<int, object[]>();

                    //assumes the first row contains column names 
                    int rowID = 0;
                    foreach (Row row in wsPart.Worksheet.Descendants<Row>())
                    {
                        bool emptyRow = true;
                        string value;
                        object[] values = new object[4];
                        int cellID = 0;
                        foreach (Cell c in row.Elements<Cell>())
                        {
                            value = GetCellValue(c);
                            emptyRow = emptyRow && string.IsNullOrWhiteSpace(value);
                            values[cellID] = value;
                            cellID++;
                        }
                        cellID = 0;
                        rowData.Add(rowID, values);
                        rowID++;
                    }
                     
                    FillDatabase(rowData);
                }

            }
        }

        private void FillDatabase(Dictionary<int, object[]> rowData)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = Database.DatabaseFactory.getInstance().getDataContext();
            foreach(KeyValuePair<int,object[]> cell in rowData)
            {
                if (cell.Key != 0)
                {
                    BarometerDataAccesLayer.User insertUser = new BarometerDataAccesLayer.User();
                    insertUser.student_number = int.Parse(cell.Value[0].ToString());
                    insertUser.firstname = cell.Value[1].ToString();
                    insertUser.lastname = cell.Value[2].ToString();
                    insertUser.email = cell.Value[3].ToString();
                    context.Users.InsertOnSubmit(insertUser);
                }
            }
            context.SubmitChanges();
        }
    }
}
