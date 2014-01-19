using BarometerDataAccesLayer.Database;
using BarometerDataAccesLayer;
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
        private Project assignableProject;
        public StudentExcel(Project toAssign = null):base()
        {
            assignableProject = toAssign;
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
                DAOStudent studentdao = DatabaseFactory.getInstance().getDAOStudent();
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

                        string groupsString = "";
                        foreach (BarometerDataAccesLayer.ProjectGroup group in studentdao.getStudentGroup((int)student.student_number))
                        {
                            groupsString += group.group_code + ",";
                        }
                        string groupAdress = "E" + rowNumber;
                        InsertCellInWorksheet(ws, groupAdress);
                        UpdateValue("Blad1", groupAdress, groupsString, 0, true);

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
                        object[] values = new object[5];
                        int cellID = 0;
                        foreach (Cell c in row.Elements<Cell>())
                        {
                            if (cellID < 5)
                            {
                                value = GetCellValue(c);
                                emptyRow = emptyRow && string.IsNullOrWhiteSpace(value);
                                values[cellID] = value;
                            }
                            cellID++;
                        }
                        cellID = 0;
                        rowData.Add(rowID, values);
                        rowID++;
                    }

                    FillDatabase(rowData, assignableProject);
                }

            }
        }

        private void FillDatabase(Dictionary<int, object[]> rowData, Project insertProject)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            foreach (KeyValuePair<int, object[]> cell in rowData)
            {
                bool userExists = false;
                int studentNumber = int.Parse(cell.Value[0].ToString());
                if (cell.Key != 0)
                {
                    BarometerDataAccesLayer.User insertUser = null;
                    var existingUser =
                        from u in context.Users
                        where u.student_number == studentNumber
                        select u;

                    int test = 0;
                    if (!existingUser.Any())
                    {
                        //If doesnt exist, create empty one 
                        insertUser = new BarometerDataAccesLayer.User();
                    }
                    else
                    {
                        //If exists, get user
                        userExists = true;
                        insertUser = existingUser.First();
                    }

                    insertUser.student_number = int.Parse(cell.Value[0].ToString());
                    insertUser.firstname = cell.Value[1].ToString();
                    insertUser.lastname = cell.Value[2].ToString();
                    insertUser.email = cell.Value[3].ToString();

                    if (cell.Value[4].ToString() != "")
                    {
                        string[] groups = cell.Value[4].ToString().Split(',');
                        foreach (string groupcode in groups)
                        {
                            bool groupExists = false;
                            var currentGroup =
                                from pg in context.ProjectGroups
                                where pg.group_code == groupcode
                                select pg;

                            ProjectGroup newGroup = null;
                            if (currentGroup.Count() == 0)
                            {
                                //Groep bestaat nog niet, aanmaken
                                newGroup = new ProjectGroup();
                                newGroup.group_code = groupcode;
                            }
                            else
                            {
                                groupExists = true;
                                newGroup = currentGroup.First();
                            }
                            if (insertProject != null)
                            {
                                newGroup.Project = insertProject;
                            }

                            ProjectMember member = new ProjectMember();
                            member.student_user_id = insertUser.id;
                            member.ProjectGroup = newGroup;
                            if (!groupExists)
                            {
                                context.ProjectGroups.InsertOnSubmit(newGroup);
                            }
                            context.ProjectMembers.InsertOnSubmit(member);
                        }
                    }
                    if (!userExists)
                    {
                        context.Users.InsertOnSubmit(insertUser);
                    }
                }
            }
            context.SubmitChanges();
        }

        internal void AddGroupsToProject(System.IO.Stream stream, Project insertProject)
        {
            BarometerDataAccesLayer.DatabaseClassesDataContext context = DatabaseFactory.getInstance().getDataContext();
            using (SpreadsheetDocument spreadsheetDocument =
                SpreadsheetDocument.Open(stream, false))
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

                    List<string> groupsToAdd = new List<string>();

                    //assumes the first row contains column names 
                    foreach (Row row in wsPart.Worksheet.Descendants<Row>())
                    {
                        bool emptyRow = true;
                        string[] values = row.ElementAt(4).ToString().Split(',');
                        emptyRow = emptyRow && (values.Count() > 0);

                        foreach (string value in values)
                        {
                            if (!groupsToAdd.Contains(value))
                            {
                                groupsToAdd.Add(value);
                            }
                        }
                    }

                    foreach (string projectgroup in groupsToAdd)
                    {
                        var groupResult =
                            from pg in context.ProjectGroups
                            where pg.group_code == projectgroup
                            select pg;

                        BarometerDataAccesLayer.ProjectGroup group = groupResult.First();

                        group.Project = insertProject;
                    }


                }

            }
        }
    }
}
