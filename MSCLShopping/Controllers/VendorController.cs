using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSCLShopping.Filter;
using MSCLShopping.Models;
using System.IO;
using System.Net.Mail;

namespace MSCLShopping.Controllers
{
    public class VendorController : VendorBaseController
    {
        public ActionResult Dashboard()
        {
            int count = 0, pancount = 0,gstcount = 0,emailcount=0,acountcount=0,signaturecount=0 ;
            Vendor model = new Vendor();
            try
            {
                model.VendorID = Session["Pk_VendorId"].ToString();
                DataSet ds = model.Dashboard();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.StoreName = ds.Tables[0].Rows[0]["StoreName"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNo"].ToString();
                    model.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                    model.GSTNo = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.AccountHolderName = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                    model.AccountNumber = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                    if(ds.Tables[0].Rows[0]["isAdharVerified"].ToString()== "Approved")
                    {
                        count = 10;
                    }
                    else
                    {
                        count = 0;
                    }
                    if (ds.Tables[0].Rows[0]["isPANVerified"].ToString() == "Approved")
                    {
                        pancount = 10;
                    }
                    else
                    {
                        pancount = 0;
                    }
                    if (ds.Tables[0].Rows[0]["isEmailVerified"].ToString() == "Approved")
                    {
                        emailcount = 10;
                    }
                    else
                    {
                        emailcount = 0;
                    }
                    if (ds.Tables[0].Rows[0]["isAccountVerified"].ToString() == "Approved")
                    {
                        acountcount = 20;
                    }
                    else
                    {
                        acountcount = 0;
                    }
                    if (ds.Tables[0].Rows[0]["isSignatureVerified"].ToString() == "Approved")
                    {
                        signaturecount = 20;
                    }
                    else
                    {
                        signaturecount = 0;
                    }
                    if (ds.Tables[0].Rows[0]["isGSTINVerified"].ToString() == "Approved")
                    {
                        gstcount = 30;
                    }
                    else
                    {
                        gstcount = 0;
                    }
                    ViewBag.Finalcount = count + pancount + gstcount + emailcount + acountcount + signaturecount;
                    ViewBag.RemainingCount = (100) - (count + pancount + gstcount + emailcount + acountcount + signaturecount);
                    Session["IsDocumentVerified"]=ds.Tables[0].Rows[0]["IsDocumentVerified"].ToString();
                    ViewBag.isAdharVerified = ds.Tables[0].Rows[0]["isAdharVerified"].ToString();
                    ViewBag.isPANVerified = ds.Tables[0].Rows[0]["isPANVerified"].ToString();
                    ViewBag.isGSTINVerified = ds.Tables[0].Rows[0]["isGSTINVerified"].ToString();
                    ViewBag.isEmailVerified = ds.Tables[0].Rows[0]["isEmailVerified"].ToString();
                    ViewBag.isAccountVerified = ds.Tables[0].Rows[0]["isAccountVerified"].ToString();
                    ViewBag.isSignatureVerified = ds.Tables[0].Rows[0]["isSignatureVerified"].ToString();
                    ViewBag.CssClass= ds.Tables[0].Rows[0]["CssClass"].ToString();
                    ViewBag.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ViewBag.Emaild = ds.Tables[0].Rows[0]["Emaild"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        #region VendorProfile
        
        public ActionResult VendorProfile()
        {
            Vendor model = new Vendor();
            try
            {
                model.VendorID = Session["Pk_VendorId"].ToString();
                DataSet ds = model.Dashboard();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNo"].ToString();
                    model.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                    model.GSTNo = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.AccountHolderName = ds.Tables[0].Rows[0]["AccountHolderName"].ToString();
                    model.AccountNumber = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                    model.Signature = ds.Tables[0].Rows[0]["Signature"].ToString();
                    ViewBag.isAdharVerified = ds.Tables[0].Rows[0]["isAdharVerified"].ToString();
                    ViewBag.isPANVerified = ds.Tables[0].Rows[0]["isPANVerified"].ToString();
                    ViewBag.isGSTINVerified = ds.Tables[0].Rows[0]["isGSTINVerified"].ToString();
                    ViewBag.isEmailVerified = ds.Tables[0].Rows[0]["isEmailVerified"].ToString();
                    ViewBag.isAccountVerified = ds.Tables[0].Rows[0]["isAccountVerified"].ToString();
                    ViewBag.isSignatureVerified = ds.Tables[0].Rows[0]["isSignatureVerified"].ToString();
                    ViewBag.CssClass = ds.Tables[0].Rows[0]["CssClass"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public ActionResult VendorProfileUpdate(Vendor model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Signature = "../images/Signature/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Signature)));
                }
                model.AddedBy = Session["Pk_VendorId"].ToString();
                DataSet ds = model.UpdateVendor();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["UpdateVendorProfile"] = "Details updated";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["UpdateVendorProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["UpdateVendorProfile"] = ex.Message;
            }
            return RedirectToAction("VendorProfile");
        }
        #endregion

        #region List-Update-Vendor
        

        public ActionResult VendorRegistration(Vendor obj,string VendorID)
        {
            obj.Fk_UserId =(VendorID);
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
        [OnAction(ButtonName = "Updates")]
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
            return RedirectToAction("VendorRegistration", "Vendor");
        }

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

        #endregion

        #region BrandRequest
        public ActionResult BrandRequest()
        {
            List<SelectListItem> lstBrandOwner = Common.lstBrandOwner();
            ViewBag.BrandOwner = lstBrandOwner;
            return View();
        }
        [HttpPost]
        [ActionName("BrandRequest")]
        [OnAction(ButtonName="btnSaverequest")]
        public ActionResult SaveBrandRequest(Vendor model, IEnumerable<HttpPostedFileBase> postedFile)
        {
            List<SelectListItem> lstBrandOwner = Common.lstBrandOwner();
            ViewBag.BrandOwner = lstBrandOwner;
            int count = 0;
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        if (count == 0)
                        {
                            model.BrandLogo = "../images/BrandImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(model.BrandLogo)));
                        }
                        if (count == 1)
                        {
                            model.DocumentPath = "../images/BrandImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath(model.DocumentPath)));
                        }
                    }
                    count++;
                }
                model.VendorID = Session["Pk_VendorId"].ToString();
                DataSet ds = model.SaveBrandRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["BrandRequestOrder"] = "Request saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["BrandRequestOrder"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["BrandRequestOrder"] = ex.Message;
            }
            return RedirectToAction("TrackRequest");
        }
        public ActionResult TrackRequest(Vendor model)
        {
            List<SelectListItem> lstStatus = Common.lstBrandStatus();
            ViewBag.Status = lstStatus;
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.LoginId = Session["LoginId"].ToString();

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
                        obj.AddedOn = r["RequestDate"].ToString();
                        obj.Status= r["BrandStatus"].ToString();
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
        #endregion

        public ActionResult CheckStoreName(string sname)
        {
            Vendor model = new Vendor();
            try
            {
                model.StoreName = sname;
                DataSet ds = model.CheckStoreName();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = "0";
                        model.Status = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    model.Result = "1";
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveStoreName(string sname)
        {
            Vendor model = new Vendor();
            try
            {
                model.StoreName = sname;
                model.VendorID = Session["Pk_VendorId"].ToString();

                DataSet ds = model.SaveStoreName();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "1";
                        model.Status = "Store Name updated successfully";
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
                model.Result = "0";
                model.Status = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductsNew()
        {

            return View();
        }

        public ActionResult ProductList(Master model)
        {
            #region MainCategory
            Master obj1 = new Master();
            int count = 0;
            List<SelectListItem> ddlMainCategory = new List<SelectListItem>();
            DataSet ds1 = obj1.MainCategoryList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMainCategory.Add(new SelectListItem { Text = "Select Main Category", Value = "0" });
                    }
                    ddlMainCategory.Add(new SelectListItem { Text = r["MainCategoryName"].ToString(), Value = r["PK_MainCategoryID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlMainCategory = ddlMainCategory;

            #endregion


            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            return View(model);
        }
        [HttpPost]
        [ActionName("ProductList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetProductList(Master model)
        {
            if (Session["UserType"].ToString() == "Admin")
            {
                model.AddedBy = null;
            }
            else
            {
                model.AddedBy = Session["Pk_VendorId"].ToString();
            }

            List<Master> lst = new List<Master>();

            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
            
            DataSet ds = model.ProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.MRP = r["MRP"].ToString();
                    obj.PrimaryImage = r["PrimaryImage"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.FlavorName = r["FlavorName"].ToString();
                    obj.MaterialName = r["MaterialName"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                    obj.RAM = r["RAM"].ToString();
                    obj.Storage = r["Storage"].ToString();

                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.FlavorID = r["FK_FlavorID"].ToString();
                    obj.MaterialID = r["FK_MaterialID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();

                    obj.Status = r["Status"].ToString();
                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }


            #region MainCategory
            Master obj1 = new Master();
            int count3 = 0;
            List<SelectListItem> ddlMainCategory = new List<SelectListItem>();
            DataSet ds3 = obj1.MainCategoryList();
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlMainCategory.Add(new SelectListItem { Text = "Select Main Category", Value = "0" });
                    }
                    ddlMainCategory.Add(new SelectListItem { Text = r["MainCategoryName"].ToString(), Value = r["PK_MainCategoryID"].ToString() });
                    count3 = count3 + 1;
                }
            }

            ViewBag.ddlMainCategory = ddlMainCategory;

            #endregion

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;
            return View(model);
        }

        public ActionResult DeleteProduct(string id)
        {
            try
            {
                Master obj = new Master();
                obj.ProductID = id;
                obj.AddedBy = Session["Pk_VendorId"].ToString();
                DataSet ds = obj.DeleteProduct();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["ProductList"] = "Product deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["ProductList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["ProductList"] = ex.Message;
            }
            return RedirectToAction("ProductList");
        }


        #region ManualStockEntry

        public ActionResult ManualStockEntry(Master model)
        {

            #region MainCategory
            Master obj1 = new Master();
            int count = 0;
            List<SelectListItem> ddlMainCategory = new List<SelectListItem>();
            DataSet ds1 = obj1.MainCategoryList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMainCategory.Add(new SelectListItem { Text = "Select Main Category", Value = "0" });
                    }
                    ddlMainCategory.Add(new SelectListItem { Text = r["MainCategoryName"].ToString(), Value = r["PK_MainCategoryID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlMainCategory = ddlMainCategory;

            #endregion

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            return View(model);
        }

        [HttpPost]
        [ActionName("ManualStockEntry")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList1(Master model)
        {
            List<Master> lst = new List<Master>();
            model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;

            model.AddedBy = Session["Pk_VendorId"].ToString();
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.MainCategoryID = r["FK_MainCategory"].ToString();
                    obj.CategoryID = r["FK_CategoryID"].ToString();
                    obj.SubCategoryID = r["FK_SubCategoryID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.Quantity = r["CreditQuantity"].ToString();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.RAM = r["RAM"].ToString();
                    obj.Storage = r["Storage"].ToString();
                    obj.FlavorID = r["FK_FlavorID"].ToString();
                    obj.FlavorName = r["FLavorName"].ToString();
                    obj.MaterialID = r["FK_MaterialID"].ToString();
                    obj.MaterialName = r["MaterialName"].ToString();
                    obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                    obj.ProductOtherInfoID = r["PK_ProductOtherInfoID"].ToString();
                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }
            #region MainCategory
            Master obj1 = new Master();
            int count = 0;
            List<SelectListItem> ddlMainCategory = new List<SelectListItem>();
            DataSet ds1 = obj1.MainCategoryList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMainCategory.Add(new SelectListItem { Text = "Select Main Category", Value = "0" });
                    }
                    ddlMainCategory.Add(new SelectListItem { Text = r["MainCategoryName"].ToString(), Value = r["PK_MainCategoryID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlMainCategory = ddlMainCategory;

            #endregion
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;
            return View(model);
        }

        [HttpPost]
        [ActionName("ManualStockEntry")]
        [OnAction(ButtonName = "Add")]
        public ActionResult AddStock(Master model)
        {
            #region MainCategory
            Master obj1 = new Master();
            int count = 0;
            List<SelectListItem> ddlMainCategory = new List<SelectListItem>();
            DataSet ds1 = obj1.MainCategoryList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMainCategory.Add(new SelectListItem { Text = "Select Main Category", Value = "0" });
                    }
                    ddlMainCategory.Add(new SelectListItem { Text = r["MainCategoryName"].ToString(), Value = r["PK_MainCategoryID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlMainCategory = ddlMainCategory;

            #endregion
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;
            try
            {
                if (Request["hdRows"] == null)
                {
                    TempData["Product"] = "Please search product first.";
                    return View(model);

                }
                string noofrows = Request["hdRows"].ToString();
                string otherinfoid = "";
                string infococode = "";
                string qty = "";
                //string prodid = "";
                //string Sizeid = "";
                //string unitid = "";
                //string colorid = "";
                //string flavorid = "";
                //string materialid = "";


                DataTable dtst = new DataTable();
                //dtst.Columns.Add("FK_ProductID", typeof(string));
                //dtst.Columns.Add("FK_UnitID", typeof(string));
                //dtst.Columns.Add("FK_SizeID", typeof(string));
                dtst.Columns.Add("Fk_ProductOtherInfoId", typeof(string));
                dtst.Columns.Add("ProductInfoCode", typeof(string));
                dtst.Columns.Add("Quantity", typeof(string));
                //dtst.Columns.Add("ColorID", typeof(string));
                //dtst.Columns.Add("FK_FlavorID", typeof(string));
                //dtst.Columns.Add("FK_MaterialID", typeof(string));
                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {

                    qty = Request["txtQty_ " + i].ToString();
                    if (qty != null && qty != "0" && qty != "")
                    {
                        infococode = Request["infocodeid_ " + i].ToString();
                        otherinfoid = Request["prodotferinfoid_ " + i].ToString();
                        //unitid = Request["unitid_ " + i].ToString();
                        //Sizeid = Request["sizeid_ " + i].ToString();
                        //colorid = Request["colorid_ " + i].ToString();
                        //flavorid = Request["flavorid_ " + i].ToString();
                        //materialid = Request["materialid_ " + i].ToString();

                        dtst.Rows.Add(otherinfoid, infococode, qty);

                    }
                }
                model.AddedBy = Session["Pk_VendorId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.AddQty();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Product"] = " Quantity added successfully !";

                    }
                    else
                    {
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["Product"] = ex.Message;
            }

          
            return View(model);
        }

        [HttpPost]
        [ActionName("ManualStockEntry")]
        [OnAction(ButtonName = "Deduct")]
        public ActionResult DeductStock(Master model)
        {
            #region MainCategory
            Master obj1 = new Master();
            int count = 0;
            List<SelectListItem> ddlMainCategory = new List<SelectListItem>();
            DataSet ds1 = obj1.MainCategoryList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMainCategory.Add(new SelectListItem { Text = "Select Main Category", Value = "0" });
                    }
                    ddlMainCategory.Add(new SelectListItem { Text = r["MainCategoryName"].ToString(), Value = r["PK_MainCategoryID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlMainCategory = ddlMainCategory;

            #endregion
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;
            try
            {
                if (Request["hdRows"] == null)
                {
                    TempData["Product"] = "Please search product first.";
                    return View(model);

                }

                string noofrows = Request["hdRows"].ToString();
                string otherinfoid = "";
                string infococode = "";
                string qty = "";
                //string prodid = "";
                //string Sizeid = "";
                //string unitid = "";
                //string colorid = "";
                //string flavorid = "";
                //string materialid = "";


                DataTable dtst = new DataTable();
                //dtst.Columns.Add("FK_ProductID", typeof(string));
                //dtst.Columns.Add("FK_UnitID", typeof(string));
                //dtst.Columns.Add("FK_SizeID", typeof(string));
                dtst.Columns.Add("Fk_ProductOtherInfoId", typeof(string));
                dtst.Columns.Add("ProductInfoCode", typeof(string));
                dtst.Columns.Add("Quantity", typeof(string));
                //dtst.Columns.Add("ColorID", typeof(string));
                //dtst.Columns.Add("FK_FlavorID", typeof(string));
                //dtst.Columns.Add("FK_MaterialID", typeof(string));
                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {

                    qty = Request["txtQty_ " + i].ToString();
                    if (qty != null && qty != "0" && qty != "")
                    {
                        infococode = Request["infocodeid_ " + i].ToString();
                        otherinfoid = Request["prodotferinfoid_ " + i].ToString();
                        //unitid = Request["unitid_ " + i].ToString();
                        //Sizeid = Request["sizeid_ " + i].ToString();
                        //colorid = Request["colorid_ " + i].ToString();
                        //flavorid = Request["flavorid_ " + i].ToString();
                        //materialid = Request["materialid_ " + i].ToString();

                        dtst.Rows.Add(otherinfoid, infococode, qty);

                    }
                }
                model.AddedBy = Session["Pk_VendorId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.DeductQty();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Product"] = " Quantity deducted successfully !";

                    }
                    else
                    {
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

           
            return View(model);
        }

        #endregion

        public ActionResult ProductsQuantityRequest(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_VendorId"].ToString();
                DataSet ds = model.ProductsAssignedByAdmin();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.Storage = r["Storage"].ToString();


                        lst.Add(obj);
                    }
                    model.lstProduct = lst;

                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        [ActionName("ProductsQuantityRequest")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ProductsQuantityRequestBy(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
             
                DataSet ds = model.ProductsAssignedByAdmin();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.Storage = r["Storage"].ToString();


                        lst.Add(obj);
                    }
                    model.lstProduct = lst;

                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        [ActionName("ProductsQuantityRequest")]
        [OnAction(ButtonName = "Save")]
        public ActionResult RequestQuantity(Master model)
        {
            try
            {
                if (Request["hdrows"] != null)
                {


                    string noofrows = Request["hdrows"].ToString();



                    int p = 0;
                    for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                    {
                        if (Request["txtqty " + i].ToString() != "")
                        {
                            model.AddedBy = Session["Pk_VendorId"].ToString();
                            model.ProductInfoCode = Request["ProductInfoCode " + i].ToString();
                            model.ProductID = Request["ProductID " + i].ToString();
                            model.DebQuantity = Request["txtqty " + i].ToString();
                            DataSet ds = model.RequestQuantity();
                            p = p + 1;
                        }

                    }
                    if (p > 0)
                    {
                        TempData["ProductToVendor"] = " Product requested successfully !";
                    }

                }
                else
                {
                    TempData["ProductToVendor"] = "Please select atleast one row";
                }
            }
            catch (Exception ex)
            {

                TempData["ProductToVendor"] = ex.Message;
            }

            return RedirectToAction("ProductsQuantityRequest");
        }


        public ActionResult ProductLedger(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_VendorId"].ToString();
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.ProductsAssignedByAdminLedger();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.Storage = r["Storage"].ToString();
                        obj.DebQuantity = r["DebitQuantity"].ToString();
                        obj.Quantity = r["CreditQuantity"].ToString();
                        obj.BalanceQuantity = r["Balance"].ToString();
                        obj.ToDate = r["Transactiondate"].ToString();
                        lst.Add(obj);
                    }
                    model.lstProduct = lst;

                }

                return View(model);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        #region OrderList
        public ActionResult CustomerOrders(CustomerManagement model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("CustomerOrders")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(CustomerManagement model)
        {
            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.AddedBy = Session["Pk_VendorId"].ToString();
            DataSet ds = model.OrderList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.OrderID = r["PK_OrderID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.OrderNO = r["OrderNO"].ToString();
                    obj.OrderDate = r["OrderDate"].ToString();
                    obj.TotalAmount = r["TotalAmount"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.Address = r["Address"].ToString();

                    obj.Mobile = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.List = lst;
            }

            return View(model);
        }


        public ActionResult Details(string id)
        {

            CustomerManagement model = new CustomerManagement();
            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderID = id;
            DataSet dsblock = model.FillDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "yes";
                    model.OrderID = dsblock.Tables[0].Rows[0]["PK_OrderID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                    model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                    model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                    model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();
                    model.Mobile = dsblock.Tables[0].Rows[0]["Contact"].ToString();

                    {
                        foreach (DataRow r in dsblock.Tables[1].Rows)
                        {
                            CustomerManagement obj = new CustomerManagement();
                            obj.ProductID = r["FK_ProductID"].ToString();
                            obj.SizeID = r["FK_SizeID"].ToString();
                            obj.UnitID = r["FK_UnitID"].ToString();
                            obj.Quantity = r["Quantity"].ToString();
                            obj.Rate = r["Rate"].ToString();
                            obj.TotalAmount = r["Amount"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.SizeName = r["SizeName"].ToString();
                            obj.UnitName = r["UnitName"].ToString();
                            obj.ColorID = r["FK_ColorID"].ToString();
                            obj.ColorName = r["ColorName"].ToString();
                            obj.OrderStatus = r["OrderStatus"].ToString();
                            lst.Add(obj);
                        }
                        model.List = lst;
                    }

                }
                else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return View(model);
        }

        #endregion

        #region UpdateOrderStatus-Packed

        public ActionResult UpdateOrderStatus(CustomerManagement model)
        {
            //#region ddlOrderStatus
            //List<SelectListItem> ddlOrderStatus = Common.BindOrderStatus();
            //ViewBag.ddlOrderStatus = ddlOrderStatus;
            //#endregion ddlOrderStatus

            return View(model);
        }

        [HttpPost]
        [ActionName("UpdateOrderStatus")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetOrderList(CustomerManagement model)
        {

            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderStatus = "Placed";
            model.AddedBy = Session["Pk_VendorId"].ToString();
            DataSet dsblock = model.GetDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {





                model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();



                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.ProductID = r["FK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.TotalAmount = r["Amount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();

                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();



                    lst.Add(obj);
                }
                model.List = lst;




            }
            else
            {
                model.Result = "No record found !";
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("UpdateOrderStatus")]
        [OnAction(ButtonName = "Update")]
        public ActionResult Update(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();
                string chk = "";
                string id = "";
                string status = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("OrderDetailsID", typeof(string));
                dtst.Columns.Add("OrderStatus", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        obj.OrderStatus = "Packed";
                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.AddedBy = Session["Pk_VendorId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.UpdateOrder();

                        try
                        {
                            string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", Your Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " containing " + ds.Tables[0].Rows[0]["ProductName"].ToString() +
                                " has been " + ds.Tables[0].Rows[0]["OrderStatus"].ToString() + ". Expected Delivery : " + ds.Tables[0].Rows[0]["ExpectedDelivery"].ToString() + ".";
                            string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                            BLSMS.SendSMSNew(mobile, message);
                        }
                        catch { }

                    }
                }
                TempData["Class"] = "alert alert-success";
                TempData["Offer"] = "Order Status updated successfully";
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Offer"] = ex.Message;
            }
            return RedirectToAction("UpdateOrderStatus");
        }
        #endregion

        #region Shipped
        public ActionResult ShipOrder(CustomerManagement model)
        {
            //#region ddlOrderStatus
            //List<SelectListItem> ddlOrderStatus = Common.BindOrderStatus();
            //ViewBag.ddlOrderStatus = ddlOrderStatus;
            //#endregion ddlOrderStatus

            return View(model);
        }

        [HttpPost]
        [ActionName("ShipOrder")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetOrderListforship(CustomerManagement model)
        {

            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderStatus = "Packed";
            model.AddedBy = Session["Pk_VendorId"].ToString();
            DataSet dsblock = model.GetDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {


                model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();



                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.ProductID = r["FK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.TotalAmount = r["Amount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.List = lst;
            }
            else
            {
                model.Result = "No record found !";
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ShipOrder")]
        [OnAction(ButtonName = "Update")]
        public ActionResult shipUpdate(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string id = "";
                string status = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("OrderDetailsID", typeof(string));
                dtst.Columns.Add("OrderStatus", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        obj.OrderStatus = "Shipped";
                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.AddedBy = Session["Pk_VendorId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.UpdateOrder();

                        try
                        {
                            string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", Your Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " containing " + ds.Tables[0].Rows[0]["ProductName"].ToString() +
                                " has been " + ds.Tables[0].Rows[0]["OrderStatus"].ToString() + ". Expected Delivery : " + ds.Tables[0].Rows[0]["ExpectedDelivery"].ToString() + ".";
                            string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                            BLSMS.SendSMSNew(mobile, message);
                        }
                        catch { }
                    }
                }
                TempData["Class"] = "alert alert-success";
                TempData["ShipOrder"] = "Order shipped successfully";
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["ShipOrder"] = ex.Message;
            }
            return RedirectToAction("ShipOrder");
        }
        #endregion 

        #region Deliver
        public ActionResult DeliverOrder(CustomerManagement model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("DeliverOrder")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetOrderListfordeliver(CustomerManagement model)
        {

            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderStatus = "Shipped";
            model.AddedBy = Session["Pk_VendorId"].ToString();
            DataSet dsblock = model.GetDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();



                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.ProductID = r["FK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.TotalAmount = r["Amount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.List = lst;
            }
            else
            {
                model.Result = "No record found !";
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("DeliverOrder")]
        [OnAction(ButtonName = "Update")]
        public ActionResult DeliverUpdate(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string id = "";
                string status = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("OrderDetailsID", typeof(string));
                dtst.Columns.Add("OrderStatus", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        obj.OrderStatus = "Delivered";
                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.AddedBy = Session["Pk_VendorId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.UpdateOrder();
                        if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                        {
                            try
                            {
                                string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", Your Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " containing " + ds.Tables[0].Rows[0]["ProductName"].ToString() +
                                    " has been " + ds.Tables[0].Rows[0]["OrderStatus"].ToString() + ".";
                                string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                                BLSMS.SendSMSNew(mobile, message);
                            }
                            catch { }
                            TempData["Class"] = "alert alert-success";
                            TempData["DeliverOrder"] = "Order Delivered successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                        {
                            TempData["Class"] = "alert alert-danger";
                            TempData["DeliverOrder"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["DeliverOrder"] = ex.Message;
            }
            return RedirectToAction("DeliverOrder");
        }
        #endregion

        #region CancelledOrderList
        public ActionResult CancelledOrderList(CustomerManagement model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("CancelledOrderList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ListOfOrders(CustomerManagement model)
        {
            try
            {
                List<CustomerManagement> lst = new List<CustomerManagement>();

                // model.SiteID = model.SiteID == "0" ? null : model.SiteID;
                model.AddedBy = Session["Pk_VendorId"].ToString();
                DataSet ds = model.CancelledList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        CustomerManagement obj = new CustomerManagement();
                        obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                        obj.OrderID = r["FK_OrderID"].ToString();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.TotalAmount = r["Amount"].ToString();
                        obj.CustomerID = r["FK_CustomerID"].ToString();
                        obj.OrderNumber = r["OrderNo"].ToString();
                        obj.OrderDate = r["OrderDate"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Mobile = r["Contact"].ToString();
                        lst.Add(obj);
                    }
                    model.List = lst;
                }
            }
            catch (Exception ex)
            {


            }

            return View(model);
        }


        [HttpPost]
        [ActionName("CancelledOrderList")]
        [OnAction(ButtonName = "Update")]
        public ActionResult ApproveCancelledOrder(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string id = "";
                string customer = "";
                string amt = "";

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {

                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.CustomerID = Request["custid_ " + i].ToString();
                        obj.TotalAmount = Request["amt_ " + i].ToString();
                        obj.AddedBy = Session["Pk_VendorId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.ApproveRequest();

                        try
                        {

                        }
                        catch { }

                    }

                }


                TempData["Offer"] = "Cancelled Order Request Is Approved ";
            }
            catch (Exception ex)
            {
                TempData["Offer"] = ex.Message;
            }
            return RedirectToAction("CancelledOrderList", "CustomerManagement");
        }
        #endregion

        public ActionResult SendVerificationLink(string MobileNo,string EmailId)
        {
            #region SendEmailForVerification
            SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
            mailServer.EnableSsl = true;
            mailServer.Credentials = new System.Net.NetworkCredential("info@afluex.com", "7985207220");

            string mobencrypt = Crypto.Encrypt(MobileNo);
            string body = "<div style = 'width:100%;background:#fff;font-size:12px;font-family:Verdana,Geneva,sans-serif' ><table style = 'width:640px;border:none;font-size:12px;margin:0 auto' cellpadding = '0' cellspacing = '0'><tbody><tr><td><div style = 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:15px 0'><div style = 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:0px 0'><div><a><img style = 'width:180px' src = '/Websitecss/img/logo.png' ></a></div><h1 style = 'color:#fff;font-weight:normal'></h1></div></div></td></tr> "
            + "<tr><td><div style='background:#fff;vertical-align:top;padding:1px 0;border-bottom-right-radius:5px;border-bottom-left-radius:5px;border-left:1px solid #ddd;border-right:1px solid #ddd'><h4 style = 'font-size:14px;padding:10px 8px'>"
            + "Dear " + Session["Name"].ToString() + ",<br/> Welcome to the seller club of Afluex Shopping.<br/>Please"
            + "<a href = '/Home/VerifyEmail?q=" + mobencrypt + "' target= '_blank'>Click Here</a> to verify your EmailId.</h4></div></td></tr><tr><td><div style = 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:1px 0'>"
            + "<div style= 'background:#173d79;text-align:center;border-top-right-radius:5px;border-top-left-radius:5px;padding:0px 0'><div></div><h1 style= 'color:#fff;font-weight:normal'> Afluex Shopping</h1>"
            + "<h4 style = 'color:#fff;font-weight:normal'> 'Shopping and Income at one place' </h4><h4 style = 'color:white'><i class='fa fa-volume-control-phone' aria-hidden='true'></i> &nbsp; Phone No : <i>+91 7985207220</i><i class='fa fa-envelope-o' aria-hidden='true'></i>&nbsp; Email : <i> info@afluex.com</i></h4>"
            + "<h4 style = 'color:white'><i class='fa fa-volume-control-phone' aria-hidden='true'></i> &nbsp; Website : <i>/</i></h4>"
            + "</div></div></td></tr><tr><td><p style='color:#888;font-size:11px;margin-bottom:20px'>© Copyright 2019 All Rights Reserved</p></td></tr></tbody></table></div>";

            MailMessage myMail = new MailMessage();
            myMail.Subject = "Verification Email from Afluex Shopping";
            myMail.Body = body;
            myMail.IsBodyHtml = true;
            myMail.From = new MailAddress("info@afluex.com", "Afluex Shopping");
            myMail.To.Add(EmailId);

            mailServer.Send(myMail);
            #endregion
            return RedirectToAction("Dashboard");
        }
        public ActionResult SaveReview(string Review)
        {
            Vendor model = new Vendor();
            try
            {
                model.Review = Review;
                model.VendorID = Session["Pk_VendorId"].ToString();

                DataSet ds = model.SaveReview();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "1";
                        model.Status = "Review Save successfully";
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
                model.Result = "0";
                model.Status = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult UpdateProfilePic(string FileName)
        {
            Vendor objmem = new Vendor();
            try
            {

               
                objmem.VendorID = Session["Pk_VendorId"].ToString();
                objmem.ProfilePic = "../../images/VendorPic/"+DateTime.Now.ToString("ddMMyyyyHHmm") + FileName;


                DataSet dsblock = objmem.UpdateProfilePic();
                if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                {
                    if (dsblock.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        objmem.Result = "Profile Pic Updated Successfully.";
                       
                    }
                    else
                    {
                        objmem.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                objmem.Result = ex.Message;
            }
            return Json(objmem, JsonRequestBehavior.AllowGet);
        }
    }
}
