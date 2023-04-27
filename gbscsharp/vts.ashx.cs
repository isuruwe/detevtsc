using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace vtscsharp
{
    /// <summary>
    /// Summary description for gbs
    /// </summary>
    public class vts : IHttpHandler
    {
        SqlConnection oSqlConnection;
        SqlCommand oSqlCommand;
        SqlDataAdapter oSqlDataAdapter;
        public string sqlQuery;
        public void ProcessRequest(HttpContext context)
        {
            string devid = context.Request["p1"];
            string distan = context.Request["distan"];
            string tempi = context.Request["tempi"];
            string humi = context.Request["humi"];
            string altt = context.Request["alti"];
            string dirr = context.Request["dirr"];
            string spd = context.Request["spd"];
            devid = devid.Trim();
            string sqlQuery;
            DataSet oDataSet = new DataSet();
            oSqlConnection = new SqlConnection("Data Source =AFHQ-VTS; Initial Catalog = VTS; User ID = sa; Password = 123@com");
            oSqlCommand = new SqlCommand();
            sqlQuery = " INSERT INTO [messagein](sender,msg,receivedtime,msgtype,Speed,Angle) VALUES ('" + devid + "','" + distan + "', GETDATE(),'" + altt + "','" + spd + "','" + dirr + "') ";

            oSqlCommand.Connection = oSqlConnection;
            oSqlCommand.CommandText = sqlQuery;
            oSqlConnection.Open();
            oSqlCommand.ExecuteNonQuery();
            oSqlConnection.Close();
            context.Response.ContentType = "text/plain";
            context.Response.Write("Success\n" + distan + "\n" + tempi + "\n" + humi);
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