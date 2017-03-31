using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using mvc_shopping_mainproject.Models;

namespace mvc_shopping_mainproject.Controllers
{
    public class HomeController : Controller
    {


        [Authorize]
        public ActionResult Customerindex()
        {
            ViewBag.userstatus = User.Identity.Name;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();

        }
        [AllowAnonymous]
        [HttpPost]

        public ActionResult Login(string CustomerEmailId, string CustomerPassword, bool rememberme)
        {
            if (Membership.ValidateUser(CustomerEmailId, CustomerPassword))
            {
                FormsAuthentication.SetAuthCookie(CustomerEmailId, rememberme);
                ModelState.Clear();

                              
               
                return RedirectToAction("Customerindex");
            }
            else
            {
                ViewBag.msg1 = "Invalid Userid And Password";
                ModelState.Clear();
                return View();
            }

        }
        [AllowAnonymous]
        public ActionResult Newcustomer()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Newcustomer(customerModel cust, string CustomerEmailId, string CustomerPassword, string SecurityQuestion, string SecurityAnswer)
        {
            customerDAL dal = new customerDAL();



            if (dal.newcustomer(cust, CustomerEmailId, CustomerPassword, SecurityQuestion, SecurityAnswer))
            {

                return Json( "You are added scuccessfully and your Customer Id =" + cust.CustomerId);
                
            }
            else
            {

              return Json( "Failed!!Pls try again");
                
            }


        }

