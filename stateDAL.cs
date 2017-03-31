using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace mvc_shopping_mainproject.Models
{
    public class stateDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public List<stateModel> getstate()
        { 
            List<stateModel > state_list=new List<stateModel>();

            SqlCommand com_state = new SqlCommand("select * from state", con);
            con.Open();
            SqlDataReader dr = com_state.ExecuteReader();
            while (dr.Read())
            {
                stateModel obj = new stateModel();
                obj.stateid = dr.GetInt32(0);
                obj.statename = dr.GetString(1);
                state_list.Add(obj);

            }
            con.Close();
            return state_list;

        
        }
    }
}