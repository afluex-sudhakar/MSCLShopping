using MSCLShopping.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SD = System.Drawing;

namespace MSCLShopping
{
    public partial class AddProduct : System.Web.UI.Page
    {
        Master obj = new Master();
        DataTable dt = new DataTable();
        DataTable dtSecondaryImages = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["tmpData"] = null;
                Session["dtSecImages"] = null;
                BindMaincategory();
                BindUnit();
                BindBrand();

            }
            //if (Session["UserType"].ToString() == "Admin")
            //{
            //    divBV.Visible = true;
            //    redeemshopping.Visible = true;
            //    Delivery.Visible = true;
            //}
            //else
            //{
            //    txtDeliveryCharge.Text = "0";
            //}
        }
        protected void BindMaincategory()
        {

            DataSet ds = obj.MainCategoryList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Common.BindDropDown(ddlmaincategory, "PK_MainCategoryID", "MainCategoryName", ds.Tables[0]);
            }
        }
        protected void BindUnit()
        {

            DataSet ds = obj.UnitList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Common.BindDropDown(ddlunit, "PK_UnitID", "UnitName", ds.Tables[0]);
            }
        }
        protected void BindBrand()
        {
            if (Session["Pk_VendorId"] != null)
            {
                obj.VendorID = Session["Pk_VendorId"].ToString();
            }

            DataSet ds = obj.BrandList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Common.BindDropDown(ddlbrand, "PK_BrandID", "BrandName", ds.Tables[0]);
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (divcolor.Visible == true)
            {
                if (ddlcolor.SelectedIndex == 0)
                {
                    ddlcolor.BorderColor = SD.Color.Red;
                    return;
                }
            }
            if (divsize.Visible == true)
            {
                if (ddlsize.SelectedIndex == 0)
                {
                    ddlsize.BorderColor = SD.Color.Red;
                    return;
                }
            }
            if (ddlflavour.Visible == true)
            {
                if (ddlflavour.SelectedIndex == 0)
                {
                    ddlflavour.BorderColor = SD.Color.Red;
                    return;
                }
            }
            if (ddlram.Visible == true)
            {
                if (ddlram.SelectedIndex == 0)
                {
                    ddlram.BorderColor = SD.Color.Red;
                    return;
                }
            }
            if (ddlstorage.Visible == true)
            {
                if (ddlstorage.SelectedIndex == 0)
                {
                    ddlstorage.BorderColor = SD.Color.Red;
                    return;
                }
            }
            #region grdview start
            try
            {

                Session["ProductInfoCode"] = DateTime.Now.ToString("ddMMyyyyHHmmss");
                obj.ProductInfoCode = Session["ProductInfoCode"].ToString();
                if (Session["tmpData"] != null)
                {
                    dt = (DataTable)Session["tmpData"];
                    DataRow dr = null;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.NewRow();
                        dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                        dr["PrimaryImage"] = "";
                        dr["FK_UnitID"] = ddlunit.SelectedValue;
                        dr["UnitName"] = ddlunit.SelectedItem.Text;
                        dr["FK_SizeID"] = ddlsize.SelectedValue;
                        if (ddlsize.SelectedIndex == 0 || ddlsize.SelectedIndex == -1)
                        {
                            dr["SizeName"] = "NA";
                        }
                        else
                        {
                            dr["SizeName"] = ddlsize.SelectedItem.Text;
                        }

                        dr["FK_ColorID"] = ddlcolor.SelectedValue;
                        if (ddlcolor.SelectedIndex == 0 || ddlcolor.SelectedIndex == -1)
                        {
                            dr["ColorName"] = "NA";
                        }
                        else
                        {
                            dr["ColorName"] = ddlcolor.SelectedItem.Text;
                        }
                        dr["MRP"] = txtmrp.Text;
                        dr["OfferPrice"] = txtofferedprice.Text;
                        dr["DealerPrice"] = "0";
                        dr["BV"] = txtbv.Text;
                        dr["CGST"] = txtcgst.Text;
                        dr["SGST"] = txtsgst.Text;
                        dr["IGST"] = txtigst.Text;
                        dr["Quantity"] = txtqty.Text;
                        dr["FK_FlavorID"] = ddlflavour.SelectedValue;
                        if (ddlflavour.SelectedIndex == 0 || ddlflavour.SelectedIndex == -1)
                        {
                            dr["FlavorName"] = "NA";
                        }
                        else
                        {
                            dr["FlavorName"] = ddlflavour.SelectedItem.Text;
                        }

                        dr["FK_MaterialID"] = ddlmaterial.SelectedValue;
                        if (ddlmaterial.SelectedIndex == 0 || ddlmaterial.SelectedIndex == -1)
                        {
                            dr["MaterialName"] = "NA";
                        }
                        else
                        {
                            dr["MaterialName"] = ddlmaterial.SelectedItem.Text;
                        }

                        dr["FK_RamID"] = ddlram.SelectedValue;
                        if (ddlram.SelectedIndex == 0 || ddlram.SelectedIndex == -1)
                        {
                            dr["RAM"] = "NA";
                        }
                        else
                        {
                            dr["RAM"] = ddlram.SelectedItem.Text;
                        }
                        dr["FK_StorageID"] = ddlstorage.SelectedValue;
                        if (ddlstorage.SelectedIndex == 0 || ddlstorage.SelectedIndex == -1)
                        {
                            dr["StorageName"] = "NA";
                        }
                        else
                        {
                            dr["StorageName"] = ddlstorage.SelectedItem.Text;
                        }
                        dr["FK_StarratingID"] = 0;
                        dr["StarRatingName"] = "N/A";
                        dr["shoppingpointredemperc"] = string.IsNullOrEmpty(txtshoppingpointredemperc.Text)?"0":txtshoppingpointredemperc.Text;

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
                    dt.Columns.Add("ShoppingPointRedemPerc", typeof(string));

                    DataRow dr = dt.NewRow();
                    dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                    dr["PrimaryImage"] = "";
                    dr["FK_UnitID"] = ddlunit.SelectedValue;
                    dr["UnitName"] = ddlunit.SelectedItem.Text;
                    dr["FK_SizeID"] = ddlsize.SelectedValue;
                    if (ddlsize.SelectedIndex == 0 || ddlsize.SelectedIndex == -1)
                    {
                        dr["SizeName"] = "NA";
                    }
                    else
                    {
                        dr["SizeName"] = ddlsize.SelectedItem.Text;
                    }

                    dr["FK_ColorID"] = ddlcolor.SelectedValue;
                    if (ddlcolor.SelectedIndex == 0 || ddlcolor.SelectedIndex == -1)
                    {
                        dr["ColorName"] = "NA";
                    }
                    else
                    {
                        dr["ColorName"] = ddlcolor.SelectedItem.Text;
                    }

                    dr["MRP"] = txtmrp.Text;
                    dr["OfferPrice"] = txtofferedprice.Text;
                    dr["DealerPrice"] = "0";
                    dr["BV"] = txtbv.Text;
                    dr["CGST"] = txtcgst.Text;
                    dr["SGST"] = txtsgst.Text;
                    dr["IGST"] = txtigst.Text;
                    dr["Quantity"] = txtqty.Text;
                    dr["FK_FlavorID"] = ddlflavour.SelectedValue;
                    if (ddlflavour.SelectedIndex == 0 || ddlflavour.SelectedIndex == -1)
                    {
                        dr["FlavorName"] = "NA";
                    }
                    else
                    {
                        dr["FlavorName"] = ddlflavour.SelectedItem.Text;
                    }

                    dr["FK_MaterialID"] = ddlmaterial.SelectedValue;
                    if (ddlmaterial.SelectedIndex == 0 || ddlmaterial.SelectedIndex == -1)
                    {
                        dr["MaterialName"] = "NA";
                    }
                    else
                    {
                        dr["MaterialName"] = ddlmaterial.SelectedItem.Text;
                    }

                    dr["FK_RamID"] = ddlram.SelectedValue;
                    if (ddlram.SelectedIndex == 0 || ddlram.SelectedIndex == -1)
                    {
                        dr["RAM"] = "NA";
                    }
                    else
                    {
                        dr["RAM"] = ddlram.SelectedItem.Text;
                    }

                    dr["FK_StorageID"] = ddlstorage.SelectedValue;
                    if (ddlstorage.SelectedIndex == 0 || ddlstorage.SelectedIndex == -1)
                    {
                        dr["StorageName"] = "NA";
                    }
                    else
                    {
                        dr["StorageName"] = ddlstorage.SelectedItem.Text;
                    }

                    dr["FK_StarratingID"] = 0;
                    dr["StarRatingName"] = "N/A";
                    dr["shoppingpointredemperc"] = string.IsNullOrEmpty(txtshoppingpointredemperc.Text) ? "0" : txtshoppingpointredemperc.Text;

                    dt.Rows.Add(dr);
                    Session["tmpData"] = dt;

                }

                dt = (DataTable)Session["tmpData"];
                List<Master> lstTmpData = new List<Master>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    grdvarients.DataSource = dt;
                    grdvarients.DataBind();
                    grdvarients.Visible = true;

                }

            }
            catch (Exception ex)
            {

            }
            #endregion gridview end

            #region ImageStart
            if (flpimages.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in flpimages.PostedFiles)
                {
                    String path = HttpContext.Current.Request.PhysicalApplicationPath + "images\\ProdSecondaryImage\\";
                    string filename = DateTime.Now.ToString("ddMMyyyyHHmmsss") + uploadedFile.FileName;

                    ///uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/images/ProdSecondaryImage"), filename));
                    Stream strm = uploadedFile.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
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
                            dr = dtSecondaryImages.NewRow();
                            dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                            dr["ImagePath"] = "../../Images/ProdSecondaryImage/" + filename;
                            dr["ImageType"] = "Primary";

                            dtSecondaryImages.Rows.Add(dr);
                            DataRow dr1 = null;
                            dr1 = dtSecondaryImages.NewRow();
                            dr1["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                            dr1["ImagePath"] = "../../Images/ProdSecondaryImage/" + filename;
                            dr1["ImageType"] = "Secondary";
                            dtSecondaryImages.Rows.Add(dr1);

                            Session["dtSecImages"] = dtSecondaryImages;
                        }
                    }
                    else
                    {
                        dtSecondaryImages.Columns.Add("ImageType", typeof(string));
                        dtSecondaryImages.Columns.Add("ImagePath", typeof(string));
                        dtSecondaryImages.Columns.Add("ProductInfoCode", typeof(string));

                        DataRow dr = dtSecondaryImages.NewRow();
                        dr["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                        dr["ImagePath"] = "../../Images/ProdPrimaryImage/" + filename;
                        dr["ImageType"] = "Primary";
                        dtSecondaryImages.Rows.Add(dr);
                        DataRow dr1 = null;
                        dr1 = dtSecondaryImages.NewRow();
                        dr1["ProductInfoCode"] = Session["ProductInfoCode"].ToString();
                        dr1["ImagePath"] = "../../Images/ProdSecondaryImage/" + filename;
                        dr1["ImageType"] = "Secondary";
                        dtSecondaryImages.Rows.Add(dr1);
                        Session["dtSecImages"] = dtSecondaryImages;
                    }

                }
            }
            #endregion ImageEnd
        }

        protected void ddlmaincategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj.MainCategoryID = ddlmaincategory.SelectedValue;
            DataSet ds = obj.CategoryList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Common.BindDropDown(ddlcategory, "PK_CategoryID", "CategoryName", ds.Tables[0]);
            }



        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj.CategoryID = ddlcategory.SelectedValue;
            DataSet ds = obj.SubCategoryList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Common.BindDropDown(ddlsubcategory, "PK_SubCategoryID", "SubCategoryName", ds.Tables[0]);
            }
            for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
            {
                if (ds.Tables[1].Rows[i]["VariantName"].ToString() == "Size")
                {
                    divsize.Visible = true;

                }
                if (ds.Tables[1].Rows[i]["VariantName"].ToString() == "Color")
                {
                    divcolor.Visible = true;

                    DataSet ds1 = obj.ColorList();
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        Common.BindDropDown(ddlcolor, "PK_ColorID", "ColorName", ds1.Tables[0]);
                    }

                }
                if (ds.Tables[1].Rows[i]["VariantName"].ToString() == "Flavour")
                {
                    divflavor.Visible = true;
                    DataSet ds1 = obj.FlavorList();
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        Common.BindDropDown(ddlflavour, "PK_FlavorID", "FlavorName", ds1.Tables[0]);
                    }

                }
                if (ds.Tables[1].Rows[i]["VariantName"].ToString() == "Material")
                {
                    divmaterial.Visible = true;
                    DataSet ds1 = obj.MaterialList();
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        Common.BindDropDown(ddlmaterial, "PK_MaterialID", "MaterialName", ds1.Tables[0]);
                    }

                }
                if (ds.Tables[1].Rows[i]["VariantName"].ToString() == "RAM")
                {
                    divram.Visible = true;
                    DataSet ds1 = obj.RAMList();
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        Common.BindDropDown(ddlram, "PK_RAM_ID", "RAM", ds1.Tables[0]);
                    }

                }
                if (ds.Tables[1].Rows[i]["VariantName"].ToString() == "Storage")
                {
                    divstorage.Visible = true;
                    DataSet ds1 = obj.StorageList();
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        Common.BindDropDown(ddlstorage, "PK_StorageID", "Storage", ds1.Tables[0]);
                    }
                }
            }
        }

        protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            obj.UnitID = ddlunit.SelectedValue;
            DataSet ds = obj.SizeList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Common.BindDropDown(ddlsize, "PK_SizeID", "SizeName", ds.Tables[0]);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdvarients.Rows.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "window.alert('Please add Atleast one varient')", true);
                    return;
                }
                obj.BrandID = ddlbrand.SelectedValue;
                obj.CategoryID = ddlcategory.SelectedValue;
                obj.SubCategoryID = ddlsubcategory.SelectedValue;
                obj.MainCategoryID = ddlmaincategory.SelectedValue;
                obj.ProductName = txtproduct.Text;
                obj.Description = txtdescription.Text;
                obj.HSNNo = txthsnno.Text;
                obj.ShortDescription = txtshortdescription.Text;
                obj.IsTimeProduct1 = IsTimeProduct.Checked;
                obj.DeliveryCharge = txtDeliveryCharge.Text;

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
                DataTable dt1 = (DataTable)Session["tmpData"];
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
                if (Session["Pk_AdminId"] != null)
                {
                    obj.AddedBy = Session["Pk_AdminId"].ToString();
                }
                else
                {
                    obj.AddedBy = Session["Pk_VendorId"].ToString();
                }
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

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "window.alert('Product Added Successfully')", true);

                       // ddlcolor.SelectedIndex = 0;
                        ddlmaincategory.SelectedIndex = 0;
                        ddlcategory.SelectedIndex = 0;
                        ddlsubcategory.SelectedIndex = 0;
                        txtproduct.Text = "";
                        txthsnno.Text = "";
                        ddlbrand.SelectedIndex = 0;
                        txtDeliveryCharge.Text = "";
                        txtshortdescription.Text = "";
                        ddlunit.SelectedIndex = 0;
                        ddlsize.SelectedIndex = 0;
                        txtshoppingpointredemperc.Text = "";
                        txtqty.Text = "";
                        txtbv.Text = "";
                        txtmrp.Text = "";
                        txtdealerprice.Text = ""; txtcgst.Text = ""; txtsgst.Text = "";
                        txtigst.Text = "";
                        grdvarients.Visible = false;
                        Session["tmpData"] = null;
                        Session["dtSecImages"] = null;
                        //Common.ClearControls(this.Page.Controls);

                        //if (Session["UserType"].ToString() == "Admin")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage","alert('my message'); window.location='" + Request.ApplicationPath + "/Master/ProductList';", true);
                        //}
                        //else
                        //{
                        //    obj.IsApproved = "0";
                        //}
                    }
                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "window.alert('" + ds.Tables[0].Rows[0]["ErrorMessage"].ToString() + "'')", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Attention", "window.alert('" + ex.Message + "'')", true);
                return;
            }
        }
    }
}