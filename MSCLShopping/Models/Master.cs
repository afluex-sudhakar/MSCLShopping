using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MSCLShopping.Models
{
    public class Master : Common
    {
        public List<Master> lstProductData { get; set; }
        public string ImagePathSecondary { get; set; }
        public string ImagePathPrimary { get; set; }
        public bool IsFile { get; set; }
        public string Image1 { get; set; }
        public DataTable dtProductVariant { get; set; }
        public List<SelectListItem> ddlSizee { get; set; }
        public List<Master> lstProductVariant { get; set; }
        public string SGST1 { get; set; }
        public string CGST1 { get; set; }
        public string DealerPrice1 { get; set; }
        public bool IsTimeProduct1 { get; set; }
        public string OfferedPrice1 { get; set; }
        public string MRP1 { get; set; }
        public string BV1 { get; set; }
        public string ProductQuantity1 { get; set; }
        public string StarRatingIDD { get; set; }
        public string StorageIDD { get; set; }
        public string RamIDD { get; set; }
        public string ColorIDD { get; set; }
        public string MaterialIDD { get; set; }
        public string FlavorIDD { get; set; }
        public string SizeIDD { get; set; }
        public string UnitIDD { get; set; }
        public string IGST1 { get; set; }
        public string ShoppingPerc { get; set; }
        public string DescriptionHTML { get; set; }
        public string RatingID { get; set; }
        public string Comment { get; set; }
        public string IsApproved { get; set; }
        public List<Master> lstVariant { get; set; }
        public List<Master> lstCategory { get; set; }
        public List<Master> lstSizeTemp { get; set; }
        public List<Master> lstSubCategory { get; set; }
        public List<Master> lstBrands { get; set; }
        public List<Master> lstProduct { get; set; }
        public List<SelectListItem> ddlCity { get; set; }
        public List<SelectListItem> ddlCategory { get; set; }
        public List<SelectListItem> ddlSubCategory { get; set; }
        public List<SelectListItem> ddlSize { get; set; }
        public List<SelectListItem> ddlProduct { get; set; }
        public string DiscountPercent { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandID { get; set; }
        public string BrandName { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string SizeName { get; set; }
        public string SizeID { get; set; }
        public string ProductQuantity { get; set; }
        public string HSNNo { get; set; }
        public string BV { get; set; }
        public string MRP { get; set; }
        public string OfferedPrice { get; set; }
        public string DealerPrice { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string OfferID { get; set; }
        public string hdOfferID { get; set; }
        public string OfferName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DisplayOrder { get; set; }
        public string OfferStatus { get; set; }
        public string OfferImage { get; set; }
        public string OfferProductID { get; set; }
        public DataTable dtProductQuantity { get; set; }
        public string IsNew { get; set; }
        public string IsBestSeller { get; set; }
        public string IsCODAvailable { get; set; }
        public string IsShippingFree { get; set; }
        public string IsFeatured { get; set; }
        public string IsProductavailable { get; set; }
        public string PrimaryImage { get; set; }
        public string SecondaryImage { get; set; }
        public DataTable dtProductImages { get; set; }
        public string MainCategoryID { get; set; }
        public string MainCategoryName { get; set; }
        public string ShortDescription { get; set; }
        public string Tags { get; set; }
        public string DebQuantity { get; set; }
        public string ColorID { get; set; }
        public string ColorName { get; set; }
        public string VariantID { get; set; }
        public string VariantName { get; set; }
        public string FlavorID { get; set; }
        public string FlavorName { get; set; }
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string VariantMapping { get; set; }
        public List<Master> lstVarient { get; set; }
        public string Images { get; set; }
        public string DiscountID { get; set; }
        public string Status { get; set; }
        public string CouponCode { get; set; }
        public string DiscountAmount { get; set; }
        public string Quantity { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string BrandImage { get; set; }
        public string Discforcustid { get; set; }
        public string BannerImage { get; set; }
        public string BannerName { get; set; }
        public string BannerDescription { get; set; }
        public string BannerID { get; set; }
        public string ProductInfoCode { get; set; }
        public string ProductOtherInfoID { get; set; }
        public string RamID { get; set; }
        public string RAM { get; set; }
        public string StarRatingID { get; set; }
        public string StarRating { get; set; }
        public string StorageID { get; set; }
        public string Storage { get; set; }
        public string ProductImageID { get; set; }
        public string OrderNumber { get; set; }
        public string OrderID { get; set; }
        public string OrderDate { get; set; }
        public string VariantItemID { get; set; }
        public string VariantItemName { get; set; }
        public string ColorCode { get; set; }
        public string CourrierID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string CourrierDate { get; set; }
        public string Amount { get; set; }
        public string PayMode { get; set; }
        public string TrackingNo { get; set; }
        public string VendorID { get; set; }
        public string BalanceQuantity { get; set; }
        public string RequestID { get; set; }
        public string Commission { get; set; }
        public string ReferralCharge { get; set; }
        public string NoOfVoucher { get; set; }
        public string LoginID { get; set; }
        public string GiftVoucherID { get; set; }

        public List<Master> lstRam { get; set; }
        public List<Master> lstStorage { get; set; }
        public string FeatureTypeId { get; set; }
        public string FeatureTypeName { get; set; }

        #region MainCategory

        public DataSet MainCategoryList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_MainCategoryID", MainCategoryID),
                                      new SqlParameter("@MainCategoryName", MainCategoryName) };
            DataSet ds = Connection.ExecuteQuery("MainCategoryList", para);
            return ds;
        }
        public DataSet SaveMainCategory()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@MainCategoryName", MainCategoryName),
                                  new SqlParameter("@AddedBy", AddedBy),
             new SqlParameter("@Images", Images),};
            DataSet ds = Connection.ExecuteQuery("AddMainCategory", para);
            return ds;
        }
        public DataSet UpdateMainCategory()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@PK_MainCategoryID", MainCategoryID),
                                      new SqlParameter("@MainCategoryName", MainCategoryName),
                                  new SqlParameter("@UpdatedBy", UpdatedBy),
               new SqlParameter("@Images", Images),};
            DataSet ds = Connection.ExecuteQuery("UpdateMainCategory", para);
            return ds;
        }
        public DataSet DeleteMainCategory()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@PK_MainCategoryID", MainCategoryID),
                                  new SqlParameter("@DeletedBy", AddedBy),


                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteMainCategory", para);
            return ds;
        }

        #endregion

        #region Category

        public DataSet SaveCategory()
        {
            SqlParameter[] para = { new SqlParameter("@CategoryName", CategoryName),
                                      new SqlParameter("@FK_MainCategory", MainCategoryID),
                                      new SqlParameter("@AddedBy", AddedBy),
                                       new SqlParameter("@dtCategoryVariantMapping", dtProductQuantity)
                                        
                                  };
            DataSet ds = Connection.ExecuteQuery("AddCategory", para);
            return ds;
        }

        public DataSet CategoryList()
        {
            SqlParameter[] para = {

                  new SqlParameter("@PK_CategoryID", CategoryID),
                new SqlParameter("@FK_MainCategory", MainCategoryID),
                                      new SqlParameter("@CategoryName", CategoryName),
                                     
                                       };

            DataSet ds = Connection.ExecuteQuery("CategoryList", para);
            return ds;
        }

        public DataSet UpdateCategory()
        {
            SqlParameter[] para = { new SqlParameter("@CategoryID", CategoryID),
                                      new SqlParameter("@CategoryName", CategoryName),
                                         new SqlParameter("@FK_MainCategory", MainCategoryID),
                                  new SqlParameter("@UpdatedBy", AddedBy),
                                   new SqlParameter("@dtCategoryVariantMapping", dtProductQuantity),
                                   new SqlParameter("Images",Images)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateCategory", para);
            return ds;
        }

        public DataSet DeleteCategory()
        {
            SqlParameter[] para = { new SqlParameter("@CategoryID", CategoryID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteCategory", para);
            return ds;
        }

        #endregion

        #region SubCategory

        public DataSet SaveSubCategory()
        {
            SqlParameter[] para = { new SqlParameter("@SubCategoryName", SubCategoryName),
                                      new SqlParameter("@FK_CategoryID", CategoryID),
                                      new SqlParameter("@AddedBy", AddedBy),
                                       new SqlParameter("@FK_MainCategory", MainCategoryID),
                                         new SqlParameter("@Images", Images)

                                  };
            DataSet ds = Connection.ExecuteQuery("AddSubCategory", para);
            return ds;
        }

        public DataSet SubCategoryList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SubCategoryName", SubCategoryName),
                                      new SqlParameter("@FKCategoryID", CategoryID) };

            DataSet ds = Connection.ExecuteQuery("SubCategoryList", para);
            return ds;
        }

        public DataSet UpdateSubCategory()
        {
            SqlParameter[] para = {   new SqlParameter("@SubCategoryID", SubCategoryID),
                                      new SqlParameter("@SubCategoryName", SubCategoryName),
                                      new SqlParameter("@UpdatedBy", UpdatedBy) ,
                                   new SqlParameter("@FK_CategoryID", CategoryID),
                                     new SqlParameter("@FK_MainCategory", MainCategoryID) ,
                                   new SqlParameter("@Images", Images),

                                  };

            DataSet ds = Connection.ExecuteQuery("UpdateSubCategory", para);
            return ds;
        }

        public DataSet DeleteSubCategory()
        {
            SqlParameter[] para = { new SqlParameter("@SubCategoryID", SubCategoryID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteSubCategory", para);
            return ds;
        }

        #endregion

        #region Brands

        public DataSet SaveBrand()
        {
            SqlParameter[] para = { new SqlParameter("@BrandName", BrandName),
                                      new SqlParameter("@AddedBy", AddedBy),
            new SqlParameter("@BrandImage", BrandImage) };
            DataSet ds = Connection.ExecuteQuery("AddBrand", para);
            return ds;
        }

        public DataSet BrandList()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_VendorId", VendorID) };
            DataSet ds = Connection.ExecuteQuery("BrandList", para);
            return ds;
        }

        public DataSet UpdateBrand()
        {
            SqlParameter[] para = { new SqlParameter("@BrandID", BrandID),
                                      new SqlParameter("@BrandName", BrandName),
                                      new SqlParameter("@UpdatedBy", UpdatedBy),
                                       new SqlParameter("@BrandImage", BrandImage),
            };
            DataSet ds = Connection.ExecuteQuery("UpdateBrand", para);
            return ds;
        }

        public DataSet DeleteBrand()
        {
            SqlParameter[] para = { new SqlParameter("@BrandID", BrandID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteBrand", para);
            return ds;
        }

        #endregion

        #region Product
        public DataSet ProductList()
        {

            SqlParameter[] para = { new SqlParameter("@SubCategoryID", SubCategoryID),
                                    new SqlParameter("@FKCategoryID", CategoryID),
                                   new SqlParameter("@ProductName", ProductName),
                                   new SqlParameter("@FK_MainCategoryID", MainCategoryID),
                                    new SqlParameter("@BrandID", BrandID),
                                      new SqlParameter("@AddedBy", AddedBy),
                                  };

            DataSet ds = Connection.ExecuteQuery("ProductList", para);
            return ds;
        }
        public DataSet SaveProduct()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_BrandID", BrandID),
                                    new SqlParameter("@FK_CategoryID", CategoryID),
                                    new SqlParameter("@FK_SubCategoryID", SubCategoryID),
                                    new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@Description", Description),

                                    new SqlParameter("@HSNNo", HSNNo),
                                    new SqlParameter("@IsNew",IsNew ),
                                    new SqlParameter("@IsBestSeller", IsBestSeller),
                                    new SqlParameter("@IsCODAvailable", IsCODAvailable),
                                    new SqlParameter("@IsShippingFree",IsShippingFree),
                                    new SqlParameter("@IsFeatured",IsFeatured),
                                    new SqlParameter("@IsProductavailable", IsProductavailable),
                                    new SqlParameter("@dtProductImages", dtProductImages),
                                    new SqlParameter("@ProdQuantityList", dtProductQuantity),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@FK_MainCategory", MainCategoryID),
                                    new SqlParameter("@ShortDescription", ShortDescription),
                                    new SqlParameter("@Tags", Tags),
                                     new SqlParameter("@IsApproved", IsApproved),
                                     new SqlParameter("@DeliveryCharge", DeliveryCharge),
                                       //new SqlParameter("@IsTimeProduct", IsTimeProduct1),
                                  };
            DataSet ds = Connection.ExecuteQuery("AddProduct", para);
            return ds;
        }

        public DataSet DeleteProduct()
        {

            SqlParameter[] para = { new SqlParameter("@ProductID", ProductID),
                                    new SqlParameter("@DeletedBy", AddedBy),

                                  };

            DataSet ds = Connection.ExecuteQuery("DeleteProduct", para);
            return ds;
        }
        public DataSet GetVariantFromCategory()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CategoryID", CategoryID), };
            DataSet ds = Connection.ExecuteQuery("GetVariantFromCategory", para);
            return ds;
        }

        #endregion

        #region Unit
        public DataSet UnitList()
        {
            SqlParameter[] para = { new SqlParameter("@UnitName", UnitName),
                                      };
            DataSet ds = Connection.ExecuteQuery("UnitList", para);
            return ds;
        }

        public DataSet SaveUnit()
        {
            SqlParameter[] para = { new SqlParameter("@UnitName", UnitName),
                                      new SqlParameter("@AddedBy", AddedBy)
                                      };
            DataSet ds = Connection.ExecuteQuery("AddUnit", para);
            return ds;
        }
        public DataSet UpdateUnit()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_UnitID", UnitID),
                                      new SqlParameter("@UnitName", UnitName),
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                      };
            DataSet ds = Connection.ExecuteQuery("UpdateUnit", para);
            return ds;
        }
        public DataSet DeleteUnit()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_UnitID", UnitID),
                                      new SqlParameter("@DeletedBy", AddedBy)
                                      };
            DataSet ds = Connection.ExecuteQuery("DeleteUnit", para);
            return ds;
        }

        #endregion

        #region Size

        public DataSet SizeList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SizeName", SizeName),
                                       new SqlParameter("@FK_UnitID", UnitID)
                                      };
            DataSet ds = Connection.ExecuteQuery("SizeList", para);
            return ds;
        }

        public DataSet SizeList1()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SizeName", SizeName),
                                       new SqlParameter("@FK_UnitID", UnitIDD)
                                      };
            DataSet ds = Connection.ExecuteQuery("SizeList", para);
            return ds;
        }
        public DataSet SaveSize()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UnitID", UnitID),
                                         new SqlParameter("@SizeName", SizeName),
                                            new SqlParameter("@AddedBy", AddedBy)

                                      };
            DataSet ds = Connection.ExecuteQuery("AddSize", para);
            return ds;
        }
        public DataSet UpdateSize()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_SizeID", SizeID),
                                         new SqlParameter("@SizeName", SizeName),
                                            new SqlParameter("@UpdatedBy", UpdatedBy),
                                       new SqlParameter("@FK_UnitID", UnitID)
                                      };
            DataSet ds = Connection.ExecuteQuery("UpdateSize", para);
            return ds;
        }
        public DataSet DeleteSize()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_SizeID", SizeID),

                                            new SqlParameter("@DeletedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("DeleteSize", para);
            return ds;
        }

        #endregion

        #region Offer

        public DataSet OfferList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OfferName", OfferName),
                                      new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate)
                                      };
            DataSet ds = Connection.ExecuteQuery("OfferList", para);
            return ds;
        }

        public DataSet SaveOffer()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OfferName", OfferName),
                                      new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate),
                                       new SqlParameter("@DisplayOrder", DisplayOrder),
                                       new SqlParameter("@OfferStatus", OfferStatus),
                                       new SqlParameter("@OfferImage", OfferImage),
                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@DiscountPercentage", DiscountPercent),
                                      };
            DataSet ds = Connection.ExecuteQuery("AddOffer", para);
            return ds;
        }
        public DataSet UpdateOffer()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@PK_OfferID", OfferID),
                                      new SqlParameter("@OfferName", OfferName),
                                      new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate),
                                       new SqlParameter("@DisplayOrder", DisplayOrder),
                                       new SqlParameter("@OfferStatus", OfferStatus),
                                       new SqlParameter("@OfferImage", OfferImage),
                                       new SqlParameter("@UpdatedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("UpdateOffer", para);
            return ds;
        }
        public DataSet DeleteOffer()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@PK_OfferID", OfferID),
                                       new SqlParameter("@DeletedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("DeleteOffer", para);
            return ds;
        }

        #endregion

        #region OfferProduct
        public DataSet OfferProductList()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FK_OfferID", OfferID),
                                   new SqlParameter("@FK_ProductID", ProductID),
                                  new SqlParameter("@FK_MainCategoryID", MainCategoryID),
                                  new SqlParameter("@FK_CategoryID", CategoryID),
                                   new SqlParameter("@FK_SubCategoryID", SubCategoryID),

                                      };
            DataSet ds = Connection.ExecuteQuery("OfferProductList", para);
            return ds;
        }
        public DataSet AddOfferToProduct()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FK_OfferID",hdOfferID),
                                    new SqlParameter("@dtOfferToProduct", dtProductQuantity),
                                    new SqlParameter("@AddedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("AddOfferToProduct", para);
            return ds;
        }
        public DataSet UpdateOfferToProduct()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FK_OfferID", OfferID),
                                   new SqlParameter("@FK_ProductID", ProductID),
                                  new SqlParameter("@FK_MainCategoryID", MainCategoryID),
                                  new SqlParameter("@FK_CategoryID", CategoryID),
                                   new SqlParameter("@FK_SubCategoryID", SubCategoryID),

                                      };
            DataSet ds = Connection.ExecuteQuery("", para);
            return ds;
        }
        public DataSet DeleteOfferProduct()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@PK_OfferProductID", OfferProductID),
                                   new SqlParameter("@DeletedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("DeleteOfferProduct", para);
            return ds;
        }

        #endregion

        #region ManualStock

        public DataSet GetList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_MainCategory", MainCategoryID),
                                    new SqlParameter("@FKCategoryID", CategoryID),
                                    new SqlParameter("@FKSubCategoryID", SubCategoryID),
                                    new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@BrandID", BrandID),
                                    new SqlParameter("@AddedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("SearchProduct", para);
            return ds;
        }


        public DataSet GetListNewArrivals()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_MainCategory", MainCategoryID),
                                    new SqlParameter("@FKCategoryID", CategoryID),
                                    new SqlParameter("@FKSubCategoryID", SubCategoryID),
                                    new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@BrandID", @BrandID),

                                      };
            DataSet ds = Connection.ExecuteQuery("SearchProductForNewArrival", para);
            return ds;
        }
        public DataSet GetListForFeatured()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_MainCategory", MainCategoryID),
                                    new SqlParameter("@FKCategoryID", CategoryID),
                                    new SqlParameter("@FKSubCategoryID", SubCategoryID),
                                    new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@BrandID", @BrandID),

                                      };
            DataSet ds = Connection.ExecuteQuery("SearchProductForFeatured", para);
            return ds;
        }
        public DataSet GetListForOffer()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_MainCategory", MainCategoryID),
                                    new SqlParameter("@FKCategoryID", CategoryID),
                                    new SqlParameter("@FKSubCategoryID", SubCategoryID),
                                    new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@BrandID", @BrandID),

                                      };
            DataSet ds = Connection.ExecuteQuery("SearchProductForOffer", para);
            return ds;
        }

        public DataSet AddQty()
        {
            SqlParameter[] para = {

                                    new SqlParameter("@dtManualStockEntry", dtProductQuantity),
                                    new SqlParameter("@AddedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("AddManualQuantity", para);
            return ds;
        }
        public DataSet DeductQty()
        {
            SqlParameter[] para = {

                                    new SqlParameter("@dtManualStockEntry", dtProductQuantity),
                                    new SqlParameter("@AddedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("DeductManualQuantity", para);
            return ds;
        }


        #endregion

        #region Color

        public DataSet ColorList()
        {
            DataSet ds = Connection.ExecuteQuery("ColorList");
            return ds;
        }
        public DataSet SaveColor()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@ColorName", ColorName),
                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@ColorCode", ColorCode),

                                      };
            DataSet ds = Connection.ExecuteQuery("AddColor", para);
            return ds;
        }
        public DataSet UpdateColor()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_ColorID", ColorID),
                                    new SqlParameter("@ColorName", ColorName),
                                    new SqlParameter("@UpdatedBy", UpdatedBy),
                                    new SqlParameter("@ColorCode", ColorCode),

                                      };
            DataSet ds = Connection.ExecuteQuery("UpdateColor", para);
            return ds;
        }
        public DataSet DeleteColor()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@ColorID", ColorID),

                                    new SqlParameter("@DeletedBy", AddedBy),


                                      };
            DataSet ds = Connection.ExecuteQuery("DeleteColor", para);
            return ds;
        }

        #endregion

        #region Variant/Flavor/Material
        public DataSet VariantList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_VariantID", VariantID), };
            DataSet ds = Connection.ExecuteQuery("VariantList", para);
            return ds;
        }

        public DataSet FlavorList()
        {

            DataSet ds = Connection.ExecuteQuery("FlavorList");
            return ds;
        }

        public DataSet MaterialList()
        {

            DataSet ds = Connection.ExecuteQuery("MaterialList");
            return ds;
        }
        #endregion

        #region Flavor
        public DataSet SaveFlavor()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FlavorName", FlavorName),
                                    new SqlParameter("@AddedBy", AddedBy),


                                      };
            DataSet ds = Connection.ExecuteQuery("AddFlavor", para);
            return ds;
        }
        public DataSet UpdateFlavor()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_FlavorID", FlavorID),
                                    new SqlParameter("@FlavorName", FlavorName),
                                    new SqlParameter("@UpdatedBy", AddedBy),


                                      };
            DataSet ds = Connection.ExecuteQuery("UpdateFlavor", para);
            return ds;
        }
        public DataSet DeleteFlavor()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FlavorID", FlavorID),
                                    new SqlParameter("@DeletedBy", AddedBy),

                                      };
            DataSet ds = Connection.ExecuteQuery("DeleteFlavor", para);
            return ds;
        }

        #endregion

        #region Material
        public DataSet SaveMaterial()
        {
            SqlParameter[] para = {

                                    new SqlParameter("@MaterialName", MaterialName),
                                    new SqlParameter("@AddedBy", AddedBy),


                                      };
            DataSet ds = Connection.ExecuteQuery("AddMaterial", para);
            return ds;

        }
        public DataSet UpdateMaterial()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_MaterialID", MaterialID),
                                    new SqlParameter("@MaterialName", MaterialName),
                                    new SqlParameter("@UpdatedBy", AddedBy),


                                      };
            DataSet ds = Connection.ExecuteQuery("UpdateMaterial", para);
            return ds;

        }
        public DataSet DeleteMaterial()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@MaterialID", MaterialID),
                                    new SqlParameter("@DeletedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("DeleteMaterial", para);
            return ds;
        }


        #endregion

        #region ProductAvailibility

        public DataSet VariantByProductList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_ProductID", ProductID),

                                      };
            DataSet ds = Connection.ExecuteQuery("GetVariantByProduct", para);
            return ds;
        }

        public DataSet StateList()
        {

            DataSet ds = Connection.ExecuteQuery("StateList");
            return ds;
        }
        public DataSet CityList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_StateID", State),

                                      };
            DataSet ds = Connection.ExecuteQuery("CityList", para);
            return ds;
        }
        public DataSet GetPin()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_StateID", State),
                                    new SqlParameter("@FK_CityID", City),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPin", para);
            return ds;
        }
        #endregion

        #region Discount 

        public DataSet SaveDiscount()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@CouponCode", CouponCode),
                                    new SqlParameter("@DiscountAmount", DiscountAmount),
                                    new SqlParameter("@ValidFrom", FromDate),
                                    new SqlParameter("@ValidTo", ToDate),
                                    new SqlParameter("@AddedBy", AddedBy),    };
            DataSet ds = Connection.ExecuteQuery("AddDiscount", para);
            return ds;
        }
        public DataSet DiscountList()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@CouponCode", CouponCode),
                                    new SqlParameter("@ValidFrom", FromDate),
                                    new SqlParameter("@ValidTo", ToDate),

            };
            DataSet ds = Connection.ExecuteQuery("DiscountList", para);
            return ds;
        }
        public DataSet UpdateDiscount()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_DiscountMasterID", DiscountID),
                                    new SqlParameter("@CouponCode", CouponCode),
                                    new SqlParameter("@DiscountAmount", DiscountAmount),
                                    new SqlParameter("@ValidFrom", FromDate),
                                    new SqlParameter("@ValidTo", ToDate),
                                    new SqlParameter("@UpdatedBy", AddedBy),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateDiscount", para);
            return ds;
        }
        public DataSet DeleteDiscount()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_DiscountMasterID", DiscountID),
                                    new SqlParameter("@DeletedBy", AddedBy),

            };
            DataSet ds = Connection.ExecuteQuery("DeleteDiscount", para);
            return ds;
        }


        #endregion

        #region DiscountForCustomer
        public DataSet CustomerList()
        {
            DataSet ds = Connection.ExecuteQuery("CustomerList");
            return ds;
        }
        public DataSet AddDiscountToCustomer()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@CouponCode", CouponCode),
                                    new SqlParameter("@DiscountAmount", DiscountAmount),

                                    new SqlParameter("@AddedBy", AddedBy),
                                    new SqlParameter("@dtDiscountForCustomers", dtProductQuantity),
            };
            DataSet ds = Connection.ExecuteQuery("AddDiscountForCustomers", para);
            return ds;
        }
        public DataSet DiscountForCustomersList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@CouponCode", CouponCode),
                                    new SqlParameter("@CustomerName", CustomerName),
                                    new SqlParameter("@ValidFrom", FromDate),
                                    new SqlParameter("@ValidTo", ToDate),


            };
            DataSet ds = Connection.ExecuteQuery("DiscountForCustomerList", para);
            return ds;
        }
        public DataSet DeleteDiscountforCustomer()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_DiscForCustID", Discforcustid),
                                    new SqlParameter("@DeletedBy", AddedBy),



            };
            DataSet ds = Connection.ExecuteQuery("DeleteDiscountforCustomer", para);
            return ds;
        }

        #endregion

        #region Banner 
        public DataSet SaveBanner()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@BannerName", @BannerName),
                                    new SqlParameter("@Description", BannerDescription),
                                    new SqlParameter("@BannerImage", BannerImage),

                                    new SqlParameter("@AddedBy", AddedBy),    };
            DataSet ds = Connection.ExecuteQuery("AddBanner", para);
            return ds;

        }
        public DataSet BannerList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@BannerName", BannerName),
                                       };
            DataSet ds = Connection.ExecuteQuery("BannerList", para);
            return ds;

        }
        public DataSet DeleteBanner()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_BannerId", BannerID),
                                    new SqlParameter("@DeletedBy", AddedBy),
                                       };
            DataSet ds = Connection.ExecuteQuery("DeleteBanner", para);
            return ds;

        }

        #endregion
         
        #region FeaturedProduct
        public DataSet AddFeaturedProduct()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@dtOfferToProduct", dtProductQuantity),
                                    new SqlParameter("@AddedBy", AddedBy),
                                       };
            DataSet ds = Connection.ExecuteQuery("AddFeaturedProduct", para);
            return ds;

        }
        public DataSet FeaturedProductList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_ProductID", ProductID),
                                    new SqlParameter("@FK_MainCategoryID", MainCategoryID),
                                      new SqlParameter("@FK_CategoryID", CategoryID),
                                    new SqlParameter("@FK_SubCategoryID", SubCategoryID),
                                       };
            DataSet ds = Connection.ExecuteQuery("FeaturedProductList", para);
            return ds;

        }
        public DataSet DeleteFeaturedProduct()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@ID", OfferProductID),
                                    new SqlParameter("@DeletedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("DeleteFeaturedProduct", para);
            return ds;

        }

        #endregion

        #region NewArrivals

        public DataSet AddNewArrivals()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@dtOfferToProduct", dtProductQuantity),
                                    new SqlParameter("@AddedBy", AddedBy),
                                       new SqlParameter("@FeatureType", FeatureTypeName),
                                       };
            DataSet ds = Connection.ExecuteQuery("AddNewArrivals", para);
            return ds;

        }
        public DataSet NewArrivalsList()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FK_ProductID", ProductID),
                                    new SqlParameter("@FK_MainCategoryID", MainCategoryID),
                                      new SqlParameter("@FK_CategoryID", CategoryID),
                                    new SqlParameter("@FK_SubCategoryID", SubCategoryID),
                                       };
            DataSet ds = Connection.ExecuteQuery("NewArrivalsList", para);
            return ds;

        }
        public DataSet DeleteNewArrivals()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@ID", OfferProductID),
                                    new SqlParameter("@DeletedBy", AddedBy),
                                       };
            DataSet ds = Connection.ExecuteQuery("DeleteNewArrivals", para);
            return ds;

        }

        #endregion

        #region Product Variants 

        public DataSet ProductDetails()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_ProductID", ProductID),
                                  //  new SqlParameter("@ColorID", ColorID),
                                    new SqlParameter("@FlavorID", FlavorID),
                                    new SqlParameter("@MaterialID", MaterialID),
                                   // new SqlParameter("@SizeID", SizeID),
                                    new SqlParameter("@ProductInfoCode", ProductInfoCode),
                                       };
            DataSet ds = Connection.ExecuteQuery("GetProductDetails", para);
            return ds;

        }

        public DataSet RAMList()
        {

            SqlParameter[] para = {
            new SqlParameter("@RAM", RAM),
                new SqlParameter("@PK_RamID", RamID) };
            DataSet ds = Connection.ExecuteQuery("RAMList", para);
            return ds;

        }
        public DataSet StorageList()
        {
            SqlParameter[] para = {
            new SqlParameter("@Storage", Storage),
                new SqlParameter("@PK_StorageID", StorageID) };
            DataSet ds = Connection.ExecuteQuery("StorageList", para);
            return ds;

        }

        public DataSet StarRatingList()
        {

            DataSet ds = Connection.ExecuteQuery("StarRatingList");
            return ds;

        }
        public DataSet DeleteProductImage()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_ProductImagesID", ProductImageID),
                                    new SqlParameter("@DeletedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("DeleteProductImageForUpdate", para);
            return ds;

        }

        public DataSet UpdateProduct()
        {
            SqlParameter[] para = {

                                  new SqlParameter("@PK_ProductOtherInfoID", ProductOtherInfoID),
                                    new SqlParameter("@FK_ProductID", ProductID),
                                    new SqlParameter("@ProductName", ProductName),
                                    new SqlParameter("@Description", Description),
                                    new SqlParameter("@BV", BV),
                                    new SqlParameter("@MRP",MRP),
                                    new SqlParameter("@OfferedPrice", OfferedPrice),
                                    new SqlParameter("@DealerPrice", DealerPrice),
                                    new SqlParameter("@CGST", CGST),
                                    new SqlParameter("@SGST", SGST),
                                    new SqlParameter("@IGST", IGST),
                                  new SqlParameter("@FK_SizeID", SizeID),
                                  new SqlParameter("@FK_UnitID", UnitID),
                                  new SqlParameter("@ColorID", ColorID),
                                  new SqlParameter("@MaterialID", MaterialID),
                                  new SqlParameter("@FlavorID ", FlavorID),
                                  new SqlParameter("@CreditQuantity", ProductQuantity),
                                  new SqlParameter("@DebitQuantity", Quantity),
                                  new SqlParameter("@UpdatedBy", AddedBy),
                                  new SqlParameter("@ShortDescription", ShortDescription),
                                  new SqlParameter("@Tags", Tags),
                                  new SqlParameter("@FK_RAM_ID", RamID),
                                  new SqlParameter("@FK_StorageID", StorageID),
                                  new SqlParameter("@FK_StarRatingID", StarRatingID),
                                  new SqlParameter("@ProductInfoCode", ProductInfoCode),
								  new SqlParameter("@dtProductImages", dtProductImages),
                                  new SqlParameter("@DeliveryCharge", DeliveryCharge),
                                  new SqlParameter("@ShoopingPerc", ShoppingPerc),
                                  new SqlParameter("@dtProductVariant", dtProductVariant)
                                    // new SqlParameter("@IsTimeProduct", IsTimeProduct1),
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateProduct", para);
            return ds;
        }


        public DataSet ListProduct()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_ProductID", ProductID),
                                    new SqlParameter("@FK_MainCategory", MainCategoryID),
                                    new SqlParameter("@FK_CategoryID", CategoryID),
                                    new SqlParameter("@FK_SubCategoryID", SubCategoryID),
                                       new SqlParameter("@AddedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("ProductDetailsForVariantAddition", para);
            return ds;

        }
        public DataSet AddVariant()
        {
            SqlParameter[] para = {
                              new SqlParameter("@ProductID", ProductID),
                              new SqlParameter("@BV", BV),
                              new SqlParameter("@MRP", MRP),
                              new SqlParameter("@OfferedPrice", DealerPrice),
                              new SqlParameter("@DealerPrice", DealerPrice),
                              new SqlParameter("@CGST", CGST),
                              new SqlParameter("@SGST", SGST),
                              new SqlParameter("@IGST", IGST),
                              new SqlParameter("@ProdQuantityList", dtProductQuantity),
                              new SqlParameter("@FK_SizeID", SizeID),
                              new SqlParameter("@FK_UnitID", UnitID),
                              new SqlParameter("@CreditQuantity", ProductQuantity),

                              new SqlParameter("@dtProductImages", dtProductImages),
                              new SqlParameter("@AddedBy", AddedBy),
                              new SqlParameter("@ProductInfoCode", ProductInfoCode),

                                       };
            DataSet ds = Connection.ExecuteQuery("AddVariantToProduct", para);
            return ds;

        }

        #endregion

        #region Variants
        public DataSet SaveVariant()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@VariantName", VariantName),
                                    new SqlParameter("@AddedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("AddVariant", para);
            return ds;

        }
        public DataSet UpdateVariant()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@PK_VariantID", VariantID),
                                    new SqlParameter("@VariantName", VariantName),
                                    new SqlParameter("@UpdatedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("UpdateVariant", para);
            return ds;

        }
        public DataSet DeleteVariant()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@VariantID  ", VariantID),

                                    new SqlParameter("@DeletedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("DeleteVariant", para);
            return ds;

        }

        #endregion

        #region VariantItem
        public DataSet VariantItemList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@VariantID", VariantID),
                                      new SqlParameter("@PK_VariantItemID", VariantItemID),

                                       };
            DataSet ds = Connection.ExecuteQuery("VariantItemList", para);
            return ds;

        }
        public DataSet SaveVariantItem()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@FK_VariantID", VariantID),
                                     new SqlParameter("@ItemName", VariantItemName),
                                      new SqlParameter("@ColorCode", ColorCode),
                                       new SqlParameter("@FK_UnitID", UnitID),
                                           new SqlParameter("@AddedBy", AddedBy),
                                       };
            DataSet ds = Connection.ExecuteQuery("AddVariantItem", para);
            return ds;

        }
        public DataSet UpdateVariantItem()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_VariantItemID", VariantItemID),
                                     new SqlParameter("@FK_VariantID", VariantID),
                                      new SqlParameter("@ItemName", VariantItemName),
                                       new SqlParameter("@ColorCode", ColorCode),
                                          new SqlParameter("@FK_UnitID", UnitID),
                                           new SqlParameter("@UpdatedBy", AddedBy),
                                       };
            DataSet ds = Connection.ExecuteQuery("UpdateVariantItem", para);
            return ds;

        }
        public DataSet DeleteVariantItem()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_VariantItemID", VariantItemID),
                                     new SqlParameter("@DeletedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("DeleteVariantItem", para);
            return ds;

        }


        #endregion

        public DataSet VendorAddedProductListBy()
        {
            DataSet ds = Connection.ExecuteQuery("VendorProductList");
            return ds;

        }
        public DataSet ApproveProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductOtherInfoID", ProductOtherInfoID),
                                    new SqlParameter("@ProductID", ProductID),
                                    new SqlParameter("@ShoppingPerc", ShoppingPerc),
                                    new SqlParameter("@Commission", Commission), 
                                    new SqlParameter("@ShippingCharge", DeliveryCharge),
                                    new SqlParameter("@UpdatedBy", AddedBy), };
            DataSet ds = Connection.ExecuteQuery("AprroveVendorProduct", para);
            return ds;

        }
        public DataSet CourrierList()
        {

            SqlParameter[] para = {
                                    new SqlParameter("@Name", Name),
                                      new SqlParameter("@PK_CourrierID", CourrierID),

                                       };
            DataSet ds = Connection.ExecuteQuery("CourrierList", para);
            return ds;

        }
        public DataSet SaveCourrierRegistration()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@Address", Address),
                                      new SqlParameter("@Pincode", Pincode),
                                    new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                          new SqlParameter("@Email", Email),
                                      new SqlParameter("@Contact", Contact),
                                    new SqlParameter("@AddedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("CourrierRegistration", para);
            return ds;

        }
        public DataSet OrdersList()
        {


            DataSet ds = Connection.ExecuteQuery("GetOrders");
            return ds;

        }
        public DataSet AssignCourrier()
        {

            SqlParameter[] para = {
                                    new SqlParameter("@OrderDetailsID", OrderID),
                                    new SqlParameter("@CourrierID", CourrierID),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                    new SqlParameter("@CourrierDate", CourrierDate),
                                     new SqlParameter("@TrackingNumber", TrackingNo),
                                       };
            DataSet ds = Connection.ExecuteQuery("AssignCourrier", para);
            return ds;

        }
        public DataSet OrderByCourrierList()
        {

            SqlParameter[] para = {
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@OrderNo", OrderNumber),
                                    new SqlParameter("@CustomerName", CustomerName),
                                    new SqlParameter("@OrderDate", OrderDate),
                                    new SqlParameter("@CourrierDate", CourrierDate),
                                      new SqlParameter("@TrackingNumber", TrackingNo),
                                       };

            DataSet ds = Connection.ExecuteQuery("CourrierOrderList", para);
            return ds;

        }


        public DataSet UpdateCourrier()
        {
            SqlParameter[] para = {
                 new SqlParameter("@PK_CourrierID", CourrierID),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@Address", Address),
                                      new SqlParameter("@Pincode", Pincode),
                                    new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                          new SqlParameter("@Email", Email),
                                      new SqlParameter("@Contact", Contact),
                                    new SqlParameter("@UpdatedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("UpdateCourrier", para);
            return ds;

        }
        public DataSet DeleteCourrier()
        {

            SqlParameter[] para = {
                                    new SqlParameter("@PK_Id", CourrierID),
                                       new SqlParameter("@DeletedBy", AddedBy),

                                       };
            DataSet ds = Connection.ExecuteQuery("DeleteCourrier", para);
            return ds;

        }

        public DataSet VendorList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),

                                  };
            DataSet ds = Connection.ExecuteQuery("VendorList", para);
            return ds;

        }
        public DataSet ProductListAdmin()
        {
            DataSet ds = Connection.ExecuteQuery("AdminProductsNameList");
            return ds;

        }
        public DataSet GetProductWithVariants()
        {
            SqlParameter[] para = {
                 new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@ProductID", ProductID),
                                  };
            DataSet ds = Connection.ExecuteQuery("ProductListWithVariantsByProduct", para);
            return ds;

        }
        public DataSet SaveProductToVendor()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@FK_ProductId", ProductID),
                                      new SqlParameter("@FK_VendorId", VendorID),
                                      new SqlParameter("@ProductinfoCode", ProductInfoCode),
                                       new SqlParameter("@Qty", DebQuantity),
                                      new SqlParameter("@AssignBy", AddedBy),
                                  };
            DataSet ds = Connection.ExecuteQuery("AssignProductToVendor", para);
            return ds;

        }
        public DataSet ProductsAssignedByAdmin()
        {
            SqlParameter[] para = {
                             new SqlParameter("@ProductName", ProductName),
                             new SqlParameter("@VendorID", AddedBy),
                                  };
            DataSet ds = Connection.ExecuteQuery("ProductAssignedByAdminList", para);
            return ds;

        }
        public DataSet ProductsAssignedByAdminLedger()
        {
            SqlParameter[] para = {

                             new SqlParameter("@VendorID", AddedBy),
                              new SqlParameter("@FromDate", FromDate),
                               new SqlParameter("@ToDate", ToDate),
                                  };
            DataSet ds = Connection.ExecuteQuery("ProductsLedgerForVendor", para);
            return ds;

        }
        public DataSet RequestQuantity()
        {
            SqlParameter[] para = {

                             new SqlParameter("@ProductID", ProductID),
                             new SqlParameter("@VendorID", AddedBy),
                             new SqlParameter("@ProductInfoCode", ProductInfoCode),
                             new SqlParameter("@Quantity", DebQuantity),
                                  };
            DataSet ds = Connection.ExecuteQuery("ProductQuantityRequest", para);
            return ds;

        }
        public DataSet VendorProductQuantityRequestsList()
        {
            SqlParameter[] para = {

                             new SqlParameter("@Name", Name),

                                  };
            DataSet ds = Connection.ExecuteQuery("QuantityRequestList", para);
            return ds;

        }
        public DataSet ApproveQty()
        {
            SqlParameter[] para = {


                             new SqlParameter("@VendorID", VendorID),

                                  new SqlParameter("@AddedBy", AddedBy),
                             new SqlParameter("@Quantity", DebQuantity),
                              new SqlParameter("@RequestID", RequestID),

                                  };
            DataSet ds = Connection.ExecuteQuery("AssignReqProductQuantity", para);
            return ds;

        }
        public DataSet ApprovedQuantityRequests()
        {
            SqlParameter[] para = {

                                  new SqlParameter("@FK_VendorId", AddedBy),
                            new SqlParameter("@ProductName", ProductName),
                                  };
            DataSet ds = Connection.ExecuteQuery("QuantityStatusList", para);
            return ds;

        }
        public DataSet ProductRatingList()
        {

            DataSet ds = Connection.ExecuteQuery("ProductRatingList");
            return ds;

        }
        public DataSet DeleteProductRating()
        {
            SqlParameter[] para = {

                                  new SqlParameter("@DeletedBy", AddedBy),
                            new SqlParameter("@PK_Id", RatingID),
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteProductRating", para);
            return ds;

        }
        public DataSet VendorRatingList()
        {

            DataSet ds = Connection.ExecuteQuery("VendorRatingList");
            return ds;

        }
        public DataSet DeleteVendorRating()
        {
            SqlParameter[] para = {

                                  new SqlParameter("@DeletedBy", AddedBy),
                            new SqlParameter("@PK_Id", RatingID),
                                  };
            DataSet ds = Connection.ExecuteQuery("DeleteVendorRating", para);
            return ds;

        }
        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }
        public DataSet SaveGiftVoucher()
        {
            SqlParameter[] para = {

                                  new SqlParameter("@FK_UserID", Fk_UserId),
                            new SqlParameter("@CouponAmount", Amount),

                            new SqlParameter("@AddedBy", AddedBy),
                             new SqlParameter("@NoOfVoucher", NoOfVoucher),
                             new SqlParameter("@PaymentType", PayMode),
                               new SqlParameter("@Description", Description),
                                  };
            DataSet ds = Connection.ExecuteQuery("AddGiftVoucher", para);
            return ds;

        }

        public DataSet GetCustomerWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetCustomerWalletBalance", para);
            return ds;
        }

        #region RAM
        public DataSet Ram()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@RAM",RAM),
                  new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("AddRAM", para);
            return ds;
        }

        public DataSet List()
        {

            SqlParameter[] para =
        {
                new SqlParameter("@RAM",RAM),
                new SqlParameter("@PK_RamID",RamID)
            };
            DataSet ds = Connection.ExecuteQuery("RAMList", para);
            return ds;
        }

        public DataSet UpdateRam()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@RAM",RAM),
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@PK_RAM_ID",RamID)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateRam", para);
            return ds;
        }

        public DataSet DeleteRam()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_RAM_ID",RamID),
                new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteRAM", para);
            return ds;
        }
        #endregion

        public DataSet AddStorage()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@Storage",Storage)
            };
            DataSet ds = Connection.ExecuteQuery("AddStorage", para);
            return ds;
        }



        public DataSet UpdateStorage()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_StorageID",StorageID),
                new SqlParameter("@Storage",Storage),
                new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateStorage", para);
            return ds;
        }

        public DataSet DeleteStorage()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_StorageID",StorageID),
                new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteStorage", para);
            return ds;
        }

        #region FeatureType

        public DataSet FeatureTypeList()
        {
          
            DataSet ds = Connection.ExecuteQuery("GetFeaturedType");
            return ds;
        }
        public DataSet SaveFeatureType()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FeatureTypeName",FeatureTypeName),
                 new SqlParameter("@Images",Images),
                
            };
            DataSet ds = Connection.ExecuteQuery("AddFeatureType", para);
            return ds;
        }
        public DataSet UpdateFeatureType()
        {
            SqlParameter[] para =
            {
                   new SqlParameter("@FeatureTypeId",FeatureTypeId),
                new SqlParameter("@FeatureTypeName",FeatureTypeName),
                 new SqlParameter("@Images",Images),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateFeatureType", para);
            return ds;
        }
        public DataSet DeleteFeatureType()
        {
            SqlParameter[] para =
            {
                   new SqlParameter("@FeatureTypeId",FeatureTypeId),
                
            };
            DataSet ds = Connection.ExecuteQuery("DeleteFeatureType", para);
            return ds;
        }

        public DataSet SaveDashboardBanner()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Images",Images),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("SaveDashboardBanner", para);
            return ds;
        }
        
        #endregion

    }
}

