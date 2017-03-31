using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;

using System.Web.Security;

namespace mvc_shopping_mainproject.Models
{
    public class statusDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public bool putorderstatus(int orderid, string status)
        {
            SqlCommand com_putorderstatus = new SqlCommand("insert status  values(@oid,@status) ", con);
            com_putorderstatus.Parameters.AddWithValue("@oid", orderid);
            com_putorderstatus.Parameters.AddWithValue("@status", status);
            con.Open();
            com_putorderstatus.ExecuteNonQuery();
            con.Close();
            return true;

        }
        public string getorderstatus(int orderid)
        {
            SqlCommand com_getorderstatus = new SqlCommand("select orderstatus from status where orderstatid=@oid", con);
            com_getorderstatus.Parameters.AddWithValue("@oid", orderid);

            con.Open();

            string status = com_getorderstatus.ExecuteScalar().ToString();
            con.Close();
            return status;
          
            

        }
    }
}