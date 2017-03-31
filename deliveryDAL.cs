using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;


namespace mvc_shopping_mainproject.Models
{
    public class deliveryDAL
    {

         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

         public bool adddelivery(deliveryModel obj, string delmail, string delpwd, string delques, string delans)
        {

            SqlCommand com_adddelivery = new SqlCommand("insert delivery values(@name,@mobile,@email) ", con);
            com_adddelivery.Parameters.AddWithValue("@name", obj.DeliveryName);
            com_adddelivery.Parameters.AddWithValue("@mobile", obj.DeliveryMobile);
            com_adddelivery.Parameters.AddWithValue("@email", obj.DeliveryEmailId);
            con.Open();

            com_adddelivery.ExecuteNonQuery();
            SqlCommand com_deliveryid = new SqlCommand("select @@identity", con);


            int id = Convert.ToInt32(com_deliveryid.ExecuteScalar());
            obj.DeliveryIdNO = id;
            con.Close();

            MembershipCreateStatus statdel;
            Membership.CreateUser(obj.DeliveryEmailId, delpwd, delmail, delques, delans, true, out statdel);
            if (statdel == MembershipCreateStatus.Success)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        public bool CheckEmailId_delivery(string mailid)
        {
            SqlCommand com_checkmailid = new SqlCommand("Select count(*) from Delivery where deliveryemailid=@emailid", con);
            com_checkmailid.Parameters.AddWithValue("@emailid", mailid);
            con.Open();
            int count = Convert.ToInt32(com_checkmailid.ExecuteScalar());
            if (count == 0)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }


        }
    
    }
}