        [AllowAnonymous]
        public ActionResult CheckEmailId_customer(string CustomerEmailId)
        {
            customerDAL dal = new customerDAL();
            bool check = dal.checkemailid_customer(CustomerEmailId);
            if (check == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [AllowAnonymous]
        public ActionResult Adminlogin()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Adminlogin(string AdminEmailId, string AdminPassword, bool rememberme)
        {

            if (Membership.ValidateUser(AdminEmailId, AdminPassword))
            {
                FormsAuthentication.SetAuthCookie(AdminEmailId, rememberme);
                ViewBag.msg1 = "Login is successfull";
                ModelState.Clear();
                return RedirectToAction("Adminindex", "Home");
            }
            else
            {
                ViewBag.msg1 = "Invalid Userid And Password";
                ModelState.Clear();
                return View();
            }
        }


        [AllowAnonymous]
        public ActionResult Deliverylogin()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Deliverylogin(string DeliveryEmailId, string DeliveryPassword, bool rememberme)
        {

            if (Membership.ValidateUser(DeliveryEmailId, DeliveryPassword))
            {
                FormsAuthentication.SetAuthCookie(DeliveryEmailId, rememberme);
                ViewBag.msg1 = "Login is successfull";
                ModelState.Clear();
                return RedirectToAction("Deliveryindex", "Home");
            }
            else
            {
                ViewBag.msg1 = "Invalid Userid And Password";
                ModelState.Clear();
                return View();
            }
        }

        [Authorize]
        public ActionResult Deliveryindex()
        {
            return View();
        }

        [Authorize]
        public ActionResult Adminindex()
        {
            return View();
        }

        [Authorize]
        public ActionResult Addadmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult addadmin(administratorModel obj, string AdminEmailId, string AdminPassword, string SecurityQuestion, string SecurityAnswer)
        {
            administratorDAL dal = new administratorDAL();
            if (dal.addadmin(obj, AdminEmailId, AdminPassword, SecurityQuestion, SecurityAnswer))
            {

                return Json("You are added scuccessfully and your Admin Id =" + obj.AdminId);

            }
            else
            {

                return Json("Failed!!Pls try again");

            }

           
        
        }

        [Authorize]
        public ActionResult CheckEmailId_admin(string AdminEmailId)
        {
            administratorDAL dal = new administratorDAL();
            bool check = dal.checkemailid_admin(AdminEmailId);
            if (check == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        public ActionResult Addproducts()
        {
            List<SelectListItem> cat_list = new List<SelectListItem>();
            cat_list.Add(new SelectListItem { Text="Mobiles"});
            cat_list.Add(new SelectListItem { Text = "Camera" });
            cat_list.Add(new SelectListItem { Text = "Laptops" });
            cat_list.Add(new SelectListItem { Text = "Televisons" });

            ViewBag.catlist = cat_list;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Addproducts(productModel obj, HttpPostedFileBase prod_images)
        {
            List<SelectListItem> cat_list = new List<SelectListItem>();
            cat_list.Add(new SelectListItem { Text = "Mobiles" });
            cat_list.Add(new SelectListItem { Text = "Camera" });
            cat_list.Add(new SelectListItem { Text = "Laptops" });
            cat_list.Add(new SelectListItem { Text = "Televisons" });
            ViewBag.catlist = cat_list;
            productsDAL dal = new productsDAL();
            dal.addproducts(obj);
            prod_images.SaveAs(Server.MapPath (obj.ProductImageAddr));


            ViewBag.msg_addprod_status = "Product Added Succesfully and Productid is " + obj.ProductId;
             return View();
        }
        [Authorize]
        public ActionResult Modifyproducts(int pid)
        {
            List<SelectListItem> cat_list = new List<SelectListItem>();
            cat_list.Add(new SelectListItem { Text = "Mobiles" });
            cat_list.Add(new SelectListItem { Text = "Camera" });
            cat_list.Add(new SelectListItem { Text = "Laptops" });
            cat_list.Add(new SelectListItem { Text = "Televisons" });
            ViewBag.catlist = cat_list;
            productsDAL dal = new productsDAL();
          productModel obj=  dal.viewproducts(pid);

            return View(obj);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Modifyproducts(int pid,productModel obj)
        {
            List<SelectListItem> cat_list = new List<SelectListItem>();
            cat_list.Add(new SelectListItem { Text = "Mobiles" });
            cat_list.Add(new SelectListItem { Text = "Camera" });
            cat_list.Add(new SelectListItem { Text = "Laptops" });
            cat_list.Add(new SelectListItem { Text = "Televisons" });
            ViewBag.catlist = cat_list;
            productsDAL dal = new productsDAL();
            dal.Modifyproducts(obj, pid);
            //prod_images.SaveAs(Server.MapPath(obj.ProductImageAddr));

            ViewBag.msg_modify_status = "Product updated Succesfully ";
            return View();
        }


        [Authorize]


        public ActionResult Showproductslist_admin()
        {
            return View(); ;
        }
       
        [Authorize]
        public ActionResult Showproductslist_cust()
        {
            
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Showproductslist_cust( string  search_name)
        {
             productsDAL dal = new productsDAL();
            List<productModel> search_list = dal.Searchprod(search_name);
            if (search_list.Count != 0)
            {
                ModelState.Clear();
                return PartialView("PV_productslist_cust", search_list);
            }
            else
            {
                ModelState.Clear();
                return View("searchresult");

            }
        }
           
          
        


        public ActionResult Showbycategoryadmin(string catname)
        {
            //catname = "Mobiles";
            productsDAL dal = new productsDAL();
            List<productModel> prod_list = dal.getproductslist_bycat(catname);
            return PartialView("PV_productslist_admin", prod_list);

        }





        public ActionResult Showbycategory(string catname)
        {
            //catname = "Mobiles";
            productsDAL dal = new productsDAL();
            List<productModel> prod_list = dal.getproductslist_bycat(catname);
            return PartialView("PV_productslist_cust", prod_list);

        }


        //public ActionResult Searchprod(string search_name)
        //{
        //    productsDAL dal = new productsDAL();
        //    List<productModel> search_list = dal.Searchprod(search_name);
        //    return PartialView("PV_productslist_cust", search_list);
        //}

       
        public ActionResult ViewProduct(int pid)
        {
            productsDAL dal = new productsDAL();
           productModel obj= dal.viewproducts(pid);
           return View(obj);
           
            
        }

        

        public ActionResult Addtocart(int pid)
        {
            string cemailid = (User.Identity.Name).ToString();
            cartModel obj = new cartModel();
            cartDAL dal = new cartDAL();
            if (dal.addtocart(pid, cemailid))
            {
                ViewBag.addtocart_status = "Your Product is added to cart";
                productsDAL dal1 = new productsDAL();
                productModel obj1 = dal1.viewproducts(pid);
                return View("ViewProduct", "customer_layout", obj1);
            }
            else {

                ViewBag.addtocart_status = "Product is already added to cart";
                productsDAL dal1 = new productsDAL();
                productModel obj1 = dal1.viewproducts(pid);
                return View("ViewProduct", "customer_layout", obj1);
            }
        }

        public ActionResult Mycart(string cemailid)
        {
            cemailid = User.Identity.Name;
            cartDAL dal = new cartDAL();
            List<productModel> cart_list = dal.cartlist(cemailid);
            if (cart_list.Count!=0)
            {
                return View(cart_list);
            }
            else
            { return View("Cartstatus");
            }

        }
        
        [Authorize]
        public ActionResult Placeorder(int pid)
        {
          

            cityDAL dal1 = new cityDAL();
            List<SelectListItem> List_city = new List<SelectListItem>();
            List<cityModel> city = dal1.getcities();
            foreach (cityModel item in city)
            {
                List_city.Add(new SelectListItem { Text = item.cityname, Value =item.cityid.ToString() });
            }
            ViewBag.cities = List_city;

            stateDAL dal2 = new stateDAL();
            List<SelectListItem> list_state = new List<SelectListItem>();
            List<stateModel> state = dal2.getstate();
           
            foreach (stateModel item in state)
            {

                list_state.Add(new SelectListItem { Text = item.statename, Value = item.stateid.ToString() });
            }

            ViewBag.states = list_state;

            List<SelectListItem> qty_list = new List<SelectListItem>();
            qty_list.Add(new SelectListItem { Text = "1" });
            qty_list.Add(new SelectListItem { Text = "2" });
            qty_list.Add(new SelectListItem { Text = "3" });
            qty_list.Add(new SelectListItem { Text = "4" });
            qty_list.Add(new SelectListItem { Text = "5" });

            ViewBag.qtylist = qty_list;

            string cemailid = User.Identity.Name;
            orderDAL dal3 = new orderDAL();
            orderModel obj = dal3.viewproducts(pid,cemailid);

           
            return View(obj);



           
        }



        [AllowAnonymous]
        [HttpPost]
        public ActionResult Placeorder(orderModel obj)
        {
           

            cityDAL dal1 = new cityDAL();
            List<SelectListItem> List_city = new List<SelectListItem>();
            List<cityModel> city = dal1.getcities();
            foreach (cityModel item in city)
            {
                List_city.Add(new SelectListItem { Text = item.cityname, Value = item.cityid.ToString() });
            }
            ViewBag.cities = List_city;

            stateDAL dal2 = new stateDAL();
            List<SelectListItem> list_state = new List<SelectListItem>();
            List<stateModel> state = dal2.getstate();

            foreach (stateModel item in state)
            {

                list_state.Add(new SelectListItem { Text = item.statename, Value = item.stateid.ToString() });
            }

            ViewBag.states = list_state;
            List<SelectListItem> qty_list = new List<SelectListItem>();
            qty_list.Add(new SelectListItem { Text = "Mobiles" });
            qty_list.Add(new SelectListItem { Text = "Camera" });
            qty_list.Add(new SelectListItem { Text = "Laptops" });
            qty_list.Add(new SelectListItem { Text = "Televisons" });
            ViewBag.qtylist = qty_list;



           orderDAL dal = new orderDAL();
          statusDAL dal3 = new statusDAL();
            
           bool check = dal.check_stock(obj);
           if (check == true)
           {
               dal.placeorder(obj);
              
               ViewBag.check_stock_status1 = "Order is placed Successfully and your order id is:"+obj.OrderID;
               dal3.putorderstatus(obj.OrderID, "Your Order has been instantiated and it will be delivered soon");

               ModelState.Clear();
               return View("Placeorder", obj);
        

           }
           else
           {
               ModelState.Clear();
               ViewBag.check_stock_status = "Sorry Currently the product is Out of Stock";
               return View();
               
           }
        }

      

        public ActionResult Myorders()
        {
            string cemailid = User.Identity.Name;
            orderDAL dal = new orderDAL();
            List<orderModel> ordlist = dal.myorderlist(cemailid);
            if (ordlist.Count != 0)
            {
                return View(ordlist);
            }
            else
            {
                return View("check_for_orders");
            }
            
        }

        public ActionResult Deliveryorderslist()
        {
            orderDAL dal = new orderDAL();
            List<orderModel> ordlist = dal.deliveryorderslist();
            if (ordlist.Count != 0)
            {
                return View(ordlist);
            }
            else
            {
                return View("check_for_orders");
            }

        }
        public ActionResult Adminorderslist()
        {
            orderDAL dal = new orderDAL();
            List<orderModel> ordlist = dal.deliveryorderslist();
            if (ordlist.Count != 0)
            {
                return View(ordlist);
            }
            else
            {
                return View("check_for_orders");
            }

        }


        public ActionResult Ordercompleted(int orderid)
        {
            deliveryordersModel obj = new deliveryordersModel();
            deliveryordersDAL dal = new deliveryordersDAL();

            if (dal.putdeliveryorders(orderid, "Your order is delivered Successfully "))
            {
                ViewBag.orderdelivery_status = "The order Id " + orderid.ToString() + "  is delivered ";
                return View();


            }
            else {
                ViewBag.orderdelivery_status = "Sry pls Try again ";
                return View();
            
            }
        }

        public ActionResult Completedorders()
        {
            orderDAL dal = new orderDAL();
            List<orderModel> ordlist = dal.Ordercompletedlist();
            if (ordlist.Count != 0)
            {
                return View(ordlist);
            }
            else
            {
                return View("check_for_orders");
            }
            

        }
        public ActionResult Completedorders_admin()
        {
            orderDAL dal = new orderDAL();
            List<orderModel> ordlist = dal.Ordercompletedlist();
            if (ordlist.Count != 0)
            {
                return View(ordlist);
            }
            else
            {
                return View("check_for_orders");
            }

           
                
            

        }
        


        //[HttpPost]

        // public ActionResult Myorders(int orderid)
        //{
        //    string cemailid = User.Identity.Name;
        //    orderDAL dal = new orderDAL();
        //    List<orderModel> ordlist = dal.myorderlist(cemailid);


           
                                     
        //    //statusDAL dal1 = new statusDAL();
        //    //string stat = dal1.getorderstatus(orderid);
        //    //ViewBag.status_msg = stat;
        //    //statusModel obj = new statusModel();
        //    //obj.orderstatus = stat;
        //    //return PartialView("PV_orderstatus", obj);
      
            

        //}
        public ActionResult Myorderstatus(int orderid)
        {

            statusDAL dal1 = new statusDAL();
            string stat = dal1.getorderstatus(orderid);
            statusModel obj = new statusModel();
            obj.orderstatus = stat;
            return View(obj);
           // return PartialView("PV_orderstatus", obj);
      
      

        }
        public ActionResult Myorderdetails(int orderid)
        {
            orderDAL dal = new orderDAL();
            orderModel obj = dal.vieworderdetails(orderid);
            return View(obj);
        
        }
        public ActionResult Myorderdetails_admin(int orderid)
        {
            orderDAL dal = new orderDAL();
            orderModel obj = dal.vieworderdetails(orderid);
            return View(obj);
        
        }

        public ActionResult Myorderdetails_delivery(int orderid)
        {
            orderDAL dal = new orderDAL();
            orderModel obj = dal.vieworderdetails(orderid);
            return View(obj);
        
        }
       






        public ActionResult Logout() {
            return View();
        }

        [Authorize]
        public ActionResult Adddelivery()
        {
            return View();
        }
        [Authorize]
       [ HttpPost]
        public ActionResult Adddelivery(deliveryModel obj, string DeliveryEmailId, string DeliveryPassword, string SecurityQuestion, string SecurityAnswer)
        {
            deliveryDAL dal = new deliveryDAL();
            if (dal.adddelivery(obj, DeliveryEmailId, DeliveryPassword, SecurityQuestion, SecurityAnswer))
            {

                return Json("You are added scuccessfully and your delivery Id =" + obj.DeliveryIdNO);

            }
            else
            {

                return Json("Failed!!Pls try again");

            }
        
        }
        [Authorize]
       public ActionResult CheckEmailId_delivery(string DeliveryEmailId)
        {
            deliveryDAL dal = new deliveryDAL();
            bool check = dal.CheckEmailId_delivery(DeliveryEmailId);
            if (check == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult deletefromcart(int pid)
        {
            string cemailid = User.Identity.Name;
            cartDAL dal = new cartDAL();
            dal.delfromcart(pid, cemailid);
            return View();
        }
       
       
        
        
       
        















    }
}
