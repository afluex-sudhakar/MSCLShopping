using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSCLShopping.Filter;
using MSCLShopping.Models;
using static MSCLShopping.Models.Home;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Net.Mail;

namespace MSCLShopping.Controllers
{
    public class HomeController : Controller
    {

        #region VendorRegistration
        public ActionResult SendVendorOTP(string mob, string FirstName)
        {
            Vendor model = new Vendor();
            try
            {
                #region CheckMobileNo
                Customer obj = new Customer();
                obj.Contact = mob;
                DataSet ds = obj.CheckMobileNo();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Result = "Mobile Number already exists.";
                }
                #endregion CheckMobileNo
                else
                {
                    Random rnd = new Random();
                    int otp = rnd.Next(111111, 999999);
                    string strOTP = "Dear " + FirstName + ", Your OTP for Registration is :" + otp;
                    BLSMS.SendSMSNew(mob, strOTP);
                    model.OTP = otp.ToString();
                    model.Result = "1";
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidatePincode(string pincode)
        {
            Common model = new Common();
            try
            {
                model.Pincode = pincode;
                DataSet ds = model.GetStateCity();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Result = "1";
                }
                else
                {
                    model.Result = "0";
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Common model = new Common();
                model.Pincode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
                    model.Result = "no";
                }

                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult VendorRegistration()
        {
            return View();
        }

        [HttpPost]
        [ActionName("VendorRegistration")]
        [OnAction(ButtonName = "btnRegisterVendor")]
        public ActionResult VendorRegistrationAction(Vendor model)
        {
            try
            {
                model.AddedBy = "1";
                DataSet ds = model.SaveVendor();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        ViewBag.VendorName = model.DisplayName;
                        try
                        {
                            #region SendEmailForVerification
                            SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                            mailServer.EnableSsl = true;
                            mailServer.Credentials = new System.Net.NetworkCredential("info@afluex.com", "7985207220");

                            string mobencrypt = Crypto.Encrypt(model.Mobile);
                            string body = "<div style = 'width:100%;background:#fff;font-size:12px;font-family:Verdana,Geneva,sans-serif' ><table style = 'width:640px;border:none;font-size:12px;margin:0 auto' cellpadding = '0' cellspacing = '0'><tbody><tr><td><div style = 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:15px 0'><div style = 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:0px 0'><div><a><img style = 'width:180px' src = '/Websitecss/img/logo.png' ></a></div><h1 style = 'color:#fff;font-weight:normal'></h1></div></div></td></tr> "
                            + "<tr><td><div style='background:#fff;vertical-align:top;padding:1px 0;border-bottom-right-radius:5px;border-bottom-left-radius:5px;border-left:1px solid #ddd;border-right:1px solid #ddd'><h4 style = 'font-size:14px;padding:10px 8px'>"
                            + "Dear " + model.DisplayName + ",<br/> Welcome to the seller club of Afluex Shopping.<br/>Please"
                            + "<a href = '/Home/VerifyEmail?q=" + mobencrypt + "' target= '_blank'>Click Here</a> to verify your EmailId.</h4></div></td></tr><tr><td><div style = 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:1px 0'>"
                            + "<div style= 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:0px 0'><div></div><h1 style= 'color:#fff;font-weight:normal'> Afluex Shopping</h1>"
                            + "<h4 style = 'color:#fff;font-weight:normal'> 'Shopping and Income at one place' </h4><h4 style = 'color:white'><i class='fa fa-volume-control-phone' aria-hidden='true'></i> &nbsp; Phone No : <i>+91 7985207220</i><i class='fa fa-envelope-o' aria-hidden='true'></i>&nbsp; Email : <i> info@afluex.com</i></h4>"
                            + "<h4 style = 'color:white'><i class='fa fa-volume-control-phone' aria-hidden='true'></i> &nbsp; Website : <i>http://www.MSCLShopping.com/</i></h4>"
                            + "</div></div></td></tr><tr><td><p style='color:#888;font-size:11px;margin-bottom:20px'>© Copyright 2019 All Rights Reserved</p></td></tr></tbody></table></div>";

                            MailMessage myMail = new MailMessage();
                            myMail.Subject = "Verification Email from Afluex Shopping";
                            myMail.Body = body;
                            myMail.IsBodyHtml = true;
                            myMail.From = new MailAddress("info@afluex.com", "Afluex Shopping");
                            myMail.To.Add(model.Email);

                            mailServer.Send(myMail);
                            #endregion
                        }

                        catch { }
                        string strOTP = "Dear " + model.DisplayName + ", Your LoginId for Registration is :" + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and Password is :" + ds.Tables[0].Rows[0]["Password"].ToString();
                        BLSMS.SendSMSNew(model.Mobile, strOTP);

                        Session["Email"] = model.Email;
                        Session["VendorName"] = model.DisplayName;
                        return RedirectToAction("Success");
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult VerifyEmail(string q)
        {
            Vendor model = new Vendor();
            try
            {
                model.Mobile = Crypto.Decrypt(q);
                DataSet ds = model.VerifyVendorEmail();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        ViewBag.Email = "Verified";
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        ViewBag.Email = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Email = ex.Message;
            }
            return RedirectToAction("Success");
        }

        #endregion

        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    //{
                    //    TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    //}

                    if (ds.Tables[0].Rows[0]["UserType"].ToString() != "Vendor")
                    {
                        //Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["PK_UserID"].ToString();
                        Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Fullname"].ToString();
                        Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();

                        FormName = "DashBoard";
                        Controller = "Admin";

                    }

                    else
                    {
                        TempData["Login"] = "Incorrect Login ID Or Password";
                        FormName = "Login";
                        Controller = "Home";
                    }
                }
                else
                {
                    TempData["Login"] = "Incorrect Login ID Or Password";
                    FormName = "Login";
                    Controller = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = "Incorrect Login ID Or Password";
                FormName = "Login";
                Controller = "Home";
            }

            return RedirectToAction(FormName, Controller);
        }



        public ActionResult LoginVendor(string LoginID, string Password)
        {
            Home Modal = new Home();
            try
            {

                Modal.LoginId = LoginID;
                Modal.Password = Password;
                DataSet ds = Modal.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {


                        if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Vendor")
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_VendorId"] = ds.Tables[0].Rows[0]["PK_UserID"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Fullname"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                            Session["IsDocumentVerified"] = ds.Tables[0].Rows[0]["IsDocumentVerified"].ToString();
                            Modal.Response = "1";
                        }
                        else
                        {
                            Modal.Response = "Incorrect Login ID Or Password";

                        }
                    }
                    else
                    {
                        Modal.Response = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    Modal.Response = "Incorrect Login ID Or Password";

                }
            }
            catch (Exception ex)
            {
                Modal.Response = ex.Message;

            }

            return Json(Modal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(Customer model, string LoginID, string Password)
        {
            Session["_Menu"] = null;
            #region GetDataFromCookies
            HttpCookie loginCookie = Request.Cookies["Login"];
            if (loginCookie != null)
            {
                Session["CustomerName"] = loginCookie.Values["CustomerName"];
                Session["CustomerID"] = loginCookie.Values["PK_CustomerID"];
                Session["Contact"] = loginCookie.Values["Contact"];
                Session["CustomerCode"] = loginCookie.Values["CustomerCode"];
                Session["Password"] = Crypto.Decrypt(loginCookie.Values["Password"]);
                Session["Fk_AssociateId"] = loginCookie.Values["Fk_AssociateId"];
                Session["TemPermanent"] = loginCookie.Values["TemPermanent"];
                Session["EmailId"] = loginCookie.Values["EmailId"];
            }
            #endregion GetDataFromCookies
            List<Customer> lstfeatureproduct = new List<Customer>();
            List<Customer> lstfeaturetypr = new List<Customer>();
            try
            {
                #region bindTrendingProducts
                List<Customer> lstTrendingProducts = new List<Customer>();
                DataSet dsTrending = model.BindTrendingProducts();

                if (dsTrending != null && dsTrending.Tables.Count > 0 && dsTrending.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsTrending.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.ProductID = Crypto.Encrypt(r["Pk_ProductId"].ToString());
                        obj.ColorID = r["PK_ColorID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.ShortDescription = r["ShortDescription"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.OfferedPrice = r["OfferedPrice"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.PrimaryImage = r["Images"].ToString();
                        obj.SoldOutCss = r["SoldOutCss"].ToString();
                        obj.RedeemPrice = r["RedeemPrice"].ToString();
                        lstTrendingProducts.Add(obj);
                    }
                    model.lstStorage = lstTrendingProducts;
                }
                #endregion


                #region Bind Brand
                List<Customer> lstBrand = new List<Customer>();
                DataSet dsbrand = model.BindBrand();
                if (dsbrand != null && dsbrand.Tables.Count > 0 && dsbrand.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsbrand.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.BrandImage = r["BrandImage"].ToString();

                        lstBrand.Add(obj);
                    }
                    model.lstBrand = lstBrand;
                }

                #endregion Bind Brand

                #region Bind Banner
                List<Customer> lstBanner = new List<Customer>();
                DataSet dsBanner = model.BindBanner();
                if (dsBanner != null && dsBanner.Tables.Count > 0 && dsBanner.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBanner.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.BannerName = r["BannerName"].ToString();
                        obj.BannerImage = r["BannerImage"].ToString();
                        obj.Description = r["Description"].ToString();

                        lstBanner.Add(obj);
                    }
                    model.lstBanner = lstBanner;
                }
                #endregion Bind Banner

                #region Bind Offer 
                List<Customer> lstoffer = new List<Customer>();
                DataSet dsoffer = model.BindOffer();
                if (dsoffer != null && dsoffer.Tables.Count > 0 && dsoffer.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsoffer.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.OfferName = r["OfferName"].ToString();
                        obj.OfferImage = r["OfferImage"].ToString();
                        obj.OfferDiscountPerc = r["DiscountPercentage"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_OfferID"].ToString());
                        lstoffer.Add(obj);
                    }
                    model.lstoffer = lstoffer;
                }
                #endregion Bind Offer
                DataSet ds = new DataSet();

                #region Bind Seller review
                List<Customer> lstseller = new List<Customer>();

                ds = model.GetSellerReview();


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();

                        obj.Comment = r["Review"].ToString();
                        obj.DisplayName = r["Name"].ToString();

                        lstseller.Add(obj);
                    }
                    model.lstseller = lstseller;
                }
                #endregion Bind Seller review

                #region Bind Offer Products
                List<Customer> lstofferproduct = new List<Customer>();

                ds = model.OfferProductList();


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();

                        obj.ProductID = Crypto.Encrypt(r["FK_ProductID"].ToString());
                        obj.ProductName = r["ProductName"].ToString();

                        obj.MRP = r["MRP"].ToString();

                        obj.PrimaryImage = r["Images"].ToString();
                        obj.OfferedPrice = r["OfferedPrice"].ToString();

                        lstofferproduct.Add(obj);
                    }
                    model.lstofferproduct = lstofferproduct;
                }
                #endregion Bind Offer Products

