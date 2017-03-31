using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace mvc_shopping_mainproject.Models
{
    public class cityDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public List<cityModel> getcities()
        {

            List<cityModel> city_list = new List<cityModel>();
            SqlCommand com_citylist = new SqlCommand("select * from city", con);
            con.Open();
            SqlDataReader dr = com_citylist.ExecuteReader();
            while (dr.Read())
            {
                cityModel obj = new cityModel();
                obj.cityid = dr.GetInt32(0);
                obj.cityname = dr.GetString(1);
                city_list.Add(obj);

            }
            con.Close();
            return city_list;

        }
    }
}