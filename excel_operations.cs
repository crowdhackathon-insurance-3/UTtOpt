using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.Odbc;


namespace UtOpt.Web.Controllers
{
    public class Excel_OperationsController : Controller
    {
        public class ope
        {
            public string openn { get; set; }


        }
        //[HttpPost]
        //[AllowAnonymous]
        public ActionResult sh(ope ope)
        {


            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            string myPath = ope.openn;
            var workbook = excelApp.Workbooks.Open(myPath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
            excelApp.Visible = true;




            return new JsonResult { Data = "dummy", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //[HttpPost]
        //[AllowAnonymous]
        public ActionResult xlfill()
        {
            List<string> reps = new List<string>();
            OdbcConnection DbConnection = new OdbcConnection("DSN=filler"); // get odbc
            DbConnection.Open(); //open odbc connection
            OdbcCommand DbCommand = DbConnection.CreateCommand();
            DbCommand.CommandText = "use filler;select distinct * report from reports;"; // query
            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            while (DbReader.Read())
            {
                reps.Add(DbReader.GetString(0));

            }


            DbReader.Close();
            DbCommand.Dispose();
            DbConnection.Close();




            return new JsonResult { Data = reps, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public class exl
        {
            public string path { get; set; }
            public string init { get; set; }


        }
        //[HttpPost]
        //[AllowAnonymous]

        public ActionResult xl(exl exl)
        {
            OdbcConnection DbConnection = new OdbcConnection("DSN=filler"); // get odbc
            DbConnection.Open(); //open odbc connection
            OdbcCommand DbCommand = DbConnection.CreateCommand();
            DbCommand.CommandText = "use filler;select excel_row,excel_column,query_final,sheet from reports_list where report in (" + exl.init + ");"; // query
            OdbcDataReader DbReader = DbCommand.ExecuteReader();


            List<string> row = new List<string>();
            List<string> column = new List<string>();
            List<string> query = new List<string>();
            List<string> report_sheet = new List<string>();


            while (DbReader.Read())
            {
                row.Add(DbReader.GetString(0));
                column.Add(DbReader.GetString(1));
                query.Add(DbReader.GetString(2));
                report_sheet.Add(DbReader.GetString(3));
            }


            DbReader.Close();
            DbCommand.Dispose();
            DbConnection.Close();


            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            string myPath = exl.path;
            var workbook = excelApp.Workbooks.Open(myPath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);

            string cell;

            for (int i = 0; i < row.Count(); i++)
            {
                DbConnection = new OdbcConnection("DSN=filler"); // get odbc
                DbConnection.Open(); //open odbc connection
                DbCommand = DbConnection.CreateCommand();
                DbCommand.CommandText = "use filler;" + query[i] + ";"; // query
                DbReader = DbCommand.ExecuteReader();
                while (DbReader.Read())
                {
                    //MessageBox.Show(report_sheet[i]);
                    Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets[report_sheet[i].Trim()];
                    cell = column[i] + row[i];
                    sheet.Range[cell].Value = DbReader.GetValue(0);

                }

                DbReader.Close();
                DbCommand.Dispose();
                DbConnection.Close();


            }
            String GetTimestamp(DateTime value)
            {
                return value.ToString("yyyyMMddHHmmss");
            };
            String timeStamp = GetTimestamp(DateTime.Now);
            workbook.SaveAs(myPath.Replace(".xlsx", "") + timeStamp + ".xlsx");
            workbook.Close();
            excelApp.Quit();



            return new JsonResult { Data = "Success", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        //[HttpPost]
        //[AllowAnonymous]

        public ActionResult op()
        {
            OdbcConnection DbConnection = new OdbcConnection("DSN=filler"); // get odbc
            DbConnection.Open(); //open odbc connection
            OdbcCommand DbCommand = DbConnection.CreateCommand();
            DbCommand.CommandText = "SELECT * FROM [filler].[dbo].[reports]"; // query
            OdbcDataReader DbReader = DbCommand.ExecuteReader();

            List<string> temp1 = new List<string>();
            List<string> temp2 = new List<string>();
            List<string> temp3 = new List<string>();
            List<string> temp4 = new List<string>();
            List<string> temp5 = new List<string>();
            List<string> temp6 = new List<string>();
            List<string> temp7 = new List<string>();
            List<string> temp8 = new List<string>();
            List<string> temp9 = new List<string>();
            List<string>[] loopkup_list = new List<string>[9];

            while (DbReader.Read())
            {
                temp1.Add(DbReader.GetString(0));
                temp2.Add(DbReader.GetString(1));
                temp3.Add(DbReader.GetString(2));
                temp4.Add(DbReader.GetString(3));
                temp5.Add(DbReader.GetString(4));
                temp6.Add(DbReader.GetString(5));
                temp7.Add(DbReader.GetString(6));
                if (DbReader.GetValue(7) != DBNull.Value)
                {
                    temp8.Add(DbReader.GetString(7));
                }
                else
                {
                    temp8.Add("");
                };
                if (DbReader.GetValue(8) != DBNull.Value)
                {
                    temp9.Add(DbReader.GetString(8));
                }
                else
                {
                    temp8.Add("");
                }


            }


            DbReader.Close();
            DbCommand.Dispose();
            DbConnection.Close();

            loopkup_list[0] = temp1;
            loopkup_list[1] = temp2;
            loopkup_list[2] = temp3;
            loopkup_list[3] = temp4;
            loopkup_list[4] = temp5;
            loopkup_list[5] = temp6;
            loopkup_list[6] = temp7;
            loopkup_list[7] = temp8;
            loopkup_list[8] = temp9;







            return new JsonResult { Data = loopkup_list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}