                #region BindFeatureType
                if (dsTrending != null && dsTrending.Tables.Count > 0 && dsTrending.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow r in dsTrending.Tables[2].Rows)
                    {
                        Customer obj = new Customer();

                        obj.DisplayName = r["Name"].ToString();
                        lstfeaturetypr.Add(obj);
                    }
                    model.lstfeaturetypr = lstfeaturetypr;
                }
                #endregion BindFeatureType

                #region BindFeatureProduct
                if (dsTrending != null && dsTrending.Tables.Count > 0 && dsTrending.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in dsTrending.Tables[1].Rows)
                    {
                        Customer obj = new Customer();
                        obj.ProductID = Crypto.Encrypt(r["Pk_ProductId"].ToString());
                        obj.ColorID = Crypto.Encrypt(r["PK_ColorID"].ToString());
                        obj.ProductName = r["ProductName"].ToString();
                        obj.ShortDescription = r["ShortDescription"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.OfferedPrice = r["OfferedPrice"].ToString();
                        obj.DisplayName = r["FeatureName"].ToString();
                        obj.PrimaryImage = r["Images"].ToString();
                        obj.SoldOutCss = r["SoldOutCss"].ToString();
                        obj.RedeemPrice = r["RedeemPrice"].ToString();
                        lstfeatureproduct.Add(obj);
                    }
                    model.lstfeatureproduct = lstfeatureproduct;
                }
                #endregion BindFeatureProduct

