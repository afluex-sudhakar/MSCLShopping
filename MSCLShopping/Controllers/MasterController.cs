using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MSCLShopping.Filter;
using MSCLShopping.Models;
using System.IO;
using System.Drawing.Drawing2D;
using SD = System.Drawing;

namespace MSCLShopping.Controllers
{
    public class MasterController : AdminBaseController
    {
        DataTable dt = new DataTable();
        DataTable dtSecondaryImages = new DataTable();
        public ActionResult GetCategoryDetails(string CategoryID)
        {
            try
            {
                Master model = new Master();
                model.CategoryID = CategoryID;


                #region GetSubCategory
                List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
                DataSet ds = model.SubCategoryList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlSubCategory.Add(new SelectListItem { Text = r["SubCategoryName"].ToString(), Value = r["PK_SubCategoryID"].ToString() });

                    }
                }
                model.ddlSubCategory = ddlSubCategory;
                #endregion
                //if (ds != null && ds.Tables[1].Rows.Count > 0)
                //{
                //    string variant = "";
                //    foreach (DataRow r in ds.Tables[0].Rows)
                //    {

                //        variant += r["FK_VariantID"].ToString() + ",";

                //    }
                //    variant = variant.Substring(0, variant.Length - 1);
                //    model.VariantMapping = variant;
                //}

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetSizeByUnit(string UnitID)
        {
            try
            {
                Master model = new Master();
                model.UnitID = UnitID;


                #region GetSize
                List<SelectListItem> ddlSize = new List<SelectListItem>();
                DataSet ds = model.SizeList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlSize.Add(new SelectListItem { Text = r["SizeName"].ToString(), Value = r["PK_SizeID"].ToString() });

                    }
                }
                model.ddlSize = ddlSize;
                #endregion


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetSizeeByUnit(string UnitID)
        {
            try
            {
                Master model = new Master();
                model.UnitID = UnitID;


                #region GetSize
                List<SelectListItem> ddlSizee = new List<SelectListItem>();
                DataSet ds = model.SizeList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlSizee.Add(new SelectListItem { Text = r["SizeName"].ToString(), Value = r["PK_SizeID"].ToString() });

                    }
                }
                model.ddlSizee = ddlSizee;
                #endregion


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetProductBySubCategory(string SubCategoryID)
        {
            try
            {
                Master model = new Master();
                model.SubCategoryID = SubCategoryID;

                #region Get
                List<Master> lst = new List<Master>();
                //List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
                DataSet ds = model.ProductList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.ProductID = r["PK_ProductID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();

                        lst.Add(obj);
                    }
                    model.lstProduct = lst;
                }
                #endregion


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetCategoryByMainCategory(string MainCategoryID)
        {
            try
            {
                Master model = new Master();
                model.MainCategoryID = MainCategoryID;

                #region Get
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                DataSet ds = model.CategoryList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlCategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryID"].ToString() });

                    }
                }
                model.ddlCategory = ddlCategory;

                #endregion


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetSubCategory(string MainCategoryID, string CategoryID)
        {
            List<Master> lst = new List<Master>();
            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            Master model = new Master();
            model.MainCategoryID = MainCategoryID;
            model.CategoryID = CategoryID;
            DataSet dsblock = model.SubCategoryList();

            #region ddl
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    ddlSubCategory.Add(new SelectListItem { Text = dr["SubCategoryName"].ToString(), Value = dr["PK_SubCategoryID"].ToString() });
                }

            }

            model.ddlSubCategory = ddlSubCategory;
            #endregion
            if (dsblock != null && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    Master obj = new Master();
                    obj.VariantName = r["VariantName"].ToString();
                    lst.Add(obj);
                }
                model.lstVarient = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVariantFromCategory(string CategoryID)
        {
            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            Master model = new Master();
            model.CategoryID = CategoryID;

            DataSet dsblock = model.GetVariantFromCategory();

            #region ddl
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    ddlSubCategory.Add(new SelectListItem { Text = dr["SubCategoryName"].ToString(), Value = dr["PK_SubCategoryID"].ToString() });
                }

            }

            model.ddlSubCategory = ddlSubCategory;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #region MainCategory

