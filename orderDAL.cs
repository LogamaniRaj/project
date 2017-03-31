using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;


namespace mvc_shopping_mainproject.Models
{

    public class orderDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        public bool placeorder(orderModel obj)
        {
            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();



                int orderprice = obj.ProductQuantity * obj.ProductPrice;


                orderModel o = new orderModel();
                SqlCommand com_placeorder = new SqlCommand("insert orders values (@cemailid,@pid,@pname,@pprice,@qty,@oprice,@cname,@daddr,@city,@state,@pin,@mobno,getdate(),@rdb)", con);


                com_placeorder.Parameters.AddWithValue("@cemailid", obj.CustomerEmailId);
                com_placeorder.Parameters.AddWithValue("@pid", obj.ProductId);
                com_placeorder.Parameters.AddWithValue("@pname", obj.ProductName);
                com_placeorder.Parameters.AddWithValue("@pprice", obj.ProductPrice);
                com_placeorder.Parameters.AddWithValue("@qty", obj.ProductQuantity);
                com_placeorder.Parameters.AddWithValue("@oprice", orderprice);
                com_placeorder.Parameters.AddWithValue("@cname ", obj.CustomerName);

                com_placeorder.Parameters.AddWithValue("@daddr", obj.DeliveryAddress);
                com_placeorder.Parameters.AddWithValue("@city", obj.DeliveryCity);
                com_placeorder.Parameters.AddWithValue("@state", obj.State);
                com_placeorder.Parameters.AddWithValue("@pin", obj.PinCode);
                com_placeorder.Parameters.AddWithValue("@mobno", obj.MobileNo);
                com_placeorder.Parameters.AddWithValue("@rdb", obj.PaymentOption);

                com_placeorder.Transaction = tran;
                com_placeorder.ExecuteNonQuery();
                SqlCommand com_orderid = new SqlCommand("select @@identity", con);
               
                com_orderid.Transaction = tran;
                int oid = Convert.ToInt32(com_orderid.ExecuteScalar());
                obj.OrderID = oid;

                SqlCommand com_getavailableqty = new SqlCommand("select AvailableQty from products where productid=@pid", con);
                com_getavailableqty.Parameters.AddWithValue("@pid", obj.ProductId);
                com_getavailableqty.Transaction = tran;
                int available_qty = Convert.ToInt32(com_getavailableqty.ExecuteScalar());
                int aqty = available_qty - obj.ProductQuantity;

                SqlCommand com_update_qty = new SqlCommand("update  products set AvailableQty=@aqty  where productid=@pid ", con);
                com_update_qty.Parameters.AddWithValue("@aqty", aqty);
                com_update_qty.Parameters.AddWithValue("@pid", obj.ProductId);
                com_update_qty.Transaction =tran;
                com_update_qty.ExecuteScalar();




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

        public orderModel viewproducts(int pid,string cemailid)
        {
            SqlCommand com_viewprod = new SqlCommand("select productid,productname,productprice,availableqty from products where productid=@pid", con);
            com_viewprod.Parameters.AddWithValue("@pid", pid);
            con.Open();
            SqlDataReader dr = com_viewprod.ExecuteReader();
            orderModel o = new orderModel();
            if (dr.Read())
            {


                o.ProductId = dr.GetInt32(0);
                o.ProductName = dr.GetString(1);
                o.ProductPrice = dr.GetInt32(2);

              
                o.CustomerEmailId = cemailid;
                con.Close();
            }
            return o;
        }

