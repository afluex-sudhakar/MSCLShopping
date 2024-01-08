using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MSCLShopping.Models;
using MSCLShopping.Filter;
using ShoppingPortal.Models;
using Newtonsoft.Json;
using System.Text;
using MSCLShopping.Models.RazpayModels;
using System.Net;
using Razorpay.Api;
using static MSCLShopping.Models.Common;
using System.IO;

namespace MSCLShopping.Controllers
{
    public class CustomerController : Controller
    {
        //public ActionResult LoginCustomer(string LoginID, string Password, string productInfoCode, string productID, string sizeID, string unitID, string colorid, string materialid, string flavorid, string rate, string qty)
        public ActionResult LoginCustomer(string LoginID, string Password, string productInfoCode, string productID, string sizeID, string unitID, string colorid, string flavorid, string ramid, string storageid, string starratingid, string rate, string qty, string FK_vendorId, string materialid, string dcharge)
        {
            Customer model = new Customer();
            try
            {
                model.LoginID = LoginID;
                DataSet ds = model.CustomerLogin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            //model.Result = "1";
                            Session["CustomerName"] = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                            Session["CustomerID"] = ds.Tables[0].Rows[0]["PK_CustomerID"].ToString();

                            model.ProductID = productID;
                            model.ProductCode = productInfoCode;
                            model.SizeID = string.IsNullOrEmpty(sizeID) ? "0" : sizeID;
                            model.UnitID = string.IsNullOrEmpty(unitID) ? "0" : unitID;
                            model.ColorID = string.IsNullOrEmpty(colorid) ? "0" : colorid;
                            model.MaterialID = string.IsNullOrEmpty(materialid) ? "0" : materialid;
                            model.FlavorID = string.IsNullOrEmpty(flavorid) ? "0" : flavorid;
                            model.RamID = string.IsNullOrEmpty(ramid) ? "0" : ramid;
                            model.StorageID = string.IsNullOrEmpty(storageid) ? "0" : storageid;
                            model.StarRatingID = string.IsNullOrEmpty(starratingid) ? "0" : starratingid;
                            model.OfferedPrice = rate;
                            model.ProductQuantity = qty;
                            model.CustomerID = Session["CustomerID"].ToString();
                            model.Fk_vendorId = FK_vendorId;
                            model.DeliveryCharge = dcharge;

                            DataSet dsCart = model.AddToCart();
                            if (dsCart != null && ds.Tables.Count > 0)
                            {
                                if (dsCart.Tables[0].Rows[0][0].ToString() == "1")
                                {
                                    model.Result = "1";
                                    TempData["msg"] = "Added to Cart";
                                }
                                else if (dsCart.Tables[0].Rows[0][0].ToString() == "0")
                                {
                                    model.Result = dsCart.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    TempData["msg"] = dsCart.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
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

        #region Logout

        public ActionResult Logout()
        {
            Customer model = new Customer();
            Session["CustomerID"] = null;
            Session.Abandon();

            model.Result = "1";
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region showCart
        public ActionResult Cart(Customer model)
        {
            if (Session["CustomerID"] != null)
            {

                
                List<Customer> lst = new List<Customer>();

                model.CustomerID = Session["CustomerID"].ToString();
                DataSet ds = model.loadCustomerCart();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.CartID = r["PK_CartID"].ToString();
                        obj.DeliveryCharge = r["DeliveryCharge"].ToString();
                        obj.RedeemPrice = r["RedeemPrice"].ToString();
                        obj.OriginalDeliveryCharge = r["OriginalDeliveryCharge"].ToString();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["FK_ProductID"].ToString());
                        obj.ProductCode = r["ProductInfoCode"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.ProductInfo = r["ProductInfo"].ToString();
                        obj.SizeID = r["FK_SizeID"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.UnitID = r["FK_UnitID"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.ColorID = r["FK_ColorID"].ToString();
                        obj.FlavorID = r["FK_FlavorID"].ToString();
                        obj.MaterialID = r["FK_MaterialID"].ToString();
                        obj.RamID = r["FK_RamID"].ToString();
                        obj.StorageID = r["FK_StorageID"].ToString();
                        obj.StarRatingID = r["FK_StarRatingID"].ToString();
                        obj.MRP = r["Rate"].ToString();
                        obj.PrimaryImage = r["PrimaryImage"].ToString();
                        obj.ProductQuantity = r["Quantity"].ToString();
                        obj.Fk_vendorId = r["VendorId"].ToString();
                        obj.VendorName = r["VendorName"].ToString();
                        obj.IsAvailable = r["IsAvailable"].ToString();
                        obj.SubTotal = r["SubTotal"].ToString();
                        obj.FinalAmount = (decimal.Parse(r["SubTotal"].ToString()) + decimal.Parse(r["DeliveryCharge"].ToString())).ToString() ;
                        //  obj.SoldOutCss = r["SoldOutCss"].ToString();
                        ViewBag.CartTotal = Math.Round(Convert.ToDecimal(ViewBag.CartTotal) + Convert.ToDecimal(r["Rate"].ToString()), 2);
                        ViewBag.TotalDelivery = Convert.ToDecimal(ViewBag.TotalDelivery) + Convert.ToDecimal(obj.DeliveryCharge);
                        lst.Add(obj);
                    }
                    ViewBag.SubTotal = Math.Round(float.Parse(ds.Tables[0].Compute("sum(SubTotal)", "").ToString()), 2);
                    ViewBag.DelievryCharge = Math.Round(float.Parse(ds.Tables[0].Compute("sum(DeliveryCharge)", "").ToString()), 2);
                    ViewBag.FinalAmount = Math.Round(float.Parse(ds.Tables[0].Compute("sum(SubTotalWithDelivery)", "").ToString()), 2);
                    ViewBag.RedeemPrice = (decimal.Parse(ds.Tables[0].Compute("sum(ShoppingPoint)", "").ToString()));
                    ViewBag.shoppingbalance = ds.Tables[4].Rows[0]["Balance"].ToString();
                    if (Convert.ToDecimal(ViewBag.RedeemPrice )< Convert.ToDecimal(ViewBag.shoppingbalance))
                    {
                        ViewBag.RedeemPrice = Math.Round(float.Parse(ds.Tables[0].Compute("sum(ShoppingPoint)", "").ToString()), 2);
                    }
                    else
                    {
                        ViewBag.RedeemPrice = ds.Tables[4].Rows[0]["Balance"].ToString();
                    }
                    model.lstProducts = lst;

                    //Load Customer's Address if saved
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        List<Customer> lstAddress = new List<Customer>();
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Customer obj = new Customer();
                            obj.FK_AddressID = r["PK_CustomerOtherInfoID"].ToString();
                            obj.HouseNo = r["HouseNo"].ToString();
                            obj.Locality = r["Locality"].ToString();
                            obj.Landmark = r["Landmark"].ToString();
                            obj.Pincode = r["Pincode"].ToString();
                            obj.AddressType = r["AddressType"].ToString();
                            obj.DisplayName = r["Name"].ToString();
                            obj.Contact = r["ContatNo"].ToString();
                            obj.State = r["Statename"].ToString();
                            obj.City = r["Districtname"].ToString();

                            lstAddress.Add(obj);
                        }
                        model.lstAddress = lstAddress;
                    }
                    //Load Customer's Address if saved

                    //Load Customer's Wallet Balance
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        ViewBag.CustomerWallet = ds.Tables[2].Rows[0]["Balance"].ToString();
                       
                    }
                    //Load Customer's Wallet Balance
                    if (Convert.ToDecimal(ViewBag.CartTotal) > Convert.ToDecimal(ViewBag.CustomerWallet))
                    {
                        ViewBag.Disable = "disabled";
                    }
                    else
                    {
                        ViewBag.Disable = "";
                    }


                    model.TemPermanent = ds.Tables[5].Rows[0]["TemPermanent"].ToString();


                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ApplyDiscount(string cno)
        {
            Customer model = new Customer();
            try
            {
                model.CustomerID = Session["CustomerID"].ToString();
                model.CouponCode = cno;

                DataSet ds = model.ApplyDiscount();
                //     model.CouponID = ds.Tables[0].Rows[0]["PK_DiscForCustID"].ToString();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "1";
                        model.DiscountAmount = ds.Tables[0].Rows[0]["DiscountAmount"].ToString();
                        model.CouponID = ds.Tables[0].Rows[0]["CouponID"].ToString();
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
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
        #endregion

        #region ShowProductDetails

        public ActionResult ProductDetails()
        {
            try
            {

            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }

        public ActionResult loadProductDetails(string productID, string sizeID, string unitID)
        {
            Customer model = new Customer();
            model.ProductID = productID;
            DataSet ds = model.ProductDetails();
            if (ds != null && ds.Tables.Count > 0)
            {
                model.Result = "1";
                model.ProductID = ds.Tables[0].Rows[0]["PK_ProductID"].ToString();
                model.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                model.Description = ds.Tables[0].Rows[0]["ShortDescription"].ToString();
                model.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                model.OfferedPrice = ds.Tables[0].Rows[0]["OfferedPrice"].ToString();
                model.SecondaryImage = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                model.UnitID = ds.Tables[0].Rows[0]["FK_UnitID"].ToString();


                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    List<SelectListItem> ddlSize = new List<SelectListItem>();
                //    foreach (DataRow r in ds.Tables[1].Rows)
                //    {
                //        ddlSize.Add(new SelectListItem { Text = r["SizeName"].ToString(), Value = r["FK_SizeID"].ToString() });
                //    }
                //    model.lstSize = ddlSize;
                //}
                //if (ds.Tables[2].Rows.Count > 0)
                //{
                //    List<SelectListItem> ddlColor = new List<SelectListItem>();
                //    foreach (DataRow r in ds.Tables[2].Rows)
                //    {
                //        ddlColor.Add(new SelectListItem { Text = r["ColorName"].ToString(), Value = r["FK_ColorID"].ToString() });
                //    }
                //    model.lstColor = ddlColor;
                //}
                //if (ds.Tables[3].Rows.Count > 0)
                //{
                //    List<SelectListItem> ddlFlavor = new List<SelectListItem>();
                //    foreach (DataRow r in ds.Tables[3].Rows)
                //    {
                //        ddlFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["FK_FlavorID"].ToString() });
                //    }
                //    model.lstFlavor = ddlFlavor;
                //}
                //if (ds.Tables[4].Rows.Count > 0)
                //{
                //    List<SelectListItem> ddlMaterial = new List<SelectListItem>();
                //    foreach (DataRow r in ds.Tables[4].Rows)
                //    {
                //        ddlMaterial.Add(new SelectListItem { Text = r["MaterialName"].ToString(), Value = r["FK_MaterialID"].ToString() });
                //    }
                //    model.lstMaterial = ddlMaterial;
                //}

            }
            else
            {
                model.Result = "0";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Checkout

        public ActionResult Checkout()
        {
            return View();
        }

        #endregion

        #region MyAccount
        public virtual PartialViewResult CustomerLeftMenu()
        {
            return PartialView("CustomerLeftMenu");
        }
        public ActionResult MyAccount(Customer model)
        {
            try
            {
                if (Session["CustomerID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.CustomerID = Session["CustomerID"].ToString();
                    DataSet ds = model.GetProfileDetails();

                    //Bind Order Numbers
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        model.CustomerID = ds.Tables[0].Rows[0]["PK_CustomerID"].ToString();
                        model.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                        model.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        Session["ProfilePic"] = ds.Tables[0].Rows[0]["ProfilePic"].ToString();

                    }
                }

            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return View(model);
        }

        
        public JsonResult UpdateProfilePic(string CustomerID)
        {
            Customer obj = new Customer();
            bool msg = false;
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];

                string fileName = file.FileName;
                obj.CustomerID = CustomerID;
                obj.ProfilePic = "/AssetsAdmin/images/users" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                DataSet ds = obj.UpdateProfilePic();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        msg = true;
                        Session["ProfilePic"] = obj.ProfilePic;
                    }
                    else
                    {
                        msg = false;
                    }
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult SaveInfo(string CustomerName,string CustomerID, string Contact,string Email)
        {
            Customer model = new Customer();
            try
            {
                model.CustomerName = CustomerName;
                model.CustomerID = CustomerID;
                model.Contact = Contact;
                model.Email = Email;
                model.UpdatedBy = Session["CustomerID"].ToString();

                DataSet ds = model.UpdateInfo();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        public ActionResult MyOrders(Customer model)
        {
            try
            {
                if (Session["CustomerID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.CustomerID = Session["CustomerID"].ToString();
                    DataSet ds = model.LoadCustomerOrders();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //Bind Order Numbers
                        List<Customer> lstOrderNo = new List<Customer>();
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Customer obj = new Customer();
                            obj.PK_OrderID = r["PK_OrderID"].ToString();
                            obj.OrderDate = r["OrderDate"].ToString();
                            obj.OrderNo = r["OrderNo"].ToString();
                            obj.SubTotal = r["OrderTotal"].ToString();
                            lstOrderNo.Add(obj);
                        }
                        model.lstOrderNo = lstOrderNo;
                        //Bind Order Numbers

                        //Order Details
                        List<Customer> lstOrders = new List<Customer>();
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Customer obj = new Customer();
                            obj.EncryptKey = Crypto.Encrypt(r["FK_ProductID"].ToString());
                            obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                            obj.OrderNo = r["OrderNo"].ToString();
                            obj.ProductID = r["FK_ProductID"].ToString();
                            obj.DeliveryDate = r["ExpectedDelivery"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.SizeName = r["SizeName"].ToString();
                            obj.ColorName = r["ColorName"].ToString();
                            obj.UnitName = r["UnitName"].ToString();
                            obj.FlavorName = r["FlavorName"].ToString();
                            obj.MaterialName = r["MaterialName"].ToString();
                            obj.ProductQuantity = r["Quantity"].ToString();
                            obj.Rate = r["Rate"].ToString();
                            obj.TotalAmount = r["Amount"].ToString();
                            obj.OrderItems = r["OrderItems"].ToString();
                            obj.OrderDate = r["OrderDate"].ToString();
                            obj.PrimaryImage = r["ProductImage"].ToString();
                            obj.Status = r["OrderStatus"].ToString();
                            obj.DayDifference = r["DayDiff"].ToString();
                            obj.DeliveryCharge = r["ShippingCharges"].ToString();
                            obj.RedeemPrice = r["RedeemPrice"].ToString();
                            lstOrders.Add(obj);
                        }
                        model.lstOrders = lstOrders;
                        //Order Details
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return View(model);
        }
        public ActionResult TrackOrder(string orderID)
        {
            if (Session["CustomerID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Customer model = new Models.Customer();
            try
            {
                model.CustomerID = Session["CustomerID"].ToString();
                model.PK_OrderID = orderID;

                DataSet ds = model.TrackOrder();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.OrderNo = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                    ViewBag.HouseNo = ds.Tables[0].Rows[0]["HouseNo"].ToString();
                    ViewBag.Locality = ds.Tables[0].Rows[0]["Locality"].ToString();
                    ViewBag.Landmark = ds.Tables[0].Rows[0]["Landmark"].ToString();
                    ViewBag.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        List<Customer> lst = new List<Customer>();
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Customer obj = new Customer();
                            obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                            obj.OrderNo = r["OrderNo"].ToString();
                            obj.DeliveryDate = r["DeliveryDate"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.SizeName = r["SizeName"].ToString();
                            obj.ColorName = r["ColorName"].ToString();
                            obj.UnitName = r["UnitName"].ToString();
                            obj.FlavorName = r["FlavorName"].ToString();
                            obj.MaterialName = r["MaterialName"].ToString();
                            obj.ProductQuantity = r["Quantity"].ToString();
                            obj.Rate = r["Rate"].ToString();
                            obj.TotalAmount = r["Amount"].ToString();
                            obj.OrderItems = r["OrderItems"].ToString();
                            obj.OrderDate = r["OrderDate"].ToString();
                            obj.PrimaryImage = r["ProductImage"].ToString();
                            obj.Status = r["OrderStatus"].ToString();
                            obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                            obj.VendorName = r["VendorName"].ToString();
                            obj.Fk_vendorId = r["Fk_vendorId"].ToString();
                            lst.Add(obj);
                        }
                        model.lstOrders = lst;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public ActionResult CancelOrder(string orderDetailsID)
        {
            Customer model = new Customer();
            try
            {
                model.CustomerID = Session["CustomerID"].ToString();
                model.OrderDetailsID = orderDetailsID;

                DataSet ds = model.CancelOrder();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "1";
                        try
                        {
                            string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", your Cancel Request for Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " has been received.";
                            string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                            BLSMS.SendSMSNew(mobile, message);
                        }
                        catch { }
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = "0";
                        model.Status = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Status = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReturnOrder(string orderDetailsID, string orderID, string desc)
        {
            Customer model = new Customer();
            try
            {
                model.CustomerID = Session["CustomerID"].ToString();
                model.PK_OrderID = orderID;
                model.OrderDetailsID = orderDetailsID;
                model.Description = desc;

                DataSet ds = model.ReturnOrder();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "1";
                        try
                        {
                            string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", your Cancel Request for Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " has been received.";
                            string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                            BLSMS.SendSMSNew(mobile, message);
                        }
                        catch { }
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = "0";
                        model.Status = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Status = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddressBook(Customer model)
        {
            try
            {
                if (Session["CustomerID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                #region ddlTypeAddress
                List<SelectListItem> TypeAddress = Common.AddressType();
                ViewBag.TypeAddress = TypeAddress;
                #endregion TypeAddress

                List<Customer> lstOrders = new List<Customer>();
                model.CustomerID = Session["CustomerID"].ToString();
                DataSet ds = model.AddressList();

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.FK_AddressID = r["PK_CustomerOtherInfoID"].ToString();
                    obj.HouseNo = r["HouseNo"].ToString();
                    obj.Landmark = r["Landmark"].ToString();
                    obj.Pincode = r["Pincode"].ToString();
                    obj.Locality = r["Locality"].ToString();
                    obj.AddressType = r["AddressType"].ToString();
                    obj.IsDefault = r["IsDefault"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    obj.Contact = r["ContatNo"].ToString();
                    lstOrders.Add(obj);
                }
                model.lstOrders = lstOrders;
                //Order Details
            }

            catch (Exception ex)
            {
                model.Result = ex.Message;
            }


            return View(model);
        }
        public ActionResult SaveAddress(Customer model)
        {
            try
            {
                string chk = "";
                chk = model.IsDefault;
                if (chk == "on")
                {
                    model.IsDefault = "1";
                }
                else
                {
                    model.IsDefault = "0";

                }

                model.AddedBy = Session["CustomerID"].ToString();
                DataSet ds = model.SaveAddress();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["SaveAddress"] = "Address saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["SaveAddress"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["SaveAddress"] = ex.Message;
            }
            return RedirectToAction("AddressBook");
        }

        public ActionResult UpdateAddress(string AddressType, string FK_AddressID, string Landmark, string Locality, string Pincode, string HouseNo,string IsDefault,string DisplayName,string Contact)
        {
            Customer obj = new Customer();
            try
            {
                obj.FK_AddressID = FK_AddressID;
                obj.Landmark = Landmark;
                obj.Locality = Locality;
                obj.Pincode = Pincode;
                obj.HouseNo = HouseNo;
                obj.AddressType = AddressType;
                obj.UpdatedBy = Session["CustomerID"].ToString();
                obj.IsDefault = IsDefault;
                string chk = "";
                chk = obj.IsDefault;
                if (chk == "True")
                {
                    obj.IsDefault = "1";
                }
                else
                {
                    obj.IsDefault = "0";

                }
                obj.DisplayName = DisplayName;
                obj.Contact = Contact;
                DataSet ds = obj.UpdateAddress();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["SaveAddress"] = "Address updated successfully";
                        obj.Result = "Color updated successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["SaveAddress"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAddress(string id)
        {
            try
            {
                Customer obj = new Customer();
                obj.FK_AddressID = id;
                obj.AddedBy = Session["CustomerID"].ToString();
                DataSet ds = obj.DeleteAddress();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["SaveAddress"] = "Address deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["SaveAddress"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["SaveAddress"] = ex.Message;
            }
            return RedirectToAction("AddressBook");
        }

        public ActionResult MyCoupons(Customer model)
        {

            try
            {
                if (Session["CustomerID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                List<Customer> lstOrders = new List<Customer>();
                model.CustomerID = Session["CustomerID"].ToString();
                DataSet ds = model.MyCoupons();

                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.CouponID = r["PK_DiscForCustID"].ToString();
                    obj.CouponCode = r["CouponCode"].ToString();
                    obj.TotalAmount = r["DiscountAmount"].ToString();
                    obj.FromDate = r["ValidFrom"].ToString();
                    obj.ToDate = r["ValidTo"].ToString();

                    lstOrders.Add(obj);
                }
                model.lstOrders = lstOrders;
                //Order Details
            }

            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return View(model);
        }

        #endregion

        public virtual PartialViewResult CustomerCart()
        {
            Customer model = new Customer();
            if (Session["CustomerID"] != null)
            {
                List<Customer> lst = new List<Customer>();

                model.CustomerID = Session["CustomerID"].ToString();
                DataSet ds = model.loadCustomerCart();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
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
                    ViewBag.ItemCount = ds.Tables[0].Rows[0]["ItemsCount"].ToString();
                    ViewBag.FinalAmount = Math.Round(float.Parse(ds.Tables[0].Compute("sum(SubTotal)", "").ToString()), 2);
                    model.lstProducts = lst;
                }
            }
            else
            {

            }
            return PartialView("CustomerCart", model);
        }

        public ActionResult CheckStock(string pcode, string fk_vendorId)
        {

            Customer model = new Customer();
            try
            {
                model.ProductCode = pcode;
                model.Fk_vendorId = fk_vendorId;
                DataSet ds = model.CheckStock();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Result = "1";
                    model.Status = ds.Tables[0].Rows[0]["StockBalance"].ToString();
                }
                else
                {
                    model.Result = "0";
                    model.Status = "Problem in fetching Stock details for the selected purpose.";
                }
            }
            catch (Exception ex)
            {
                model.Result = "0";
                model.Status = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductList(string id, string Status, string f, string value)
        {
            Session["ColorDetails"] = null;
            Session["SizeDetails"] = null;
            Session["FlavourDetails"] = null;
            Session["MaterialDetails"] = null;
            Session["RamDetails"] = null;
            Session["StorageDetails"] = null;
            Customer model = new Customer();
            List<Customer> lstVarient = new List<Customer>();
            List<Customer> lstVarientDetail = new List<Customer>();
            model.Status = Status;
            #region Bind Filters
            if (Status == "Category")
            {
                model.CategoryID = id;


            }
            else if (Status == "Main category")
            {
                model.MainCategoryID = id;
            }
            else if (Status == "Sub Category")
            {
                model.SubCategoryID = id;
            }
            else
            {
                model.OtherStatus = Status;
            }

            DataSet ds1 = model.BindFiltersForProductList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.VarientName = r["VariantName"].ToString();

                    lstVarient.Add(obj);
                }
                model.lstVarient = lstVarient;
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[1].Rows)
                {
                    Customer obj = new Customer();
                    obj.Status = r["Status"].ToString();
                    obj.ColorCode = r["ColorCode"].ToString();

                    lstVarientDetail.Add(obj);
                }
                model.lstVarientDetail = lstVarientDetail;
            }

            #endregion Bind Filters
            List<Customer> lst = new List<Customer>();
            if (f == "menu")
            {
                model.SubCategoryID = id;
                model.Status = Status;
                model.ShortValue = value;
                DataSet ds = model.GetProductSubcategoryWise();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.ProductID = r["PK_ProductID"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_ProductID"].ToString());
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Description = r["ShortDescription"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.OfferedPrice = r["OfferedPrice"].ToString();
                        obj.PrimaryImage = r["Images"].ToString();
                        obj.SoldOutCss = r["SoldOutCss"].ToString();
                        obj.RedeemPrice = r["RedeemPrice"].ToString();
                        lst.Add(obj);
                    }
                    model.lstProducts = lst;
                }
            }
            else
            {
                model.DisplayName = Status;
                DataSet ds = model.GetGlobalData();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.ProductID = r["PK_ProductID"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_ProductID"].ToString());
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Description = r["ShortDescription"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.OfferedPrice = r["OfferedPrice"].ToString();
                        obj.PrimaryImage = r["Images"].ToString();
                        obj.SoldOutCss = r["SoldOutCss"].ToString();
                        obj.RedeemPrice = r["RedeemPrice"].ToString();
                        lst.Add(obj);
                    }
                    model.lstProducts = lst;
                }
            }
            return View(model);
        }

        public ActionResult AutoSearchProduct(string q)
        {
            List<Customer> lst = new List<Customer>();
            Customer model = new Customer();
            model.DisplayName = q;
            DataSet ds = model.AutoSearchProduct();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();

                    obj.ProductName = r["ProductName"].ToString();

                    lst.Add(obj);
                }

            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckedItem(string VarientDetails, string Varient, string Status, string MainCategoryID, string CategoryID, string SubCategoryID)
        {
            Customer obj = new Customer();
            #region ForColor
            if (Varient == "Color")
            {
                if (Session["ColorDetails"] == null)
                {
                    Session["ColorDetails"] = VarientDetails;
                    Session["ColorDetails"] = ',' + VarientDetails + ',';
                }
                else
                {
                    if (Session["ColorDetails"].ToString().Contains(VarientDetails + ','))
                    {

                        Session["ColorDetails"] = Session["ColorDetails"].ToString().Replace(',' + VarientDetails + ',', ",");

                    }
                    else
                    {


                        Session["ColorDetails"] = ',' + VarientDetails + Session["ColorDetails"].ToString();

                    }

                }

            }
            #endregion ForColor
            #region ForMaterial
            else if (Varient == "Material")
            {
                if (Session["MaterialDetails"] == null)
                {
                    Session["MaterialDetails"] = VarientDetails;
                    Session["MaterialDetails"] = ',' + VarientDetails + ',';
                }
                else
                {
                    if (Session["MaterialDetails"].ToString().Contains(VarientDetails + ','))
                    {

                        Session["MaterialDetails"] = Session["MaterialDetails"].ToString().Replace(',' + VarientDetails + ',', ",");

                    }
                    else
                    {


                        Session["MaterialDetails"] = ',' + VarientDetails + Session["MaterialDetails"].ToString();

                    }

                }

            }
            #endregion ForMaterial
            #region SizeDetails
            else if (Varient == "Size")
            {
                if (Session["SizeDetails"] == null)
                {
                    Session["SizeDetails"] = VarientDetails;
                    Session["SizeDetails"] = ',' + VarientDetails + ',';
                }
                else
                {
                    if (Session["SizeDetails"].ToString().Contains(VarientDetails + ','))
                    {

                        Session["SizeDetails"] = Session["SizeDetails"].ToString().Replace(',' + VarientDetails + ',', ",");

                    }
                    else
                    {


                        Session["SizeDetails"] = ',' + VarientDetails + Session["SizeDetails"].ToString();

                    }

                }

            }
            #endregion SizeDetails
            #region FlavourDetails
            else if (Varient == "Flavour")
            {
                if (Session["FlavourDetails"] == null)
                {
                    Session["FlavourDetails"] = VarientDetails;
                    Session["FlavourDetails"] = ',' + VarientDetails + ',';
                }
                else
                {
                    if (Session["FlavourDetails"].ToString().Contains(VarientDetails + ','))
                    {

                        Session["FlavourDetails"] = Session["FlavourDetails"].ToString().Replace(',' + VarientDetails + ',', ",");

                    }
                    else
                    {


                        Session["FlavourDetails"] = ',' + VarientDetails + Session["FlavourDetails"].ToString();

                    }

                }

            }
            #endregion FlavourDetails
            #region RamDetails
            else if (Varient == "RAM")
            {
                if (Session["RamDetails"] == null)
                {
                    Session["RamDetails"] = VarientDetails;
                    Session["RamDetails"] = ',' + VarientDetails + ',';
                }
                else
                {
                    if (Session["RamDetails"].ToString().Contains(VarientDetails + ','))
                    {

                        Session["RamDetails"] = Session["RamDetails"].ToString().Replace(',' + VarientDetails + ',', ",");

                    }
                    else
                    {


                        Session["RamDetails"] = ',' + VarientDetails + Session["RamDetails"].ToString();

                    }

                }

            }
            #endregion RamDetails
            #region StorageDetails
            else if (Varient == "Storage")
            {
                if (Session["StorageDetails"] == null)
                {
                    Session["StorageDetails"] = VarientDetails;
                    Session["StorageDetails"] = ',' + VarientDetails + ',';
                }
                else
                {
                    if (Session["StorageDetails"].ToString().Contains(VarientDetails + ','))
                    {

                        Session["StorageDetails"] = Session["StorageDetails"].ToString().Replace(',' + VarientDetails + ',', ",");

                    }
                    else
                    {


                        Session["StorageDetails"] = ',' + VarientDetails + Session["StorageDetails"].ToString();

                    }

                }

            }
            #endregion StorageDetails
            List<Customer> lst = new List<Customer>();
            obj.Status = Status;
            if (Session["ColorDetails"] != null)
            {
                obj.ColorDetails = Session["ColorDetails"].ToString();
                if (obj.ColorDetails == ",")
                {
                    obj.ColorDetails = null;

                }

            }
            else
            {
                obj.ColorDetails = null;
            }
            if (Session["MaterialDetails"] != null)
            {
                obj.MaterialDetails = Session["MaterialDetails"].ToString();
                if (obj.MaterialDetails == ",")
                {
                    obj.MaterialDetails = null;

                }
            }
            else
            {
                obj.MaterialDetails = null;
            }
            if (Session["SizeDetails"] != null)
            {
                obj.SizeDetails = Session["SizeDetails"].ToString();
                if (obj.SizeDetails == ",")
                {
                    obj.SizeDetails = null;

                }
            }
            else
            {
                obj.SizeDetails = null;
            }
            if (Session["FlavourDetails"] != null)
            {
                obj.FlavourDetails = Session["FlavourDetails"].ToString();
                if (obj.FlavourDetails == ",")
                {
                    obj.FlavourDetails = null;

                }
            }
            else
            {
                obj.FlavourDetails = null;
            }
            if (Session["RamDetails"] != null)
            {
                obj.RamDetails = Session["RamDetails"].ToString();
                if (obj.RamDetails == ",")
                {
                    obj.RamDetails = null;

                }
            }
            else
            {
                obj.RamDetails = null;
            }
            if (Session["StorageDetails"] != null)
            {
                obj.StorageDetails = Session["StorageDetails"].ToString();
                if (obj.StorageDetails == ",")
                {
                    obj.StorageDetails = null;

                }
            }
            else
            {
                obj.StorageDetails = null;
            }
            if (obj.Status == "Main category")
            {
                obj.Id = MainCategoryID;
            }
            if (obj.Status == "Category")
            {
                obj.Id = CategoryID;
            }
            if (obj.Status == "Sub Category")
            {
                obj.Id = SubCategoryID;
            }
            DataSet ds = obj.GetFilterData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj1 = new Customer();
                        obj1.ProductID = r["PK_ProductID"].ToString();
                        obj1.EncryptKey = Crypto.Encrypt(r["PK_ProductID"].ToString());
                        obj1.ProductName = r["ProductName"].ToString();

                        obj1.MRP = r["MRP"].ToString();
                        obj1.OfferedPrice = r["OfferedPrice"].ToString();
                        obj1.PrimaryImage = r["Images"].ToString();
                        obj1.SoldOutCss = r["SoldOutCss"].ToString();
                        lst.Add(obj1);
                    }
                    obj.lstProducts = lst;
                }

            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductsPage(Customer model)
        {
            List<Customer> lst = new List<Customer>();

            DataSet ds = model.ProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.ProductCode = r["ProductCode"].ToString();
                    obj.BrandName = r["BrandName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();

                    lst.Add(obj);
                }
                model.lstProducts = lst;
            }
            return View(model);
        }

        public ActionResult AddToCart(string productInfoCode, string productID, string sizeID, string unitID, string colorid, string flavorid, string ramid, string storageid, string starratingid, string rate, string qty, string vendorID, string materialid, string delcharge)
        {
            Customer model = new Customer();
            if (Session["CustomerID"] != null)
            {
                model.ProductID = productID;
                model.ProductCode = productInfoCode;
                model.SizeID = sizeID;
                model.UnitID = unitID;
                model.ColorID = colorid;
                model.MaterialID = materialid;
                model.FlavorID = flavorid;
                model.RamID = ramid;
                model.StorageID = storageid;
                model.StarRatingID = starratingid;
                model.OfferedPrice = rate;
                model.ProductQuantity = qty;
                model.CustomerID = Session["CustomerID"].ToString();
                model.Fk_vendorId = vendorID;
                model.DeliveryCharge = delcharge;

                DataSet ds = model.AddToCart();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                        TempData["msg"] = "Added to Cart";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        model.Result = "0";
                        model.Status = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        //TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            else
            {
                model.Result = "2";
            }
            //return RedirectToAction("ProductsPage");
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClearCart(string ProductID, string CartID)
        {

            Customer model = new Customer();
            model.CustomerID = Session["CustomerID"].ToString();
            model.ProductID = string.IsNullOrEmpty(ProductID) ? null : ProductID;
            model.CartID = string.IsNullOrEmpty(CartID) ? null : CartID;

            DataSet dsblock = model.ClearCart();

            return RedirectToAction("Cart");
        }

        //public ActionResult QuickViewNew(string pid, string colorid, string sizeid, string flavorid, string materialid, string ramid, string storageid, string starid)
        //{
        //    Customer model = new Customer();
        //    List<Customer> lst = new List<Customer>();
        //    //    model.ProductID = pid;
        //    model.EncryptKey = pid;
        //    model.ProductID = Crypto.Decrypt(pid);
        //    model.ColorID = string.IsNullOrEmpty(colorid) ? null : colorid;

        //    model.SizeID = string.IsNullOrEmpty(sizeid) ? null : sizeid;
        //    model.FlavorID = string.IsNullOrEmpty(flavorid) ? null : flavorid;
        //    model.MaterialID = string.IsNullOrEmpty(materialid) ? null : materialid;
        //    model.RamID = string.IsNullOrEmpty(ramid) ? null : ramid;
        //    model.StorageID = string.IsNullOrEmpty(storageid) ? null : storageid;
        //    model.StarRatingID = string.IsNullOrEmpty(starid) ? null : starid;

        //    DataSet ds = model.QuickView();
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        model.Result = "1";
        //        model.EncryptKey = Crypto.Encrypt(ds.Tables[0].Rows[0]["PK_ProductID"].ToString());
        //        model.ProductID = ds.Tables[0].Rows[0]["PK_ProductID"].ToString();
        //        model.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
        //        model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
        //        model.UnitID = ds.Tables[0].Rows[0]["FK_UnitID"].ToString();
        //        model.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
        //        model.OfferedPrice = ds.Tables[0].Rows[0]["OfferedPrice"].ToString();
        //        model.SecondaryImage = ds.Tables[0].Rows[0]["ImagePath"].ToString();
        //        model.ShortDescription = ds.Tables[0].Rows[0]["ShortDescription"].ToString();
        //        model.ProductCode = ds.Tables[0].Rows[0]["ProductInfoCode"].ToString();
        //        model.ColorID = ds.Tables[0].Rows[0]["FK_ColorID"].ToString();
        //        model.SizeID = ds.Tables[0].Rows[0]["FK_SizeID"].ToString();
        //        model.FlavorID = ds.Tables[0].Rows[0]["FK_FlavorID"].ToString();
        //        model.RamID = ds.Tables[0].Rows[0]["FK_RAM_ID"].ToString();
        //        model.StorageID = ds.Tables[0].Rows[0]["FK_StorageID"].ToString();
        //        model.StarRatingID = ds.Tables[0].Rows[0]["FK_StarRatingID"].ToString();
        //        model.OfferDiscountPerc = ds.Tables[0].Rows[0]["DiscountPerc"].ToString();
        //    }

        //    if (ds.Tables[1].Rows.Count > 0)
        //    {
        //        List<Customer> lstSize = new List<Customer>();
        //        int i = 0;
        //        foreach (DataRow r in ds.Tables[1].Rows)
        //        {

        //            Customer obj = new Customer();
        //            if(r["FK_SizeID"].ToString()== ds.Tables[9].Rows[0]["PK_SizeID"].ToString())
        //            {
        //                obj.SizeID = r["FK_SizeID"].ToString();
        //                obj.SizeName = r["SizeName"].ToString();
        //                obj.Status = "firsttime";
        //                obj.CssClass = "selectedSize";
        //                model.SizeID = r["FK_SizeID"].ToString();
        //            }
        //            else if (r["FK_SizeID"].ToString()==sizeid)
        //            {
        //                obj.SizeID = r["FK_SizeID"].ToString();
        //                obj.SizeName = r["SizeName"].ToString();
        //                obj.Status = "firsttime";
        //                obj.CssClass = "selectedSize";
        //            }
        //            else
        //            {
        //                obj.SizeID = r["FK_SizeID"].ToString();
        //                obj.SizeName = r["SizeName"].ToString();
        //                obj.Status = r["MappedStatus"].ToString();
        //                obj.CssClass = r["Class"].ToString();
        //            }

        //            lstSize.Add(obj);
        //            i++;
        //        }
        //        model.lstSizeMapped = lstSize;

        //    }

        //    if (ds.Tables[2].Rows.Count > 0)
        //    {
        //        List<Customer> lstColor = new List<Customer>();

        //        foreach (DataRow r in ds.Tables[2].Rows)
        //        {
        //            Customer obj = new Customer();

        //            if (r["FK_ColorID"].ToString() == ds.Tables[9].Rows[0]["PK_ColorID"].ToString())
        //            {
        //                obj.ColorID = r["FK_ColorID"].ToString();
        //                obj.ColorCode = r["ColorCode"].ToString();
        //                obj.ColorName = r["ColorName"].ToString();
        //                obj.Status = r["ColorName"].ToString();
        //                model.ColorID = r["FK_ColorID"].ToString();
        //            }
        //            else if (r["FK_ColorID"].ToString() == colorid)
        //            {
        //                obj.ColorID = r["FK_ColorID"].ToString();
        //                obj.ColorCode = r["ColorCode"].ToString();
        //                obj.ColorName = r["ColorName"].ToString();
        //                obj.Status = r["ColorName"].ToString();
        //            }
        //            else
        //            {
        //                obj.ColorID = r["FK_ColorID"].ToString();
        //                obj.ColorCode = r["ColorCode"].ToString();
        //                obj.ColorName = r["ColorName"].ToString();
        //                obj.Status = r["MappedStatus"].ToString();
        //                obj.CssClass = r["Class"].ToString();
        //            }


        //            lstColor.Add(obj);
        //        }
        //        model.lstColorMapped = lstColor;

        //    }

        //    if (ds.Tables[3].Rows.Count > 0)
        //    {
        //        int count = 0;
        //        List<SelectListItem> lstFlavor = new List<SelectListItem>();
        //        foreach (DataRow r in ds.Tables[3].Rows)
        //        {
        //            if (count == 0)
        //            {
        //                lstFlavor.Add(new SelectListItem { Text = "Select Flavor", Value = "0" });
        //            }
        //            lstFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["FK_FlavorID"].ToString() });
        //            count = count + 1;
        //        }
        //        ViewBag.Flavors = lstFlavor;
        //        //model.lstFlavorMapped = lstFlavor;
        //        //model.FlavorID = string.IsNullOrEmpty(flavorid) ? ds.Tables[3].Rows[0]["FK_FlavorID"].ToString() : flavorid;
        //    }


        //    if (ds.Tables[4].Rows.Count > 0)
        //    {
        //        List<Customer> lstMaterial = new List<Customer>();
        //        foreach (DataRow r in ds.Tables[4].Rows)
        //        {
        //            Customer obj = new Customer();
        //            obj.MaterialID = r["FK_MaterialID"].ToString();
        //            obj.MaterialName = r["MaterialName"].ToString();

        //            lstMaterial.Add(obj);
        //        }

        //        model.lstMaterialMapped = lstMaterial;

        //    }

        //    if (ds.Tables[5].Rows.Count > 0)
        //    {
        //        List<Customer> lstRam = new List<Customer>();
        //        foreach (DataRow r in ds.Tables[5].Rows)
        //        {
        //            Customer obj = new Customer();
        //            if (r["FK_RAM_ID"].ToString() == ds.Tables[9].Rows[0]["PK_RAM_ID"].ToString())
        //            {
        //                obj.RamID = r["FK_RAM_ID"].ToString();
        //                obj.RamName = r["RAM"].ToString();
        //                obj.Status = "firsttime";
        //                obj.CssClass = "selectedSize";
        //                model.RamID = r["FK_RAM_ID"].ToString();
        //            }
        //            else if (r["FK_RAM_ID"].ToString() == ramid)
        //            {
        //                obj.RamID = r["FK_RAM_ID"].ToString();
        //                obj.RamName = r["RAM"].ToString();
        //                obj.Status = "firsttime";
        //                obj.CssClass = "selectedSize";
        //            }
        //            else
        //            {
        //                obj.RamID = r["FK_RAM_ID"].ToString();
        //                obj.RamName = r["RAM"].ToString();
        //                obj.Status = r["MappedStatus"].ToString();
        //                obj.CssClass = r["Class"].ToString();
        //            }

        //            lstRam.Add(obj);
        //        }

        //        model.lstRam = lstRam;

        //    }

        //    if (ds.Tables[6].Rows.Count > 0)
        //    {
        //        List<Customer> lstStorage = new List<Customer>();
        //        foreach (DataRow r in ds.Tables[6].Rows)
        //        {
        //            Customer obj = new Customer();
        //            if (r["FK_StorageID"].ToString() == ds.Tables[9].Rows[0]["PK_StorageID"].ToString())
        //            {
        //                obj.StorageID = r["FK_StorageID"].ToString();
        //                obj.StorageName = r["Storage"].ToString();
        //                obj.Status = "firsttime";
        //                obj.CssClass = "selectedSize";
        //                model.StorageID = r["FK_StorageID"].ToString();
        //            }
        //            else if (r["FK_StorageID"].ToString() ==storageid)
        //            {
        //                obj.StorageID = r["FK_StorageID"].ToString();
        //                obj.StorageName = r["Storage"].ToString();
        //                obj.Status = "firsttime";
        //                obj.CssClass = "selectedSize";
        //            }
        //            else
        //            {
        //                obj.StorageID = r["FK_StorageID"].ToString();
        //                obj.StorageName = r["Storage"].ToString();
        //                obj.Status = r["MappedStatus"].ToString();
        //                obj.CssClass = r["Class"].ToString();
        //            }


        //            lstStorage.Add(obj);
        //        }

        //        model.lstStorage = lstStorage;

        //    }

        //    if (ds.Tables[8].Rows.Count > 0)
        //    {
        //        model.VendorName = ds.Tables[8].Rows[0]["VendorName"].ToString();

        //        model.Fk_vendorId = ds.Tables[8].Rows[0]["Fk_vendorId"].ToString();

        //    }
        //    DataSet dsImage = model.QuickView();
        //    foreach (DataRow r in dsImage.Tables[0].Rows)
        //    {
        //        Customer obj = new Customer();
        //        obj.SecondaryImage = r["ImagePath"].ToString();

        //        lst.Add(obj);
        //    }
        //    model.lstproductimages = lst;


        //    return View(model);
        //}

        public ActionResult PlaceCustomerOrder(Customer model)
        {
            Customer objcus = new Customer();
            try
            {
                string noofrows = Request["hdRows"].ToString();
                string productID, productinfocode, unitID, sizeID, colorID, flavorID, materialID, ramID, storageID, starratingID, quantity, rate, amount = "";
                string ordertotal = "0";
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductID", typeof(string));
                dt.Columns.Add("ProductInfoCode", typeof(string));
                dt.Columns.Add("UnitID", typeof(string));
                dt.Columns.Add("SizeID", typeof(string));
                dt.Columns.Add("ColorID", typeof(string));
                dt.Columns.Add("FlavorID", typeof(string));
                dt.Columns.Add("MaterialID", typeof(string));
                dt.Columns.Add("RamID", typeof(string));
                dt.Columns.Add("StorageID", typeof(string));
                dt.Columns.Add("StarRatingID", typeof(string));
                dt.Columns.Add("Quantity", typeof(string));
                dt.Columns.Add("Rate", typeof(string));
                dt.Columns.Add("Amount", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    #region CheckStock
                    objcus.Fk_vendorId = Request["txtvendorId_" + i].ToString();
                    objcus.ProductInfoCode = Request["productinfocode_" + i].ToString();
                    objcus.ProductQuantity = Request["qty_" + i].ToString();
                    DataSet dsResult = objcus.CheckProductStock();
                    if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            productID = Request["productID_" + i].ToString();
                            productinfocode = Request["productinfocode_" + i].ToString();
                            unitID = Request["unitID_" + i].ToString();
                            sizeID = string.IsNullOrEmpty(Request["sizeID_" + i].ToString()) ? null : Request["sizeID_" + i].ToString();
                            colorID = string.IsNullOrEmpty(Request["colorID_" + i].ToString()) ? null : Request["colorID_" + i].ToString();
                            flavorID = string.IsNullOrEmpty(Request["flavorID_" + i].ToString()) ? null : Request["flavorID_" + i].ToString();
                            materialID = string.IsNullOrEmpty(Request["materialID_" + i].ToString()) ? null : Request["materialID_" + i].ToString();
                            ramID = string.IsNullOrEmpty(Request["ramID_" + i].ToString()) ? null : Request["ramID_" + i].ToString();
                            storageID = string.IsNullOrEmpty(Request["storageID_" + i].ToString()) ? null : Request["storageID_" + i].ToString();
                            starratingID = string.IsNullOrEmpty(Request["starratingID_" + i].ToString()) ? null : Request["starratingID_" + i].ToString();
                            quantity = Request["qty_" + i].ToString();
                            rate = Request["rate_" + i].ToString();
                            amount = (decimal.Parse(rate) * decimal.Parse(quantity)).ToString();
                            ordertotal = (Convert.ToDecimal(ordertotal) + Convert.ToDecimal(amount)).ToString();

                            dt.Rows.Add(productID, productinfocode, unitID, sizeID, colorID, flavorID, materialID, ramID, storageID, starratingID, quantity, rate, amount);
                        }
                    }

                    #endregion CheckStock
                }

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    if (Request["txtsod_" + i].ToString() == "")
                    {
                        model.dtCustomerOrderDetails = dt;
                        model.TotalAmount = ordertotal;
                        model.CustomerID = Session["CustomerID"].ToString();
                        model.PaymentModeID = Request["paymentMode"].ToString();
                       
                        model.CouponID = null;
                        if (Request["addressType"] != null)
                        {
                            model.AddressType = Request["addressType"].ToString();
                        }

                        DataSet dsCustomerOrder = model.PlaceCustomerOrder();
                        if (dsCustomerOrder != null && dsCustomerOrder.Tables.Count > 0)
                        {
                            if (dsCustomerOrder.Tables[0].Rows[0][0].ToString() == "1")
                            {
                                try
                                {
                                    string message = "Order Placed : Your order with order ID : " + dsCustomerOrder.Tables[0].Rows[0]["OrderNo"].ToString() + " of amount Rs. " + dsCustomerOrder.Tables[0].Rows[0]["OrderAmount"].ToString() + " has been placed.";
                                    string mobile = dsCustomerOrder.Tables[0].Rows[0]["CustomerMobile"].ToString();

                                    BLSMS.SendSMSNew(mobile, message);
                                }
                                catch
                                { }
                                return RedirectToAction("MyOrders");
                            }
                            else if (dsCustomerOrder.Tables[0].Rows[0][0].ToString() == "0")
                            {
                                TempData["CustomerOrder"] = dsCustomerOrder.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CustomerOrder"] = ex.Message;
            }
            return RedirectToAction("Cart");
        }

        public ActionResult Invoice(string ono)
        {
            Customer model = new Customer();
            model.OrderNo = ono;
            try
            {
                DataSet ds = model.Invoice();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.InvoiceNo = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                    //ViewBag.CompanyName = "MSCLShopping";
                    //ViewBag.CompanyAddress = "D-54 Arjun Tower, Vibhuti Khand, Lucknow - 226010";
                    //ViewBag.Website = "www.MSCLShopping.com";
                    //ViewBag.CompnayEmail = "support@MSCLShopping.com";

                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerMobile = ds.Tables[0].Rows[0]["Contact"].ToString();
                    ViewBag.CustomerAddress = ds.Tables[0].Rows[0]["Address"].ToString();

                    ViewBag.State = ds.Tables[0].Rows[0]["StateName"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["DistrictName"].ToString();
                    ViewBag.Pin = ds.Tables[0].Rows[0]["Pincode"].ToString();

                    ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                    ViewBag.OrderDate = ds.Tables[0].Rows[0]["OrderDate"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        List<Customer> lstOrderDetails = new List<Customer>();
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Customer obj = new Customer();
                            obj.VendorName = r["Fullname"].ToString();
                            obj.AdharNo = r["AdharNo"].ToString();
                            obj.PAN = r["PAN"].ToString();
                            obj.GSTNo = r["GSTNo"].ToString();
                            obj.Address = r["Address"].ToString();
                            obj.Pincode = r["PinCode"].ToString();
                            obj.State = r["State"].ToString();
                            obj.City = r["City"].ToString();
                            obj.GSTNo = r["GSTNo"].ToString();
                          
                            obj.ProductName = r["ProductName"].ToString();
                            obj.Description = r["ProductInfo"].ToString();
                            obj.MRP = r["Rate"].ToString();
                            obj.ProductQuantity = r["Quantity"].ToString();
                            obj.TotalAmount = r["Amount"].ToString();
                            obj.Signature = r["Signature"].ToString();
                            obj.TaxableAmt = r["TaxableAmount"].ToString();
                            obj.ShippingTaxable = r["ShippingTaxable"].ToString();
                            obj.FinalAmount = r["TotalAmount"].ToString();
                            obj.RedeemPrice = r["RedeemPrice"].ToString();
                            obj.SubTotal = r["SubTotal"].ToString();
                            obj.DeliveryCharge = r["ShippingCharges"].ToString();
                            obj.AmtInwords = r["AmtInwords"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CustomerState = r["StateName"].ToString();
                            obj.InvoiceNo = r["InvoiceNo"].ToString();

                            obj.ShippingCGST = r["ShippingCGST"].ToString();
                            obj.ShippingSGST = r["ShippingSGST"].ToString();
                            obj.ShippingIGST = r["ShippingIGST"].ToString();
                            lstOrderDetails.Add(obj);
                        }
                        model.lstOrders = lstOrderDetails;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return View(model);
        }

        public ActionResult RateChangeQuickView(string productID, string colorid, string sizeid, string flavorid, string materialid,
                string ramid, string storageid, string starid, string unitid)
        {
            Customer model = new Customer();
            List<Customer> lst = new List<Customer>();
            model.ProductID = productID;

            model.ColorID = string.IsNullOrEmpty(colorid) ? null : colorid;
            model.SizeID = string.IsNullOrEmpty(sizeid) ? null : sizeid;
            model.FlavorID = string.IsNullOrEmpty(flavorid) ? null : flavorid;
            model.MaterialID = string.IsNullOrEmpty(materialid) ? null : materialid;
            model.RamID = string.IsNullOrEmpty(ramid) ? null : ramid;
            model.StorageID = string.IsNullOrEmpty(storageid) ? null : storageid;
            model.StarRatingID = string.IsNullOrEmpty(starid) ? null : starid;
            model.UnitID = string.IsNullOrEmpty(unitid) ? null : unitid;
            DataSet ds = model.QuickView();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.Result = "1";
                model.ProductID = ds.Tables[0].Rows[0]["PK_ProductID"].ToString();
                model.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                model.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                model.OfferedPrice = ds.Tables[0].Rows[0]["OfferedPrice"].ToString();
                model.OfferDiscountPerc = ds.Tables[0].Rows[0]["DiscountPerc"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                List<Customer> lstSize = new List<Customer>();

                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Customer obj = new Customer();

                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.Status = r["MappedStatus"].ToString();
                    obj.CssClass = r["Class"].ToString();
                    obj.UnitID = r["PK_UnitID"].ToString();

                    lstSize.Add(obj);
                }
                model.lstSizeMapped = lstSize;

            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                List<Customer> lstColor = new List<Customer>();

                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Customer obj = new Customer();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorCode = r["ColorCode"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.Status = r["MappedStatus"].ToString();
                    lstColor.Add(obj);
                }
                model.lstColorMapped = lstColor;

            }

            if (ds.Tables[3].Rows.Count > 0)
            {

                List<Customer> lstFlavor = new List<Customer>();
                foreach (DataRow r in ds.Tables[3].Rows)
                {
                    Customer obj = new Customer();
                    obj.FlavorID = r["FK_FlavorID"].ToString();
                    obj.FlavorName = r["FlavorName"].ToString();

                    obj.Status = r["MappedStatus"].ToString();
                    lstFlavor.Add(obj);
                }

                model.lstFlavorMapped = lstFlavor;

            }


            if (ds.Tables[5].Rows.Count > 0)
            {
                List<Customer> lstRam = new List<Customer>();
                foreach (DataRow r in ds.Tables[5].Rows)
                {
                    Customer obj = new Customer();
                    obj.RamID = r["FK_RAM_ID"].ToString();
                    obj.RamName = r["RAM"].ToString();
                    obj.Status = r["MappedStatus"].ToString();
                    lstRam.Add(obj);
                }

                model.lstRam = lstRam;

            }

            if (ds.Tables[6].Rows.Count > 0)
            {
                List<Customer> lstStorage = new List<Customer>();
                foreach (DataRow r in ds.Tables[6].Rows)
                {
                    Customer obj = new Customer();
                    obj.StorageID = r["FK_StorageID"].ToString();
                    obj.StorageName = r["Storage"].ToString();
                    obj.Status = r["MappedStatus"].ToString();
                    lstStorage.Add(obj);
                }

                model.lstStorage = lstStorage;

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePassword(Customer model)
        {

            return View(model);
        }
        
        public ActionResult UpdatePassword(string Password, string NewPassword, string ConfirmNewPassword)
        {
            Customer obj = new Customer();
            try
            {
                obj.Password = Crypto.Encrypt(Password);
                obj.NewPassword = Crypto.Encrypt(NewPassword);
                obj.UpdatedBy = Session["CustomerID"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.Result = "1";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region Complain
        public ActionResult Complains()
        {
            Customer model = new Customer();
            try
            {
                model.Fk_UserId = Session["CustomerID"].ToString();
                DataSet ds = model.GetAllComplains();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Customer> lstComplains = new List<Customer>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.EncrptNo = Crypto.Encrypt(r["PK_ComplainID"].ToString());
                        obj.PK_ComplainID = r["PK_ComplainID"].ToString();
                        obj.ComplainID = r["ComplainID"].ToString();
                        obj.ComplainDate = r["ComplainDate"].ToString();
                        obj.Issue = r["Issue"].ToString();
                        obj.ComplainStatus1 = r["Status"].ToString();
                        lstComplains.Add(obj);
                    }
                    model.lstComplains = lstComplains;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Complains")]
        [OnAction(ButtonName = "btnInsertComplain")]
        public ActionResult InsertComplain(Customer model)
        {
            try
            {
                model.Fk_UserId = Session["CustomerID"].ToString();
                model.LoginID = Session["CustomerID"].ToString();

                DataSet ds = model.InsertComplain();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Complain"] = "Your ticket has been submitted successfully. Your Ticket Number is " + ds.Tables[0].Rows[0]["ComplainID"].ToString();
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Complain"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Complain"] = ex.Message;
            }
            return RedirectToAction("Complains");
        }

        public ActionResult ComplainReply(string id)
        {
            Customer model = new Customer();
            try
            {
                List<Customer> lstResponse = new List<Customer>();
                model.PK_ComplainID = Crypto.Decrypt(id);
                DataSet ds = model.GetComplainResponseAdmin();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.PK_ComplainID = ds.Tables[0].Rows[0]["PK_ComplainID"].ToString();
                    model.ComplainID = ds.Tables[0].Rows[0]["ComplainID"].ToString();
                    model.Issue = ds.Tables[0].Rows[0]["Issue"].ToString();
                    model.ComplainDate = ds.Tables[0].Rows[0]["ComplainDate"].ToString();
                    model.ComplainStatus1 = ds.Tables[0].Rows[0]["Status"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["CustomerName"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Customer obj = new Customer();
                            obj.Reply = r["Reply"].ToString();
                            obj.ReplyPerson = r["ReplyPerson"].ToString();
                            obj.ReplyDate = r["ReplyDate"].ToString();
                            lstResponse.Add(obj);
                        }
                        model.lstComplainResponse = lstResponse;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public ActionResult ComplainReplySave(string cid, string rp)
        {
            Customer model = new Customer();
            try
            {
                model.PK_ComplainID = cid;
                model.Reply = rp;
                model.Fk_UserId = Session["CustomerID"].ToString();

                DataSet ds = model.ReplyAssociate();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Reply"] = "Reply saved successfully";
                        model.Result = "1";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Reply"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Reply"] = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult QuickView(string pid, string colorid, string sizeid, string flavorid, string materialid, string ramid, string storageid, string starid, string last)
        {
            if (pid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Customer model = new Customer();
             
            List<Customer> lst = new List<Customer>();
            model.ProductID = Crypto.Decrypt(pid);
            model.ColorID = string.IsNullOrEmpty(colorid) ? null : colorid;
            model.SizeID = string.IsNullOrEmpty(sizeid) ? null : sizeid;
            model.FlavorID = string.IsNullOrEmpty(flavorid) ? null : flavorid;
            model.MaterialID = string.IsNullOrEmpty(materialid) ? null : materialid;
            model.RamID = string.IsNullOrEmpty(ramid) ? null : ramid;
            model.StorageID = string.IsNullOrEmpty(storageid) ? null : storageid;
            model.StarRatingID = string.IsNullOrEmpty(starid) ? null : starid;
            model.Landmark = string.IsNullOrEmpty(last) ? null : last;

            DataSet ds = model.QuickViewNew();
            if (ds != null && ds.Tables.Count > 8)
            {
                model.EncryptKey = Crypto.Encrypt(ds.Tables[8].Rows[0]["PK_ProductID"].ToString());
                model.ProductID = ds.Tables[8].Rows[0]["PK_ProductID"].ToString();
                model.ProductName = ds.Tables[8].Rows[0]["ProductName"].ToString();
                model.DeliveryCharge = ds.Tables[8].Rows[0]["DeliveryCharge"].ToString();
                model.Description = ds.Tables[8].Rows[0]["Description"].ToString();
                //model.UnitID = ds.Tables[8].Rows[0]["FK_UnitID"].ToString();
                model.MRP = ds.Tables[8].Rows[0]["MRP"].ToString();
                model.OfferedPrice = ds.Tables[8].Rows[0]["OfferedPrice"].ToString();
                model.BV = ds.Tables[8].Rows[0]["BV"].ToString();
                model.SecondaryImage = ds.Tables[8].Rows[0]["ImagePath"].ToString();
                model.ShortDescription = ds.Tables[8].Rows[0]["ShortDescription"].ToString();
                model.ProductCode = ds.Tables[8].Rows[0]["ProductInfoCode"].ToString();
                model.RedeemPrice = ds.Tables[8].Rows[0]["RedeemPrice"].ToString();
                //model.OfferDiscountPerc = ds.Tables[8].Rows[0]["DiscountPerc"].ToString();
            }
            else
            {
                model.EncryptKey = Crypto.Encrypt(ds.Tables[7].Rows[0]["PK_ProductID"].ToString());
                model.ProductID = ds.Tables[7].Rows[0]["PK_ProductID"].ToString();
                model.ProductName = ds.Tables[7].Rows[0]["ProductName"].ToString();
                model.DeliveryCharge = ds.Tables[7].Rows[0]["DeliveryCharge"].ToString();
                model.Description = ds.Tables[7].Rows[0]["Description"].ToString();
                model.MRP = ds.Tables[7].Rows[0]["MRP"].ToString();
                model.OfferedPrice = ds.Tables[7].Rows[0]["OfferedPrice"].ToString();
                model.BV = ds.Tables[7].Rows[0]["BV"].ToString();
                model.SecondaryImage = ds.Tables[7].Rows[0]["ImagePath"].ToString();
                model.ShortDescription = ds.Tables[7].Rows[0]["ShortDescription"].ToString();
                model.ProductCode = ds.Tables[7].Rows[0]["ProductInfoCode"].ToString();
                model.RedeemPrice = ds.Tables[7].Rows[0]["RedeemPrice"].ToString();
                //model.OfferDiscountPerc = ds.Tables[7].Rows[0]["DiscountPerc"].ToString();
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.Result = "1";

                model.UnitID = ds.Tables[0].Rows[0]["FK_UnitID"].ToString();
                model.ColorID = ds.Tables[0].Rows[0]["FK_ColorID"].ToString();
                model.SizeID = ds.Tables[0].Rows[0]["FK_SizeID"].ToString();
                model.FlavorID = ds.Tables[0].Rows[0]["FK_FlavorID"].ToString();
                model.RamID = ds.Tables[0].Rows[0]["FK_RAM_ID"].ToString();
                model.StorageID = ds.Tables[0].Rows[0]["FK_StorageID"].ToString();
                model.SizeID = ds.Tables[0].Rows[0]["FK_SizeID"].ToString();
                model.MaterialID = ds.Tables[0].Rows[0]["Fk_MaterialId"].ToString();
              
            }
            else
            {
                model.Result = "1";
                model.UnitID = ds.Tables[7].Rows[0]["FK_UnitID"].ToString();
                model.ColorID = ds.Tables[7].Rows[0]["FK_ColorID"].ToString();
                model.SizeID = ds.Tables[7].Rows[0]["FK_SizeID"].ToString();
                model.FlavorID = ds.Tables[7].Rows[0]["FK_FlavorID"].ToString();
                model.RamID = ds.Tables[7].Rows[0]["FK_RAM_ID"].ToString();
                model.StorageID = ds.Tables[7].Rows[0]["FK_StorageID"].ToString();
                model.SizeID = ds.Tables[7].Rows[0]["FK_SizeID"].ToString();
                model.MaterialID = ds.Tables[7].Rows[0]["Fk_MaterialId"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                List<Customer> lstSize = new List<Customer>();

                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_SizeID"].ToString() == model.SizeID)
                    {
                        obj.SizeID = r["FK_SizeID"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.Status = "selectedSize";
                    }
                    else
                    {
                        obj.SizeID = r["FK_SizeID"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.Status = "";
                    }
                    lstSize.Add(obj);
                }
                model.lstSizeMapped = lstSize;
                //model.SizeID = string.IsNullOrEmpty(sizeid) ? ds.Tables[1].Rows[0]["FK_SizeID"].ToString() : sizeid;
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                List<Customer> lstColor = new List<Customer>();

                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_ColorID"].ToString() == model.ColorID)
                    {
                        obj.ColorID = r["FK_ColorID"].ToString();
                        obj.ColorCode = r["ColorCode"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.Status = "selectedColor";
                    }
                    else
                    {
                        obj.ColorID = r["FK_ColorID"].ToString();
                        obj.ColorCode = r["ColorCode"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.Status = "";
                    }

                    lstColor.Add(obj);
                }
                model.lstColorMapped = lstColor;
                //model.ColorID = string.IsNullOrEmpty(colorid) ? ds.Tables[2].Rows[0]["FK_ColorID"].ToString() : colorid;
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                List<Customer> lstflavor = new List<Customer>();

                foreach (DataRow r in ds.Tables[3].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_FlavorID"].ToString() == model.FlavorID)
                    {
                        obj.FlavorID = r["FK_FlavorID"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.Status = "selectedSize";
                    }
                    else
                    {
                        obj.FlavorID = r["FK_FlavorID"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.Status = "";
                    }
                    lstflavor.Add(obj);
                }
                model.lstFlavorMapped = lstflavor;

            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                List<Customer> lstRam = new List<Customer>();
                foreach (DataRow r in ds.Tables[4].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_RAM_ID"].ToString() == model.RamID)
                    {
                        obj.RamID = r["FK_RAM_ID"].ToString();
                        obj.RamName = r["RAM"].ToString();
                        obj.Status = "selectedRam";
                    }
                    else
                    {
                        obj.RamID = r["FK_RAM_ID"].ToString();
                        obj.RamName = r["RAM"].ToString();
                        obj.Status = "";
                    }

                    lstRam.Add(obj);
                }

                model.lstRam = lstRam;
                //model.RamID = string.IsNullOrEmpty(ramid) ? ds.Tables[5].Rows[0]["FK_RAM_ID"].ToString() : ramid;
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                List<Customer> lstStorage = new List<Customer>();
                foreach (DataRow r in ds.Tables[5].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_StorageID"].ToString() == model.StorageID)
                    {
                        obj.StorageID = r["FK_StorageID"].ToString();
                        obj.StorageName = r["Storage"].ToString();
                        obj.Status = "selectedStorage";
                    }
                    else
                    {
                        obj.StorageID = r["FK_StorageID"].ToString();
                        obj.StorageName = r["Storage"].ToString();
                        obj.Status = "";
                    }
                    lstStorage.Add(obj);
                }

                model.lstStorage = lstStorage;

            }

            if (ds.Tables[6].Rows.Count > 0)
            {
                List<Customer> lstMaterial = new List<Customer>();
                foreach (DataRow r in ds.Tables[6].Rows)
                {
                    Customer obj = new Customer();
                    if (r["Fk_MaterialId"].ToString() == model.MaterialID)
                    {
                        obj.MaterialID = r["Fk_MaterialId"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.Status = "selectedMaterial";
                    }
                    else
                    {
                        obj.MaterialID = r["Fk_MaterialId"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.Status = "";
                    }
                    lstMaterial.Add(obj);
                }
                model.lstMaterialMapped = lstMaterial;

            }

            //if (ds.Tables[8].Rows.Count > 0)
            //{
            //    model.VendorName = ds.Tables[8].Rows[0]["VendorName"].ToString();

            //    model.Fk_vendorId = ds.Tables[8].Rows[0]["Fk_vendorId"].ToString();

            //}
            DataSet dsImage = model.QuickView();
            if (ds.Tables.Count > 8)
            {
                foreach (DataRow r in ds.Tables[8].Rows)
                {
                    Customer obj = new Customer();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.SecondaryImage = r["ImagePath"].ToString();
                    lst.Add(obj);
                }
            }
            else
            {
                foreach (DataRow r in ds.Tables[7].Rows)
                {
                    Customer obj = new Customer();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.SecondaryImage = r["ImagePath"].ToString();
                    lst.Add(obj);
                }
            }
            model.lstproductimages = lst;

            #region BindVendor
            DataSet dsVendor = model.GetVendorForProduct();
            if (dsVendor != null && dsVendor.Tables[0].Rows.Count > 0)
            {
                model.VendorName = dsVendor.Tables[0].Rows[0]["VendorName"].ToString();
                model.Fk_vendorId = dsVendor.Tables[0].Rows[0]["Fk_vendorId"].ToString();

                if (dsVendor != null && dsVendor.Tables[0].Rows.Count > 1)
                {
                    List<Customer> lstVendor = new List<Customer>();
                    foreach (DataRow r in dsVendor.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.Fk_vendorId = r["Fk_vendorId"].ToString();
                        obj.VendorName = r["VendorName"].ToString();
                        lstVendor.Add(obj);
                    }
                    model.lstVendors = lstVendor;
                }
                if (dsVendor != null && dsVendor.Tables[1].Rows.Count > 0)
                {
                    List<Customer> lstcomment = new List<Customer>();
                    foreach (DataRow r in dsVendor.Tables[1].Rows)
                    {
                        Customer obj = new Customer();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.StarRating = r["StarRating"].ToString();
                        obj.Comment = r["Comment"].ToString();
                        obj.AddedOn = r["AddedOn"].ToString();
                        lstcomment.Add(obj);
                    }
                    model.lstrating = lstcomment;
                }
            }
            #endregion

            Session["PrimaryImage"] = dsVendor.Tables[2].Rows[0]["ImagePath"].ToString();
            ViewBag.Title = model.ProductName + ' ' + model.ShortDescription;
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

            #region Bind New Arrivals

            List<Customer> lstnew = new List<Customer>();

            ds = model.GetNewArrivalProducts();


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.ProductID = Crypto.Encrypt(r["Pk_ProductId"].ToString());
                    obj.ColorID = r["PK_ColorID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.ShortDescription = r["ShortDescription"].ToString();
                    obj.MRP = r["MRP"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.PrimaryImage = r["Images"].ToString();
                    obj.OfferedPrice = r["OfferedPrice"].ToString();
                    obj.SoldOutCss = r["SoldOutCss"].ToString();
                    lstnew.Add(obj);
                }
                model.lstnewarrivals = lstnew;
            }
            #endregion Bind New Arrivals
            return View(model);
        }
        public ActionResult RateProduct(string Comment, string StarRating, string productinfocode)
        {
            if (Session["CustomerID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Customer obj = new Customer();
            obj.Comment = Comment;
            obj.StarRating = StarRating;
            obj.ProductInfoCode = productinfocode;
            obj.AddedBy = Session["CustomerID"].ToString();
            DataSet ds = obj.SaveProductRating();
            obj.Result = "1";
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RateVendor(string Comment, string StarRating, string Fk_vendorId)
        {
            if (Session["CustomerID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Customer obj = new Customer();
            obj.Comment = Comment;
            obj.StarRating = StarRating;
            obj.Fk_vendorId = Fk_vendorId;
            obj.AddedBy = Session["CustomerID"].ToString();
            DataSet ds = obj.SaveVendorRating();
            obj.Result = "1";
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #region OrderExchange
        public ActionResult Exchange(string pid, string colorid, string sizeid, string flavorid, string materialid, string ramid, string storageid, string starid, string last, string od, string dd, string ot)
        {
            if (pid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Customer model = new Customer();
            List<Customer> lst = new List<Customer>();
            Session["OrderID"] = od;
            Session["OrderDetailsID"] = dd;
            Session["OrderTotal"] = ot;
            model.ProductID = Crypto.Decrypt(pid);
            model.ColorID = string.IsNullOrEmpty(colorid) ? null : colorid;
            model.SizeID = string.IsNullOrEmpty(sizeid) ? null : sizeid;
            model.FlavorID = string.IsNullOrEmpty(flavorid) ? null : flavorid;
            model.MaterialID = string.IsNullOrEmpty(materialid) ? null : materialid;
            model.RamID = string.IsNullOrEmpty(ramid) ? null : ramid;
            model.StorageID = string.IsNullOrEmpty(storageid) ? null : storageid;
            model.StarRatingID = string.IsNullOrEmpty(starid) ? null : starid;
            model.Landmark = string.IsNullOrEmpty(last) ? null : last;

            DataSet ds = model.QuickViewNew();
            if (ds != null && ds.Tables.Count > 8)
            {
                model.EncryptKey = Crypto.Encrypt(ds.Tables[8].Rows[0]["PK_ProductID"].ToString());
                model.ProductID = ds.Tables[8].Rows[0]["PK_ProductID"].ToString();
                model.ProductName = ds.Tables[8].Rows[0]["ProductName"].ToString();
                model.DeliveryCharge = ds.Tables[8].Rows[0]["DeliveryCharge"].ToString();
                model.Description = ds.Tables[8].Rows[0]["Description"].ToString();
                //model.UnitID = ds.Tables[8].Rows[0]["FK_UnitID"].ToString();
                model.MRP = ds.Tables[8].Rows[0]["MRP"].ToString();
                model.OfferedPrice = ds.Tables[8].Rows[0]["OfferedPrice"].ToString();
                model.SecondaryImage = ds.Tables[8].Rows[0]["ImagePath"].ToString();
                model.ShortDescription = ds.Tables[8].Rows[0]["ShortDescription"].ToString();
                model.ProductCode = ds.Tables[8].Rows[0]["ProductInfoCode"].ToString();
                //model.OfferDiscountPerc = ds.Tables[8].Rows[0]["DiscountPerc"].ToString();
            }
            else
            {
                model.EncryptKey = Crypto.Encrypt(ds.Tables[7].Rows[0]["PK_ProductID"].ToString());
                model.ProductID = ds.Tables[7].Rows[0]["PK_ProductID"].ToString();
                model.ProductName = ds.Tables[7].Rows[0]["ProductName"].ToString();
                model.DeliveryCharge = ds.Tables[7].Rows[0]["DeliveryCharge"].ToString();
                model.Description = ds.Tables[7].Rows[0]["Description"].ToString();
                model.MRP = ds.Tables[7].Rows[0]["MRP"].ToString();
                model.OfferedPrice = ds.Tables[7].Rows[0]["OfferedPrice"].ToString();
                model.SecondaryImage = ds.Tables[7].Rows[0]["ImagePath"].ToString();
                model.ShortDescription = ds.Tables[7].Rows[0]["ShortDescription"].ToString();
                model.ProductCode = ds.Tables[7].Rows[0]["ProductInfoCode"].ToString();
                //model.OfferDiscountPerc = ds.Tables[7].Rows[0]["DiscountPerc"].ToString();
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.Result = "1";

                model.UnitID = ds.Tables[0].Rows[0]["FK_UnitID"].ToString();
                model.ColorID = ds.Tables[0].Rows[0]["FK_ColorID"].ToString();
                model.SizeID = ds.Tables[0].Rows[0]["FK_SizeID"].ToString();
                model.FlavorID = ds.Tables[0].Rows[0]["FK_FlavorID"].ToString();
                model.RamID = ds.Tables[0].Rows[0]["FK_RAM_ID"].ToString();
                model.StorageID = ds.Tables[0].Rows[0]["FK_StorageID"].ToString();
                model.SizeID = ds.Tables[0].Rows[0]["FK_SizeID"].ToString();
                model.MaterialID = ds.Tables[0].Rows[0]["Fk_MaterialId"].ToString();
            }
            else
            {
                model.Result = "1";
                model.UnitID = ds.Tables[7].Rows[0]["FK_UnitID"].ToString();
                model.ColorID = ds.Tables[7].Rows[0]["FK_ColorID"].ToString();
                model.SizeID = ds.Tables[7].Rows[0]["FK_SizeID"].ToString();
                model.FlavorID = ds.Tables[7].Rows[0]["FK_FlavorID"].ToString();
                model.RamID = ds.Tables[7].Rows[0]["FK_RAM_ID"].ToString();
                model.StorageID = ds.Tables[7].Rows[0]["FK_StorageID"].ToString();
                model.SizeID = ds.Tables[7].Rows[0]["FK_SizeID"].ToString();
                model.MaterialID = ds.Tables[7].Rows[0]["Fk_MaterialId"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                List<Customer> lstSize = new List<Customer>();

                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_SizeID"].ToString() == model.SizeID)
                    {
                        obj.SizeID = r["FK_SizeID"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.Status = "selectedSize";
                    }
                    else
                    {
                        obj.SizeID = r["FK_SizeID"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.Status = "";
                    }
                    lstSize.Add(obj);
                }
                model.lstSizeMapped = lstSize;
                //model.SizeID = string.IsNullOrEmpty(sizeid) ? ds.Tables[1].Rows[0]["FK_SizeID"].ToString() : sizeid;
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                List<Customer> lstColor = new List<Customer>();

                foreach (DataRow r in ds.Tables[2].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_ColorID"].ToString() == model.ColorID)
                    {
                        obj.ColorID = r["FK_ColorID"].ToString();
                        obj.ColorCode = r["ColorCode"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.Status = "selectedColor";
                    }
                    else
                    {
                        obj.ColorID = r["FK_ColorID"].ToString();
                        obj.ColorCode = r["ColorCode"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.Status = "";
                    }

                    lstColor.Add(obj);
                }
                model.lstColorMapped = lstColor;
                //model.ColorID = string.IsNullOrEmpty(colorid) ? ds.Tables[2].Rows[0]["FK_ColorID"].ToString() : colorid;
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                List<Customer> lstflavor = new List<Customer>();

                foreach (DataRow r in ds.Tables[3].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_FlavorID"].ToString() == model.FlavorID)
                    {
                        obj.FlavorID = r["FK_FlavorID"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.Status = "selectedSize";
                    }
                    else
                    {
                        obj.FlavorID = r["FK_FlavorID"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.Status = "";
                    }
                    lstflavor.Add(obj);
                }
                model.lstFlavorMapped = lstflavor;

            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                List<Customer> lstRam = new List<Customer>();
                foreach (DataRow r in ds.Tables[4].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_RAM_ID"].ToString() == model.RamID)
                    {
                        obj.RamID = r["FK_RAM_ID"].ToString();
                        obj.RamName = r["RAM"].ToString();
                        obj.Status = "selectedRam";
                    }
                    else
                    {
                        obj.RamID = r["FK_RAM_ID"].ToString();
                        obj.RamName = r["RAM"].ToString();
                        obj.Status = "";
                    }

                    lstRam.Add(obj);
                }

                model.lstRam = lstRam;
                //model.RamID = string.IsNullOrEmpty(ramid) ? ds.Tables[5].Rows[0]["FK_RAM_ID"].ToString() : ramid;
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                List<Customer> lstStorage = new List<Customer>();
                foreach (DataRow r in ds.Tables[5].Rows)
                {
                    Customer obj = new Customer();
                    if (r["FK_StorageID"].ToString() == model.StorageID)
                    {
                        obj.StorageID = r["FK_StorageID"].ToString();
                        obj.StorageName = r["Storage"].ToString();
                        obj.Status = "selectedStorage";
                    }
                    else
                    {
                        obj.StorageID = r["FK_StorageID"].ToString();
                        obj.StorageName = r["Storage"].ToString();
                        obj.Status = "";
                    }
                    lstStorage.Add(obj);
                }

                model.lstStorage = lstStorage;

            }

            if (ds.Tables[6].Rows.Count > 0)
            {
                List<Customer> lstMaterial = new List<Customer>();
                foreach (DataRow r in ds.Tables[6].Rows)
                {
                    Customer obj = new Customer();
                    if (r["Fk_MaterialId"].ToString() == model.MaterialID)
                    {
                        obj.MaterialID = r["Fk_MaterialId"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.Status = "selectedMaterial";
                    }
                    else
                    {
                        obj.MaterialID = r["Fk_MaterialId"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.Status = "";
                    }
                    lstMaterial.Add(obj);
                }
                model.lstMaterialMapped = lstMaterial;

            }

            //if (ds.Tables[8].Rows.Count > 0)
            //{
            //    model.VendorName = ds.Tables[8].Rows[0]["VendorName"].ToString();

            //    model.Fk_vendorId = ds.Tables[8].Rows[0]["Fk_vendorId"].ToString();

            //}
            DataSet dsImage = model.QuickView();
            if (ds.Tables.Count > 8)
            {
                foreach (DataRow r in ds.Tables[8].Rows)
                {
                    Customer obj = new Customer();
                    obj.SecondaryImage = r["ImagePath"].ToString();
                    lst.Add(obj);
                }
            }
            else
            {
                foreach (DataRow r in ds.Tables[7].Rows)
                {
                    Customer obj = new Customer();
                    obj.SecondaryImage = r["ImagePath"].ToString();
                    lst.Add(obj);
                }
            }
            model.lstproductimages = lst;

            #region BindVendor
            DataSet dsVendor = model.GetVendorForProduct();
            if (dsVendor != null && dsVendor.Tables[0].Rows.Count > 0)
            {
                model.VendorName = dsVendor.Tables[0].Rows[0]["VendorName"].ToString();
                model.Fk_vendorId = dsVendor.Tables[0].Rows[0]["Fk_vendorId"].ToString();

                if (dsVendor != null && dsVendor.Tables[0].Rows.Count > 1)
                {
                    List<Customer> lstVendor = new List<Customer>();
                    foreach (DataRow r in dsVendor.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.Fk_vendorId = r["Fk_vendorId"].ToString();
                        obj.VendorName = r["VendorName"].ToString();
                        lstVendor.Add(obj);
                    }
                    model.lstVendors = lstVendor;
                }
                if (dsVendor != null && dsVendor.Tables[1].Rows.Count > 0)
                {
                    List<Customer> lstcomment = new List<Customer>();
                    foreach (DataRow r in dsVendor.Tables[1].Rows)
                    {
                        Customer obj = new Customer();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.StarRating = r["StarRating"].ToString();
                        obj.Comment = r["Comment"].ToString();
                        obj.AddedOn = r["AddedOn"].ToString();
                        lstcomment.Add(obj);
                    }
                    model.lstrating = lstcomment;
                }
            }
            #endregion

            Session["PrimaryImage"] = dsVendor.Tables[2].Rows[0]["ImagePath"].ToString();
            ViewBag.Title = model.ProductName + ' ' + model.ShortDescription;

            return View(model);
        }
        public ActionResult ConfirmExchange(string pcd, string qty, string rt, string sc, string vd)
        {
            Customer model = new Customer();
            try
            {
                model.CustomerID = Session["CustomerID"].ToString();
                model.PK_OrderID = Session["OrderID"].ToString();
                model.OrderDetailsID = Session["OrderDetailsID"].ToString();
                model.ProductInfoCode = pcd;
                model.ProductQuantity = qty;
                model.Rate = rt;
                model.DeliveryCharge = sc;
                model.Fk_vendorId = vd;

                DataSet ds = model.SaveExchangeDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "1";
                        TempData["Exchange"] = "Exchange request has been saved";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        TempData["Exchange"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Exchange"] = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("MyOrders");
        }
        public ActionResult ValidateExchangeAmount(string xcamt)
        {
            Customer model = new Customer();
            if (decimal.Parse(xcamt) > decimal.Parse(Session["OrderTotal"].ToString()))
            {
                model.Result = "1";
                model.DiscountAmount = xcamt;
                model.TotalAmount = Session["OrderTotal"].ToString();
            }
            else
            {
                model.Result = "0";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult CreateOrder(string Amount)
        {
            CreateOrders obj = new CreateOrders();

            obj.amount = (decimal.Parse(Amount) * 100).ToString();
            CreateOrderResponses obj1 = new CreateOrderResponses();
            string random = Common.GenerateRandom();
            try
            {

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", obj.amount); // amount in the smallest currency unit
                options.Add("receipt", random);
                options.Add("currency", "INR");
                options.Add("payment_capture", "1");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                Razorpay.Api.Order order = client.Order.Create(options);
                obj1.OrderId = order["id"].ToString();
                obj1.Status = "0";
                obj.OrderId = order["id"].ToString();
                obj.Pk_UserId = Session["CustomerID"].ToString();
                obj.TransactionType = "Wallet Web";
                obj.Type = "Card";
                DataSet ds = obj.SaveOrderDetails();
            
                Session["OrderId"] = order["id"].ToString();
                Session["Amount"] = obj.amount;

            }
            catch (Exception ex)
            {
                obj1.Status = "1";
                obj1.ErrorMessage = ex.Message;
            }
            
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public static HttpWebRequest GetCreateOrderURL()
        {
            var url = PaymentGateWayDetails.CreateOrder;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        public ActionResult Payment()
        {
            Customer obj = new Customer();
  
            obj.TotalAmount = Session["Amount"].ToString();
            obj.OrderNo = Session["OrderId"].ToString();
            obj.Name = Session["CustomerName"].ToString();
            obj.ContactNo = Session["Contact"].ToString();
            obj.EmailId = Session["EmailId"].ToString();
            return View(obj);
        }
        public ActionResult PaymentSuccess()
        {
             return RedirectToAction("Cart");
        }
    }
}