                #region BindFeatureProduct
                List<Customer> lstDashboardBanner = new List<Customer>();
                DataSet dsDbBann = model.BindDashBannerImage();
                if (dsDbBann != null && dsDbBann.Tables.Count > 0 && dsDbBann.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsDbBann.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.BannerImage = r["DashBannerImage"].ToString();

                        lstDashboardBanner.Add(obj);
                    }
                    model.lstDashboardBanner = lstDashboardBanner;
                }
                #endregion BindFeatureProduct

                #region BindTotalProductItems
                List<Customer> lstTotalProductItems = new List<Customer>();
                DataSet dsproducts = model.TotalProductItems();
                if (dsproducts != null && dsproducts.Tables.Count > 0 && dsproducts.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsproducts.Tables[0].Rows)
                    {
                        Customer objproducts = new Customer();
                        objproducts.TotalProduct = r["TotalProduct"].ToString();
                        objproducts.MainCategoryName = r["MainCategoryName"].ToString();
                        objproducts.MainCategoryImages = r["Images"].ToString();

                        lstTotalProductItems.Add(objproducts);
                    }
                    model.lstTotalProductItems = lstTotalProductItems;
                }
                #endregion BindTotalProductItems

            }
            catch
            {

            }
            return View(model);
        }
        public virtual PartialViewResult ShoppingMenu()
        {
            Home Menu = null;

            if (Session["_MenuAdmin"] != null)
            {
                Menu = (Home)Session["_MenuAdmin"];
            }
            else
            {

                Menu = Home.GetMenusShopping(Session["Pk_AdminId"].ToString(), Session["UserType"].ToString()); // pass employee id here
                Session["_MenuAdmin"] = Menu;
            }
            return PartialView("_MenuAdmin", Menu);
        }

        public ActionResult CustomerRegistration(string CustomerName, string Contact, string Email)
        {
            Customer model = new Customer();
            try
            {
                Random rnd = new Random();
                string pass = rnd.Next(111111, 999999).ToString();

                model.CustomerName = CustomerName;
                model.Contact = Contact;
                model.Email = Email;
                model.Password = Crypto.Encrypt(pass);

                DataSet ds = model.CustomerRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                        model.Description = "Registration was succesfull\nPassword is " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        string str = "Dear " + CustomerName + ",You have been successfully registered as MSCLShopping Customer, your Login ID is " + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and  Password is : " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        BLSMS.SendSMSNew(Contact, str);
                        TempData["CustomerName"] = CustomerName;
                        TempData["SuccesMessage"] = str;
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        TempData["SuccesMessage"]= ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
                TempData["SuccesMessage"] = ex.Message;
            }
            return RedirectToAction("SuccessRegistration");
        }

        public ActionResult SendOTP(string MemberName, string Contact)
        {
            Customer model = new Customer();
            try
            {
                Random rnd = new Random();
                string ctrOTP = rnd.Next(111111, 999999).ToString();
                string strOTP = "Dear " + MemberName + ", Your OTP for Registration is :" + ctrOTP;
                BLSMS.SendSMSNew(Contact, strOTP);

                model.OTP = ctrOTP;
                model.CustomerName = MemberName;
                model.Contact = Contact;
                model.Result = "1";
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoginCustomer(string LoginID, string Password)
        {
            Customer model = new Customer();
            try
            {
                model.LoginID = LoginID;
                model.Password = Password;
                DataSet ds = model.CustomerLogin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (model.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            model.Result = "1";
                            Session["CustomerName"] = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                            Session["CustomerID"] = ds.Tables[0].Rows[0]["PK_CustomerID"].ToString();
                            Session["Contact"] = ds.Tables[0].Rows[0]["Contact"].ToString();
                            Session["CustomerCode"] = ds.Tables[0].Rows[0]["CustomerCode"].ToString();
                            Session["Password"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            Session["Fk_AssociateId"] = ds.Tables[0].Rows[0]["Fk_AssociateId"].ToString();
                            Session["TemPermanent"] = ds.Tables[0].Rows[0]["TemPermanent"].ToString();
                            Session["EmailId"] = ds.Tables[0].Rows[0]["Email"].ToString();
                            Session["TotalCount"] = ds.Tables[1].Rows[0]["TotalCount"].ToString();



                            #region SetDataInCookies
                            HttpCookie loginCookie = new HttpCookie("Login");
                            loginCookie.Values["CustomerName"] = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                            loginCookie.Values["PK_CustomerID"] = ds.Tables[0].Rows[0]["PK_CustomerID"].ToString();
                            loginCookie.Values["Contact"] = ds.Tables[0].Rows[0]["Contact"].ToString();
                            loginCookie.Values["CustomerCode"] = ds.Tables[0].Rows[0]["CustomerCode"].ToString();
                            loginCookie.Values["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                            loginCookie.Values["Fk_AssociateId"] = ds.Tables[0].Rows[0]["Fk_AssociateId"].ToString();
                            loginCookie.Values["TemPermanent"] = ds.Tables[0].Rows[0]["TemPermanent"].ToString();
                            loginCookie.Values["EmailId"] = ds.Tables[0].Rows[0]["Email"].ToString();
                            loginCookie.Path = Request.ApplicationPath;
                            Response.Cookies.Add(loginCookie);
                            #endregion SetDataInCookies

                            //Load Customer Cart
                            List<Customer> lst = new List<Customer>();

                            model.CustomerID = Session["CustomerID"].ToString();
                            DataSet dsCart = model.loadCustomerCart();
                            if (dsCart != null && dsCart.Tables.Count > 0 && dsCart.Tables[0].Rows.Count > 0)
                            {

                                int count = 0;
                                foreach (DataRow r in dsCart.Tables[0].Rows)
                                {
                                    if (count < 2)
                                    {
                                        Customer obj = new Customer();
                                        obj.ProductID = r["FK_ProductID"].ToString();
                                        obj.ProductName = r["ProductName"].ToString();
                                        obj.SizeName = r["SizeName"].ToString();
                                        obj.UnitName = r["UnitName"].ToString();
                                        obj.ColorID = r["FK_ColorID"].ToString();
                                        obj.FlavorID = r["FK_FlavorID"].ToString();
                                        obj.MaterialID = r["FK_MaterialID"].ToString();
                                        obj.MRP = r["Rate"].ToString();
                                        obj.PrimaryImage = r["PrimaryImage"].ToString();
                                        obj.ProductQuantity = r["Quantity"].ToString();
                                        obj.SubTotal = r["SubTotal"].ToString();

                                        ViewBag.CartTotal = Math.Round(Convert.ToDecimal(ViewBag.CartTotal) + Convert.ToDecimal(r["Rate"].ToString()), 2);
                                        lst.Add(obj);
                                    }
                                    count++;
                                }
                                //ViewBag.FinalAmount = Math.Round(float.Parse(ds.Tables[0].Compute("sum(SubTotal)", "").ToString()), 2);
                                ViewBag.TotalCount = dsCart.Tables[3].Rows[0]["TotalCount"].ToString();
                                model.lstCart = lst;
                            }
                            //Load Customer Cart
                        }
                        else
                        {
                            model.Result = "Incorrect Password";
                        }
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Logout()
        {
            Customer model = new Customer();
            Session["CustomerID"] = null;
            Session.Abandon();
            HttpCookie loginCookie = Request.Cookies["Login"];
            if (loginCookie != null)
            {
                loginCookie.Expires = DateTime.Now;
                loginCookie.Values["ExpiresDate"] = DateTime.Now.ToString();
                Response.Cookies.Add(loginCookie);
            }

            model.Result = "1";
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public virtual PartialViewResult Menu()
        {
            Customer Menu = null;

            if (Session["_Menu"] != null)
            {
                Menu = (Customer)Session["_Menu"];
            }
            else
            {

                Menu = Customer.GetMenus(); // pass employee id here
                Session["_Menu"] = Menu;
            }
            return PartialView("_Menu", Menu);
        }

        public virtual PartialViewResult MobileMenu()
        {
            Customer Menu = null;

            if (Session["_Menu"] != null)
            {
                Menu = (Customer)Session["_Menu"];
            }
            else
            {

                Menu = Customer.GetMenus(); // pass employee id here
                Session["_Menu"] = Menu;
            }
            return PartialView("_MobileMenu", Menu);
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult SaveContact(Home model)
        {
            try
            {
                model.CreatedBy = "0";
                DataSet ds = model.SaveContactUs();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SuccesMessage"] = "Your contact details has been submitted !!";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["SuccesMessage"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SuccesMessage"] = ex.Message;
            }
            return RedirectToAction("Contact");
        }

        #region ProductsWithOffer

        public ActionResult ProductWithOfferList(string id)
        {
            Customer model = new Customer();

            //    model.OfferID = id;
            model.OfferID = Crypto.Decrypt(id);
            List<Customer> lst = new List<Customer>();

            DataSet ds = model.GetProductByOffer();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_ProductID"].ToString());
                    obj.ProductName = r["ProductName"].ToString();
                    obj.Description = r["ShortDescription"].ToString();
                    obj.BrandName = r["BrandName"].ToString();
                    obj.PrimaryImage = r["ProductImage"].ToString();
                    obj.MRP = r["MRP"].ToString();
                    obj.OfferedPrice = r["OfferedPrice"].ToString();
                    obj.SoldOutCss = r["SoldOutCss"].ToString();
                    obj.RedeemPrice = r["RedeemPrice"].ToString();
                    lst.Add(obj);
                }
                model.lstProducts = lst;
            }
            return View(model);
        }

        #endregion

        public ActionResult ForgotPassword(Home model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("ForgotPassword")]
        [OnAction(ButtonName = "btnGetPassword")]
        public ActionResult ValidateData(Home obj)
        {

            DataSet ds = obj.ValidateData();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    string strOTP = " Your Password is :" + ds.Tables[0].Rows[0]["Password"].ToString();
                    BLSMS.SendSMSNew(ds.Tables[0].Rows[0]["Mobile"].ToString(), strOTP);

                    TempData["recoverPassword"] = "Password is sent on your registered mobile no.";
                }
            }
            else
            {
                TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return RedirectToAction("ForgotPassword", "Home");
        }

        public ActionResult ValidateData(string Contact)
        {
            Customer obj = new Customer();
            obj.Contact = Contact;
            DataSet ds = obj.ValidateData();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    obj.Result = "Incorrect mobile no.";
                }
                else
                {
                    string strOTP = " Your Password is :" + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());

                    BLSMS.SendSMSNew(Contact, strOTP);

                    obj.Result = "Password is sent on your registered mobile no.";
                    TempData["recoverPassword"] = "Password is sent on your registered mobile no.";
                }
            }
            else
            {
                TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                obj.Result = "Incorrect mobile no.";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DecriptPass()
        {
            return View();
        }

        [HttpPost]
        [ActionName("DecriptPass")]
        [OnAction(ButtonName = "btndecript")]
        public ActionResult GetPassword(Home obj)
        {
            obj.DecriptPass = Crypto.Decrypt(obj.Password);
            return View(obj);
        }


        public virtual PartialViewResult MenuShopping()
        {
            Home Menu = null;

            if (Session["_MenuShopping"] != null)
            {
                Menu = (Home)Session["_MenuShopping"];
            }
            else
            {

                Menu = Home.GetMenus(Session["Pk_AdminId"].ToString(), Session["UserType"].ToString()); // pass employee id here
                Session["_MenuShopping"] = Menu;
            }
            return PartialView("_MenuShopping", Menu);
        }

        public ActionResult TryPrime()
        {
            try
            {
                Master model = new Master();
                model.MainCategoryID = "7";

                #region Get
                List<Master> lst = new List<Master>();




                DataSet ds = model.CategoryList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[2].Rows)
                    {
                        Master obj = new Master();
                        obj.CategoryName = r["CategoryName"].ToString();
                        obj.CategoryID = r["PK_CategoryID"].ToString();
                        obj.Images = r["Images"].ToString();
                        lst.Add(obj);
                    }
                    model.lstCategory = lst;
                }


                #endregion


                return View(model);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult SuccessRegistration()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult CustomerService()
        {
            return View();
        }
        public ActionResult Specials()
        {
            return View();
        }
    }
}
