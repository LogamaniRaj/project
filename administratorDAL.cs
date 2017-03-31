using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
namespace mvc_shopping_mainproject.Models
{
    public class administratorDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public bool addadmin(administratorModel obj, string adminmail, string adminpwd, string adminques, string adminans)
        {

            SqlCommand com_addadmin = new SqlCommand("insert Administrator values(@name,@mobile,@email) ", con);
            com_addadmin.Parameters.AddWithValue("@name", obj.AdminName);
            com_addadmin.Parameters.AddWithValue("@mobile", obj.AdminMobile);
            com_addadmin.Parameters.AddWithValue("@email", obj.AdminEmailId);
            con.Open();

            com_addadmin.ExecuteNonQuery();
            SqlCommand com_adminid = new SqlCommand("select @@identity", con);


            int id = Convert.ToInt32(com_adminid.ExecuteScalar());
            obj.AdminId = id;
            con.Close();

            MembershipCreateStatus statadmin;
            Membership.CreateUser(obj.AdminEmailId, adminpwd, adminmail, adminques, adminans, true, out statadmin);
            if (statadmin == MembershipCreateStatus.Success)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        // public bool newcustomer(customerModel cust, string mail, string pwd, string ques, string ans)
        //{

        //    SqlCommand com_addcustomer = new SqlCommand("insert customers values(@name,@mobile,@email) ", con);
        //    com_addcustomer.Parameters.AddWithValue("@name", cust.CustomerName);
        //    com_addcustomer.Parameters.AddWithValue("@mobile", cust.CustomerMobile);
        //    com_addcustomer.Parameters.AddWithValue("@email",cust.CustomerEmailId);
            

        //    con.Open();
            
        //    com_addcustomer.ExecuteNonQuery();
        //    SqlCommand com_custid = new SqlCommand("select @@identity", con);


        //    int id = Convert.ToInt32(com_custid.ExecuteScalar());
        //    cust.CustomerId = id;
        //    con.Close();

        //    MembershipCreateStatus stat;
        //    Membership.CreateUser(cust.CustomerEmailId, pwd, mail, ques, ans, true, out stat);
        //    if (stat == MembershipCreateStatus.Success)
        //    {
        //        return true;
        //    }
        //    else
        //    {

        public bool checkemailid_admin(string mailid)
        {
            SqlCommand com_checkmailid = new SqlCommand("Select count(*) from Administrator where adminemailid=@emailid", con);
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