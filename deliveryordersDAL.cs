using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;


namespace mvc_shopping_mainproject.Models
{
    public class deliveryordersDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public bool putdeliveryorders(int orderid,string status)
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                SqlCommand com_check = new SqlCommand("select count(*) from deliveryorders where orderid=@oid", con);
                com_check.Parameters.AddWithValue("@oid", orderid);
                com_check.Transaction = tran;
                int count = Convert.ToInt32(com_check.ExecuteScalar());
                if (count == 0)
                {

                    SqlCommand com_putdeliveryorders = new SqlCommand("insert deliveryorders values(@oid,getdate())", con);
                    com_putdeliveryorders.Parameters.AddWithValue("@oid", orderid);

                    com_putdeliveryorders.Transaction = tran;
                    com_putdeliveryorders.ExecuteNonQuery();

                    SqlCommand com_delorder_id = new SqlCommand("select @@identity", con);
                    com_delorder_id.Transaction = tran;
                    com_delorder_id.ExecuteScalar();



                    SqlCommand com_updateorder_status = new SqlCommand("update status set orderstatus=@status where orderstatid=@oid", con);
                    com_updateorder_status.Parameters.AddWithValue("@oid", orderid);
                    com_updateorder_status.Parameters.AddWithValue("@status", status);
                    com_updateorder_status.Transaction = tran;
                    com_updateorder_status.ExecuteNonQuery();
                    tran.Commit();


                   
                    return true;
                }
                else
                {
                 
                    return false;
                }
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }


            }

            

        }
    }
}