        public ActionResult MainCategory(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.MainCategoryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.MainCategoryID = r["PK_MainCategoryID"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.Images = r["Images"].ToString();
                    lst.Add(obj);
                }
                model.lstCategory = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("MainCategory")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveMainCategory(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                string path = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                if (postedFile != null)
                {
                    model.Images = "../images/MainCategoryImage/" + path;
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Images)));
                }
                model.Images = "../images/MainCategoryImage/" + path;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveMainCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["MainCategory"] = "Main Category saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["MainCategory"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["MainCategory"] = ex.Message;
            }
            return RedirectToAction("MainCategory");
        }
        [HttpPost]
        [ActionName("MainCategory")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateMainCategory(HttpPostedFileBase postedFile, Master obj)
        {
            
            try
            {
                string path = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                if (postedFile != null)
                {
                    obj.Images = "../images/MainCategoryImage/" + path;
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.Images)));
                   
                    
                }
               
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateMainCategory();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "Main Category updated successfully";
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
            return RedirectToAction("MainCategory");
        }

        public ActionResult DeleteMainCategory(string id)
        {
            try
            {
                Master obj = new Master();
                obj.MainCategoryID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteMainCategory();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["MainCategory"] = "Main Category deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["MainCategory"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["MainCategory"] = ex.Message;
            }
            return RedirectToAction("MainCategory");
        }

        #endregion

        #region Category
        public ActionResult Category(string CategoryID)
        {
            Master model = new Master();
            if (CategoryID != null)
            {
                model.CategoryID = CategoryID;
                List<Master> lst = new List<Master>();
                List<Master> lstvarient = new List<Master>();
                List<Master> lstvarient1 = new List<Master>();
                DataSet ds = model.CategoryList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    //  model.DiscountID = DiscountID;

                    model.CategoryID = ds.Tables[0].Rows[0]["PK_CategoryID"].ToString();
                    model.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                    model.MainCategoryID = ds.Tables[0].Rows[0]["FK_MainCategory"].ToString();
                    model.MainCategoryName = ds.Tables[0].Rows[0]["MainCategoryName"].ToString();
                    
                    //foreach (DataRow r in ds.Tables[0].Rows)
                    //{
                    //    Master obj = new Master();
                    //    obj.CategoryID = r["PK_CategoryID"].ToString();
                    //    obj.CategoryName = r["CategoryName"].ToString();
                    //    obj.MainCategoryID = r["FK_MainCategory"].ToString();
                    //    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    //    lst.Add(obj);
                    //}
                    model.lstCategory = lst;
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            Master obj = new Master();
                            obj.VariantID = r["FK_VariantID"].ToString();
                            obj.VariantName = r["VariantName"].ToString();
                            obj.Status = r["Status"].ToString();
                            lstvarient.Add(obj);
                        }
                        model.lstVariant = lstvarient;
                    }
                }
            }
            else
            {
                List<Master> lst1 = new List<Master>();
                DataSet dsv = model.VariantList();

                if (dsv != null && dsv.Tables.Count > 0 && dsv.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsv.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.VariantID = r["PK_VariantID"].ToString();
                        obj.VariantName = r["VariantName"].ToString();

                        lst1.Add(obj);
                    }
                    model.lstVariant = lst1;
                }
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


            return View(model);

        }

        //public ActionResult EditCategory(Master model)
        //{
        //    try
        //    {
        //        List<Master> lst = new List<Master>();
        //        DataSet ds = model.CategoryList();
        //        if(ds!=null && ds.Tables[0].Rows.Count>0)
        //        {
        //            foreach (DataRow r in ds.Tables[0].Rows)
        //            {
        //                Master obj = new Master();
        //                obj.CategoryID = r["PK_CategoryID"].ToString();
        //                obj.CategoryName = r["CategoryName"].ToString();
        //                obj.MainCategoryID = r["FK_MainCategory"].ToString();
        //                obj.MainCategoryName = r["MainCategoryName"].ToString();
        //                lst.Add(obj);
        //            }
        //            model.lstCategory = lst;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View();
        //}
        [HttpPost]
        [ActionName("Category")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveCategory(Master model)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();
                string varid = "";
                string chk = "";
                DataTable dtst = new DataTable();
                dtst.Columns.Add("FK_VariantID", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        varid = Request["variantid_ " + i].ToString();

                        dtst.Rows.Add(varid);
                    }
                }
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.SaveCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Category"] = " Category added successfully !";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("Category");
        }
        [HttpPost]
        [ActionName("Category")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateCategory(Master obj)
        {

            try
            {

                string noofrows = Request["hdRows"].ToString();
                string varid = "";
                string chk = "";
                DataTable dtst = new DataTable();
                dtst.Columns.Add("FK_VariantID", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        varid = Request["variantid_ " + i].ToString();

                        dtst.Rows.Add(varid);
                    }
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtProductQuantity = dtst;

                DataSet ds = obj.UpdateCategory();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Category"] = " Category Updated successfully !";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("Category");
        }

        public ActionResult DeleteCategory(string id)
        {
            try
            {
                Master obj = new Master();
                obj.CategoryID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteCategory();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Category"] = "Category deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("Category");
        }

        public ActionResult CategoryList(Master model)
        {
            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.CategoryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CategoryID = r["PK_CategoryID"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.MainCategoryID = r["FK_MainCategory"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();


                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }

        #endregion

        #region SubCategory

        public ActionResult SubCategory(Master model)
        {
            try
            {
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                ViewBag.ddlCategory = ddlCategory;

                List<Master> lst = new List<Master>();

                DataSet ds = model.SubCategoryList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj2 = new Master();
                        obj2.SubCategoryID = r["PK_SubCategoryID"].ToString();
                        obj2.CategoryName = r["CategoryName"].ToString();
                        obj2.SubCategoryName = r["SubCategoryName"].ToString();
                        obj2.CategoryID = r["FK_CategoryID"].ToString();
                        obj2.MainCategoryID = r["FK_MainCategory"].ToString();
                        obj2.MainCategoryName = r["MainCategoryName"].ToString();
                        obj2.Images = r["Images"].ToString();
                        lst.Add(obj2);
                    }
                    model.lstSubCategory = lst;
                }


                #region MainCategory
                Master obj1 = new Master();
                int count = 0;
                List<SelectListItem> ddlMainCategory = new List<SelectListItem>();
                DataSet ds3 = obj1.MainCategoryList();
                if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds3.Tables[0].Rows)
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
                //List<SelectListItem> ddlCategory1 = new List<SelectListItem>();
                //DataSet ds4 = obj1.CategoryList();
                //if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow r in ds4.Tables[0].Rows)
                //    {
                //        if (count == 0)
                //        {
                //            ddlCategory1.Add(new SelectListItem { Text = "Select  Category", Value = "0" });
                //        }
                //        ddlCategory1.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryID"].ToString() });
                //        count = count + 1;
                //    }
                //}
                //ViewBag.ddlCategory1 = ddlCategory1;
            }

            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("SubCategory")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveSubCategory(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                string path = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                if (postedFile != null)
                {
                    
                    model.Images = "../images/SubCategoryImage/" + path;
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Images)));

                    #region cropImage
                    Stream strm = postedFile.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        #region 60*60
                        int newWidth = Convert.ToInt32(60);
                        int newHeight = Convert.ToInt32(60);

                        var thumbImg = new SD.Bitmap(newWidth, newHeight);
                        var thumbGraph = SD.Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);

                        model.Images = Server.MapPath("../images/SubCategoryImage/Crop/") + path;
                        thumbImg.Save(model.Images, image.RawFormat);
                        #endregion
                    }
                    #endregion
                }
                model.Images= "../images/SubCategoryImage/" + path;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveSubCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["SubCategory"] = "SubCategory saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["SubCategory"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["SubCategory"] = ex.Message;
            }
            return RedirectToAction("SubCategory");
            //return View();
        }
        [HttpPost]
        [ActionName("SubCategory")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateSubCategory(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                if (postedFile != null)
                {
                    string path = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    model.Images = "../images/SubCategoryImage/" + path;
                    Session["SubCatImg"] = model.Images;
                    #region cropImage
                    Stream strm = postedFile.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        #region 60*60
                        int newWidth = Convert.ToInt32(60);
                        int newHeight = Convert.ToInt32(60);

                        var thumbImg = new SD.Bitmap(newWidth, newHeight);
                        var thumbGraph = SD.Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);

                        model.Images = Server.MapPath("../images/SubCategoryImage/Crop/") + path;
                        thumbImg.Save(model.Images, image.RawFormat);
                        #endregion
                    }
                    #endregion
                }
                DataSet ds = model.UpdateSubCategory();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        model.Result = "Sub-Category updated successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                model.Result = ex.Message;
            }
            return RedirectToAction("SubCategory");
        }

        public ActionResult DeleteSubCategory(string id)
        {
            try
            {
                Master obj = new Master();
                obj.SubCategoryID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSubCategory();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["SubCategory"] = "Sub Category deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["SubCategory"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["SubCategory"] = ex.Message;
            }
            return RedirectToAction("SubCategory");
        }

        #endregion

        #region Brands

        public ActionResult Brands(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.BrandList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BrandID = r["PK_BrandID"].ToString();
                    obj.BrandName = r["BrandName"].ToString();
                    obj.BrandImage = r["BrandImage"].ToString();
                    lst.Add(obj);
                }
                model.lstBrands = lst;
            }
            return View(model);
        }

        public ActionResult SaveBrand(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                if (postedFile != null)
                {
                    model.BrandImage = "../images/BrandImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.BrandImage)));

                }
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveBrand();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Brand"] = "Brand saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Brand"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Brand"] = ex.Message;
            }
            return RedirectToAction("Brands");
        }

        public ActionResult UpdateBrand(HttpPostedFileBase postedFile, string BrandID, string BrandName)
        {
            Master obj = new Master();
            try
            {
                if (postedFile != null)
                {
                    obj.BrandImage = "../images/BrandImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.BrandImage)));

                }
                obj.BrandID = BrandID;
                obj.BrandName = BrandName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateBrand();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        obj.Result = "Brand updated successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteBrand(string id)
        {
            try
            {
                Master obj = new Master();
                obj.BrandID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteBrand();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Category"] = "Brand deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("Brands");
        }

        #endregion

        #region Unit

        public ActionResult Unit(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.UnitList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.UnitID = r["PK_UnitID"].ToString();
                    obj.UnitName = r["UnitName"].ToString();

                    lst.Add(obj);
                }
                model.lstBrands = lst;
            }
            return View(model);
        }

        public ActionResult SaveUnit(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveUnit();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Unit"] = "Unit saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Unit"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Unit"] = ex.Message;
            }
            return RedirectToAction("Unit");
        }

        public ActionResult UpdateUnit(string UnitID, string UnitName)
        {
            Master obj = new Master();
            try
            {
                obj.UnitID = UnitID;
                obj.UnitName = UnitName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateUnit();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        obj.Result = "Unit updated successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUnit(string id)
        {
            try
            {
                Master obj = new Master();
                obj.UnitID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteUnit();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-succes";
                        TempData["Unit"] = "Unit deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Unit"] = ex.Message;
            }
            return RedirectToAction("Unit");
        }

        #endregion

        #region Size

        public ActionResult Size(Master model)
        {
            try
            {
                #region Unit
                Master obj = new Master();
                int count = 0;
                List<SelectListItem> ddlUnit = new List<SelectListItem>();
                DataSet ds1 = obj.UnitList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                        }
                        ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlUnit = ddlUnit;

                #endregion

                List<Master> lst = new List<Master>();

                DataSet ds = model.SizeList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj1 = new Master();
                        obj1.SizeID = r["PK_SizeID"].ToString();
                        obj1.UnitID = r["FK_UnitID"].ToString();
                        obj1.SizeName = r["SizeName"].ToString();
                        obj1.UnitName = r["UnitName"].ToString();

                        lst.Add(obj1);
                    }
                    model.lstSubCategory = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult SaveSize(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveSize();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Size"] = "Size saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Size"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["SizeSize"] = ex.Message;
            }
            return RedirectToAction("Size");
        }

        public ActionResult UpdateSize(string SizeID, string UnitID, string SizeName)
        {
            Master obj = new Master();
            try
            {
                obj.SizeID = SizeID;
                obj.UnitID = UnitID;
                obj.SizeName = SizeName;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateSize();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        obj.Result = "Size updated successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSize(string id)
        {
            try
            {
                Master obj = new Master();
                obj.SizeID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteSize();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Size"] = "Size deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Size"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Size"] = ex.Message;
            }
            return RedirectToAction("Size");
        }
        #endregion

        #region Color

        public ActionResult Color(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.ColorList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ColorID = r["PK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.ColorCode = r["ColorCode"].ToString();
                    lst.Add(obj);
                }
                model.lstCategory = lst;
            }
            return View(model);
        }

        public ActionResult SaveColor(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveColor();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Color"] = "Color saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Color"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Color"] = ex.Message;
            }
            return RedirectToAction("Color");
        }

        public ActionResult UpdateColor(string ColorID, string ColorName, string ColorCode)
        {
            Master obj = new Master();
            try
            {
                obj.ColorID = ColorID;
                obj.ColorName = ColorName;
                obj.ColorCode = ColorCode;
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateColor();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Color"] = "Color updated successfully";
                        obj.Result = "Color updated successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Color"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
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

        public ActionResult DeleteColor(string id)
        {
            try
            {
                Master obj = new Master();
                obj.ColorID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteColor();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Color"] = "Color deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Color"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Color"] = ex.Message;
            }
            return RedirectToAction("Color");
        }
        #endregion 

        #region Flavor

        public ActionResult Flavor(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.FlavorList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.FlavorID = r["PK_FlavorID"].ToString();
                    obj.FlavorName = r["FlavorName"].ToString();

                    lst.Add(obj);
                }
                model.lstCategory = lst;
            }
            return View(model);
        }

        public ActionResult SaveFlavor(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveFlavor();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Flavor"] = "Flavor saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "Red";
                        TempData["Flavor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Flavor"] = ex.Message;
            }
            return RedirectToAction("Flavor");
        }

        public ActionResult UpdateFlavor(string FlavorID, string FlavorName)
        {
            Master obj = new Master();
            try
            {
                obj.FlavorID = FlavorID;
                obj.FlavorName = FlavorName;
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateFlavor();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "Flavor updated successfully";
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

        public ActionResult DeleteFlavor(string id)
        {
            try
            {
                Master obj = new Master();
                obj.FlavorID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteFlavor();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Flavor"] = "Flavor deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Flavor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Flavor"] = ex.Message;
            }
            return RedirectToAction("Flavor");
        }
        #endregion 

        #region Material

        public ActionResult Material(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.MaterialList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.MaterialID = r["PK_MaterialID"].ToString();
                    obj.MaterialName = r["MaterialName"].ToString();

                    lst.Add(obj);
                }
                model.lstCategory = lst;
            }
            return View(model);
        }

        public ActionResult SaveMaterial(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveMaterial();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Material"] = "Material saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "Red";
                        TempData["Material"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Material"] = ex.Message;
            }
            return RedirectToAction("Material");
        }

        public ActionResult UpdateMaterial(string MaterialID, string MaterialName)
        {
            Master obj = new Master();
            try
            {
                obj.MaterialID = MaterialID;
                obj.MaterialName = MaterialName;
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateMaterial();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "Material updated successfully";
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

        public ActionResult DeleteMaterial(string id)
        {
            try
            {
                Master obj = new Master();
                obj.MaterialID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteMaterial();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Material"] = "Material deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Material"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Material"] = ex.Message;
            }
            return RedirectToAction("Material");
        }
        #endregion 

        #region Products

        public ActionResult Products(string ProductID)
        {
            Session["tmpData"] = null;
            Master model = new Master();

            if (ProductID != null)
            {
                model.ProductID = ProductID;

                DataSet Branch = model.ProductList();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.ProductID = Branch.Tables[0].Rows[0]["PK_ProductID"].ToString();
                    model.ProductName = Branch.Tables[0].Rows[0]["ProductName"].ToString();
                    model.ProductCode = Branch.Tables[0].Rows[0]["ProductCode"].ToString();
                    model.BrandID = Branch.Tables[0].Rows[0]["FK_BrandID"].ToString();
                    model.BrandName = Branch.Tables[0].Rows[0]["BrandName"].ToString();
                    model.UnitID = Branch.Tables[0].Rows[0]["FK_UnitID"].ToString();
                    model.UnitName = Branch.Tables[0].Rows[0]["UnitName"].ToString();
                    model.CategoryID = Branch.Tables[0].Rows[0]["FK_CategoryID"].ToString();
                    model.CategoryName = Branch.Tables[0].Rows[0]["CategoryName"].ToString();
                    model.SubCategoryID = Branch.Tables[0].Rows[0]["FK_SubCategoryID"].ToString();
                    model.SubCategoryName = Branch.Tables[0].Rows[0]["SubCategoryName"].ToString();
                    model.MainCategoryName = Branch.Tables[0].Rows[0]["MainCategoryName"].ToString();
                    model.MainCategoryID = Branch.Tables[0].Rows[0]["FK_MainCategory"].ToString();
                    model.SizeName = Branch.Tables[0].Rows[0]["SizeName"].ToString();
                    model.ProductQuantity = Branch.Tables[0].Rows[0]["DebitQuantity"].ToString();
                    // model.IsNew = Request["IsNew"] = "1" ? checked; 

                }
            }
            else
            {
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                ViewBag.ddlCategory = ddlCategory;

                List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
                ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
                ViewBag.ddlSubCategory = ddlSubCategory;

                List<SelectListItem> ddlSize = new List<SelectListItem>();
                ddlSize.Add(new SelectListItem { Text = "Select Size", Value = "0" });
                ViewBag.ddlSize = ddlSize;
            }

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            #region Unit
            Master objUnit = new Master();
            int count4 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds4 = objUnit.UnitList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion
            #region Color
            Master objColor = new Master();
            int countc = 0;
            List<SelectListItem> ddlColor = new List<SelectListItem>();
            DataSet ds5 = objColor.ColorList();
            if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds5.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlColor.Add(new SelectListItem { Text = "Select Color", Value = "0" });
                    }
                    ddlColor.Add(new SelectListItem { Text = r["ColorName"].ToString(), Value = r["PK_ColorID"].ToString() });
                    countc = countc + 1;
                }
            }

            ViewBag.ddlColor = ddlColor;

            #endregion
            #region Flavor
            Master objFlavor = new Master();
            int countF = 0;
            List<SelectListItem> ddlFlavor = new List<SelectListItem>();
            DataSet dsf = objFlavor.FlavorList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countF == 0)
                    {
                        ddlFlavor.Add(new SelectListItem { Text = "Select Flavor", Value = "0" });
                    }
                    ddlFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["PK_FlavorID"].ToString() });
                    countF = countF + 1;
                }
            }

            ViewBag.ddlFlavor = ddlFlavor;

            #endregion
            #region Material
            Master objMaterial = new Master();
            int countm = 0;
            List<SelectListItem> ddlMaterial = new List<SelectListItem>();
            DataSet dsm = objMaterial.MaterialList();
            if (dsm != null && dsm.Tables.Count > 0 && dsm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsm.Tables[0].Rows)
                {
                    if (countm == 0)
                    {
                        ddlMaterial.Add(new SelectListItem { Text = "Select Material", Value = "0" });
                    }
                    ddlMaterial.Add(new SelectListItem { Text = r["MaterialName"].ToString(), Value = r["PK_MaterialID"].ToString() });
                    countm = countm + 1;
                }
            }

            ViewBag.ddlMaterial = ddlMaterial;

            #endregion
            #region RAM
            Master objRAM = new Master();
            int countr = 0;
            List<SelectListItem> ddlRAM = new List<SelectListItem>();
            DataSet dsRm = objRAM.RAMList();
            if (dsRm != null && dsRm.Tables.Count > 0 && dsRm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsRm.Tables[0].Rows)
                {
                    if (countr == 0)
                    {
                        ddlRAM.Add(new SelectListItem { Text = "Select RAM", Value = "0" });
                    }
                    ddlRAM.Add(new SelectListItem { Text = r["RAM"].ToString(), Value = r["PK_RAM_ID"].ToString() });
                    countr = countr + 1;
                }
            }

            ViewBag.ddlRAM = ddlRAM;
            #endregion
            #region Storage
            Master objStorage = new Master();
            int countst = 0;
            List<SelectListItem> ddlStorage = new List<SelectListItem>();
            DataSet dsSt = objStorage.StorageList();
            if (dsSt != null && dsSt.Tables.Count > 0 && dsSt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSt.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        ddlStorage.Add(new SelectListItem { Text = "Select Storage", Value = "0" });
                    }
                    ddlStorage.Add(new SelectListItem { Text = r["Storage"].ToString(), Value = r["PK_StorageID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.ddlStorage = ddlStorage;
            #endregion
            #region StarRating
            Master objStarRating = new Master();
            int countstr = 0;
            List<SelectListItem> ddlStarRating = new List<SelectListItem>();
            DataSet dsStr = objStarRating.StarRatingList();
            if (dsStr != null && dsStr.Tables.Count > 0 && dsStr.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsStr.Tables[0].Rows)
                {
                    if (countstr == 0)
                    {
                        ddlStarRating.Add(new SelectListItem { Text = "Select Star Rating", Value = "0" });
                    }
                    ddlStarRating.Add(new SelectListItem { Text = r["StarRating"].ToString(), Value = r["PK_StarRatingID"].ToString() });
                    countstr = countstr + 1;
                }
            }

            ViewBag.ddlStarRating = ddlStarRating;
            #endregion
            return View(model);
        }

        public ActionResult saveDataTemporary(string sizeID, string colorID, string qty, string bv, string mrp, string offeredprice, string dealerprice,
            string cgst, string sgst, string igst, string unitID, string unitname, string sizename, string colorname, string flavorid, string flavorname,
            string materialid, string materialname, string ramid, string ramname, string storageid, string storagename, string starratingid, string starratingname

            )
        {


            Master model = new Master();
            try
            {

                Session["ProductInfoCode"] = DateTime.Now.ToString("ddMMyyyyHHmmss");
                model.ProductInfoCode = Session["ProductInfoCode"].ToString();
                if (Session["tmpData"] != null)
                {
                    dt = (DataTable)Session["tmpData"];
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();
                        dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                        dr["PrimaryImage"] = "";
                        dr["FK_UnitID"] = unitID;
                        dr["UnitName"] = unitname;
                        dr["FK_SizeID"] = sizeID;
                        dr["SizeName"] = sizename == "Select Size" ? "NA" : sizename;
                        dr["FK_ColorID"] = colorID;
                        dr["ColorName"] = colorname == "Select Color" ? "NA" : colorname;
                        dr["MRP"] = mrp;
                        dr["OfferPrice"] = offeredprice;
                        dr["DealerPrice"] = dealerprice;
                        dr["BV"] = bv;
                        dr["CGST"] = cgst;
                        dr["SGST"] = sgst;
                        dr["IGST"] = igst;
                        dr["Quantity"] = qty;
                        dr["FK_FlavorID"] = flavorid;
                        dr["FlavorName"] = flavorname == "Select Flavor" ? "NA" : flavorname;
                        dr["FK_MaterialID"] = materialid;
                        dr["MaterialName"] = materialname == "Select Material" ? "NA" : materialname;

                        dr["FK_RamID"] = ramid;
                        dr["RAM"] = ramname == "Select RAM" ? "NA" : ramname;
                        dr["FK_StorageID"] = storageid;
                        dr["StorageName"] = storagename == "Select Storage" ? "NA" : storagename;
                        dr["FK_StarratingID"] = starratingid;
                        dr["StarRatingName"] = starratingname == "Select Star Rating" ? "NA" : starratingname;

                        dt.Rows.Add(dr);
                        Session["tmpData"] = dt;
                    }
                }
                else
                {
                    dt.Columns.Add("ProductInfoCode", typeof(string));
                    dt.Columns.Add("PrimaryImage", typeof(string));
                    dt.Columns.Add("FK_UnitID", typeof(string));
                    dt.Columns.Add("UnitName", typeof(string));
                    dt.Columns.Add("FK_SizeID", typeof(string));
                    dt.Columns.Add("SizeName", typeof(string));
                    dt.Columns.Add("FK_ColorID", typeof(string));
                    dt.Columns.Add("ColorName", typeof(string));
                    dt.Columns.Add("MRP", typeof(string));
                    dt.Columns.Add("OfferPrice", typeof(string));
                    dt.Columns.Add("DealerPrice", typeof(string));
                    dt.Columns.Add("BV", typeof(string));
                    dt.Columns.Add("CGST", typeof(string));
                    dt.Columns.Add("SGST", typeof(string));
                    dt.Columns.Add("IGST", typeof(string));
                    dt.Columns.Add("Quantity", typeof(string));
                    dt.Columns.Add("FK_FlavorID", typeof(string));
                    dt.Columns.Add("FlavorName", typeof(string));
                    dt.Columns.Add("FK_MaterialID", typeof(string));
                    dt.Columns.Add("MaterialName", typeof(string));

                    dt.Columns.Add("FK_RamID", typeof(string));
                    dt.Columns.Add("RAM", typeof(string));
                    dt.Columns.Add("FK_StorageID", typeof(string));
                    dt.Columns.Add("StorageName", typeof(string));
                    dt.Columns.Add("FK_StarratingID", typeof(string));
                    dt.Columns.Add("StarRatingName", typeof(string));

                    DataRow dr = dt.NewRow();
                    dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                    dr["PrimaryImage"] = "";
                    dr["FK_UnitID"] = unitID;
                    dr["UnitName"] = unitname;
                    dr["FK_SizeID"] = sizeID;
                    dr["SizeName"] = sizename == "Select Size" ? "NA" : sizename;
                    dr["FK_ColorID"] = colorID;
                    dr["ColorName"] = colorname == "Select Color" ? "NA" : colorname;
                    dr["MRP"] = mrp;
                    dr["OfferPrice"] = offeredprice;
                    dr["DealerPrice"] = dealerprice;
                    dr["BV"] = bv;
                    dr["CGST"] = cgst;
                    dr["SGST"] = sgst;
                    dr["IGST"] = igst;
                    dr["Quantity"] = qty;
                    dr["FK_FlavorID"] = flavorid;
                    dr["FlavorName"] = flavorname == "Select Flavor" ? "NA" : flavorname;
                    dr["FK_MaterialID"] = materialid;
                    dr["MaterialName"] = materialname == "Select Material" ? "NA" : materialname;
                    dr["FK_RamID"] = ramid;
                    dr["RAM"] = ramname == "Select RAM" ? "NA" : ramname;
                    dr["FK_StorageID"] = storageid;
                    dr["StorageName"] = storagename == "Select Storage" ? "NA" : storagename;
                    dr["FK_StarratingID"] = starratingid;
                    dr["StarRatingName"] = starratingname == "Select Star Rating" ? "NA" : starratingname;

                    dt.Rows.Add(dr);
                    Session["tmpData"] = dt;

                }

                dt = (DataTable)Session["tmpData"];
                List<Master> lstTmpData = new List<Master>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        Master obj = new Master();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.PrimaryImage = r["PrimaryImage"].ToString();
                        obj.UnitID = r["FK_UnitID"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeID = r["FK_SizeID"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.ColorID = r["FK_ColorID"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.OfferedPrice = r["OfferPrice"].ToString();
                        obj.DealerPrice = r["DealerPrice"].ToString();
                        obj.BV = r["BV"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.FlavorID = r["FK_FlavorID"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialID = r["FK_MaterialID"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.RamID = r["FK_RamID"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.StorageID = r["FK_StorageID"].ToString();
                        obj.Storage = r["StorageName"].ToString();
                        obj.StarRatingID = r["FK_StarratingID"].ToString();
                        obj.StarRating = r["StarRatingName"].ToString();
                        lstTmpData.Add(obj);
                    }
                    model.lstSizeTemp = lstTmpData;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("EditProduct")]
        [OnAction(ButtonName = "btnVariant")]
        public ActionResult AddVariantData(Master model, HttpPostedFileBase postedFile1)
        {

            List<Master> lst = new List<Master>();

            #region Productlist
            if (model.ProductID != null)
            {
                model.ProductID = model.ProductID;
                model.ColorID = model.ColorID == "0" ? null : model.ColorID;
                model.FlavorID = model.FlavorID == "0" ? null : model.FlavorID;
                model.MaterialID = model.MaterialID == "0" ? null : model.MaterialID;
                model.SizeID = model.SizeID == "0" ? null : model.SizeID;
                model.ProductInfoCode = model.ProductInfoCode;
                model.RamID = model.RamID == "0" ? null : model.RamID;
                model.StorageID = model.StorageID == "0" ? null : model.StorageID;
                model.StarRatingID = model.StarRatingID == "0" ? null : model.StarRatingID;

                DataSet Branch = model.ProductDetails();
                if (Branch != null && Branch.Tables.Count > 0)
                {
                    model.ProductOtherInfoID = Branch.Tables[0].Rows[0]["PK_ProductOtherInfoID"].ToString();
                    model.ProductID = Branch.Tables[0].Rows[0]["FK_ProductID"].ToString();
                    model.ProductName = Branch.Tables[0].Rows[0]["ProductName"].ToString();
                    model.ProductInfoCode = Branch.Tables[0].Rows[0]["ProductInfoCode"].ToString();
                    model.BrandID = Branch.Tables[0].Rows[0]["FK_BrandID"].ToString();
                    model.BrandName = Branch.Tables[0].Rows[0]["BrandName"].ToString();
                    model.UnitID = Branch.Tables[0].Rows[0]["FK_UnitID"].ToString();
                    model.UnitName = Branch.Tables[0].Rows[0]["UnitName"].ToString();
                    model.CategoryID = Branch.Tables[0].Rows[0]["FK_CategoryID"].ToString();
                    model.CategoryName = Branch.Tables[0].Rows[0]["CategoryName"].ToString();
                    model.SubCategoryID = Branch.Tables[0].Rows[0]["FK_SubCategoryID"].ToString();
                    model.SubCategoryName = Branch.Tables[0].Rows[0]["SubCategoryName"].ToString();
                    model.MainCategoryName = Branch.Tables[0].Rows[0]["MainCategoryName"].ToString();
                    model.MainCategoryID = Branch.Tables[0].Rows[0]["FK_MainCategory"].ToString();
                    model.SizeName = Branch.Tables[0].Rows[0]["SizeName"].ToString();
                    model.ProductQuantity = Branch.Tables[0].Rows[0]["CreditQuantity"].ToString();
                    model.HSNNo = Branch.Tables[0].Rows[0]["HSNNo"].ToString();
                    model.Tags = Branch.Tables[0].Rows[0]["Tags"].ToString();
                    model.Description = Branch.Tables[0].Rows[0]["Description"].ToString();
                    model.ShortDescription = Branch.Tables[0].Rows[0]["ShortDescription"].ToString();
                    //model.UnitID = Branch.Tables[0].Rows[0]["FK_UnitID"].ToString();
                    model.UnitName = Branch.Tables[0].Rows[0]["UnitName"].ToString();
                    model.SizeID = Branch.Tables[0].Rows[0]["FK_SizeID"].ToString();
                    model.SizeName = Branch.Tables[0].Rows[0]["SizeName"].ToString();
                    model.MaterialID = Branch.Tables[0].Rows[0]["FK_MaterialID"].ToString();
                    model.MaterialName = Branch.Tables[0].Rows[0]["MaterialName"].ToString();
                    model.FlavorID = Branch.Tables[0].Rows[0]["FK_FlavorID"].ToString();
                    model.FlavorName = Branch.Tables[0].Rows[0]["FlavorName"].ToString();
                    model.ColorID = Branch.Tables[0].Rows[0]["FK_ColorID"].ToString();
                    model.ColorName = Branch.Tables[0].Rows[0]["ColorName"].ToString();
                    model.BV = Branch.Tables[0].Rows[0]["BV"].ToString();
                    model.MRP = Branch.Tables[0].Rows[0]["MRP"].ToString();
                    model.OfferedPrice = Branch.Tables[0].Rows[0]["OfferedPrice"].ToString();
                    model.DealerPrice = Branch.Tables[0].Rows[0]["DealerPrice"].ToString();
                    model.CGST = Branch.Tables[0].Rows[0]["CGST"].ToString();
                    model.SGST = Branch.Tables[0].Rows[0]["SGST"].ToString();
                    model.IGST = Branch.Tables[0].Rows[0]["IGST"].ToString();
                    model.RamID = Branch.Tables[0].Rows[0]["FK_RAM_ID"].ToString();
                    model.RAM = Branch.Tables[0].Rows[0]["RAM"].ToString();
                    model.StorageID = Branch.Tables[0].Rows[0]["FK_StorageID"].ToString();
                    model.Storage = Branch.Tables[0].Rows[0]["Storage"].ToString();
                    model.StarRatingID = Branch.Tables[0].Rows[0]["FK_StarRatingID"].ToString();
                    model.StarRating = Branch.Tables[0].Rows[0]["StarRating"].ToString();
                    model.DeliveryCharge = Branch.Tables[0].Rows[0]["DeliveryCharge"].ToString();
                    model.ShoppingPerc = Branch.Tables[0].Rows[0]["ShoppingPerc"].ToString();
                   
                    // model.IsNew = Request["IsNew"] = "1" ? checked; 

                    Session["SizeID"] = model.SizeID;
                    Session["FK_MaterialID"] = model.MaterialID;
                    Session["FK_FlavorID"] = model.FlavorID;
                    Session["FK_ColorID"] = model.ColorID;
                    Session["FK_RAM_ID"] = model.RamID;
                    Session["FK_StorageID"] = model.StorageID;
                    Session["FK_StarRatingID"] = model.StarRatingID;

                    if (Branch != null && Branch.Tables.Count > 0 && Branch.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in Branch.Tables[1].Rows)
                        {
                            Master obj = new Master();
                            obj.Images = r["ImagePath"].ToString();
                            obj.ProductImageID = r["PK_ProductImagesID"].ToString();
                            obj.ProductOtherInfoID = r["Fk_ProductOtherInfoId"].ToString();

                            lst.Add(obj);
                        }
                        model.lstProduct = lst;
                    }

                }
            }

            #endregion

            #region Add new Variant

            #region add image

            string filename = "";
         if (postedFile1 != null)
                {
                    filename = DateTime.Now.ToString("ddMMyyyyHHmmsss") + postedFile1.FileName;
                    string path = Guid.NewGuid() + Path.GetExtension(postedFile1.FileName);
                    Stream strm = postedFile1.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        //String path = HttpContext.Current.Request.PhysicalApplicationPath + "images\\ProdSecondaryImage\\";


                        #region 400
                        int newWidth = Convert.ToInt32(400);
                        int newHeight = Convert.ToInt32(400);

                        var thumbImg = new SD.Bitmap(newWidth, newHeight);
                        var thumbGraph = SD.Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);

                        string targetPath = Server.MapPath("~/Images/ProdPrimaryImage/") + filename;
                        thumbImg.Save(targetPath, image.RawFormat);
                        #endregion 225

                        #region 800
                        newWidth = Convert.ToInt32(800);
                        newHeight = Convert.ToInt32(800);

                        thumbImg = new SD.Bitmap(newWidth, newHeight);
                        thumbGraph = SD.Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);

                        targetPath = Server.MapPath("~/Images/ProdSecondaryImage/") + filename;
                        thumbImg.Save(targetPath, image.RawFormat);
                        #endregion 800

                    }
            }

            #endregion

            #region bind datable

            DataTable dt = new DataTable();
            DataRow dr = null;
            Session["ProductInfoCode"] = DateTime.Now.ToString("ddMMyyyyHHmmss");
            model.ProductInfoCode = Session["ProductInfoCode"].ToString();
            if (Session["tmpData1"] != null)
            {
                dt = (DataTable)Session["tmpData1"];
               
                if (dt.Rows.Count > 0)
                {
                    dr = dt.NewRow();

                    dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                    dr["PrimaryImage"] = "";
                    dr["FK_UnitID"] = model.UnitIDD;
                    dr["FK_SizeID"] = model.SizeIDD;
                    dr["FK_ColorID"] = model.ColorIDD;
					dr["FK_MaterialID"] = model.MaterialID;
					dr["FK_FlavorID"]=model.FlavorID;
					dr["FlavorName"]=model.FlavorName;
                    dr["MRP"] = model.MRP1;
                    dr["OfferPrice"] = model.OfferedPrice1;
                    dr["DealerPrice"] = model.DealerPrice1;
                    dr["BV"] = model.BV1;
                    dr["CGST"] = model.CGST1;
                    dr["SGST"] = model.SGST1;
                    dr["IGST"] = model.IGST1;
                    dr["IsFile"] = filename != "" ? true : false;
				    dr["FK_RamID"]=model.RamID;
					dr["RAM"]=model.RAM;
					dr["FK_StorageID"]=model.StorageID;
					dr["StorageName"]=model.Storage;
					dr["FK_StarratingID"]=model.StarRatingID;
					dr["StarRatingName"]=model.StarRating;
					dr["shoppingpointredemperc"]=model.ShoppingPerc;
					dr["UnitName"]=model.UnitName;
					dr["SizeName"]=model.SizeName;
					dr["ColorName"]=model.ColorName;
					dr["MaterialName"]=model.MaterialName;
                    dr["ProductInfoCode"] = model.ProductInfoCode;
                    dr["ImagePathPrimary"] = "../../Images/ProdPrimaryImage/" + filename;
                    dr["ImagePathSecondary"] = "../../Images/ProdSecondaryImage/" + filename;
                    dt.Rows.Add(dr);
                    Session["tmpData1"] = dt;
                }
            }
            else
            {
                dr = dt.NewRow();
                dt.Columns.Add("ProductInfoCode", typeof(string));
                dt.Columns.Add("PrimaryImage", typeof(string));
                dt.Columns.Add("FK_UnitID", typeof(string));
                dt.Columns.Add("FK_SizeID", typeof(string));
                dt.Columns.Add("FK_ColorID", typeof(string));
				dt.Columns.Add("FK_MaterialID", typeof(string));
				dt.Columns.Add("FK_FlavorID", typeof(string));
				dt.Columns.Add("FlavorName", typeof(string));
                dt.Columns.Add("MRP", typeof(string));
                dt.Columns.Add("OfferPrice", typeof(string));
                dt.Columns.Add("DealerPrice", typeof(string));
                dt.Columns.Add("BV", typeof(string));
                dt.Columns.Add("CGST", typeof(string));
                dt.Columns.Add("SGST", typeof(string));
                dt.Columns.Add("IGST", typeof(string));
                dt.Columns.Add("Quantity", typeof(string));
				dt.Columns.Add("FK_RamID", typeof(string));
                dt.Columns.Add("RAM", typeof(string));
                dt.Columns.Add("FK_StorageID", typeof(string));
                dt.Columns.Add("StorageName", typeof(string));
                dt.Columns.Add("FK_StarratingID", typeof(string));
                dt.Columns.Add("StarRatingName", typeof(string));
                dt.Columns.Add("shoppingpointredemperc", typeof(string));
				dt.Columns.Add("UnitName", typeof(string));
				dt.Columns.Add("SizeName", typeof(string));
				dt.Columns.Add("ColorName", typeof(string));
				dt.Columns.Add("MaterialName", typeof(string));
                dt.Columns.Add("ImagePathSecondary");
                dt.Columns.Add("ImagePathPrimary");
                dt.Columns.Add("IsFile");
                

                dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                   dr["PrimaryImage"] = "";
                dr["FK_UnitID"] = model.UnitIDD;
                dr["FK_SizeID"] = model.SizeIDD;
                dr["FK_ColorID"] = model.ColorIDD;
				dr["FK_MaterialID"] = model.MaterialID;
				dr["FK_FlavorID"]=model.FlavorID;
				dr["FlavorName"]=model.FlavorName;
                dr["MRP"] = model.MRP1;
                dr["OfferPrice"] = model.OfferedPrice1;
                dr["DealerPrice"] = model.DealerPrice1;
                dr["BV"] = model.BV1;
                dr["CGST"] = model.CGST1;
                dr["SGST"] = model.SGST1;
                dr["IGST"] = model.IGST1;
				dr["FK_RamID"]=model.RamID;
				dr["RAM"]=model.RAM;
				dr["FK_StorageID"]=model.StorageID;
				dr["StorageName"]=model.Storage;
				dr["FK_StarratingID"]=model.StarRatingID;
				dr["StarRatingName"]=model.StarRating;
				dr["shoppingpointredemperc"]=model.ShoppingPerc;
				dr["UnitName"]=model.UnitName;
				dr["SizeName"]=model.SizeName;
				dr["ColorName"]=model.ColorName;
				dr["MaterialName"]=model.MaterialName;
                dr["IsFile"] = filename != "" ? true : false;
                dr["ImagePathPrimary"] = "../../Images/ProdPrimaryImage/" + filename; 
                dr["ImagePathSecondary"] = "../../Images/ProdSecondaryImage/" + filename; 

                dt.Rows.Add(dr);
                Session["tmpData1"] = dt;
            }
            #endregion
            List<Master> lstTmpData = new List<Master>();
            if(dt!=null && dt.Rows.Count > 0)
            {
                foreach(DataRow r in dt.Rows)
                {

                    Master obj = new Master();
                    obj.UnitIDD = r["FK_UnitID"].ToString();
                    obj.SizeIDD = r["FK_SizeID"].ToString();
                    obj.ColorIDD = r["FK_ColorID"].ToString();
                    obj.MRP1 = r["MRP"].ToString();
                    obj.OfferedPrice1 = r["OfferPrice"].ToString();
                    obj.DealerPrice1 = r["DealerPrice"].ToString();
                    obj.BV1 = r["BV"].ToString();
                    obj.CGST1 = r["CGST"].ToString();
                    obj.SGST1 = r["SGST"].ToString();
                    obj.IGST1 = r["IGST"].ToString();
                    obj.ProductQuantity1 = r["Quantity"].ToString();
                    obj.IsFile = Convert.ToBoolean(r["IsFile"]);
                    obj.ImagePathPrimary = r["ImagePathPrimary"].ToString();
                    obj.ImagePathSecondary = r["ImagePathSecondary"].ToString();
                    lstTmpData.Add(obj);
                }

                model.lstProductVariant = lstTmpData;
            }

            model.dtProductVariant = dt;

            model.dtProductImages = dtSecondaryImages;


           
                #endregion


                #region Dropdowns

                #region 
                Master obj1 = new Master();
            obj1.UnitIDD = model.UnitIDD;
            List<SelectListItem> ddlSizee = new List<SelectListItem>();
            DataSet dsSector = obj1.SizeList1();

            if (dsSector != null && dsSector.Tables.Count > 0)
            {
                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    ddlSizee.Add(new SelectListItem { Text = r["SizeName"].ToString(), Value = r["PK_SizeID"].ToString() });

                }
                ViewBag.ddlSizee = ddlSizee;
            }
            #endregion

            #region 
            List<SelectListItem> ddlSize = new List<SelectListItem>();
            DataSet dsSector1 = model.SizeList();

            if (dsSector1 != null && dsSector1.Tables.Count > 0)
            {
                foreach (DataRow r in dsSector1.Tables[0].Rows)
                {
                    ddlSize.Add(new SelectListItem { Text = r["SizeName"].ToString(), Value = r["PK_SizeID"].ToString() });

                }
                ViewBag.ddlSize = ddlSize;
            }
            #endregion

            #region Unit
            Master objUnit = new Master();
            int count4 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds4 = objUnit.UnitList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion
            #region Color
            Master objColor = new Master();
            int countc = 0;
            List<SelectListItem> ddlColor = new List<SelectListItem>();
            DataSet ds5 = objColor.ColorList();
            if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds5.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlColor.Add(new SelectListItem { Text = "Select Color", Value = "0" });
                    }
                    ddlColor.Add(new SelectListItem { Text = r["ColorName"].ToString(), Value = r["PK_ColorID"].ToString() });
                    countc = countc + 1;
                }
            }

            ViewBag.ddlColor = ddlColor;

            #endregion
            #region Flavor
            Master objFlavor = new Master();
            int countF = 0;
            List<SelectListItem> ddlFlavor = new List<SelectListItem>();
            DataSet dsf = objFlavor.FlavorList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countF == 0)
                    {
                        ddlFlavor.Add(new SelectListItem { Text = "Select Flavor", Value = "0" });
                    }
                    ddlFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["PK_FlavorID"].ToString() });
                    countF = countF + 1;
                }
            }

            ViewBag.ddlFlavor = ddlFlavor;

            #endregion
            #region Material
            Master objMaterial = new Master();
            int countm = 0;
            List<SelectListItem> ddlMaterial = new List<SelectListItem>();
            DataSet dsm = objMaterial.MaterialList();
            if (dsm != null && dsm.Tables.Count > 0 && dsm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsm.Tables[0].Rows)
                {
                    if (countm == 0)
                    {
                        ddlMaterial.Add(new SelectListItem { Text = "Select Material", Value = "0" });
                    }
                    ddlMaterial.Add(new SelectListItem { Text = r["MaterialName"].ToString(), Value = r["PK_MaterialID"].ToString() });
                    countm = countm + 1;
                }
            }

            ViewBag.ddlMaterial = ddlMaterial;

            #endregion
            #region RAM
            Master objRAM = new Master();
            int countr = 0;
            List<SelectListItem> ddlRAM = new List<SelectListItem>();
            DataSet dsRm = objRAM.RAMList();
            if (dsRm != null && dsRm.Tables.Count > 0 && dsRm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsRm.Tables[0].Rows)
                {
                    if (countr == 0)
                    {
                        ddlRAM.Add(new SelectListItem { Text = "Select RAM", Value = "0" });
                    }
                    ddlRAM.Add(new SelectListItem { Text = r["RAM"].ToString(), Value = r["PK_RAM_ID"].ToString() });
                    countr = countr + 1;
                }
            }

            ViewBag.ddlRAM = ddlRAM;
            #endregion
            #region Storage
            Master objStorage = new Master();
            int countst = 0;
            List<SelectListItem> ddlStorage = new List<SelectListItem>();
            DataSet dsSt = objStorage.StorageList();
            if (dsSt != null && dsSt.Tables.Count > 0 && dsSt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSt.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        ddlStorage.Add(new SelectListItem { Text = "Select Storage", Value = "0" });
                    }
                    ddlStorage.Add(new SelectListItem { Text = r["Storage"].ToString(), Value = r["PK_StorageID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.ddlStorage = ddlStorage;
            #endregion
            #region StarRating
            Master objStarRating = new Master();
            int countstr = 0;
            List<SelectListItem> ddlStarRating = new List<SelectListItem>();
            DataSet dsStr = objStarRating.StarRatingList();
            if (dsStr != null && dsStr.Tables.Count > 0 && dsStr.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsStr.Tables[0].Rows)
                {
                    if (countstr == 0)
                    {
                        ddlStarRating.Add(new SelectListItem { Text = "Select Star Rating", Value = "0" });
                    }
                    ddlStarRating.Add(new SelectListItem { Text = r["StarRating"].ToString(), Value = r["PK_StarRatingID"].ToString() });
                    countstr = countstr + 1;
                }
            }

            ViewBag.ddlStarRating = ddlStarRating;
            #endregion

            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string path = Server.MapPath("../images/ProdSecondaryImage/");
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                var extension = file.ContentType.Split('/')[1].ToString();
                var name = Guid.NewGuid() + "." + extension;
                var fileName = path + name;
                var newfilename = "../images/ProdSecondaryImage/" + name;
                var filepath = path + fileName;
                file.SaveAs(Path.Combine(path, fileName));
                //file.PostedFile.SaveAs(Server.MapPath("~/Images/Profile/") + DateTime.Now.ToString("ddMMyyyyHHmm") + fileName);

                if (Session["dtSecImages"] != null)
                {
                    dtSecondaryImages = (DataTable)Session["dtSecImages"];
                    DataRow dr = null;
                    if (dtSecondaryImages.Rows.Count > 0)
                    {
                        dr = dtSecondaryImages.NewRow();
                        dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                        dr["ImagePath"] = newfilename;


                        dtSecondaryImages.Rows.Add(dr);
                        Session["dtSecImages"] = dtSecondaryImages;
                    }
                }
                else
                {
                    dtSecondaryImages.Columns.Add("ProductInfoCode", typeof(string));

                    dtSecondaryImages.Columns.Add("ImagePath", typeof(string));

                    DataRow dr = dtSecondaryImages.NewRow();
                    dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                    dr["ImagePath"] = newfilename;



                    dtSecondaryImages.Rows.Add(dr);
                    Session["dtSecImages"] = dtSecondaryImages;
                }
            }

            return Json(files.Count + " Files Uploaded!");
        }

        [HttpPost]
        [ActionName("SaveProduct")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveProduct(Master obj)
        {
            string FormName = "";
            string Controller = "";

            try
            {
                DateTime d1 = DateTime.Now;
                string ctrC1 = d1.Year.ToString() + d1.Day.ToString() + d1.Month.ToString() + d1.Hour.ToString() + d1.Minute.ToString() + d1.Second.ToString() + d1.Millisecond.ToString();
                obj.ProductInfoCode = ctrC1;

                obj.IsNew = Request["IsNew"] != null ? "1" : "0";
                obj.IsBestSeller = Request["IsBestSeller"] != null ? "1" : "0";
                obj.IsCODAvailable = Request["IsCODAvailable"] != null ? "1" : "0";
                obj.IsFeatured = Request["IsFeatured"] != null ? "1" : "0";
                obj.IsProductavailable = Request["IsProductavailable"] != null ? "1" : "0";
                obj.IsShippingFree = Request["IsShippingFree"] != null ? "1" : "0";

                string noofrows = Request["hdrows"].ToString();
                string primaryimage = "";
                string unitid = "";
                string unitname = "";
                string sizeid = "";
                string sizename = "";
                string colorid = "";
                string colorname = "";
                string qty = "";
                string bv = "";
                string mrp = "";
                string offprice = "";
                string dealprice = "";
                string cgst = "";
                string sgst = "";
                string igst = "";
                string img = "";
                string flavorid = "";
                string flavorname = "";
                string materialid = "";
                string materialname = "";
                string infocode = "";
                string ramid = "";
                string ramname = "";
                string storageid = "";
                string storagename = "";
                string starratingid = "";
                string starratingname = "";


                DataTable dtst = new DataTable();
                dtst.Columns.Add("ProductInfoCode", typeof(string));
                dtst.Columns.Add("PrimaryImage", typeof(string));
                dtst.Columns.Add("FK_UnitID", typeof(string));
                dtst.Columns.Add("UnitName", typeof(string));
                dtst.Columns.Add("FK_SizeID", typeof(string));
                dtst.Columns.Add("SizeName", typeof(string));
                dtst.Columns.Add("FK_ColorID", typeof(string));
                dtst.Columns.Add("ColorName", typeof(string));
                dtst.Columns.Add("FK_MaterialID", typeof(string));
                dtst.Columns.Add("MaterialName", typeof(string));
                dtst.Columns.Add("FK_FlavorID", typeof(string));
                dtst.Columns.Add("FlavorName", typeof(string));
                dtst.Columns.Add("MRP", typeof(string));
                dtst.Columns.Add("OfferPrice", typeof(string));
                dtst.Columns.Add("DealerPrice", typeof(string));
                dtst.Columns.Add("BV", typeof(string));
                dtst.Columns.Add("CGST", typeof(string));
                dtst.Columns.Add("SGST", typeof(string));
                dtst.Columns.Add("IGST", typeof(string));
                dtst.Columns.Add("Quantity", typeof(string));
                dtst.Columns.Add("FK_RamID", typeof(string));
                dtst.Columns.Add("RAM", typeof(string));
                dtst.Columns.Add("FK_StorageID", typeof(string));
                dtst.Columns.Add("StorageName", typeof(string));
                dtst.Columns.Add("FK_StarratingID", typeof(string));
                dtst.Columns.Add("StarRatingName", typeof(string));

                obj.SizeID = obj.SizeID == "0" ? null : obj.SizeID;
                obj.ColorID = obj.ColorID == "0" ? null : obj.ColorID;
                obj.MaterialID = obj.MaterialID == "0" ? null : obj.MaterialID;
                obj.FlavorID = obj.FlavorID == "0" ? null : obj.FlavorID;

                obj.RamID = obj.RamID == "0" ? null : obj.RamID;
                obj.StorageID = obj.StorageID == "0" ? null : obj.StorageID;
                obj.StarRatingID = obj.StarRatingID == "0" ? null : obj.StarRatingID;

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    sizeid = Request["sizeid_ " + i].ToString();
                    unitid = Request["unitid_ " + i].ToString();
                    colorid = Request["colorid_ " + i].ToString();
                    qty = Request["txtQuantity_ " + i].ToString();
                    bv = Request["txtBV_ " + i].ToString();
                    mrp = Request["txtMRP_ " + i].ToString();
                    offprice = Request["txtOfferedPrice_ " + i].ToString();
                    dealprice = Request["txtDealerPrice_ " + i].ToString();
                    cgst = Request["txtCGST_ " + i].ToString();
                    sgst = Request["txtSGST_ " + i].ToString();
                    igst = Request["txtIGST_ " + i].ToString();
                    primaryimage = "../images/no-profile.png";
                    unitname = Request["unitname_ " + i].ToString();
                    sizename = Request["sizename_ " + i].ToString();
                    colorname = Request["colorname_ " + i].ToString();
                    flavorid = Request["flavorid_ " + i].ToString();
                    flavorname = Request["flavorname_ " + i].ToString();
                    materialid = Request["materialid_ " + i].ToString();
                    materialname = Request["materialname_ " + i].ToString();
                    infocode = Request["productinfocode_ " + i].ToString();

                    ramid = Request["ramid_ " + i].ToString();
                    ramname = Request["ramname_ " + i].ToString();
                    storageid = Request["storageid_ " + i].ToString();
                    storagename = Request["storagename_ " + i].ToString();
                    starratingid = Request["starrate_ " + i].ToString();
                    starratingname = Request["starratingname_ " + i].ToString();

                    dtst.Rows.Add(infocode, primaryimage, unitid, unitname, sizeid, sizename, colorid, colorname, materialid, materialname, flavorid,
                        flavorname, mrp, offprice, dealprice, bv, cgst, sgst, igst, qty,
                        ramid, ramname, storageid, storagename, starratingid, starratingname
                        );
                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtProductQuantity = dtst;

                dtSecondaryImages = (DataTable)Session["dtSecImages"];
                obj.dtProductImages = dtSecondaryImages;

                if (Session["UserType"].ToString() == "Admin")
                {
                    obj.IsApproved = "1";
                }
                else
                {
                    obj.IsApproved = "0";
                }
                DataSet ds = obj.SaveProduct();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Product"] = " Product Saved successfully.";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Product"] = ex.Message;
            }
            FormName = "Products";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
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
                model.AddedBy = Session["Pk_AdminId"].ToString();
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
                    obj.HSNNo = r["HSNNo"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.FlavorID = r["FK_FlavorID"].ToString();
                    obj.MaterialID = r["FK_MaterialID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
					obj.UnitID=r["FK_UnitID"].ToString();
					obj.UnitName=r["UnitName"].ToString();
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
                obj.AddedBy = Session["Pk_AdminId"].ToString();
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

        public ActionResult EditProduct(string ProductID, string ColorID, string FlavorID, string MaterialID,string UnitID, string SizeID, string ProductInfoCode, string RamID, string StorageID, string StarRatingID)

        {
            Master model = new Master();
            List<Master> lst = new List<Master>();

            List<Master> lst1 = new List<Master>();
            if (ProductID != null)
            {
                model.ProductID = ProductID;
                model.ColorID = ColorID == "0" ? null : ColorID;
                model.FlavorID = FlavorID == "0" ? null : FlavorID;
                model.MaterialID = MaterialID == "0" ? null : MaterialID;
				model.UnitID=UnitID=="0"?null:UnitID;
                model.SizeID = SizeID == "0" ? null : SizeID;
                model.ProductInfoCode = ProductInfoCode;
                model.RamID = RamID == "0" ? null : RamID;
                model.StorageID = StorageID == "0" ? null : StorageID;
                model.StarRatingID = StarRatingID == "0" ? null : StarRatingID;

              

                DataSet Branch = model.ProductDetails();
                if (Branch != null && Branch.Tables.Count > 0)
                {

                    model.CategoryID = Branch.Tables[0].Rows[0]["FK_CategoryID"].ToString();
                    model.CategoryName = Branch.Tables[0].Rows[0]["CategoryName"].ToString();
                    model.SubCategoryID = Branch.Tables[0].Rows[0]["FK_SubCategoryID"].ToString();
                    model.SubCategoryName = Branch.Tables[0].Rows[0]["SubCategoryName"].ToString();
                    model.MainCategoryName = Branch.Tables[0].Rows[0]["MainCategoryName"].ToString();
                    model.MainCategoryID = Branch.Tables[0].Rows[0]["FK_MainCategory"].ToString();
                  //  model.IsTimeProduct1 = Convert.ToBoolean(Branch.Tables[0].Rows[0]["IsTimeProduct"].ToString());
                    model.DeliveryCharge = Branch.Tables[0].Rows[0]["DeliveryCharge"].ToString();
                    model.ShoppingPerc = Branch.Tables[0].Rows[0]["ShoppingPerc"].ToString();
                    model.ProductName = Branch.Tables[0].Rows[0]["ProductName"].ToString();
                    model.HSNNo = Branch.Tables[0].Rows[0]["HSNNo"].ToString();
                    model.Description = Branch.Tables[0].Rows[0]["Description"].ToString();
                    model.ShortDescription = Branch.Tables[0].Rows[0]["ShortDescription"].ToString();
                    model.ProductOtherInfoID = Branch.Tables[0].Rows[0]["PK_ProductOtherInfoID"].ToString();
                    model.ProductID = Branch.Tables[0].Rows[0]["FK_ProductID"].ToString();

                    foreach (DataRow r1 in Branch.Tables[0].Rows)
                    {
                        #region foreach
                        Master obj1 = new Master();
                       
                        obj1.ProductInfoCode = r1["ProductInfoCode"].ToString();
                        obj1.BrandID = r1["FK_BrandID"].ToString();
                        obj1.BrandName = r1["BrandName"].ToString();
                        obj1.UnitID = r1["FK_UnitID"].ToString();
                        obj1.UnitName = r1["UnitName"].ToString();
                        
                        obj1.SizeName = r1["SizeName"].ToString();
                        obj1.ProductQuantity = r1["CreditQuantity"].ToString();
                        obj1.Tags = r1["Tags"].ToString();
                       
                        //obj1el.UnitID = r1["FK_UnitID"].ToString();
                        obj1.UnitName = r1["UnitName"].ToString();
                        obj1.SizeID = r1["FK_SizeID"].ToString();
                        obj1.SizeName = r1["SizeName"].ToString();
                        obj1.MaterialID = r1["FK_MaterialID"].ToString();
                        obj1.MaterialName = r1["MaterialName"].ToString();
                        obj1.FlavorID = r1["FK_FlavorID"].ToString();
                        obj1.FlavorName = r1["FlavorName"].ToString();
                        obj1.ColorID = r1["FK_ColorID"].ToString();
                        obj1.ColorName = r1["ColorName"].ToString();
                        obj1.BV = r1["BV"].ToString();
                        obj1.MRP = r1["MRP"].ToString();
                        obj1.OfferedPrice = r1["OfferedPrice"].ToString();
                        obj1.DealerPrice = r1["DealerPrice"].ToString();
                        obj1.CGST = r1["CGST"].ToString();
                        obj1.SGST = r1["SGST"].ToString();
                        obj1.IGST = r1["IGST"].ToString();
                        obj1.RamID = r1["FK_RAM_ID"].ToString();
                        obj1.RAM = r1["RAM"].ToString();
                        obj1.StorageID = r1["FK_StorageID"].ToString();
                        obj1.Storage = r1["Storage"].ToString();
                        obj1.StarRatingID = r1["FK_StarRatingID"].ToString();
                        obj1.StarRating = r1["StarRating"].ToString();
                        #region 
                        List<SelectListItem> ddlSize = new List<SelectListItem>();
                        DataSet dsSector = model.SizeList();

                        if (dsSector != null && dsSector.Tables.Count > 0)
                        {
                            foreach (DataRow r in dsSector.Tables[0].Rows)
                            {
                                ddlSize.Add(new SelectListItem { Text = r["SizeName"].ToString(), Value = r["PK_SizeID"].ToString() });

                            }
                            ViewBag.ddlSize = ddlSize;
                        }
                        #endregion




                        // model.IsNew = Request["IsNew"] = "1" ? checked; 
						
                        if (model.UnitID == null)
                        {
                            model.UnitID = "0";
                        }
                        Session["UnitID"] = model.UnitID;

                        if (model.SizeID == null)
                        {
                            model.SizeID = "0";
                        }
                        Session["SizeID"] = model.SizeID;
                        if (model.MaterialID == null)
                        {
                            model.MaterialID = "0";
                        }

                        Session["FK_MaterialID"] = model.MaterialID;

                        if (model.FlavorID == null)
                        {
                            model.FlavorID = "0";
                        }

                        Session["FK_FlavorID"] = model.FlavorID;
                        if (model.ColorID == null)
                        {
                            model.ColorID = "0";
                        }
                        Session["FK_ColorID"] = model.ColorID;

                        if (model.RamID == null)
                        {
                            model.RamID = "0";
                        }
                        Session["FK_RAM_ID"] = model.RamID;
                        if (model.StorageID == null)
                        {
                            model.StorageID = "0";
                        }
                        Session["FK_StorageID"] = model.StorageID;
                        if (model.StarRatingID == null)
                        {
                            model.StarRatingID = "0";
                        }
                        Session["FK_StarRatingID"] = model.StarRatingID;

                        if (Branch != null && Branch.Tables.Count > 0 && Branch.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow r in Branch.Tables[1].Rows)
                            {
                                Master obj = new Master();
                                obj.Images = r["ImagePath"].ToString();
                                obj.ProductImageID = r["PK_ProductImagesID"].ToString();
                                obj.ProductOtherInfoID = r["Fk_ProductOtherInfoId"].ToString();

                                lst.Add(obj);
                            }
                            model.lstProduct = lst;
                        }

                        #endregion

                        lst1.Add(obj1);
                    }

                    model.lstProductData = lst1;
                }
            }

            else
            {
                List<SelectListItem> ddlSize = new List<SelectListItem>();
                ddlSize.Add(new SelectListItem { Text = "Select Size", Value = "0" });
                ViewBag.ddlSize = ddlSize;
            }

            List<SelectListItem> ddlSizee = new List<SelectListItem>();
            ddlSizee.Add(new SelectListItem { Text = "Select Size", Value = "0" });
            ViewBag.ddlSizee = ddlSizee;

            #region Unit
            Master objUnit = new Master();
            int count4 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds4 = objUnit.UnitList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion
            #region Color
            Master objColor = new Master();
            int countc = 0;
            List<SelectListItem> ddlColor = new List<SelectListItem>();
            DataSet ds5 = objColor.ColorList();
            if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds5.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlColor.Add(new SelectListItem { Text = "Select Color", Value = "0" });
                    }
                    ddlColor.Add(new SelectListItem { Text = r["ColorName"].ToString(), Value = r["PK_ColorID"].ToString() });
                    countc = countc + 1;
                }
            }

            ViewBag.ddlColor = ddlColor;

            #endregion
            #region Flavor
            Master objFlavor = new Master();
            int countF = 0;
            List<SelectListItem> ddlFlavor = new List<SelectListItem>();
            DataSet dsf = objFlavor.FlavorList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countF == 0)
                    {
                        ddlFlavor.Add(new SelectListItem { Text = "Select Flavor", Value = "0" });
                    }
                    ddlFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["PK_FlavorID"].ToString() });
                    countF = countF + 1;
                }
            }

            ViewBag.ddlFlavor = ddlFlavor;

            #endregion
            #region Material
            Master objMaterial = new Master();
            int countm = 0;
            List<SelectListItem> ddlMaterial = new List<SelectListItem>();
            DataSet dsm = objMaterial.MaterialList();
            if (dsm != null && dsm.Tables.Count > 0 && dsm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsm.Tables[0].Rows)
                {
                    if (countm == 0)
                    {
                        ddlMaterial.Add(new SelectListItem { Text = "Select Material", Value = "0" });
                    }
                    ddlMaterial.Add(new SelectListItem { Text = r["MaterialName"].ToString(), Value = r["PK_MaterialID"].ToString() });
                    countm = countm + 1;
                }
            }

            ViewBag.ddlMaterial = ddlMaterial;

            #endregion
            #region RAM
            Master objRAM = new Master();
            int countr = 0;
            List<SelectListItem> ddlRAM = new List<SelectListItem>();
            DataSet dsRm = objRAM.RAMList();
            if (dsRm != null && dsRm.Tables.Count > 0 && dsRm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsRm.Tables[0].Rows)
                {
                    if (countr == 0)
                    {
                        ddlRAM.Add(new SelectListItem { Text = "Select RAM", Value = "0" });
                    }
                    ddlRAM.Add(new SelectListItem { Text = r["RAM"].ToString(), Value = r["PK_RAM_ID"].ToString() });
                    countr = countr + 1;
                }
            }

            ViewBag.ddlRAM = ddlRAM;
            #endregion
            #region Storage
            Master objStorage = new Master();
            int countst = 0;
            List<SelectListItem> ddlStorage = new List<SelectListItem>();
            DataSet dsSt = objStorage.StorageList();
            if (dsSt != null && dsSt.Tables.Count > 0 && dsSt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSt.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        ddlStorage.Add(new SelectListItem { Text = "Select Storage", Value = "0" });
                    }
                    ddlStorage.Add(new SelectListItem { Text = r["Storage"].ToString(), Value = r["PK_StorageID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.ddlStorage = ddlStorage;
            #endregion
            #region StarRating
            Master objStarRating = new Master();
            int countstr = 0;
            List<SelectListItem> ddlStarRating = new List<SelectListItem>();
            DataSet dsStr = objStarRating.StarRatingList();
            if (dsStr != null && dsStr.Tables.Count > 0 && dsStr.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsStr.Tables[0].Rows)
                {
                    if (countstr == 0)
                    {
                        ddlStarRating.Add(new SelectListItem { Text = "Select Star Rating", Value = "0" });
                    }
                    ddlStarRating.Add(new SelectListItem { Text = r["StarRating"].ToString(), Value = r["PK_StarRatingID"].ToString() });
                    countstr = countstr + 1;
                }
            }

            ViewBag.ddlStarRating = ddlStarRating;
            #endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("EditProduct")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateProduct(Master obj, HttpPostedFileBase postedFile)
        {
            string FormName = "";
            string Controller = "";
            dtSecondaryImages.Columns.Add("ImageType");
            dtSecondaryImages.Columns.Add("ImagePath");
            dtSecondaryImages.Columns.Add("ProductInfoCode");
            
         
            try
            {
               
                if (postedFile != null)
                {
                    string filename = DateTime.Now.ToString("ddMMyyyyHHmmsss") + postedFile.FileName;
                    string path = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    Stream strm = postedFile.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        //String path = HttpContext.Current.Request.PhysicalApplicationPath + "images\\ProdSecondaryImage\\";
                       

                        #region 400
                        int newWidth = Convert.ToInt32(400);
                        int newHeight = Convert.ToInt32(400);

                        var thumbImg = new SD.Bitmap(newWidth, newHeight);
                        var thumbGraph = SD.Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);

                        string targetPath = Server.MapPath("~/Images/ProdPrimaryImage/") + filename;
                        thumbImg.Save(targetPath, image.RawFormat);
                        #endregion 225

                        #region 800
                        newWidth = Convert.ToInt32(800);
                        newHeight = Convert.ToInt32(800);

                        thumbImg = new SD.Bitmap(newWidth, newHeight);
                        thumbGraph = SD.Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);

                        targetPath = Server.MapPath("~/Images/ProdSecondaryImage/") + filename;
                        thumbImg.Save(targetPath, image.RawFormat);
                        #endregion 800

                    }
					 if (Session["dtSecImages"] != null)
                    {
                        dtSecondaryImages = (DataTable)Session["dtSecImages"];
                        DataRow dr = null;


                        if (dtSecondaryImages.Rows.Count > 0)
                        {
						    dtSecondaryImages =null;
                            dr = dtSecondaryImages.NewRow();
                            dr["ProductInfoCode"] = obj.ProductInfoCode;
                            dr["ImagePath"] = "../../Images/ProdSecondaryImage/" + filename;
                            dr["ImageType"] = "Primary";

                            dtSecondaryImages.Rows.Add(dr);
                            DataRow dr1 = null;
                            dr1 = dtSecondaryImages.NewRow();
                            dr1["ProductInfoCode"] =obj.ProductInfoCode;
                            dr1["ImagePath"] = "../../Images/ProdSecondaryImage/" + filename;
                            dr1["ImageType"] = "Secondary";
                            dtSecondaryImages.Rows.Add(dr1);
							
                            Session["dtSecImages"] = dtSecondaryImages;
                        }
                    }
					 else
                    {
                        DataRow dr = dtSecondaryImages.NewRow();
                        dr["ProductInfoCode"] =  obj.ProductInfoCode;
                        dr["ImagePath"] = "../../Images/ProdPrimaryImage/" + filename;
                        dr["ImageType"] = "Primary";
                        dtSecondaryImages.Rows.Add(dr);
                        DataRow dr1 = null;
                        dr1 = dtSecondaryImages.NewRow();
                        dr1["ProductInfoCode"] = obj.ProductInfoCode;
                        dr1["ImagePath"] = "../../Images/ProdSecondaryImage/" + filename;
                        dr1["ImageType"] = "Secondary";
                        dtSecondaryImages.Rows.Add(dr1);
						Session["dtSecImages"] = dtSecondaryImages;
                    }

                    //DataRow dr = dtSecondaryImages.NewRow();

                    //dr["ProductInfoCode"] = obj.ProductInfoCode;
                    //dr["ImagePath"] = "../../Images/ProdPrimaryImage/" + filename;
                    //dr["ImageType"] = "Primary";
                    //dtSecondaryImages.Rows.Add(dr);
                    //DataRow dr1 = null;
                    //dr1 = dtSecondaryImages.NewRow();
                    //dr1["ProductInfoCode"] = obj.ProductInfoCode;
                    //dr1["ImagePath"] = "../../Images/ProdSecondaryImage/" + filename;
                    //dr1["ImageType"] = "Secondary";
                    //dtSecondaryImages.Rows.Add(dr1);
                    //obj.Images = "../images/ProdPrimaryImage/" + path;
                    //postedFile.SaveAs(Path.Combine(Server.MapPath(obj.Images)));


                }
                
                DataTable dtst = new DataTable();
               
                dtst.Columns.Add("ProductInfoCode", typeof(string));
                dtst.Columns.Add("PrimaryImage", typeof(string));
                dtst.Columns.Add("FK_UnitID", typeof(string));
                dtst.Columns.Add("UnitName", typeof(string));
                dtst.Columns.Add("FK_SizeID", typeof(string));
                dtst.Columns.Add("SizeName", typeof(string));
                dtst.Columns.Add("FK_ColorID", typeof(string));
                dtst.Columns.Add("ColorName", typeof(string));
                dtst.Columns.Add("FK_MaterialID", typeof(string));
                dtst.Columns.Add("MaterialName", typeof(string));
                dtst.Columns.Add("FK_FlavorID", typeof(string));
                dtst.Columns.Add("FlavorName", typeof(string));
                dtst.Columns.Add("MRP", typeof(string));
                dtst.Columns.Add("OfferPrice", typeof(string));
                dtst.Columns.Add("DealerPrice", typeof(string));
                dtst.Columns.Add("BV", typeof(string));
                dtst.Columns.Add("CGST", typeof(string));
                dtst.Columns.Add("SGST", typeof(string));
                dtst.Columns.Add("IGST", typeof(string));
                dtst.Columns.Add("Quantity", typeof(string));
                dtst.Columns.Add("FK_RamID", typeof(string));
                dtst.Columns.Add("RAM", typeof(string));
                dtst.Columns.Add("FK_StorageID", typeof(string));
                dtst.Columns.Add("StorageName", typeof(string));
                dtst.Columns.Add("FK_StarratingID", typeof(string));
                dtst.Columns.Add("StarRatingName", typeof(string));
                dtst.Columns.Add("shoppingpointredemperc", typeof(string));
                
				if(Session["tmpData1"]!=null)
				{
				DataTable dt1 = (DataTable)Session["tmpData1"];
                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {
				dtst.Rows.Add(dt1.Rows[i]["ProductInfoCode"].ToString(), dt1.Rows[i]["PrimaryImage"].ToString(), dt1.Rows[i]["FK_UnitID"].ToString()
                        , dt1.Rows[i]["UnitName"].ToString(), dt1.Rows[i]["FK_SizeID"].ToString(), dt1.Rows[i]["SizeName"].ToString(), dt1.Rows[i]["FK_ColorID"].ToString()
                        , dt1.Rows[i]["ColorName"].ToString(), dt1.Rows[i]["FK_MaterialID"].ToString(), dt1.Rows[i]["MaterialName"].ToString(),
                         dt1.Rows[i]["FK_FlavorID"].ToString(), dt1.Rows[i]["FlavorName"].ToString(), dt1.Rows[i]["MRP"].ToString(),
                         dt1.Rows[i]["OfferPrice"].ToString(), dt1.Rows[i]["DealerPrice"].ToString(), string.IsNullOrEmpty(dt1.Rows[i]["BV"].ToString()) ? "0" : dt1.Rows[i]["BV"].ToString(),
                         dt1.Rows[i]["CGST"].ToString(), dt1.Rows[i]["SGST"].ToString(), dt1.Rows[i]["IGST"].ToString(),
                         dt1.Rows[i]["Quantity"].ToString(), dt1.Rows[i]["FK_RamID"].ToString(), dt1.Rows[i]["RAM"].ToString(),
                         dt1.Rows[i]["FK_StorageID"].ToString(), dt1.Rows[i]["StorageName"].ToString(), dt1.Rows[i]["FK_StarratingID"].ToString(),
                         dt1.Rows[i]["StarRatingName"].ToString(), dt1.Rows[i]["shoppingpointredemperc"].ToString()
                        );
                }
                
				}
                
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                
                obj.dtProductImages = (DataTable)Session["dtSecImages"];
				Session["dtSecImages"]=null;
                obj.dtProductVariant = dtst;
                DataSet ds = obj.UpdateProduct();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["EditProduct"] = " Product updated successfully !";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["EditProduct"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["EditProduct"] = ex.Message;
            }
            FormName = "EditProduct";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult DeleteProductImage(string ImageID)
        {
            Master obj = new Master();
            try
            {

                obj.ProductImageID = ImageID;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteProductImage();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Discount"] = " Deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Discount"] = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Offer

        public ActionResult Offer(string OfferID)
        {
            Master model = new Master();
            if (OfferID != null)
            {
                model.OfferID = Crypto.Decrypt(OfferID);

                DataSet ds = model.OfferList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    model.OfferID = OfferID;
                    model.OfferID = ds.Tables[0].Rows[0]["PK_OfferID"].ToString();
                    model.OfferName = ds.Tables[0].Rows[0]["OfferName"].ToString();
                    model.FromDate = ds.Tables[0].Rows[0]["FromDate"].ToString();
                    model.ToDate = ds.Tables[0].Rows[0]["ToDate"].ToString();
                    model.DisplayOrder = ds.Tables[0].Rows[0]["DisplayOrder"].ToString();
                    model.OfferStatus = ds.Tables[0].Rows[0]["OfferStatus"].ToString();
                    model.OfferImage = ds.Tables[0].Rows[0]["OfferImage"].ToString();
                }

            }
            #region ddlOfferStatus
            List<SelectListItem> ddlOfferStatus = Common.BindOfferStatus();
            ViewBag.ddlOfferStatus = ddlOfferStatus;
            #endregion ddlOfferStatus
            return View(model);
        }

        [HttpPost]
        [ActionName("SaveOffer")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveOffer(HttpPostedFileBase postedFile, Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedFile != null)
                {
                    obj.OfferImage = "../images/OfferImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.OfferImage)));
                }
                obj.FromDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.FromDate) ? null : obj.FromDate, "dd/MM/yyyy");
                obj.ToDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.ToDate) ? null : obj.ToDate, "dd/MM/yyyy");

                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveOffer();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Offer"] = " Offer Saved successfully !";

                    }
                    else
                    {
                        TempData["Offer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Offer"] = ex.Message;
            }
            FormName = "Offer";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult OfferList(Master model)
        {
            #region ddlOfferStatus
            List<SelectListItem> ddlOfferStatus = Common.BindOfferStatus();
            ViewBag.ddlOfferStatus = ddlOfferStatus;
            #endregion ddlOfferStatus
            return View(model);
        }

        [HttpPost]
        [ActionName("OfferList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(Master model)
        {
            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.OfferList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.OfferID = r["PK_OfferID"].ToString();
                    obj.OfferName = r["OfferName"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    obj.DisplayOrder = r["DisplayOrder"].ToString();
                    obj.OfferStatus = r["OfferStatus"].ToString();


                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SaveOffer")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateOffer(HttpPostedFileBase postedFile, Master obj)
        {
            try
            {
                if (postedFile != null)
                {
                    obj.OfferImage = "../images/OfferImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.OfferImage)));
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateOffer();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Offer"] = "Offer updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Offer"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["Offer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Offer"] = ex.Message;
            }
            return RedirectToAction("Offer", "Master");
        }

        public ActionResult DeleteOffer(string id)
        {
            try
            {
                Master obj = new Master();
                obj.OfferID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteOffer();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Offer"] = "Offer deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Offer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Offer"] = ex.Message;
            }
            return RedirectToAction("OfferList");
        }
        #endregion

        #region OfferProduct

        public ActionResult OfferProduct(Master model)
        {

            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.BrandID = model.BrandID == "0" ? null : model.BrandID;

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            #region Offer
            Master objOffer = new Master();
            int count4 = 0;
            List<SelectListItem> ddlOffer = new List<SelectListItem>();
            DataSet ds4 = objOffer.OfferList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlOffer.Add(new SelectListItem { Text = "Select Offer", Value = "0" });
                    }
                    ddlOffer.Add(new SelectListItem { Text = r["OfferName"].ToString(), Value = r["PK_OfferID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlOffer = ddlOffer;

            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("OfferProduct")]
        [OnAction(ButtonName = "Get")]
        public ActionResult GetProductListForOffer(Master model)
        {
            List<Master> lst = new List<Master>();

            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.BrandID = model.BrandID == "0" ? null : model.BrandID;
            DataSet ds = model.GetListForOffer();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.MainCategoryID = r["FK_MainCategory"].ToString();
                    obj.CategoryID = r["FK_CategoryID"].ToString();
                    obj.SubCategoryID = r["FK_SubCategoryID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.FlavorName = r["FlavorName"].ToString();
                    obj.MaterialName = r["MaterialName"].ToString(); 
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.FlavorID = r["FK_FlavorID"].ToString();
                    obj.RamID = r["FK_RAM_ID"].ToString();
                    obj.StorageID = r["FK_StorageID"].ToString();
                    obj.StarRatingID = r["FK_StarRatingID"].ToString(); 
                    obj.RAM = r["RAM"].ToString();
                    obj.Storage = r["Storage"].ToString();
                    obj.MaterialID = r["FK_MaterialID"].ToString();
                    obj.ProductInfoCode= r["ProductInfoCode"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;
                model.hdOfferID = model.OfferID;
            }
            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            //ViewBag.ddlMainCategory = ddlMainCategory;
            //List<SelectListItem> ddlCategory = new List<SelectListItem>();
            //ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            //ViewBag.ddlCategory = ddlCategory;

            //List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            //ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            //ViewBag.ddlSubCategory = ddlSubCategory;
            Master objC1 = new Master();
            int countc = 0;
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            objC1.MainCategoryID = model.MainCategoryID;
            DataSet ds1c = objC1.CategoryList();
            if (ds1c != null && ds1c.Tables.Count > 0 && ds1c.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1c.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                    }
                    ddlCategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryID"].ToString() });
                    countc = 1;
                }
            }
            ViewBag.ddlCategory = ddlCategory;
            Master objsubcat = new Master();
            int countcs = 0;
            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            objsubcat.CategoryID = model.CategoryID;
            DataSet ds1cs = objsubcat.SubCategoryList();
            if (ds1cs != null && ds1cs.Tables.Count > 0 && ds1cs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1cs.Tables[0].Rows)
                {
                    if (countcs == 0)
                    {
                        ddlSubCategory.Add(new SelectListItem { Text = "Select sub Category", Value = "0" });
                    }
                    ddlSubCategory.Add(new SelectListItem { Text = r["SubCategoryName"].ToString(), Value = r["PK_SubCategoryID"].ToString() });
                    countcs = 1;
                }
            }
            ViewBag.ddlSubCategory = ddlSubCategory;
            #endregion
            #region Offer
            Master objOffer = new Master();
            int count4 = 0;
            List<SelectListItem> ddlOffer = new List<SelectListItem>();
            DataSet ds4 = objOffer.OfferList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlOffer.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlOffer.Add(new SelectListItem { Text = r["OfferName"].ToString(), Value = r["PK_OfferID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlOffer = ddlOffer;
            //List<SelectListItem> ddlCategory = new List<SelectListItem>();
            //ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            //ViewBag.ddlCategory = ddlCategory;

            //List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            //ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            //ViewBag.ddlSubCategory = ddlSubCategory;

            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("OfferProduct")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveOfferforProduct(Master model)
        {

            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string prodid = "";
                string Sizeid = "";
                string unitid = "";
                string mcatid = "";
                string catid = "";
                string subcatid = "";
                string colorid = "";
                string flavorid = "";
                string ram = "";
                string storage = "";
                string star = "";
                string materila = "";
                string prodinfocode = "";


                DataTable dtst = new DataTable();
                dtst.Columns.Add("FK_ProductID", typeof(string));
                dtst.Columns.Add("FK_MainCategoryID", typeof(string));
                dtst.Columns.Add("FK_CategoryID", typeof(string));
                dtst.Columns.Add("FK_SubCategoryID", typeof(string));
                dtst.Columns.Add("FK_UnitID", typeof(string));
                dtst.Columns.Add("FK_SizeID", typeof(string));

                dtst.Columns.Add("ColorID", typeof(string));
                dtst.Columns.Add("RamID", typeof(string));
                dtst.Columns.Add("StorageID", typeof(string));
                dtst.Columns.Add("StarRatingID", typeof(string));
                dtst.Columns.Add("FlavorID", typeof(string));
                dtst.Columns.Add("MaterialID", typeof(string));
                dtst.Columns.Add("ProductInfoCode", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {


                        prodid = Request["productid_ " + i].ToString();
                        unitid = Request["unitid_ " + i].ToString();
                        Sizeid = Request["sizeid_ " + i].ToString();
                        mcatid = Request["mcatid_ " + i].ToString();
                        catid = Request["catid_ " + i].ToString();
                        subcatid = Request["subcatid_ " + i].ToString();
                        colorid = Request["ColorID " + i].ToString();
                        flavorid = Request["FlavorID " + i].ToString();
                        ram = Request["RamID " + i].ToString();
                        storage = Request["StorageID " + i].ToString();
                        star = Request["StarRatingID " + i].ToString();
                        materila = Request["MaterialID " + i].ToString();
                        prodinfocode = Request["ProductInfoCode " + i].ToString();
                        dtst.Rows.Add(prodid, mcatid, catid, subcatid, unitid, Sizeid, colorid, ram, storage, star, flavorid, materila, prodinfocode);
                    }



                }

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.AddOfferToProduct();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Product"] = "  Added successfully !";

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

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            #region Offer
            Master objOffer = new Master();
            int count4 = 0;
            List<SelectListItem> ddlOffer = new List<SelectListItem>();
            DataSet ds4 = objOffer.OfferList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlOffer.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlOffer.Add(new SelectListItem { Text = r["OfferName"].ToString(), Value = r["PK_OfferID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlOffer = ddlOffer;
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            #endregion
            return View(model);
        }


        public ActionResult OfferProductList(Master model)
        {
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;


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
            #region Offer
            Master objOffer = new Master();
            int count4 = 0;
            List<SelectListItem> ddlOffer = new List<SelectListItem>();
            DataSet ds4 = objOffer.OfferList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlOffer.Add(new SelectListItem { Text = "Select Offer", Value = "0" });
                    }
                    ddlOffer.Add(new SelectListItem { Text = r["OfferName"].ToString(), Value = r["PK_OfferID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlOffer = ddlOffer;

            #endregion
            #region Product
            Master objProduct = new Master();
            int countP = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet dsProduct = objProduct.ProductList();
            if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsProduct.Tables[0].Rows)
                {
                    if (countP == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    countP = countP + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("OfferProductList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult List(Master model)
        {

            List<Master> lst = new List<Master>();

            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
            model.OfferID = model.OfferID == "0" ? null : model.OfferID;
            model.ProductID = model.ProductID == "0" ? null : model.ProductID;

            DataSet ds = model.OfferProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    //obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.OfferName = r["OfferName"].ToString();
                    obj.OfferProductID = r["PK_OfferProductID"].ToString();
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
            #region Offer
            Master objOffer = new Master();
            int count4 = 0;
            List<SelectListItem> ddlOffer = new List<SelectListItem>();
            DataSet ds4 = objOffer.OfferList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlOffer.Add(new SelectListItem { Text = "Select Offer", Value = "0" });
                    }
                    ddlOffer.Add(new SelectListItem { Text = r["OfferName"].ToString(), Value = r["PK_OfferID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlOffer = ddlOffer;
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            #endregion
            #region Product
            Master objProduct = new Master();
            int countP = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet dsProduct = objProduct.ProductList();
            if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsProduct.Tables[0].Rows)
                {
                    if (countP == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    countP = countP + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View(model);
        }


        public ActionResult DeleteOfferOnProduct(string id)
        {
            try
            {
                Master obj = new Master();
                obj.OfferProductID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteOfferProduct();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Offer"] = "Offer deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Offer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Offer"] = ex.Message;
            }
            return RedirectToAction("OfferProductList");
        }
        #endregion

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

            model.AddedBy = Session["Pk_AdminId"].ToString();
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
            try
            {
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
                model.AddedBy = Session["Pk_AdminId"].ToString();
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
            catch (Exception)
            {

                throw;
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
        [OnAction(ButtonName = "Deduct")]
        public ActionResult DeductStock(Master model)
        {
            try
            {
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
                model.AddedBy = Session["Pk_AdminId"].ToString();
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

        #endregion

        #region ProductAvailability

        public ActionResult ProductAvailability(Master model)
        {
            #region Product
            Master objProduct = new Master();
            int count1 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds2 = objProduct.ProductList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select  Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            #region Color
            Master objColor = new Master();
            int countc = 0;
            List<SelectListItem> ddlColor = new List<SelectListItem>();
            DataSet ds5 = objColor.ColorList();
            if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds5.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlColor.Add(new SelectListItem { Text = "All", Value = "0" });
                    }
                    ddlColor.Add(new SelectListItem { Text = r["ColorName"].ToString(), Value = r["PK_ColorID"].ToString() });
                    countc = countc + 1;
                }
            }

            ViewBag.ddlColor = ddlColor;

            #endregion

            #region Flavor
            Master objFlavor = new Master();
            int countF = 0;
            List<SelectListItem> ddlFlavor = new List<SelectListItem>();
            DataSet dsf = objFlavor.FlavorList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countF == 0)
                    {
                        ddlFlavor.Add(new SelectListItem { Text = "", Value = "All" });
                    }
                    ddlFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["PK_FlavorID"].ToString() });
                    countF = countF + 1;
                }
            }

            ViewBag.ddlFlavor = ddlFlavor;

            #endregion

            #region Material
            Master objMaterial = new Master();
            int countm = 0;
            List<SelectListItem> ddlMaterial = new List<SelectListItem>();
            DataSet dsm = objMaterial.MaterialList();
            if (dsm != null && dsm.Tables.Count > 0 && dsm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsm.Tables[0].Rows)
                {
                    if (countm == 0)
                    {
                        ddlMaterial.Add(new SelectListItem { Text = "All", Value = "0" });
                    }
                    ddlMaterial.Add(new SelectListItem { Text = r["MaterialName"].ToString(), Value = r["PK_MaterialID"].ToString() });
                    countm = countm + 1;
                }
            }

            ViewBag.ddlMaterial = ddlMaterial;

            #endregion

            #region Size
            Master objSize = new Master();
            int counts = 0;
            List<SelectListItem> ddlSize = new List<SelectListItem>();
            DataSet dss = objSize.SizeList();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (counts == 0)
                    {
                        ddlSize.Add(new SelectListItem { Text = "All", Value = "0" });
                    }
                    ddlSize.Add(new SelectListItem { Text = r["SizeName"].ToString(), Value = r["PK_SizeID"].ToString() });
                    counts = counts + 1;
                }
            }

            ViewBag.ddlSize = ddlSize;

            #endregion

            #region State
            Master objState = new Master();
            int countst = 0;
            List<SelectListItem> ddlState = new List<SelectListItem>();
            DataSet dsstate = objState.StateList();
            if (dsstate != null && dsstate.Tables.Count > 0 && dsstate.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsstate.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        ddlState.Add(new SelectListItem { Text = "Select State", Value = "0" });
                    }
                    ddlState.Add(new SelectListItem { Text = r["statename"].ToString(), Value = r["FK_StateID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.ddlState = ddlState;

            #endregion

            List<SelectListItem> ddlCity = new List<SelectListItem>();
            ddlCity.Add(new SelectListItem { Text = "Select City", Value = "0" });
            ViewBag.ddlCity = ddlCity;

            return View(model);
        }
        public ActionResult GetVariantByProduct(string ProductID)
        {
            List<Master> lst = new List<Master>();
            // List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            Master model = new Master();

            model.ProductID = ProductID;
            DataSet dsblock = model.VariantByProductList();


            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.VariantName = r["VariantName"].ToString();
                    lst.Add(obj);

                }

                model.lstVarient = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCityByState(string State)
        {
            try
            {
                Master model = new Master();
                model.State = State;


                #region Get
                List<SelectListItem> ddlCity = new List<SelectListItem>();
                DataSet ds = model.CityList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlCity.Add(new SelectListItem { Text = r["Districtname"].ToString(), Value = r["FK_CityID"].ToString() });

                    }
                }
                model.ddlCity = ddlCity;
                #endregion
                //List<Master> lst = new List<Master>();

                //DataSet dspin = model.GetStateCity();

                //if (dspin != null && dspin.Tables.Count > 0 && dspin.Tables[1].Rows.Count > 0)
                //{
                //    foreach (DataRow r in dspin.Tables[1].Rows)
                //    {
                //        Master obj = new Master();
                //        obj.Pincode = r["pincode"].ToString();

                //    }
                //    model.lstProduct = lst;
                //}

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult Getpin(string State, string City)
        {
            try
            {
                Master model = new Master();
                model.City = City;
                model.State = State;
                List<Master> lst = new List<Master>();

                DataSet dspin = model.GetPin();

                if (dspin != null && dspin.Tables.Count > 0 && dspin.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dspin.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.Pincode = r["pincode"].ToString();
                        lst.Add(obj);

                    }
                    model.lstProduct = lst;
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion

        #region Discount
        public ActionResult Discount(string DiscountID)
        {
            Master model = new Master();
            if (DiscountID != null)
            {
                model.DiscountID = Crypto.Decrypt(DiscountID);

                DataSet ds = model.DiscountList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    //  model.DiscountID = DiscountID;

                    model.CouponCode = ds.Tables[0].Rows[0]["CouponCode"].ToString();
                    model.FromDate = ds.Tables[0].Rows[0]["ValidFrom"].ToString();
                    model.ToDate = ds.Tables[0].Rows[0]["ValidTo"].ToString();
                    model.DiscountAmount = ds.Tables[0].Rows[0]["DiscountAmount"].ToString();
                    model.DiscountID = ds.Tables[0].Rows[0]["PK_DiscountMasterID"].ToString();

                }
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("SaveDiscount")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveDiscount(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.FromDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.FromDate) ? null : obj.FromDate, "dd/MM/yyyy");
                obj.ToDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.ToDate) ? null : obj.ToDate, "dd/MM/yyyy");

                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveDiscount();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Discount"] = " Discount Saved successfully !";

                    }
                    else
                    {
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Discount"] = ex.Message;
            }
            FormName = "Discount";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult DiscountList(Master model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("DiscountList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ListDiscount(Master model)
        {
            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.DiscountList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DiscountID = r["PK_DiscountMasterID"].ToString();
                    obj.DiscountAmount = r["DiscountAmount"].ToString();
                    obj.CouponCode = r["CouponCode"].ToString();
                    obj.ToDate = r["ValidTo"].ToString();
                    obj.FromDate = r["ValidFrom"].ToString();


                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("SaveDiscount")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateDiscount(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateDiscount();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Discount"] = "Discount updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Discount"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Discount"] = ex.Message;
            }
            return RedirectToAction("Discount", "Master");
        }

        public ActionResult DeleteDiscount(string id)
        {
            try
            {
                Master obj = new Master();
                obj.DiscountID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteDiscount();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Discount"] = "Discount deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Discount"] = ex.Message;
            }
            return RedirectToAction("DiscountList");
        }
        #endregion

        #region DiscountForCustomers
        public ActionResult DiscountForCustomers(Master model)
        {
            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.CustomerList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CustomerID = r["PK_CustomerID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.CustomerContact = r["Contact"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);

        }

        [HttpPost]
        [ActionName("DiscountForCustomers")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveDiscountForCustomers(Master model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string custid = "";
                string coupon = "";
                string amt = "";
                string from = "";
                string to = "";


                DataTable dtst = new DataTable();
                dtst.Columns.Add("CouponCode", typeof(string));
                dtst.Columns.Add("Discount", typeof(string));
                dtst.Columns.Add("FromDate", typeof(string));
                dtst.Columns.Add("ToDate", typeof(string));
                dtst.Columns.Add("FK_CustomerID", typeof(string));


                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {


                        coupon = model.CouponCode;
                        amt = model.DiscountAmount;
                        from = Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                        to = Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                        custid = Request["customerid_ " + i].ToString();


                        dtst.Rows.Add(coupon, amt, from, to, custid);
                    }

                }

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.AddDiscountToCustomer();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DiscountC"] = "  Added successfully !";

                    }
                    else
                    {
                        TempData["DiscountC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            FormName = "DiscountForCustomers";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);


        }

        public ActionResult DiscountForCustomersList(Master model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("DiscountForCustomersList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult bINDDiscountForCustomersList(Master model)
        {
            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.DiscountForCustomersList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CouponCode = r["CouponCode"].ToString();
                    obj.DiscountAmount = r["DiscountAmount"].ToString();
                    obj.FromDate = r["ValidFrom"].ToString();
                    obj.ToDate = r["ValidTo"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Discforcustid = r["PK_DiscForCustID"].ToString();


                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }

        public ActionResult DeleteDiscountforCustomer(string id)
        {
            try
            {
                Master obj = new Master();
                obj.Discforcustid = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteDiscountforCustomer();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Discount"] = "Discount deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Discount"] = ex.Message;
            }
            return RedirectToAction("DiscountForCustomersList");
        }
        #endregion

        #region BannerMaster

        public ActionResult Banner(Master model)
        {

            return View(model);
        }
        [HttpPost]
        [ActionName("SaveBanner")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveBanner(HttpPostedFileBase postedFile, Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedFile != null)
                {
                    obj.BannerImage = "../images/BannerImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.BannerImage)));


                    string path = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                 //   obj.Images = "../../BannerImage/BannerMobile/" + path;
                  
                    #region cropImage
                    Stream strm = postedFile.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        #region 200*350
                        int newWidth = Convert.ToInt32(200);
                        int newHeight = Convert.ToInt32(350);

                        var thumbImg = new SD.Bitmap(newWidth, newHeight);
                        var thumbGraph = SD.Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new SD.Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);

                        obj.Images = Server.MapPath("../images/BannerImage/BannerMobile/") + path;
                        thumbImg.Save(obj.Images, image.RawFormat);
                        #endregion
                    }
                    #endregion
                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveBanner();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Banner"] = " Banner Saved successfully !";

                    }
                    else
                    {
                        TempData["Banner"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Banner"] = ex.Message;
            }
            FormName = "Banner";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult BannerList(Master model)
        {

            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.BannerList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BannerID = r["PK_BannerId"].ToString();
                    obj.BannerName = r["BannerName"].ToString();
                    obj.BannerDescription = r["Description"].ToString();
                    obj.BannerImage = r["BannerImage"].ToString();


                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("BannerList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult BindBannerList(Master model)
        {
            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.BannerList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BannerID = r["PK_BannerId"].ToString();
                    obj.BannerName = r["BannerName"].ToString();
                    obj.BannerDescription = r["Description"].ToString();
                    obj.BannerImage = r["BannerImage"].ToString();


                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }


        public ActionResult DeleteBanner(string id)
        {
            try
            {
                Master obj = new Master();
                obj.BannerID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteBanner();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["BannerList"] = "Banner deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["BannerList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["BannerList"] = ex.Message;
            }
            return RedirectToAction("BannerList");
        }

        #endregion

        #region FeaturedProduct
        public ActionResult FeaturedProduct(Master model)
        {
            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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


            return View(model);
        }

        [HttpPost]
        [ActionName("FeaturedProduct")]
        [OnAction(ButtonName = "Get")]
        public ActionResult GetListForFeaturedProduct(Master model)
        {
            List<Master> lst = new List<Master>();
            model.MainCategoryID= model.MainCategoryID == "0" ? null : model.MainCategoryID;
            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.BrandID = model.BrandID == "0" ? null : model.BrandID;
            DataSet ds = model.GetListForFeatured();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    //obj.BV = r["BV"].ToString();
                    //obj.MRP = r["MRP"].ToString();
                    //obj.PrimaryImage = r["PrimaryImage"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();

                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.MainCategoryID = r["FK_MainCategory"].ToString();
                    obj.CategoryID = r["FK_CategoryID"].ToString();
                    obj.SubCategoryID = r["FK_SubCategoryID"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.FlavorID = r["FK_FlavorID"].ToString();
                    obj.FlavorName = r["FlavorName"].ToString();
                    obj.RamID = r["Fk_Ram_Id"].ToString();
                    obj.RAM = r["RAM"].ToString();
                    obj.Storage = r["Storage"].ToString();
                    obj.StorageID = r["Fk_storageId"].ToString();
                    obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                    lst.Add(obj);
                }
                model.lstProduct = lst;
                model.hdOfferID = model.OfferID;
            }
            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            //ViewBag.ddlMainCategory = ddlMainCategory;
            //List<SelectListItem> ddlCategory = new List<SelectListItem>();
            //ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            //ViewBag.ddlCategory = ddlCategory;

            //List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            //ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            //ViewBag.ddlSubCategory = ddlSubCategory;
            Master objC1 = new Master();
            int countc = 0;
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            DataSet ds1c = objC1.CategoryList();
            if (ds1c != null && ds1c.Tables.Count > 0 && ds1c.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1c.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                    }
                    ddlCategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryID"].ToString() });
                    countc = 1;
                }
            }
            ViewBag.ddlCategory = ddlCategory;
            Master objsubcat = new Master();
            int countcs = 0;
            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            DataSet ds1cs = objsubcat.SubCategoryList();
            if (ds1cs != null && ds1cs.Tables.Count > 0 && ds1cs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1cs.Tables[0].Rows)
                {
                    if (countcs == 0)
                    {
                        ddlSubCategory.Add(new SelectListItem { Text = "Select sub Category", Value = "0" });
                    }
                    ddlSubCategory.Add(new SelectListItem { Text = r["SubCategoryName"].ToString(), Value = r["PK_SubCategoryID"].ToString() });
                    countcs = 1;
                }
            }
            ViewBag.ddlSubCategory = ddlSubCategory;
            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("FeaturedProduct")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveFeaturedProduct(Master model)
        {

            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string ProductInfoCode = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("ProductInfoCode", typeof(string));


                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {


                        ProductInfoCode = Request["ProductInfoCode_ " + i].ToString();

                        dtst.Rows.Add(ProductInfoCode);
                    }

                }

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.AddFeaturedProduct();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Product"] = "Featured Product added successfully !";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Product"] = ex.Message;
            }

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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

        public ActionResult FeaturedProductList(Master model)
        {
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;


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

            #region Product
            Master objProduct = new Master();
            int countP = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet dsProduct = objProduct.ProductList();
            if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsProduct.Tables[0].Rows)
                {
                    if (countP == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    countP = countP + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("FeaturedProductList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult FeaturedList(Master model)
        {

            List<Master> lst = new List<Master>();

            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
            //  model.OfferID = model.OfferID == "0" ? null : model.OfferID;
            model.ProductID = model.ProductID == "0" ? null : model.ProductID;

            DataSet ds = model.FeaturedProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    //obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.OfferProductID = r["PK_FeatureProductID"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();



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

            #region Product
            Master objProduct = new Master();
            int countP = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet dsProduct = objProduct.ProductList();
            if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsProduct.Tables[0].Rows)
                {
                    if (countP == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    countP = countP + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;
            #endregion
            return View(model);
        }

        public ActionResult DeleteFeaturedProduct(string id)
        {
            try
            {
                Master obj = new Master();
                obj.OfferProductID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteFeaturedProduct();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Discount"] = " Deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Discount"] = ex.Message;
            }
            return RedirectToAction("FeaturedProductList");
        }
        #endregion 

        #region NewArrivals
        public ActionResult NewArrivals(Master model)
        {
            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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


            return View(model);
        }

        [HttpPost]
        [ActionName("NewArrivals")]
        [OnAction(ButtonName = "Get")]
        public ActionResult GetNewArrivals(Master model)
        {
            List<Master> lst = new List<Master>();
            model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.BrandID = model.BrandID == "0" ? null : model.BrandID;
            DataSet ds = model.GetListNewArrivals();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    //obj.BV = r["BV"].ToString();
                    //obj.MRP = r["MRP"].ToString();
                    //obj.PrimaryImage = r["PrimaryImage"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();

                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.MainCategoryID = r["FK_MainCategory"].ToString();
                    obj.CategoryID = r["FK_CategoryID"].ToString();
                    obj.SubCategoryID = r["FK_SubCategoryID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.FlavorName = r["FlavorName"].ToString();
                    obj.MaterialName = r["MaterialName"].ToString();
                    obj.RAM = r["RAM"].ToString();
                    obj.Storage = r["Storage"].ToString();
                    obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                    lst.Add(obj);
                }
                model.lstProduct = lst;
                model.hdOfferID = model.OfferID;
            }
            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            //ViewBag.ddlMainCategory = ddlMainCategory;
            //List<SelectListItem> ddlCategory = new List<SelectListItem>();
            //ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            //ViewBag.ddlCategory = ddlCategory;

            //List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            //ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            //ViewBag.ddlSubCategory = ddlSubCategory;
            Master objC1 = new Master();
            int countc = 0;
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            DataSet ds1c = objC1.CategoryList();
            if (ds1c != null && ds1c.Tables.Count > 0 && ds1c.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1c.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                    }
                    ddlCategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryID"].ToString() });
                    countc = 1;
                }
            }
            ViewBag.ddlCategory = ddlCategory;
            Master objsubcat = new Master();
            int countcs = 0;
            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            DataSet ds1cs = objsubcat.SubCategoryList();
            if (ds1cs != null && ds1cs.Tables.Count > 0 && ds1cs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1cs.Tables[0].Rows)
                {
                    if (countcs == 0)
                    {
                        ddlSubCategory.Add(new SelectListItem { Text = "Select sub Category", Value = "0" });
                    }
                    ddlSubCategory.Add(new SelectListItem { Text = r["SubCategoryName"].ToString(), Value = r["PK_SubCategoryID"].ToString() });
                    countcs = 1;
                }
            }
            ViewBag.ddlSubCategory = ddlSubCategory;
            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("NewArrivals")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveNewArrivals(Master model)
        {

            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string ProductInfoCode = "";


                DataTable dtst = new DataTable();
                dtst.Columns.Add("ProductInfoCode", typeof(string));


                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {


                        ProductInfoCode = Request["ProductInfoCode_ " + i].ToString();


                        dtst.Rows.Add(ProductInfoCode);
                    }

                }

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.AddNewArrivals();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Product"] = "  Added successfully !";

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

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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

        public ActionResult NewArrivalsList(Master model)
        {
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;


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

            #region Product
            Master objProduct = new Master();
            int countP = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet dsProduct = objProduct.ProductList();
            if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsProduct.Tables[0].Rows)
                {
                    if (countP == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    countP = countP + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("NewArrivalsList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SearchNewArrivals(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();

                model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
                model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
                model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
                //  model.OfferID = model.OfferID == "0" ? null : model.OfferID;
                model.ProductID = model.ProductID == "0" ? null : model.ProductID;

                DataSet ds = model.NewArrivalsList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        //obj.ProductID = r["PK_ProductID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.CategoryName = r["CategoryName"].ToString();
                        obj.SubCategoryName = r["SubCategoryName"].ToString();
                        obj.MainCategoryName = r["MainCategoryName"].ToString();
                        obj.OfferProductID = r["PK_NewArrivalProdID"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();



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

                #region Product
                Master objProduct = new Master();
                int countP = 0;
                List<SelectListItem> ddlProduct = new List<SelectListItem>();
                DataSet dsProduct = objProduct.ProductList();
                if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsProduct.Tables[0].Rows)
                    {
                        if (countP == 0)
                        {
                            ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                        }
                        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                        countP = countP + 1;
                    }
                }

                ViewBag.ddlProduct = ddlProduct;
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                ViewBag.ddlCategory = ddlCategory;

                List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
                ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
                ViewBag.ddlSubCategory = ddlSubCategory;
                #endregion
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Product"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult DeleteNewArrivals(string id)
        {
            try
            {
                Master obj = new Master();
                obj.OfferProductID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteNewArrivals();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Discount"] = " Deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Discount"] = ex.Message;
            }
            return RedirectToAction("NewArrivalsList");
        }
        #endregion

        #region AddNewVariantToProduct

        public ActionResult AddNewVariantToProduct(Master model)
        {

            Session["tmpData"] = null;
            Session["dtSecImages"] = null;

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            List<SelectListItem> ddlSize = new List<SelectListItem>();
            ddlSize.Add(new SelectListItem { Text = "Select Size", Value = "0" });
            ViewBag.ddlSize = ddlSize;

            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            ViewBag.ddlProduct = ddlProduct;

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            #region Unit
            Master objUnit = new Master();
            int count4 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds4 = objUnit.UnitList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion
            #region Color
            Master objColor = new Master();
            int countc = 0;
            List<SelectListItem> ddlColor = new List<SelectListItem>();
            DataSet ds5 = objColor.ColorList();
            if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds5.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlColor.Add(new SelectListItem { Text = "Select Color", Value = "0" });
                    }
                    ddlColor.Add(new SelectListItem { Text = r["ColorName"].ToString(), Value = r["PK_ColorID"].ToString() });
                    countc = countc + 1;
                }
            }

            ViewBag.ddlColor = ddlColor;

            #endregion
            #region Flavor
            Master objFlavor = new Master();
            int countF = 0;
            List<SelectListItem> ddlFlavor = new List<SelectListItem>();
            DataSet dsf = objFlavor.FlavorList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countF == 0)
                    {
                        ddlFlavor.Add(new SelectListItem { Text = "Select Flavor", Value = "0" });
                    }
                    ddlFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["PK_FlavorID"].ToString() });
                    countF = countF + 1;
                }
            }

            ViewBag.ddlFlavor = ddlFlavor;

            #endregion
            #region Material
            Master objMaterial = new Master();
            int countm = 0;
            List<SelectListItem> ddlMaterial = new List<SelectListItem>();
            DataSet dsm = objMaterial.MaterialList();
            if (dsm != null && dsm.Tables.Count > 0 && dsm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsm.Tables[0].Rows)
                {
                    if (countm == 0)
                    {
                        ddlMaterial.Add(new SelectListItem { Text = "Select Material", Value = "0" });
                    }
                    ddlMaterial.Add(new SelectListItem { Text = r["MaterialName"].ToString(), Value = r["PK_MaterialID"].ToString() });
                    countm = countm + 1;
                }
            }

            ViewBag.ddlMaterial = ddlMaterial;

            #endregion
            #region RAM
            Master objRAM = new Master();
            int countr = 0;
            List<SelectListItem> ddlRAM = new List<SelectListItem>();
            DataSet dsRm = objRAM.RAMList();
            if (dsRm != null && dsRm.Tables.Count > 0 && dsRm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsRm.Tables[0].Rows)
                {
                    if (countr == 0)
                    {
                        ddlRAM.Add(new SelectListItem { Text = "Select RAM", Value = "0" });
                    }
                    ddlRAM.Add(new SelectListItem { Text = r["RAM"].ToString(), Value = r["PK_RAM_ID"].ToString() });
                    countr = countr + 1;
                }
            }

            ViewBag.ddlRAM = ddlRAM;
            #endregion
            #region Storage
            Master objStorage = new Master();
            int countst = 0;
            List<SelectListItem> ddlStorage = new List<SelectListItem>();
            DataSet dsSt = objStorage.StorageList();
            if (dsSt != null && dsSt.Tables.Count > 0 && dsSt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSt.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        ddlStorage.Add(new SelectListItem { Text = "Select Storage", Value = "0" });
                    }
                    ddlStorage.Add(new SelectListItem { Text = r["Storage"].ToString(), Value = r["PK_StorageID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.ddlStorage = ddlStorage;
            #endregion
            #region StarRating
            Master objStarRating = new Master();
            int countstr = 0;
            List<SelectListItem> ddlStarRating = new List<SelectListItem>();
            DataSet dsStr = objStarRating.StarRatingList();
            if (dsStr != null && dsStr.Tables.Count > 0 && dsStr.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsStr.Tables[0].Rows)
                {
                    if (countstr == 0)
                    {
                        ddlStarRating.Add(new SelectListItem { Text = "Select Star Rating", Value = "0" });
                    }
                    ddlStarRating.Add(new SelectListItem { Text = r["StarRating"].ToString(), Value = r["PK_StarRatingID"].ToString() });
                    countstr = countstr + 1;
                }
            }

            ViewBag.ddlStarRating = ddlStarRating;
            #endregion
            return View(model);
        }

        public ActionResult GetProductsInDDL(string MainCategoryID, string CategoryID, string SubCategoryID)
        {

            List<Master> lst = new List<Master>();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            Master model = new Master();
            if (Session["UserType"].ToString() == "Admin")
            {
                model.AddedBy = null;
            }
            else
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
            }
            model.MainCategoryID = MainCategoryID;
            model.CategoryID = CategoryID;
            model.SubCategoryID = SubCategoryID;
            DataSet dsblock = model.ListProduct();

            #region ddl
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    ddlProduct.Add(new SelectListItem { Text = dr["ProductName"].ToString(), Value = dr["PK_ProductID"].ToString() });
                }

            }

            model.ddlProduct = ddlProduct;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("AddNewVariantToProduct")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DetailedList(Master model)
        {
            List<Master> lst = new List<Master>();

            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
            model.ProductID = model.ProductID == "0" ? null : model.ProductID;
            DataSet Branch = model.ListProduct();
            if (Branch != null && Branch.Tables.Count > 0)
            {
                model.ProductID = Branch.Tables[0].Rows[0]["PK_ProductID"].ToString();
                model.HSNNo = Branch.Tables[0].Rows[0]["HSNNo"].ToString();
                model.Tags = Branch.Tables[0].Rows[0]["Tags"].ToString();
                model.Description = Branch.Tables[0].Rows[0]["Description"].ToString();
                model.ShortDescription = Branch.Tables[0].Rows[0]["ShortDescription"].ToString();
                model.BrandName = Branch.Tables[0].Rows[0]["BrandName"].ToString();
                Session["ProductID"] = model.ProductID;
                if (Branch != null && Branch.Tables.Count > 0 && Branch.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in Branch.Tables[1].Rows)
                    {
                        Master obj = new Master();
                        obj.ProductOtherInfoID = r["PK_ProductOtherInfoID"].ToString();
                        obj.BV = r["BV"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.OfferedPrice = r["OfferedPrice"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.DealerPrice = r["DealerPrice"].ToString();
                        //      obj.Quantity = r["CreditQuantity"].ToString();
                        obj.CGST = r["CGST"].ToString();
                        obj.SGST = r["SGST"].ToString();
                        obj.IGST = r["IGST"].ToString();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.Storage = r["Storage"].ToString();
                        obj.StarRating = r["StarRating"].ToString();
                        lst.Add(obj);
                    }
                    model.lstProduct = lst;
                }

            }

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            List<SelectListItem> ddlSize = new List<SelectListItem>();
            ddlSize.Add(new SelectListItem { Text = "Select Size", Value = "0" });
            ViewBag.ddlSize = ddlSize;

            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
            ViewBag.ddlProduct = ddlProduct;


            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            #region Unit
            Master objUnit = new Master();
            int count4 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds4 = objUnit.UnitList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion
            #region Color
            Master objColor = new Master();
            int countc = 0;
            List<SelectListItem> ddlColor = new List<SelectListItem>();
            DataSet ds5 = objColor.ColorList();
            if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds5.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlColor.Add(new SelectListItem { Text = "Select Color", Value = "0" });
                    }
                    ddlColor.Add(new SelectListItem { Text = r["ColorName"].ToString(), Value = r["PK_ColorID"].ToString() });
                    countc = countc + 1;
                }
            }

            ViewBag.ddlColor = ddlColor;

            #endregion
            #region Flavor
            Master objFlavor = new Master();
            int countF = 0;
            List<SelectListItem> ddlFlavor = new List<SelectListItem>();
            DataSet dsf = objFlavor.FlavorList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countF == 0)
                    {
                        ddlFlavor.Add(new SelectListItem { Text = "Select Flavor", Value = "0" });
                    }
                    ddlFlavor.Add(new SelectListItem { Text = r["FlavorName"].ToString(), Value = r["PK_FlavorID"].ToString() });
                    countF = countF + 1;
                }
            }

            ViewBag.ddlFlavor = ddlFlavor;

            #endregion
            #region Material
            Master objMaterial = new Master();
            int countm = 0;
            List<SelectListItem> ddlMaterial = new List<SelectListItem>();
            DataSet dsm = objMaterial.MaterialList();
            if (dsm != null && dsm.Tables.Count > 0 && dsm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsm.Tables[0].Rows)
                {
                    if (countm == 0)
                    {
                        ddlMaterial.Add(new SelectListItem { Text = "Select Material", Value = "0" });
                    }
                    ddlMaterial.Add(new SelectListItem { Text = r["MaterialName"].ToString(), Value = r["PK_MaterialID"].ToString() });
                    countm = countm + 1;
                }
            }

            ViewBag.ddlMaterial = ddlMaterial;

            #endregion
            #region RAM
            Master objRAM = new Master();
            int countr = 0;
            List<SelectListItem> ddlRAM = new List<SelectListItem>();
            DataSet dsRm = objRAM.RAMList();
            if (dsRm != null && dsRm.Tables.Count > 0 && dsRm.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsRm.Tables[0].Rows)
                {
                    if (countr == 0)
                    {
                        ddlRAM.Add(new SelectListItem { Text = "Select RAM", Value = "0" });
                    }
                    ddlRAM.Add(new SelectListItem { Text = r["RAM"].ToString(), Value = r["PK_RAM_ID"].ToString() });
                    countr = countr + 1;
                }
            }

            ViewBag.ddlRAM = ddlRAM;
            #endregion
            #region Storage
            Master objStorage = new Master();
            int countst = 0;
            List<SelectListItem> ddlStorage = new List<SelectListItem>();
            DataSet dsSt = objStorage.StorageList();
            if (dsSt != null && dsSt.Tables.Count > 0 && dsSt.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSt.Tables[0].Rows)
                {
                    if (countst == 0)
                    {
                        ddlStorage.Add(new SelectListItem { Text = "Select Storage", Value = "0" });
                    }
                    ddlStorage.Add(new SelectListItem { Text = r["Storage"].ToString(), Value = r["PK_StorageID"].ToString() });
                    countst = countst + 1;
                }
            }

            ViewBag.ddlStorage = ddlStorage;
            #endregion
            #region StarRating
            Master objStarRating = new Master();
            int countstr = 0;
            List<SelectListItem> ddlStarRating = new List<SelectListItem>();
            DataSet dsStr = objStarRating.StarRatingList();
            if (dsStr != null && dsStr.Tables.Count > 0 && dsStr.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsStr.Tables[0].Rows)
                {
                    if (countstr == 0)
                    {
                        ddlStarRating.Add(new SelectListItem { Text = "Select Star Rating", Value = "0" });
                    }
                    ddlStarRating.Add(new SelectListItem { Text = r["StarRating"].ToString(), Value = r["PK_StarRatingID"].ToString() });
                    countstr = countstr + 1;
                }
            }

            ViewBag.ddlStarRating = ddlStarRating;
            #endregion

            DataSet dsblock = model.SubCategoryList();

            #region ddl
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    ddlSubCategory.Add(new SelectListItem { Text = dr["SubCategoryName"].ToString(), Value = dr["PK_SubCategoryID"].ToString() });
                }

            }

            model.ddlSubCategory = ddlSubCategory;
            #endregion

            List<Master> lstVariant = new List<Master>();

            DataSet dsVariants = model.GetVariantFromCategory();
            if (dsVariants != null && dsVariants.Tables.Count > 0 && dsVariants.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsVariants.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.VariantName = dr["VariantName"].ToString();
                    lstVariant.Add(obj);
                }
                model.lstVariant = lstVariant;
            }



            return View(model);
        }

        [HttpPost]
        [ActionName("AddNewVariantToProduct")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveVariantToProduct(Master obj)
        {
            Session["tmpData"] = null;
            string FormName = "";
            string Controller = "";

            try
            {
                DateTime d1 = DateTime.Now;
                string ctrC1 = d1.Year.ToString() + d1.Day.ToString() + d1.Month.ToString() + d1.Hour.ToString() + d1.Minute.ToString() + d1.Second.ToString() + d1.Millisecond.ToString();
                obj.ProductInfoCode = ctrC1;

                string noofrows = Request["hdrows"].ToString();
                string primaryimage = "";
                string unitid = "";
                string unitname = "";
                string sizeid = "";
                string sizename = "";
                string colorid = "";
                string colorname = "";
                string qty = "";
                string bv = "";
                string mrp = "";
                string offprice = "";
                string dealprice = "";
                string cgst = "";
                string sgst = "";
                string igst = "";
                string img = "";
                string flavorid = "";
                string flavorname = "";
                string materialid = "";
                string materialname = "";
                string infocode = "";
                string ramid = "";
                string ramname = "";
                string storageid = "";
                string storagename = "";
                string starratingid = "";
                string starratingname = "";


                DataTable dtst = new DataTable();
                dtst.Columns.Add("ProductInfoCode", typeof(string));
                dtst.Columns.Add("PrimaryImage", typeof(string));
                dtst.Columns.Add("FK_UnitID", typeof(string));
                dtst.Columns.Add("UnitName", typeof(string));
                dtst.Columns.Add("FK_SizeID", typeof(string));
                dtst.Columns.Add("SizeName", typeof(string));
                dtst.Columns.Add("FK_ColorID", typeof(string));
                dtst.Columns.Add("ColorName", typeof(string));
                dtst.Columns.Add("FK_MaterialID", typeof(string));
                dtst.Columns.Add("MaterialName", typeof(string));
                dtst.Columns.Add("FK_FlavorID", typeof(string));
                dtst.Columns.Add("FlavorName", typeof(string));
                dtst.Columns.Add("MRP", typeof(string));
                dtst.Columns.Add("OfferPrice", typeof(string));
                dtst.Columns.Add("DealerPrice", typeof(string));
                dtst.Columns.Add("BV", typeof(string));
                dtst.Columns.Add("CGST", typeof(string));
                dtst.Columns.Add("SGST", typeof(string));
                dtst.Columns.Add("IGST", typeof(string));
                dtst.Columns.Add("Quantity", typeof(string));
                dtst.Columns.Add("FK_RamID", typeof(string));
                dtst.Columns.Add("RAM", typeof(string));
                dtst.Columns.Add("FK_StorageID", typeof(string));
                dtst.Columns.Add("StorageName", typeof(string));
                dtst.Columns.Add("FK_StarratingID", typeof(string));
                dtst.Columns.Add("StarRatingName", typeof(string));

                obj.SizeID = obj.SizeID == "0" ? null : obj.SizeID;
                obj.ColorID = obj.ColorID == "0" ? null : obj.ColorID;
                obj.MaterialID = obj.MaterialID == "0" ? null : obj.MaterialID;
                obj.FlavorID = obj.FlavorID == "0" ? null : obj.FlavorID;

                obj.RamID = obj.RamID == "0" ? null : obj.RamID;
                obj.StorageID = obj.StorageID == "0" ? null : obj.StorageID;
                obj.StarRatingID = obj.StarRatingID == "0" ? null : obj.StarRatingID;

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    sizeid = Request["sizeid_ " + i].ToString();
                    unitid = Request["unitid_ " + i].ToString();
                    colorid = Request["colorid_ " + i].ToString();
                    qty = Request["txtQuantity_ " + i].ToString();
                    bv = Request["txtBV_ " + i].ToString();
                    mrp = Request["txtMRP_ " + i].ToString();
                    offprice = Request["txtDealerPrice_ " + i].ToString();
                    dealprice = Request["txtDealerPrice_ " + i].ToString();
                    cgst = Request["txtCGST_ " + i].ToString();
                    sgst = Request["txtSGST_ " + i].ToString();
                    igst = Request["txtIGST_ " + i].ToString();
                    primaryimage = "../images/no-profile.png";
                    unitname = Request["unitname_ " + i].ToString();
                    sizename = Request["sizename_ " + i].ToString();
                    colorname = Request["colorname_ " + i].ToString();
                    flavorid = Request["flavorid_ " + i].ToString();
                    flavorname = Request["flavorname_ " + i].ToString();
                    materialid = Request["materialid_ " + i].ToString();
                    materialname = Request["materialname_ " + i].ToString();
                    infocode = Request["productinfocode_ " + i].ToString();

                    ramid = Request["ramid_ " + i].ToString();
                    ramname = Request["ramname_ " + i].ToString();
                    storageid = Request["storageid_ " + i].ToString();
                    storagename = Request["storagename_ " + i].ToString();
                    starratingid = Request["starrate_ " + i].ToString();
                    starratingname = Request["starratingname_ " + i].ToString();

                    dtst.Rows.Add(infocode, primaryimage, unitid, unitname, sizeid, sizename, colorid, colorname, materialid, materialname, flavorid,
                        flavorname, mrp, offprice, dealprice, bv, cgst, sgst, igst, qty,
                        ramid, ramname, storageid, storagename, starratingid, starratingname
                        );
                }
                obj.ProductID = Session["ProductID"].ToString();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtProductQuantity = dtst;

                dtSecondaryImages = (DataTable)Session["dtSecImages"];
                obj.dtProductImages = dtSecondaryImages;
                DataSet ds = obj.AddVariant();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Product"] = " New Variant Added !";

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
            FormName = "AddNewVariantToProduct";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }


        #endregion

        #region VariantMaster
        public ActionResult VariantMaster(string VariantID)
        {
            Master model = new Master();
            if (VariantID != null)
            {
                model.VariantID = VariantID;

                DataSet ds = model.VariantList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    model.VariantID = ds.Tables[0].Rows[0]["PK_VariantID"].ToString();
                    model.VariantName = ds.Tables[0].Rows[0]["VariantName"].ToString();

                }


            }
            return View(model);
        }


        [HttpPost]
        [ActionName("VariantMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveVariant(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.SaveVariant();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Variant"] = " Variant Added successfully !";

                    }
                    else
                    {
                        TempData["Variant"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Variant"] = ex.Message;
            }
            return RedirectToAction("VariantMaster");
        }

        [HttpPost]
        [ActionName("VariantMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateVariant(Master obj)
        {

            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.UpdateVariant();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Variant"] = " Variant Updated successfully !";

                    }
                    else
                    {
                        TempData["Variant"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }



            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Variant"] = ex.Message;
            }
            return RedirectToAction("VariantList");
        }

        public ActionResult DeleteVariant(string id)
        {
            try
            {
                Master obj = new Master();
                obj.VariantID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteVariant();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Variant"] = "Variant deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Variant"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Variant"] = ex.Message;
            }
            return RedirectToAction("VariantList");
        }

        public ActionResult VariantList(Master model)
        {
            List<Master> lst = new List<Master>();

            DataSet ds = model.VariantList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.VariantID = r["PK_VariantID"].ToString();
                    obj.VariantName = r["VariantName"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }


        #endregion

        #region VariantItemMaster

        public ActionResult VariantItemMaster(string VariantItemID)
        {
            Master model = new Master();
            if (VariantItemID != null)
            {
                model.VariantItemID = VariantItemID;

                DataSet ds = model.VariantItemList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    model.VariantItemID = ds.Tables[0].Rows[0]["PK_VariantItemID"].ToString();
                    model.VariantID = ds.Tables[0].Rows[0]["FK_VariantID"].ToString();
                    model.VariantName = ds.Tables[0].Rows[0]["VariantName"].ToString();
                    model.VariantItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    model.ColorCode = ds.Tables[0].Rows[0]["ColorCode"].ToString();
                    model.UnitID = ds.Tables[0].Rows[0]["FK_UnitID"].ToString();
                    model.UnitName = ds.Tables[0].Rows[0]["UnitName"].ToString();

                }


            }
            #region Unit
            Master objUnit = new Master();
            int count4 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds4 = objUnit.UnitList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion

            #region Variant
            Master objVariant = new Master();
            int count = 0;
            List<SelectListItem> ddlVariant = new List<SelectListItem>();
            DataSet ds1 = objVariant.VariantList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVariant.Add(new SelectListItem { Text = "Select Variant", Value = "0" });
                    }
                    ddlVariant.Add(new SelectListItem { Text = r["VariantName"].ToString(), Value = r["PK_VariantID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlVariant = ddlVariant;

            #endregion
            return View(model);
        }


        [HttpPost]
        [ActionName("VariantItemMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveVariantItem(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.SaveVariantItem();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["VariantItem"] = " Variant Added successfully !";

                    }
                    else
                    {
                        TempData["VariantItem"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["VariantItem"] = ex.Message;
            }
            return RedirectToAction("VariantItemMaster");
        }

        [HttpPost]
        [ActionName("VariantItemMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateVariantItem(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UpdateVariantItem();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["VariantItem"] = " Variant Item  Updated successfully !";

                    }
                    else
                    {
                        TempData["VariantItem"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["VariantItem"] = ex.Message;
            }
            return RedirectToAction("VariantItemMaster");
        }
        public ActionResult VariantItemList(Master model)
        {
            #region Variant
            Master objVariant = new Master();
            int count = 0;
            List<SelectListItem> ddlVariant = new List<SelectListItem>();
            DataSet ds1 = objVariant.VariantList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVariant.Add(new SelectListItem { Text = "Select Variant", Value = "0" });
                    }
                    ddlVariant.Add(new SelectListItem { Text = r["VariantName"].ToString(), Value = r["PK_VariantID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlVariant = ddlVariant;

            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("VariantItemList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetListItem(Master model)
        {
            List<Master> lst = new List<Master>();

            model.VariantID = model.VariantID == "0" ? null : model.VariantID;

            DataSet ds = model.VariantItemList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.VariantItemID = r["PK_VariantItemID"].ToString();
                    obj.VariantID = r["FK_VariantID"].ToString();
                    obj.VariantName = r["VariantName"].ToString();
                    obj.VariantItemName = r["ItemName"].ToString();
                    obj.ColorCode = r["ColorCode"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.UnitName = r["UnitName"].ToString();


                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }
            #region Variant
            Master objVariant = new Master();
            int count = 0;
            List<SelectListItem> ddlVariant = new List<SelectListItem>();
            DataSet ds1 = objVariant.VariantList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVariant.Add(new SelectListItem { Text = "Select Variant", Value = "0" });
                    }
                    ddlVariant.Add(new SelectListItem { Text = r["VariantName"].ToString(), Value = r["PK_VariantID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlVariant = ddlVariant;

            #endregion
            return View(model);
        }
        public ActionResult DeleteVariantItem(string id)
        {
            try
            {
                Master obj = new Master();
                obj.VariantItemID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteVariantItem();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["VariantItem"] = "Variant item deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["VariantItem"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["VariantItem"] = ex.Message;
            }
            return RedirectToAction("VariantItemList");
        }

        #endregion

        #region Complains
        public ActionResult Complains()
        {
            Customer model = new Customer();
            try
            {
                DataSet ds = model.GetAllComplainsAdmin();
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
                        obj.DisplayName = r["CustomerName"].ToString();
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
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult SearchComplains(Customer model)
        {
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

                DataSet ds = model.GetAllComplainsAdmin();
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
                        obj.DisplayName = r["AssociateName"].ToString();
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
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ReplyAdmin();
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
            //return RedirectToAction("ComplainReply?id=" + Crypto.Encrypt(model.PK_ComplainID));
        }

        #endregion

        public ActionResult ProductsNew()
        {

            return View();
        }

        #region VendorAddedProduct
        public ActionResult VendorAddedProductList(Master model)
        {
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;



            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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

            return View(model);
        }
        [HttpPost]
        [ActionName("VendorAddedProductList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult VendorAddedProductListBy(Master model)
        {
            List<Master> lst = new List<Master>();

            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.BrandID = model.BrandID == "0" ? null : model.BrandID;
            DataSet ds = model.VendorAddedProductListBy();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductOtherInfoID = r["PK_ProductOtherInfoID"].ToString();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    obj.Description = r["ProductInfo"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.CustomerName = r["VendorName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstProduct = lst;
                model.hdOfferID = model.OfferID;
            }

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;


            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            #region Unit
            Master objUnit = new Master();
            int count4 = 0;
            List<SelectListItem> ddlUnit = new List<SelectListItem>();
            DataSet ds4 = objUnit.UnitList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlUnit.Add(new SelectListItem { Text = "Select Unit", Value = "0" });
                    }
                    ddlUnit.Add(new SelectListItem { Text = r["UnitName"].ToString(), Value = r["PK_UnitID"].ToString() });
                    count4 = count4 + 1;
                }
            }

            ViewBag.ddlUnit = ddlUnit;

            #endregion


            return View(model);
        }


        [HttpPost]
        [ActionName("VendorAddedProductList")]
        [OnAction(ButtonName = "Save")]
        public ActionResult ApproveProduct(Master model)
        {
            try
            {
                if (Request["hdRows"] != null)
                {
                    string noofrows = Request["hdRows"].ToString();

                    string chk = "";
                    string productid = "";
                    int p = 0;
                    for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                    {
                        chk = Request["Chk_ " + i];
                        if (chk == "on")
                        {
                            model.ProductOtherInfoID = Request["productotherinfoid_ " + i].ToString();
                            model.ProductID = Request["productid_ " + i].ToString();
                            model.ShoppingPerc = Request["txtBV_ " + i].ToString();
                            model.Commission = Request["txtCommission_ " + i].ToString();
                            
                            model.DeliveryCharge = Request["txtShippingCharge_ " + i].ToString();
                            model.AddedBy = Session["Pk_AdminId"].ToString();

                            DataSet ds = model.ApproveProduct();
                            p = p + 1;
                        }
                    }
                    if (p > 0)
                    {
                        TempData["ApprovedProduct"] = " Approved successfully !";
                    }
                }
                else
                {
                    TempData["ApprovedProduct"] = "Please select atleast one row";
                }
            }
            catch (Exception ex)
            {

                TempData["ApprovedProduct"] = ex.Message;
            }

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
        #endregion

        #region CourrierRegistration
        public ActionResult CourrierRegistration(string CourrierID)
        {
            Master model = new Master();
            if (CourrierID != null)
            {
                model.CourrierID = CourrierID;

                DataSet ds = model.CourrierList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    model.CourrierID = CourrierID;
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.Pincode = ds.Tables[0].Rows[0]["Pincode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.Contact = ds.Tables[0].Rows[0]["Contact"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }

            }

            return View(model);
        }


        [HttpPost]
        [ActionName("CourrierRegistration")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveCourrierRegistration(Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {


                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveCourrierRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["CourrierRegistration"] = " Courrier Saved successfully !";

                    }
                    else
                    {
                        TempData["CourrierRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["CourrierRegistration"] = ex.Message;
            }
            FormName = "CourrierRegistration";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("CourrierRegistration")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateCourrier(Master obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.UpdateCourrier();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["CourrierRegistration"] = "Courrier updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["CourrierRegistration"] = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                else
                {
                    TempData["CourrierRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["CourrierRegistration"] = ex.Message;
            }
            return RedirectToAction("CourrierRegistration", "Master");
        }
        public ActionResult DeleteCourrier(string CourrierID)
        {
            try
            {
                Master obj = new Master();
                obj.CourrierID = CourrierID;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteCourrier();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["CourrierDel"] = "Courrier deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["CourrierDel"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["CourrierDel"] = ex.Message;
            }
            return RedirectToAction("CourrierList");
        }

        public ActionResult CourrierList(Master model)
        {

            List<Master> lst = new List<Master>();
            DataSet ds = model.CourrierList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();

                    obj.CourrierID = r["PK_CourrierID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Pincode = r["Pincode"].ToString();
                    obj.State = r["State"].ToString();
                    obj.City = r["City"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;

            }


            return View(model);
        }

        [HttpPost]
        [ActionName("CourrierList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult CourrierListBy(Master model)
        {

            List<Master> lst = new List<Master>();
            DataSet ds = model.CourrierList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();

                    obj.CourrierID = r["PK_CourrierID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Pincode = r["Pincode"].ToString();
                    obj.State = r["State"].ToString();
                    obj.City = r["City"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;

            }


            return View(model);
        }

        public ActionResult OrderDeliveryByCourrier(Master model)
        {
            #region Courrier
            Master objCourrier = new Master();
            int count1 = 0;
            List<SelectListItem> ddlCourrier = new List<SelectListItem>();
            DataSet ds2 = objCourrier.CourrierList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlCourrier.Add(new SelectListItem { Text = "Select  Courrier", Value = "0" });
                    }
                    ddlCourrier.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_CourrierID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlCourrier = ddlCourrier;

            #endregion

            List<Master> lst = new List<Master>();
            DataSet ds = model.OrdersList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();

                    obj.OrderID = r["PK_OrderDetailsID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.OrderNumber = r["OrderNo"].ToString();
                    obj.OrderDate = r["OrderDate"].ToString();
                    obj.Name = r["CustomerName"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PayMode = r["PaymentMode"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;

            }

            return View(model);
        }

        [HttpPost]
        [ActionName("OrderDeliveryByCourrier")]
        [OnAction(ButtonName = "Save")]
        public ActionResult AssignCourrier(Master model)
        {
            try
            {
                if (Request["hdRows"] != null)
                {
                    model.CourrierDate = string.IsNullOrEmpty(model.CourrierDate) ? null : Common.ConvertToSystemDate(model.CourrierDate, "dd/MM/yyyy");

                    string noofrows = Request["hdRows"].ToString();

                    string chk = "";

                    int p = 0;
                    for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                    {
                        chk = Request["Chk_ " + i];
                        if (chk == "on")
                        {
                            model.OrderID = Request["OrderID_ " + i].ToString();
                            model.AddedBy = Session["Pk_AdminId"].ToString();
                            model.TrackingNo = Request["trackno_ " + i].ToString();
                            DataSet ds = model.AssignCourrier();
                            p = p + 1;
                        }

                    }
                    if (p > 0)
                    {
                        TempData["AssignCourrier"] = " Courrier assigned to order successfully !";
                    }

                }
                else
                {
                    TempData["AssignCourrier"] = "Please select atleast one row";
                }
            }
            catch (Exception ex)
            {

                TempData["AssignCourrier"] = ex.Message;
            }
            #region Courrier
            Master objCourrier = new Master();
            int count1 = 0;
            List<SelectListItem> ddlCourrier = new List<SelectListItem>();
            DataSet ds2 = objCourrier.CourrierList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlCourrier.Add(new SelectListItem { Text = "Select  Courrier", Value = "0" });
                    }
                    ddlCourrier.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_CourrierID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlCourrier = ddlCourrier;

            #endregion
            List<Master> lst = new List<Master>();
            DataSet ds1 = model.OrdersList();

            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Master obj = new Master();

                    obj.OrderID = r["PK_OrderDetailsID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.OrderNumber = r["OrderNo"].ToString();
                    obj.OrderDate = r["OrderDate"].ToString();
                    obj.Name = r["CustomerName"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PayMode = r["PaymentMode"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;

            }
            return View(model);
        }


        public ActionResult OrderByCourrierList(Master model)
        {

            List<Master> lst = new List<Master>();
            DataSet ds = model.OrderByCourrierList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();


                    obj.ProductName = r["ProductName"].ToString();
                    obj.OrderNumber = r["OrderNo"].ToString();
                    obj.OrderDate = r["OrderDate"].ToString();
                    obj.Name = r["CustomerName"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PayMode = r["PaymentMode"].ToString();
                    obj.TrackingNo = r["TrackingNumber"].ToString();
                    obj.CourrierID = r["Name"].ToString();
                    obj.CourrierDate = r["CourrierDate"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;

            }

            return View(model);
        }

        [HttpPost]
        [ActionName("OrderByCourrierList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult OrderByCourrierListBy(Master model)
        {

            List<Master> lst = new List<Master>();
            //model.CourrierDate = string.IsNullOrEmpty(model.CourrierDate) ? null : Common.ConvertToSystemDate(model.CourrierDate, "dd/MM/yyyy");
            //model.OrderDate = string.IsNullOrEmpty(model.OrderDate) ? null : Common.ConvertToSystemDate(model.OrderDate, "dd/MM/yyyy");
            DataSet ds = model.OrderByCourrierList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();

                    obj.TrackingNo = r["TrackingNumber"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.OrderNumber = r["OrderNo"].ToString();
                    obj.OrderDate = r["OrderDate"].ToString();
                    obj.Name = r["CustomerName"].ToString();
                    obj.Contact = r["Contact"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PayMode = r["PaymentMode"].ToString();
                    obj.CourrierID = r["Name"].ToString();
                    obj.CourrierDate = r["CourrierDate"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;

            }

            return View(model);
        }

        #endregion

        #region AssignProductToVendor
        public ActionResult AssignProductToVendor(Master model)
        {
            #region Vendor
            Master objVendor = new Master();
            int count1 = 0;
            List<SelectListItem> ddlVendor = new List<SelectListItem>();
            DataSet ds2 = objVendor.VendorList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlVendor.Add(new SelectListItem { Text = "Select  Vendor", Value = "0" });
                    }
                    ddlVendor.Add(new SelectListItem { Text = r["FullName"].ToString(), Value = r["PK_UserID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlVendor = ddlVendor;

            #endregion
            #region Product
            Master objProduct = new Master();
            int count2 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objVendor.ProductListAdmin();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select  Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count2 = count2 + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View(model);
        }
        [HttpPost]
        [ActionName("AssignProductToVendor")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetProductWithVariants(Master model)
        {
            #region Vendor
            Master objVendor = new Master();
            int count1 = 0;
            List<SelectListItem> ddlVendor = new List<SelectListItem>();
            DataSet ds2 = objVendor.VendorList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlVendor.Add(new SelectListItem { Text = "Select  Vendor", Value = "0" });
                    }
                    ddlVendor.Add(new SelectListItem { Text = r["FullName"].ToString(), Value = r["PK_UserID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlVendor = ddlVendor;

            #endregion
            #region Product
            Master objProduct = new Master();
            int count2 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objVendor.ProductListAdmin();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select  Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count2 = count2 + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            try
            {
                //Master model = new Master();
                //model.ProductID = ProductID;
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.GetProductWithVariants();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.MainCategoryName = r["MainCategoryName"].ToString();
                        obj.CategoryName = r["CategoryName"].ToString();
                        obj.SubCategoryName = r["SubCategoryName"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.MRP = r["MRP"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.Storage = r["Storage"].ToString();
                        obj.StarRating = r["StarRating"].ToString();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.Quantity = r["BalanceStock"].ToString();


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
        [ActionName("AssignProductToVendor")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveProductToVendor(Master model)
        {
            try
            {
                if (Request["hdrows"] != null)
                {


                    string noofrows = Request["hdrows"].ToString();

                    string chk = "";

                    int p = 0;
                    for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                    {
                        chk = Request["Chk_ " + i];
                        if (chk == "on")
                        {

                            model.AddedBy = Session["Pk_AdminId"].ToString();
                            model.ProductInfoCode = Request["ProductInfoCode " + i].ToString();
                            model.DebQuantity = Request["txtqty " + i].ToString();
                            DataSet ds = model.SaveProductToVendor();
                            p = p + 1;
                        }

                    }
                    if (p > 0)
                    {
                        TempData["ProductToVendor"] = " Product assigned to order successfully !";
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
            #region Vendor
            Master objVendor = new Master();
            int count1 = 0;
            List<SelectListItem> ddlVendor = new List<SelectListItem>();
            DataSet ds2 = objVendor.VendorList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlVendor.Add(new SelectListItem { Text = "Select  Vendor", Value = "0" });
                    }
                    ddlVendor.Add(new SelectListItem { Text = r["FullName"].ToString(), Value = r["PK_UserID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlVendor = ddlVendor;

            #endregion
            #region Product
            Master objProduct = new Master();
            int count2 = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objVendor.ProductListAdmin();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select  Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count2 = count2 + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View(model);
        }
        #endregion
        
        #region VendorReports

        public ActionResult ProductsAssignedByAdmin(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ProductsAssignedByAdmin();
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
        [ActionName("ProductsAssignedByAdmin")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ProductsAssignedByAdminBy(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ProductsAssignedByAdmin();
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

        public ActionResult ProductsAssignedByAdminLedger(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_AdminId"].ToString();
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


      


        public ActionResult ApprovedQuantityRequests(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApprovedQuantityRequests();
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
                        obj.Quantity = r["RequestedQty"].ToString();
                        obj.DebQuantity = r["AssignedQty"].ToString();
                        obj.Status =r["Status"].ToString();
                        obj.ToDate =r["AssignedDate"].ToString();
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
        [ActionName("ApprovedQuantityRequests")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ApprovedQuantityRequestsBy(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApprovedQuantityRequests();
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
                        obj.Quantity = r["RequestedQty"].ToString();
                        obj.DebQuantity = r["AssignedQty"].ToString();
                        obj.Status = r["Status"].ToString();
                        obj.ToDate = r["AssignedDate"].ToString();
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
        #endregion

        #region VendorRequests

        public ActionResult VendorProductQuantityRequests(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();

                DataSet ds = model.VendorProductQuantityRequestsList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.RequestID = r["PK_QtyReqID"].ToString();
                        obj.VendorID = r["FK_VendorId"].ToString();
                        obj.ProductID = r["FK_ProductId"].ToString();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.Name = r["Fullname"].ToString();
                        obj.Quantity = r["RequestedQty"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.Storage = r["Storage"].ToString();
                        obj.BalanceQuantity = r["Balance"].ToString();

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
        [ActionName("VendorProductQuantityRequests")]
        [OnAction(ButtonName = "Search")]
        public ActionResult VendorProductQuantityRequestsBy(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();

                DataSet ds = model.VendorProductQuantityRequestsList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.RequestID = r["PK_QtyReqID"].ToString();
                        obj.VendorID= r["FK_VendorId"].ToString();
                        obj.ProductID = r["FK_ProductId"].ToString();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.Name = r["Fullname"].ToString();
                        obj.Quantity = r["RequestedQty"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.BrandName = r["BrandName"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.FlavorName = r["FlavorName"].ToString();
                        obj.MaterialName = r["MaterialName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.RAM = r["RAM"].ToString();
                        obj.Storage = r["Storage"].ToString();
                        obj.BalanceQuantity = r["Balance"].ToString();

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
        [ActionName("VendorProductQuantityRequests")]
        [OnAction(ButtonName = "Save")]
        public ActionResult ApproveQty(Master model)
        {
            try
            {
                if (Request["hdrows"] != null )
                { 
                    string noofrows = Request["hdrows"].ToString(); 
                    int p = 0;
                    for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                    {
                        if(Request["txtqty " + i].ToString() != "")
                        {
                            model.AddedBy = Session["Pk_AdminId"].ToString();

                            model.VendorID = Request["VendorID " + i].ToString();
                            model.DebQuantity = Request["txtqty " + i].ToString();
                            model.RequestID = Request["RequestID " + i].ToString();

                            DataSet ds = model.ApproveQty();
                            p = p + 1;
                        }
                      
                    }
                    if (p > 0)
                    {
                        TempData["ApproveQty"] = "Quantity assigned successfully !";
                    } 
                }
                else
                {
                    TempData["ApproveQty"] = "Please select atleast one row";
                }
            }
            catch (Exception ex)
            {

                TempData["ApproveQty"] = ex.Message;
            }

            return View(model);
        }
        #endregion
        
        #region Comments

        public ActionResult ProductRatingList(Master model  )
        {

            try
            {
                List<Master> lst = new List<Master>();

                DataSet ds = model.ProductRatingList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.RatingID = r["Pk_RatingId"].ToString();
                        obj.StarRating = r["StarRating"].ToString();
                        obj.Comment = r["Comment"].ToString();
                        
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
        public ActionResult DeleteProductRating(string id)
        {
            try
            {
                Master obj = new Master();
                obj.RatingID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteProductRating();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["ProductRatingList"] = "Product Rating deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["ProductRatingList"] = ex.Message;
            }
            return RedirectToAction("ProductRatingList");
        }


        public ActionResult VendorRatingList(Master model)
        {

            try
            {
                List<Master> lst = new List<Master>();

                DataSet ds = model.VendorRatingList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.RatingID = r["Pk_RatingId"].ToString();
                        obj.StarRating = r["StarRating"].ToString();
                        obj.Comment = r["Comment"].ToString();

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
        public ActionResult DeleteVendorRating(string id)
        {
            try
            {
                Master obj = new Master();
                obj.RatingID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteVendorRating();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["VendorRating"] = "Vendor Rating deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["VendorRating"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["VendorRating"] = ex.Message;
            }
            return RedirectToAction("VendorRatingList");
        }
        #endregion


        #region GiftVoucher

        public ActionResult GiftVoucher(Master model)
        {

            #region lstPayment
            List<SelectListItem> lstPayment = Common.lstPayment();
            ViewBag.lstPayment = lstPayment;
            #endregion lstPayment
            return View(model);
        }
        [HttpPost]
        [ActionName("GiftVoucher")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveGiftVoucher(  Master obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                 
                //obj.ToDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.ToDate) ? null : obj.ToDate, "dd/MM/yyyy");

                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveGiftVoucher();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["GiftVoucher"] = " Gift Voucher Generated successfully !";

                    }
                    else
                    {
                        TempData["GiftVoucher"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["GiftVoucher"] = ex.Message;
            }
            FormName = "GiftVoucher";
            Controller = "Master";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GiftVoucherList(Master model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("GiftVoucherList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GiftVoucherListBy(Master model)
        {
            List<Master> lst = new List<Master>();

            // model.SiteID = model.SiteID == "0" ? null : model.SiteID;

            DataSet ds = model.DiscountForCustomersList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Master obj = new Master();
                    obj.CouponCode = r["CouponCode"].ToString();
                    obj.DiscountAmount = r["DiscountAmount"].ToString();
                    obj.FromDate = r["ValidFrom"].ToString();
                    obj.ToDate = r["ValidTo"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Discforcustid = r["PK_DiscForCustID"].ToString();
                    obj.PayMode = r["PaymentType"].ToString();
                    obj.Description = r["Description"].ToString();

                    lst.Add(obj);
                }
                model.lstProduct = lst;
            }

            return View(model);
        }
        public ActionResult GetSponsorName(string LoginID)
        {
            try
            {
                Master model = new Master();
                model.LoginID = LoginID;


                DataSet dsSponsorName = model.GetAssociateList();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.Name = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.Fk_UserId = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();



                    model.Result = "yes";
                }
                else
                {
                    model.Name = "";
                    model.Result = "Invalid  Id.";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Master model = new Master();
                model.Result = ex.Message;

                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
      public ActionResult ShoppingBalance(string Fk_UserId)
        {
            Master model = new Master();
            model.Fk_UserId = Fk_UserId;
            DataSet ds1 = model.GetCustomerWalletBalance();
            if (ds1 != null && ds1.Tables.Count > 0)
            {

                model.BalanceQuantity = ds1.Tables[0].Rows[0]["Balance"].ToString();
              
            }
          
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region RAM


        public ActionResult Ram(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.List();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.RAM = r["RAM"].ToString();
                    obj.RamID = r["PK_RAM_ID"].ToString();

                    lst.Add(obj);
                }
                model.lstRam = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Ram")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveRam(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                
                DataSet ds = model.Ram();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Ram"] = "RAM Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Ram"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Ram"] = ex.Message;
            }
            return RedirectToAction("Ram");
        }

        public ActionResult UpdateRam(string RamID, string RAM)
        {
            Master model = new Master();
            try
            {
                model.RamID = RamID;
                model.RAM = RAM;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateRam();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
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

        public ActionResult DeleteRam(string RamID)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.RamID = RamID;

                DataSet ds = model.DeleteRam();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Ram"] = "Deleted Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Ram"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Ram"] = ex.Message;
            }
            return RedirectToAction("Ram");
        }
        #endregion

        #region Storage

        public ActionResult Storage(Master model)
        {
            List<Master> list = new List<Master>();

            DataSet ds = model.StorageList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.StorageID = r["PK_StorageID"].ToString();
                    obj.Storage = r["Storage"].ToString();
                    list.Add(obj);

                }
                model.lstStorage = list;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Storage")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveStorage(Master model)
        {

            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.AddStorage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["temp"] = "Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {

                        TempData["temp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["temp"] = ex.Message;
            }
            return RedirectToAction("Storage");
        }

        public ActionResult UpdateStorage(string StorageID, string Storage)
        {
            Master model = new Master();
            try
            {
                model.StorageID = StorageID;
                model.Storage = Storage;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UpdateStorage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
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

        public ActionResult DeleteStorage(string StorageID)
        {
            try
            {
                Master model = new Master();
                model.StorageID = StorageID;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteStorage();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["temp"] = "Deleted Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["temp"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["temp"] = ex.Message;
            }

            return RedirectToAction("Storage");

        }


        #endregion

        #region FeatureType

        public ActionResult FeatureType(Master model)
        {
            try
            {
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
               

                List<Master> lst = new List<Master>();

                DataSet ds = model.FeatureTypeList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj2 = new Master();
                        obj2.FeatureTypeId = r["Pk_Id"].ToString();
                        obj2.FeatureTypeName = r["Name"].ToString(); 
                        obj2.Images = r["Images"].ToString();
                        lst.Add(obj2);
                    }
                    model.lstSubCategory = lst;
                }
                 
               
            }

            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("FeatureType")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveFeatureType(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Images = "../images/MainCategoryImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Images)));
                }

                DataSet ds = model.SaveFeatureType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["FeatureType"] = "Feature Type saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["FeatureType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["FeatureType"] = ex.Message;
            }
            return RedirectToAction("FeatureType");
            //return View();
        }
        [HttpPost]
        [ActionName("FeatureType")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateFeatureType(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                if (postedFile != null)
                {
                    model.Images = "../images/MainCategoryImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Images)));
                }
                DataSet ds = model.UpdateFeatureType();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        model.Result = "FeatureType updated successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                model.Result = ex.Message;
            }
            return RedirectToAction("FeatureType");
        }

        public ActionResult DeleteFeatureType(string id)
        {
            try
            {
                Master obj = new Master();
                obj.FeatureTypeId = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteFeatureType();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["FeatureType"] = "FeatureType deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["FeatureType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["FeatureType"] = ex.Message;
            }
            return RedirectToAction("FeatureType");
        }

        #endregion

        #region AssignProductFeatureType
        public ActionResult AssignProductFeatureType(Master model)
        {
            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;

            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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

            #region FeatureType
            Master objf = new Master();
            int countf = 0;
            List<SelectListItem> ddlFeature = new List<SelectListItem>();
            DataSet dsf = objf.FeatureTypeList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countf == 0)
                    {
                        ddlFeature.Add(new SelectListItem { Text = "Select Feature", Value = "0" });
                    }
                    ddlFeature.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    countf = countf + 1;
                }
            }

            ViewBag.ddlFeature = ddlFeature;

            //List<Master> lst1 = new List<Master>();
            //DataSet dsv = model.FeatureTypeList();

            //if (dsv != null && dsv.Tables.Count > 0 && dsv.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dsv.Tables[0].Rows)
            //    {
            //        Master obj = new Master();
            //        obj.FeatureTypeId = r["Pk_Id"].ToString();
            //        obj.FeatureTypeName = r["Name"].ToString();

            //        lst1.Add(obj);
            //    }
            //    model.lstVariant = lst1;
            //}
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("AssignProductFeatureType")]
        [OnAction(ButtonName = "Get")]
        public ActionResult GetAssignProductFeatureType(Master model)
        {
            List<Master> lst = new List<Master>();
            model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
            model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
            model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
            model.BrandID = model.BrandID == "0" ? null : model.BrandID;
            DataSet ds = model.GetListNewArrivals();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ProductID = r["PK_ProductID"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    obj.SubCategoryName = r["SubCategoryName"].ToString();
                    //obj.BV = r["BV"].ToString();
                    //obj.MRP = r["MRP"].ToString();
                    //obj.PrimaryImage = r["PrimaryImage"].ToString();
                    obj.MainCategoryName = r["MainCategoryName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();

                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.MainCategoryID = r["FK_MainCategory"].ToString();
                    obj.CategoryID = r["FK_CategoryID"].ToString();
                    obj.SubCategoryID = r["FK_SubCategoryID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.FlavorName = r["FlavorName"].ToString();
                    obj.MaterialName = r["MaterialName"].ToString();
                    obj.RAM = r["RAM"].ToString();
                    obj.Storage = r["Storage"].ToString();
                    obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                    lst.Add(obj);
                }
                model.lstProduct = lst;
                model.hdOfferID = model.OfferID;
            }
            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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
            //ViewBag.ddlMainCategory = ddlMainCategory;
            //List<SelectListItem> ddlCategory = new List<SelectListItem>();
            //ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            //ViewBag.ddlCategory = ddlCategory;

            //List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            //ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            //ViewBag.ddlSubCategory = ddlSubCategory;
            Master objC1 = new Master();
            int countc = 0;
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            DataSet ds1c = objC1.CategoryList();
            if (ds1c != null && ds1c.Tables.Count > 0 && ds1c.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1c.Tables[0].Rows)
                {
                    if (countc == 0)
                    {
                        ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                    }
                    ddlCategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryID"].ToString() });
                    countc = 1;
                }
            }
            ViewBag.ddlCategory = ddlCategory;
            Master objsubcat = new Master();
            int countcs = 0;
            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            DataSet ds1cs = objsubcat.SubCategoryList();
            if (ds1cs != null && ds1cs.Tables.Count > 0 && ds1cs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1cs.Tables[0].Rows)
                {
                    if (countcs == 0)
                    {
                        ddlSubCategory.Add(new SelectListItem { Text = "Select sub Category", Value = "0" });
                    }
                    ddlSubCategory.Add(new SelectListItem { Text = r["SubCategoryName"].ToString(), Value = r["PK_SubCategoryID"].ToString() });
                    countcs = 1;
                }
            }
            ViewBag.ddlSubCategory = ddlSubCategory;
            #endregion
            Master objf = new Master();
            int countf = 0;
            List<SelectListItem> ddlFeature = new List<SelectListItem>();
            DataSet dsf = objf.FeatureTypeList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countf == 0)
                    {
                        ddlFeature.Add(new SelectListItem { Text = "Select Feature", Value = "0" });
                    }
                    ddlFeature.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    countf = countf + 1;
                }
            }

            ViewBag.ddlFeature = ddlFeature;
            //#region FeatureType
            //List<Master> lst1 = new List<Master>();
            //DataSet dsv = model.FeatureTypeList();

            //if (dsv != null && dsv.Tables.Count > 0 && dsv.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dsv.Tables[0].Rows)
            //    {
            //        Master obj = new Master();
            //        obj.FeatureTypeId = r["Pk_Id"].ToString();
            //        obj.FeatureTypeName = r["Name"].ToString();

            //        lst1.Add(obj);
            //    }
            //    model.lstVariant = lst1;
            //}
            //#endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("AssignProductFeatureType")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveAssignProductFeatureType(Master model)
        {

            try
            {
             //   #region  for feature 
             //   string noofrows1 = Request["hdrows1"].ToString();
             //   string varid = "";
             //   string chk1 = "";
             ////   DataTable dtst = new DataTable();
             //  // dtst.Columns.Add("FK_VariantID", typeof(string));

             //   for (int j = 1; j <= int.Parse(noofrows1) - 1; j++)
             //   {
             //       chk1 = Request["Chk1_ " + j];
             //       if (chk1 == "on")
             //       {
             //           varid = model.FeatureTypeId;

             //           //dtst.Rows.Add(varid);
             //       }
             //   }
             //   #endregion


                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string ProductInfoCode = "";


                DataTable dtst = new DataTable();
                dtst.Columns.Add("ProductInfoCode", typeof(string));
                
                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        ProductInfoCode = Request["ProductInfoCode_ " + i].ToString();
                        dtst.Rows.Add(ProductInfoCode);
                    }
                }

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.dtProductQuantity = dtst;
                DataSet ds = model.AddNewArrivals();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Product"] = "  Added successfully !";

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

            #region Brand
            Master objBrand = new Master();
            int count1 = 0;
            List<SelectListItem> ddlBrand = new List<SelectListItem>();
            DataSet ds2 = objBrand.BrandList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlBrand.Add(new SelectListItem { Text = "Select  Brand", Value = "0" });
                    }
                    ddlBrand.Add(new SelectListItem { Text = r["BrandName"].ToString(), Value = r["PK_BrandID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlBrand = ddlBrand;

            #endregion
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

            Master objf = new Master();
            int countf = 0;
            List<SelectListItem> ddlFeature = new List<SelectListItem>();
            DataSet dsf = objf.FeatureTypeList();
            if (dsf != null && dsf.Tables.Count > 0 && dsf.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsf.Tables[0].Rows)
                {
                    if (countf == 0)
                    {
                        ddlFeature.Add(new SelectListItem { Text = "Select Feature", Value = "0" });
                    }
                    ddlFeature.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    countf = countf + 1;
                }
            }

            ViewBag.ddlFeature = ddlFeature;


            //#region FeatureType
            //List<Master> lst1 = new List<Master>();
            //DataSet dsv = model.FeatureTypeList();

            //if (dsv != null && dsv.Tables.Count > 0 && dsv.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dsv.Tables[0].Rows)
            //    {
            //        Master obj = new Master();
            //        obj.FeatureTypeId = r["Pk_Id"].ToString();
            //        obj.FeatureTypeName = r["Name"].ToString();

            //        lst1.Add(obj);
            //    }
            //    model.lstVariant = lst1;
            //}
            //#endregion
            return RedirectToAction("AssignProductFeatureType");
        }

        public ActionResult AssignProductFeatureTypeList(Master model)
        {
            List<SelectListItem> ddlCategory = new List<SelectListItem>();
            ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
            ViewBag.ddlCategory = ddlCategory;

            List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
            ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
            ViewBag.ddlSubCategory = ddlSubCategory;


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

            #region Product
            Master objProduct = new Master();
            int countP = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet dsProduct = objProduct.ProductList();
            if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsProduct.Tables[0].Rows)
                {
                    if (countP == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    countP = countP + 1;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            return View(model);
        }

        [HttpPost]
        [ActionName("AssignProductFeatureTypeList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SearchAssignProductFeatureType(Master model)
        {
            try
            {
                List<Master> lst = new List<Master>();

                model.CategoryID = model.CategoryID == "0" ? null : model.CategoryID;
                model.SubCategoryID = model.SubCategoryID == "0" ? null : model.SubCategoryID;
                model.MainCategoryID = model.MainCategoryID == "0" ? null : model.MainCategoryID;
                //  model.OfferID = model.OfferID == "0" ? null : model.OfferID;
                model.ProductID = model.ProductID == "0" ? null : model.ProductID;

                DataSet ds = model.NewArrivalsList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        //obj.ProductID = r["PK_ProductID"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.CategoryName = r["CategoryName"].ToString();
                        obj.SubCategoryName = r["SubCategoryName"].ToString();
                        obj.MainCategoryName = r["MainCategoryName"].ToString();
                        obj.OfferProductID = r["PK_NewArrivalProdID"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.SizeName = r["SizeName"].ToString();



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

                #region Product
                Master objProduct = new Master();
                int countP = 0;
                List<SelectListItem> ddlProduct = new List<SelectListItem>();
                DataSet dsProduct = objProduct.ProductList();
                if (dsProduct != null && dsProduct.Tables.Count > 0 && dsProduct.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsProduct.Tables[0].Rows)
                    {
                        if (countP == 0)
                        {
                            ddlProduct.Add(new SelectListItem { Text = "Select Product", Value = "0" });
                        }
                        ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                        countP = countP + 1;
                    }
                }

                ViewBag.ddlProduct = ddlProduct;
                List<SelectListItem> ddlCategory = new List<SelectListItem>();
                ddlCategory.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                ViewBag.ddlCategory = ddlCategory;

                List<SelectListItem> ddlSubCategory = new List<SelectListItem>();
                ddlSubCategory.Add(new SelectListItem { Text = "Select SubCategory", Value = "0" });
                ViewBag.ddlSubCategory = ddlSubCategory;
                #endregion
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Product"] = ex.Message;
            }
            return View(model);
        }

        public ActionResult DeleteAssignProductFeatureType(string id)
        {
            try
            {
                Master obj = new Master();
                obj.OfferProductID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteNewArrivals();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "Green";
                        TempData["Discount"] = " Deleted successfully";
                    }
                    else
                    {
                        TempData["Class"] = "Red";
                        TempData["Discount"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "Red";
                TempData["Discount"] = ex.Message;
            }
            return RedirectToAction("AssignProductFeatureTypeList");
        }
        #endregion

        #region DashboardBanner

        public ActionResult DashboardBanner()
        {
            return View();
        }

        [HttpPost]
        [ActionName("DashboardBanner")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveDashboardBanner(HttpPostedFileBase postedFile, Master model)
        {
            try
            {
                string path = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                if (postedFile != null)
                {

                    model.Images = "../images/BannerImage/" + path;
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Images)));


                }
                model.Images = "../images/BannerImage/" + path;
               
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveDashboardBanner();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["DBBanner"] = "Dashboard Banner saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["DBBanner"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["DBBanner"] = ex.Message;
            }
            return RedirectToAction("DashboardBanner");
            //return View();
        }

        #endregion
    }
}

