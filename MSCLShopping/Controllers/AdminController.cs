using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSCLShopping.Models;
using MSCLShopping.Filter;

namespace MSCLShopping.Controllers
{
    public class AdminController : AdminBaseController
    {
        #region VerifyVendorDetails
        public ActionResult VendorList(Vendor model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("VendorList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(Vendor model)
        {
            List<Vendor> lst = new List<Vendor>();
            DataSet ds = model.VendorList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Vendor obj = new Vendor();
                    obj.VendorID = r["PK_UserID"].ToString();
                    obj.FirstName = r["FullName"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.Pincode = r["Pincode"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.PAN = r["PAN"].ToString();
                    obj.GSTNo = r["GSTNo"].ToString();
                    obj.AdharNo = r["AdharNo"].ToString();
                    obj.LoginId = r["Username"].ToString();
                    obj.Password = r["Password"].ToString();
                    obj.AccountNumber = r["AccountNumber"].ToString();
                    obj.AccountHolderName = r["AccountHolderName"].ToString();
                    obj.Signature = r["Signature"].ToString();
                    obj.isAdharVerified = r["isAdharVerified"].ToString();
                    obj.isPANVerified = r["isPANVerified"].ToString();
                    obj.isGSTINVerified = r["isGSTINVerified"].ToString();
                    obj.isAccountVerified = r["isAccountVerified"].ToString();
                    obj.isSignatureVerified = r["isSignatureVerified"].ToString();
                    obj.isDocumentVerified = r["IsDocumentVerified"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserID"].ToString());
                    lst.Add(obj);
                }
                model.List = lst;
            }

            return View(model);
        }

        
        public ActionResult VerifyVendorDocuments(string doctype, string status, string vid)
        {
            Vendor model = new Vendor();
            try
            {
                model.VendorID = vid;
                model.DocumentType = doctype;
                model.VerificationStatus = status;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.VerifyVendorDocuments();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "1";
                        model.isAdharVerified = ds.Tables[0].Rows[0]["isAdharVerified"].ToString();
                        model.isPANVerified = ds.Tables[0].Rows[0]["isPANVerified"].ToString();
                        model.isGSTINVerified = ds.Tables[0].Rows[0]["isGSTINVerified"].ToString();
                        model.isAccountVerified = ds.Tables[0].Rows[0]["isAccountVerified"].ToString();
                        model.isSignatureVerified = ds.Tables[0].Rows[0]["isSignatureVerified"].ToString();
                        model.isDocumentVerified = ds.Tables[0].Rows[0]["IsDocumentVerified"].ToString();
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

        #region BrandApproval
        public ActionResult BrandApproval(Vendor model)
        {
            List<SelectListItem> lstStatus = Common.lstBrandStatus();
            ViewBag.Status = lstStatus;
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

                DataSet ds = model.TrackRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Vendor> lstRequests = new List<Vendor>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Vendor obj = new Vendor();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.BrandLogo = r["BrandLogo"].ToString();
                        obj.WebsiteLink = r["WebsiteLink"].ToString();
                        obj.DocumentType = r["DocumentType"].ToString();
                        obj.DocumentPath = r["DocumentPath"].ToString();
                        obj.AddedOn = r["RequestDate"].ToString();
                        obj.Status = r["BrandStatus"].ToString();
                        lstRequests.Add(obj);
                    }
                    model.lstRequests = lstRequests;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("BrandApproval")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SearchBrandRequests(Vendor model)
        {
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

                DataSet ds = model.TrackRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Vendor> lstRequests = new List<Vendor>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Vendor obj = new Vendor();
                        obj.RequestCode = r["RequestCode"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.BrandLogo = r["BrandLogo"].ToString();
                        obj.WebsiteLink = r["WebsiteLink"].ToString();
                        obj.DocumentType = r["DocumentType"].ToString();
                        obj.DocumentPath = r["DocumentPath"].ToString();
                        obj.AddedOn = r["RequestDate"].ToString();
                        obj.Status = r["BrandStatus"].ToString();
                        lstRequests.Add(obj);
                    }
                    model.lstRequests = lstRequests;
                }
            }
            catch (Exception ex)
            {

            }
            List<SelectListItem> lstStatus = Common.lstBrandStatus();
            ViewBag.Status = lstStatus;
            return View(model);
        }
        public ActionResult ApproveBrandrequest(string rcode)
        {
            try
            {
                Vendor model = new Vendor();
                model.RequestCode = rcode;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApproveBrandRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["BrandApproval"] = "Brand Approved";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["BrandApproval"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["BrandApproval"] = ex.Message;
            }
            return RedirectToAction("BrandApproval");
        }
        #endregion

        public ActionResult Dashboard()
        {
            Admin model = new Admin();
            try
            {

                DataSet Ds = model.GetDetails();
                ViewBag.Products = Ds.Tables[0].Rows[0]["Products"].ToString();
                ViewBag.Offers = Ds.Tables[0].Rows[0]["Offers"].ToString();
               // ViewBag.Vendors = Ds.Tables[0].Rows[0]["Plots"].ToString();
                ViewBag.Customers = Ds.Tables[0].Rows[0]["Customers"].ToString();
				ViewBag.Invoices = Ds.Tables[0].Rows[0]["Invoices"].ToString();
                List<Admin> lst = new List<Admin>();
                DataSet dsblock = model.GetList();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsblock.Tables[0].Rows)
                    {
                        Admin obj = new Admin();
                        obj.OfferID = r["PK_OfferID"].ToString();
                        obj.OfferName = r["OfferName"].ToString();
                        obj.FromDate = r["FromDate"].ToString();
                        obj.ToDate = r["ToDate"].ToString();
                        obj.OfferStatus = r["OfferStatus"].ToString();
                       


                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lst.Add(obj);
                    }

                    model.List = lst;
                }

            }


            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult VendorDashboard()
        {
            Admin model = new Admin();
            try 
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet Ds = model.GetDetailsVendor();
                ViewBag.Products = Ds.Tables[0].Rows[0]["Products"].ToString();
               
                List<Admin> lst = new List<Admin>();
              
            }


            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult GetCustomerList(Customer model)
        {
            List<Customer> lst = new List<Customer>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? Common.ConvertToSystemDate(DateTime.Now.ToString("dd/MM/yyyy"),"dd/MM/yyyy") : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? Common.ConvertToSystemDate(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy") : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet dsblock = model.GetCustomerList();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    Customer obj = new Customer();

                    obj.Password =Crypto.Decrypt( r["Password"].ToString());
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.CustomerID= r["PK_CustomerID"].ToString();
                    lst.Add(obj);
                }

                model.lstcustomer = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("GetCustomerList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetCustomerListBy(Customer model)
        {
            List<Customer> lst = new List<Customer>();
            DataSet dsblock = model.GetCustomerList();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.CustomerID = r["PK_CustomerID"].ToString();
                    lst.Add(obj);
                }

                model.lstcustomer = lst;
            }
            return View(model);
        }
        public ActionResult DeleteCustomer(string id)
        {
            try
            {
                Customer obj = new Customer();
                obj.CustomerID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteCustomer();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["CustomerD"] = "Customer deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["CustomerD"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["CustomerD"] = ex.Message;
            }
            return RedirectToAction("GetCustomerList");
        }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public ActionResult DeleteVendor(string id)
        {
            try
            {
                Vendor obj = new Vendor();
                obj.VendorID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteVendor();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Vendor"] = "Vendor deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            } 
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Vendor"] = ex.Message;
            }
            return RedirectToAction("VendorList");
        }



        public ActionResult VendorRegistration(Vendor obj, string VendorID)
        {
            obj.Fk_UserId = (VendorID);
            DataSet ds = obj.EditVendorList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.VendorID = ds.Tables[0].Rows[0]["PK_UserID"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                obj.GSTNo = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                obj.AdharNo = ds.Tables[0].Rows[0]["AdharNo"].ToString();
                obj.LoginId = ds.Tables[0].Rows[0]["Username"].ToString();
                obj.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                obj.AccountHolderName = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                obj.Signature = ds.Tables[0].Rows[0]["Signature"].ToString();
                obj.isAdharVerified = ds.Tables[0].Rows[0]["isAdharVerified"].ToString();
                obj.isPANVerified = ds.Tables[0].Rows[0]["isPANVerified"].ToString();
                obj.isGSTINVerified = ds.Tables[0].Rows[0]["isGSTINVerified"].ToString();
                obj.isAccountVerified = ds.Tables[0].Rows[0]["isAccountVerified"].ToString();
                obj.isSignatureVerified = ds.Tables[0].Rows[0]["isSignatureVerified"].ToString();
                obj.isDocumentVerified = ds.Tables[0].Rows[0]["IsDocumentVerified"].ToString();
                obj.EncryptKey = Crypto.Encrypt(ds.Tables[0].Rows[0]["PK_UserID"].ToString());
            }
            return View(obj);
        }



        [HttpPost]
        [ActionName("VendorRegistration")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateVendor(Vendor obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateVendor();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Vendor"] = "Vendor updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Vendor"] = ex.Message;
            }
            return RedirectToAction("VendorRegistration", "Admin");
        }





    }
}
