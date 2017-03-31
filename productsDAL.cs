using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
namespace mvc_shopping_mainproject.Models
{
    public class productsDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
       

        public bool addproducts(productModel obj)
        {
            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                SqlCommand com_addproducts = new SqlCommand("insert products values (@cname,@pname,@pprice,@pdesc,null,@qty)", con);
                com_addproducts.Parameters.AddWithValue("@cname", obj.CategoryName);
                com_addproducts.Parameters.AddWithValue("@pname", obj.ProductName);
                com_addproducts.Parameters.AddWithValue("@pprice", obj.ProductPrice);
                com_addproducts.Parameters.AddWithValue("@pdesc", obj.ProductDescription);
                com_addproducts.Parameters.AddWithValue("@qty", obj.AvailableQty);
               
                

                com_addproducts.Transaction = tran;
                com_addproducts.ExecuteNonQuery();
                SqlCommand Com_prodid = new SqlCommand("select @@identity", con);
                Com_prodid.Transaction = tran;
                int pid = Convert.ToInt32(Com_prodid.ExecuteScalar());
                obj.ProductId = pid;
                obj.ProductImageAddr = "/Product_images/" + pid + ".jpg";

                SqlCommand com_prodimage = new SqlCommand("update products set productimageaddr=@imgaddr where productid=@pid", con);
                com_prodimage.Parameters.AddWithValue("@imgaddr", obj.ProductImageAddr);
                com_prodimage.Parameters.AddWithValue("@pid", obj.ProductId);
                com_prodimage.Transaction = tran;
                com_prodimage.ExecuteNonQuery();
                
                tran.Commit();
                return true;
            }
            finally 
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

            }




        }
        public List<productModel> getproductslist()
        {
         
                    List<productModel> prod_list=new List<productModel>();
                    SqlCommand com_prod_list=new SqlCommand("select * from products",con);

                    con.Open();                   
                    
                            SqlDataReader dr= com_prod_list.ExecuteReader();
                        while(dr.Read())
                        {
                        productModel obj=new productModel();
                            obj.CategoryName=dr.GetString(0);
                            obj.ProductId=dr.GetInt32(1);
                            obj.ProductName=dr.GetString(2);
                            obj.ProductPrice=dr.GetInt32(3);
                            obj.ProductDescription=dr.GetString(4);
                            obj.ProductImageAddr=dr.GetString(5);
                            obj.AvailableQty = dr.GetInt32(6);
                            prod_list.Add(obj);
                            
                                        
                        }
                        
                        con.Close();                        
                       
                        return prod_list;
            }

        public List<productModel> getproductslist_bycat(string catname)
        {

            List<productModel> prod_list_cat = new List<productModel>();
            SqlCommand com_prod_list = new SqlCommand("select * from products where categoryname=@catname", con);
            com_prod_list.Parameters.AddWithValue("@catname", catname);
            con.Open();

            SqlDataReader dr = com_prod_list.ExecuteReader();
            while (dr.Read())
            {
                productModel obj = new productModel();
                obj.CategoryName = dr.GetString(0);
                obj.ProductId = dr.GetInt32(1);
                obj.ProductName = dr.GetString(2);
                obj.ProductPrice = dr.GetInt32(3);
                obj.ProductDescription = dr.GetString(4);
                obj.ProductImageAddr = dr.GetString(5);
                obj.AvailableQty = dr.GetInt32(6);
                prod_list_cat.Add(obj);


            }

            con.Close();

            return prod_list_cat;
        }

        public productModel viewproducts(int pid)
        {
            SqlCommand com_viewprod = new SqlCommand("select * from products where productid=@pid", con);
            com_viewprod.Parameters.AddWithValue("@pid", pid);
            con.Open();
            SqlDataReader dr = com_viewprod.ExecuteReader();
            productModel p = new productModel();
            if(dr.Read())

                {
                   
                    p.CategoryName = dr.GetString(0);
                    p.ProductId = dr.GetInt32(1);
                    p.ProductName = dr.GetString(2);
                    p.ProductPrice = dr.GetInt32(3);
                    p.ProductDescription = dr.GetString(4);
                    p.ProductImageAddr = dr.GetString(5);
                    p.AvailableQty = dr.GetInt32(6);
                    con.Close();
                  }
            return p;
            
            

        }
        public bool Modifyproducts(productModel obj,int pid)
        {
            SqlCommand com_modifyprod = new SqlCommand("update products set productname=@pname,categoryname=@cname,productprice=@pprice,productdescription=@pdesc,AvailableQty=@aqty where productid=@pid",con);
            com_modifyprod.Parameters.AddWithValue("@pname", obj.ProductName);
            com_modifyprod.Parameters.AddWithValue("@pid", pid);
            com_modifyprod.Parameters.AddWithValue("@pprice", obj.ProductPrice);
            com_modifyprod.Parameters.AddWithValue("@cname", obj.CategoryName);
            com_modifyprod.Parameters.AddWithValue("@pdesc", obj.ProductDescription);
            //obj.ProductImageAddr = "/Product_images/" + pid + ".jpg";
            //com_modifyprod.Parameters.AddWithValue("pimageaddr", obj.ProductImageAddr);
            com_modifyprod.Parameters.AddWithValue("@aqty", obj.AvailableQty);
            con.Open();
            com_modifyprod.ExecuteNonQuery();
            con.Close();
            return true;
        }

        public List<productModel> Searchprod(string prodname)
        {
            List<productModel> search_list = new List<productModel>();
            SqlCommand com_searchprod = new SqlCommand("select * from products where productname like '%" + prodname + "%' or categoryname like '%" + prodname + "%' ", con);
       
            con.Open();
            SqlDataReader dr = com_searchprod.ExecuteReader();
            while (dr.Read())
            {
                productModel obj = new productModel();
                obj.CategoryName = dr.GetString(0);
                obj.ProductId = dr.GetInt32(1);
                obj.ProductName = dr.GetString(2);
                obj.ProductPrice = dr.GetInt32(3);
                obj.ProductDescription = dr.GetString(4);
                obj.ProductImageAddr = dr.GetString(5);
                obj.AvailableQty = dr.GetInt32(6);
               search_list.Add(obj);


            }
            con.Close();
            return search_list;
        
        }
        

        

    }
}