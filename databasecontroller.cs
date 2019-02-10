using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Http;
using UtOpt.Model;
using UtOpt.Model.Data;
using UtOpt.Model.Model;

namespace UtOpt.Web.Controllers.Api
{
    [RoutePrefix("databaseinfo")]
    public class DatabaseInfoController : ApiController
    {
        private readonly EFRepository db;

        public DatabaseInfoController()
        {
            db = new EFRepository();
        }

        [HttpGet]
        [OverrideActionFilters]
        [ActionName("tables")]
        public async Task<IHttpActionResult> GetTablesList()
        {

            var _retVal = await db.Database.SqlQuery<DatabaseTable>("SELECT  OBJECT_ID(TABLE_NAME) AS ID, TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME,  TABLE_TYPE " +
                                                 " FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT IN('dtproperties', 'sysdiagrams')").ToListAsync();
            return Json(new { data = _retVal });
        }
        //dimos 29.1.2019
        [HttpGet]
        [OverrideActionFilters]
        [ActionName("getreportslist")]
        public async Task<IHttpActionResult> GetTablesListDistinct()
        {

            var _retVal = await db.Database.SqlQuery<aggregatedreports>("SELECT distinct category, report, sheet_report_name,rtrim(ltrim(convert(nvarchar(255),sheet))) as sheet, Stored_Path," +
                            "iif(len(generic1) > 0, rtrim(ltrim(substring(generic1, CHARINDEX('= ', generic1) + 3, len(generic1) - CHARINDEX('= ', generic1) - 3))), '') as generic1," +
                           "iif(len(generic2) > 0, rtrim(ltrim(substring(generic2, CHARINDEX('= ', generic2) + 3, len(generic2) - CHARINDEX('= ', generic2) - 3))), '') as generic2, " +
                        "iif(len(generic3) > 0, rtrim(ltrim(substring(generic3, CHARINDEX('= ', generic3) + 3, len(generic3) - CHARINDEX('= ', generic3) - 3))), '') as generic3" +

                        ", 'Draft' as status" +
                        " FROM[filler].[dbo].[reports_list]").ToListAsync();
            return Json(new { data = _retVal });
        }
        //dimos 29.1.2019 end
        //dimos 9.2.2019
        [HttpGet]
        [OverrideActionFilters]
        [ActionName("getreportssimplelist")]
        public async Task<IHttpActionResult> Getreports()
        {

            var _retVal = await db.Database.SqlQuery<aggregatedreports_simple>("select distinct report  FROM [filler].[dbo].[reports] where report <>''").ToListAsync();
            return Json(new { data = _retVal });
        }
        //dimos 9.2.2019 end
        [HttpPost]
        [OverrideActionFilters]
        [ActionName("tablecolumns")]
        public async Task<IHttpActionResult> PostTablesColumnsListAsync()
        {

            var _retVal = await db.Database.SqlQuery<DatabaseTableColumns>(" SELECT TABLE_SCHEMA,TABLE_NAME ,COLUMN_NAME ,COLUMN_DEFAULT ,DATA_TYPE ,CHARACTER_MAXIMUM_LENGTH  " +
                                                                    "  ,NUMERIC_PRECISION ,NUMERIC_PRECISION_RADIX , NUMERIC_SCALE FROM   INFORMATION_SCHEMA.COLUMNS " + 
                                                                    "  WHERE TABLE_NAME NOT IN('dtproperties', 'sysdiagrams')").ToListAsync();
            return Json(new { data = _retVal });
        }


        [HttpPost]
        [OverrideActionFilters]
        [ActionName("tablecolumnsbyid")]
        public async Task<IHttpActionResult> GetTablesColumnsList(string tableName)
        {

            var _retVal = await db.Database.SqlQuery<DatabaseTableColumns>(string.Format(" SELECT TABLE_SCHEMA,TABLE_NAME ,COLUMN_NAME ,COLUMN_DEFAULT ,DATA_TYPE ,CHARACTER_MAXIMUM_LENGTH  " +
                                                                    "  ,NUMERIC_PRECISION ,NUMERIC_PRECISION_RADIX , NUMERIC_SCALE FROM   INFORMATION_SCHEMA.COLUMNS " +
                                                                    "  WHERE  TABLE_NAME ='{0}'",tableName)).ToListAsync();
            return Json(new { data = _retVal });
        }


        [HttpPost]
        [OverrideActionFilters]
        [ActionName("field-value")]
        public async Task<IHttpActionResult> GetTablesColumnsList([FromBody]FieldValue fieldValue)
        {
            if (fieldValue == null)
                throw new ArgumentNullException(nameof(fieldValue));
        
            var _retVal = await db.Database.SqlQuery<DataBaseTableColumnValues>($"SELECT DISTINCT  TOP 1000  CONVERT(VARCHAR(max),{fieldValue.FieldName }) AS ColumnValue FROM [dbo].[{fieldValue.TableName}] WITH (NOLOCK);").ToListAsync();
            return Json(new { data = _retVal });
        }


        [HttpPost]
        [OverrideActionFilters]
        [ActionName("merge-query")]
        public IHttpActionResult PerformMergeQuery([FromBody]MergeObject @object)
        {
            if (@object == null)
                throw new ArgumentNullException(nameof(@object));

            List<string>[] loopkup = new List<string>[3];


            string query = $"SELECT DISTINCT t1.{@object.ColumnShow}, t2.{@object.ColumnMf} FROM  [dbo].[{@object.FileJoin}] t1 " +
                           $" RIGHT JOIN [dbo].[{@object.FileInit}] t2 ON t2.{@object.ColumnMf} = t1.{@object.ColumnJoin} " +
                           $" WHERE t1.{@object.ColumnShow} IS NOT NULL ORDER BY 1 ASC";

            var lookup_values = default(List<string>);
            var temp2 = default(List<string>);

            try
            {
                using (var con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["UtOptModel"].ConnectionString))
                {
                    lookup_values = new List<string>();
                    temp2 = new List<string>();

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = query;
                    using (var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (rdr.Read())
                        {
                            lookup_values.Add(rdr.GetValue(0).ToString());
                            temp2.Add(rdr.GetValue(1).ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
               // lookup_values.Add(string.Empty);
             //   temp2.Add(string.Empty);
            }

          //  loopkup[0] = lookup_values;
         //   loopkup[1] = lookup_values;
         //   loopkup[2] = temp2;

            //  var _retVal = await db.Database.SqlQuery<DataBaseTableColumnValues>($"SELECT DISTINCT  TOP 1000  CONVERT(VARCHAR(max),{fieldValue.FieldName }) AS ColumnValue FROM [dbo].[{fieldValue.TableName}] WITH (NOLOCK);").ToListAsync();


            //var query2 = $"SELECT * FROM dbo.reports WHERE generic IS NULL AND source_file= '{@object.FileJoin}' AND mf_column='{@object.ColumnMf}' " + 
            //             $" AND report='{@object.ReportJoin}' AND r_row='{@object.ReportRowJoin}' AND r_column='{@object.ReportColumnJoin}'";



            return Json(new { data = new LookupValues { Values = lookup_values } });
        }

    }
}
