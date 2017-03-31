using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;


namespace mvc_shopping_mainproject.Models
{
    public class cartDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public bool addtocart(int pid,string cemailid)
        {
             SqlCommand con_check = new SqlCommand("select count(*) from cart where productid=@pid and customeremailid=@cemail", con);
             con_check.Parameters.AddWithValue("@pid", pid);
             con_check.Parameters.AddWithValue("@cemail", cemailid);
             con.Open();
             int count = Convert.ToInt32(con_check.ExecuteScalar());
             if (count == 0)
             {

                 SqlCommand com_addtocart = new SqlCommand("insert cart values (@cemailid,@pid,getdate())", con);
                 com_addtocart.Parameters.AddWithValue("@cemailid", cemailid);


                 com_addtocart.Parameters.AddWithValue("@pid", pid);
              
                 com_addtocart.ExecuteNonQuery();
                 con.Close();
                 return true;
             }
             else
             {
                 con.Close();
                 return false;
             }
        }


        public List<productModel> cartlist(string cemail)
        { 
           // SqlCommand com_checkcart=new SqlCommand("select prodic")

            List<productModel> cart_list = new List<productModel>();
            SqlCommand com_cart_list = new SqlCommand("select  productname,productprice,Productimageaddr,productid from products where productid in (select productid from cart where customeremailid=@cemailid) ", con);
            com_cart_list.Parameters.AddWithValue("@cemailid", cemail);
            con.Open();
            SqlDataReader dr = com_cart_list.ExecuteReader();
            while (dr.Read())
            {
                productModel obj = new productModel();
                
                obj.ProductName = dr.GetString(0);
                obj.ProductPrice = dr.GetInt32(1);
                obj.ProductImageAddr = dr.GetString(2);
                obj.ProductId = dr.GetInt32(3);
                cart_list.Add(obj);
            }
            con.Close();
            return cart_list;
            
        }

        public bool delfromcart(int pid,string cemailid)
        {
            SqlCommand com_delfrom = new SqlCommand("delete cart where productid=@pid and customeremailid=@cid ", con);
            com_delfrom.Parameters.AddWithValue("@pid", pid);
            com_delfrom.Parameters.AddWithValue("@cid", cemailid);
            con.Open();
            com_delfrom.ExecuteNonQuery();
            con.Close();
            return true;
        }
    }
}