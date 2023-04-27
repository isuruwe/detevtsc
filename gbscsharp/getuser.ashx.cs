using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace vtscsharp
{
    /// <summary>
    /// Summary description for getgbs
    /// </summary>
    public class getuser : IHttpHandler
    {
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        public void ProcessRequest(HttpContext context)
        {
            //string devid = context.Request["p1"];
            string dt1 = context.Request["uname"];
            string dt2 = context.Request["pword"];
            string sqlQuery;
            DataSet oDataSet = new DataSet();
            oSqlConnection = new SqlConnection("Data Source =AFHQ-VTS; Initial Catalog = detevts; User ID = sa; Password = 123@com");
            oSqlCommand = new SqlCommand();
            sqlQuery = " SELECT TOP 1 devid FROM userdata where uname='" + dt1 + "' and pword='" + dt2 + "'  ORDER BY uid DESC ";
            // sqlQuery = " SELECT TOP 1 locdata FROM vtsdata where devid='" + devid + "' and  createddate between   '" + dt1 + "' and '" + dt2 + "'  ORDER BY vtsid DESC ";
            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlDataAdapter = new SqlDataAdapter(oSqlCommand);
            oSqlDataAdapter.Fill(oDataSet);
            oSqlConnection.Close();
            string json = JsonConvert.SerializeObject(oDataSet);
            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}