        public bool check_stock(orderModel obj)
        {
            SqlCommand com_getavailableqty1 = new SqlCommand("select AvailableQty from products where productid=@pid", con);
            com_getavailableqty1.Parameters.AddWithValue("@pid",obj.ProductId);
            con.Open();
            int available_qty = Convert.ToInt32(com_getavailableqty1.ExecuteScalar());

            if (available_qty >= obj.ProductQuantity)
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

        public List< orderModel> myorderlist(string cemailid)
        { 
            List<orderModel> ord_list=new List<orderModel>();
            SqlCommand com_orderlist = new SqlCommand("select *,dbo.getcityname(Deliverycity),dbo.getstatename(state) from orders where CustomerEmailId=@cemailid", con);
            com_orderlist.Parameters.AddWithValue("@cemailid", cemailid);
            con.Open();
            SqlDataReader dr = com_orderlist.ExecuteReader();
            while (dr.Read())
            {
                orderModel o = new orderModel();
                o.OrderID = dr.GetInt32(0);
                o.CustomerEmailId = dr.GetString(1);
                o.ProductId = dr.GetInt32(2);
                o.ProductName = dr.GetString(3);
                o.ProductPrice = dr.GetInt32(4);
                o.ProductQuantity = dr.GetInt32(5);
                o.OrderPrice = dr.GetInt32(6);
                o.CustomerName = dr.GetString(7);
                o.DeliveryAddress = dr.GetString(8);
                o.DeliveryCity = dr.GetString(15);
                o.State = dr.GetString(16);
                o.PinCode = dr.GetString(11);
                o.MobileNo = dr.GetString(12);
                o.OrderDate = dr.GetDateTime(13);
                o.PaymentOption = dr.GetString(14);
                ord_list.Add(o);

            }
            con.Close();
            return ord_list;

        }


      
     public List< orderModel> deliveryorderslist()
        { 
            List<orderModel> ord_list=new List<orderModel>();
            SqlCommand com_orderlist = new SqlCommand("select *,dbo.getcityname(Deliverycity),dbo.getstatename(state) from orders where  orderid not in (select orderid from deliveryorders)", con);
            con.Open();
            SqlDataReader dr = com_orderlist.ExecuteReader();
            while (dr.Read())
            {
                orderModel o = new orderModel();
                o.OrderID = dr.GetInt32(0);
                o.CustomerEmailId = dr.GetString(1);
                o.ProductId = dr.GetInt32(2);
                o.ProductName = dr.GetString(3);
                o.ProductPrice = dr.GetInt32(4);
                o.ProductQuantity = dr.GetInt32(5);
                o.OrderPrice = dr.GetInt32(6);
                o.CustomerName = dr.GetString(7);
                o.DeliveryAddress = dr.GetString(8);
                o.DeliveryCity = dr.GetString(15);
                o.State = dr.GetString(16);
                o.PinCode = dr.GetString(11);
                o.MobileNo = dr.GetString(12);
                o.OrderDate = dr.GetDateTime(13);
                o.PaymentOption = dr.GetString(14);
                ord_list.Add(o);

            }
            con.Close();
            return ord_list;

        }

     public List<orderModel> Ordercompletedlist()
     {
         List<orderModel> ord_list = new List<orderModel>();
         SqlCommand com_orderlist = new SqlCommand("select *,dbo.getcityname(Deliverycity),dbo.getstatename(state) from orders where  orderid  in (select orderid from deliveryorders)", con);
         con.Open();
         SqlDataReader dr = com_orderlist.ExecuteReader();
         while (dr.Read())
         {
             orderModel o = new orderModel();
             o.OrderID = dr.GetInt32(0);
             o.CustomerEmailId = dr.GetString(1);
             o.ProductId = dr.GetInt32(2);
             o.ProductName = dr.GetString(3);
             o.ProductPrice = dr.GetInt32(4);
             o.ProductQuantity = dr.GetInt32(5);
             o.OrderPrice = dr.GetInt32(6);
             o.CustomerName = dr.GetString(7);
             o.DeliveryAddress = dr.GetString(8);
             o.DeliveryCity = dr.GetString(15);
             o.State = dr.GetString(16);
             o.PinCode = dr.GetString(11);
             o.MobileNo = dr.GetString(12);
             o.OrderDate = dr.GetDateTime(13);
             o.PaymentOption = dr.GetString(14);
             ord_list.Add(o);

         }
         con.Close();
         return ord_list;

     }

     public orderModel vieworderdetails(int oid)
     {
         SqlCommand com_vieworderdetails = new SqlCommand("select *,dbo.getcityname(Deliverycity),dbo.getstatename(state) from orders where orderid=@oid", con);
         com_vieworderdetails.Parameters.AddWithValue("@oid", oid);
         con.Open();
         SqlDataReader dr = com_vieworderdetails.ExecuteReader();
         orderModel o = new orderModel();
         if (dr.Read())
         {
             
             o.OrderID = dr.GetInt32(0);
             o.CustomerEmailId = dr.GetString(1);
             o.ProductId = dr.GetInt32(2);
             o.ProductName = dr.GetString(3);
             o.ProductPrice = dr.GetInt32(4);
             o.ProductQuantity = dr.GetInt32(5);
             o.OrderPrice = dr.GetInt32(6);
             o.CustomerName = dr.GetString(7);
             o.DeliveryAddress = dr.GetString(8);
             o.DeliveryCity = dr.GetString(15);
             o.State = dr.GetString(16);
             o.PinCode = dr.GetString(11);
             o.MobileNo = dr.GetString(12);
             o.OrderDate = dr.GetDateTime(13);
             o.PaymentOption = dr.GetString(14);
            

         }
         con.Close();
         return o;
     }
    
    
    
    }
}