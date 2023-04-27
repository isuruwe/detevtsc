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
    public class getvts : IHttpHandler
    {
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        public void ProcessRequest(HttpContext context)
        {
            string devid = context.Request["p1"];
            string dt1 = context.Request["dt1"];
            string dt2 = context.Request["dt2"];
            string sqlQuery;
            DataSet oDataSet = new DataSet();
            oSqlConnection = new SqlConnection("Data Source =AFHQ-VTS; Initial Catalog = detevts; User ID = sa; Password = 123@com");
            oSqlCommand = new SqlCommand();
            sqlQuery = " select ds.locdata,ds.devid,ds.createddate,ds.spd,ds.dirr,ds.altt" +
 "from[detevts].[dbo].[devuser] as d"+
 "cross apply"+
     "(select top 1 locdata, devid, createddate,spd,dirr,altt" +
      "from[detevts].[dbo].[vtsdata]"+
     " where devid = d.devid and  d.uname='"+ devid + "'" +
      "order by createddate desc) as ds";
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