using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MSCLShopping.Models
{
    public class Customer : Common
    {
        public List<Customer> lstfeatureproduct { get; set; }
        public List<Customer> lstTotalProductItems { get; set; }

        public List<Customer> lstfeaturetypr { get; set; }
        public string MobileNo { get; set; }
        public string MainCategoryImages { get; set; }
        public string TotalProduct { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password1 { get; set; }
        public string RequestID { get; set; }
        public string DayDifference { get; set; }
        public string OriginalDeliveryCharge { get; set; }
        public string Comment { get; set; }
        public string StarRating { get; set; }
        public string VendorName { get; set; }
        public string Fk_vendorId { get; set; }
        public string OfferDiscountPerc { get; set; }
        public string ColorDetails { get; set; }
        public string SizeDetails { get; set; }
        public string FlavourDetails { get; set; }
        public string MaterialDetails { get; set; }
        public string RamDetails { get; set; }
        public string StorageDetails { get; set; }
        public string Id { get; set; }
        public List<Customer> lstrating { get; set; }
        public List<Customer> lstVendors { get; set; }
        public List<Customer> lstcustomer { get; set; }
        public List<Customer> lstVarient { get; set; }
        public List<Customer> lstVarientDetail { get; set; }
        public string VarientName { get; set; }
        public string EncryptKey { get; set; }
        public string CouponCode { get; set; }
        public string CouponID { get; set; }
        public string DiscountAmount { get; set; }
        public string CartID { get; set; }
        public string ProductInfo { get; set; }
        public string CssClass { get; set; }
        public string CustomerWallet { get; set; }
        public string ColorCode { get; set; }
        public string PK_OrderID { get; set; }
        public string OrderDetailsID { get; set; }
        public string OrderTotal { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string OrderItems { get; set; }
        public string DeliveryDate { get; set; }
        public string Signature { get; set; }
        public string TotalAmount { get; set; }
        public string ColorName { get; set; }
        public string FlavorName { get; set; }
        public string MaterialName { get; set; }
        public List<Customer> lstnewarrivals { get; set; }

        public List<Customer> lstseller { get; set; }

        public List<Customer> lstproductimages { get; set; }

        public List<Customer> lstoffer { get; set; }
        public List<Customer> lstOrderNo { get; set; }
        public List<Customer> lstOrders { get; set; }
        public List<Customer> lstBanner { get; set; }
        public List<Customer> lstColorMapped { get; set; }
        public List<Customer> lstSizeMapped { get; set; }
        public List<Customer> lstFlavorMapped { get; set; }
        public List<Customer> lstMaterialMapped { get; set; }
        public List<Customer> lstRam { get; set; }
        public List<Customer> lstStorage { get; set; }
        public List<Customer> lstStarRating { get; set; }
        public List<Customer> lstDashboardBanner { get; set; }
        public List<Customer> lstBrand { get; set; }
        public string CardNo { get; set; }
        public string CardHolder { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        #region Properties
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string HouseNo { get; set; }
        public string Locality { get; set; }
        public string Landmark { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }

        public string SecondaryImage { get; set; }
        public string MainCategoryID { get; set; }
        public string MainCategoryName { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandID { get; set; }
        public string BrandImage { get; set; }
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
        public string Description { get; set; }
        public string OfferID { get; set; }
        public string OfferName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DisplayOrder { get; set; }
        public string OfferStatus { get; set; }
        public string OfferImage { get; set; }
        public string PrimaryImage { get; set; }
        public string SoldOutCss { get; set; }
        public string BrandName { get; set; }
        public string OfferProductID { get; set; }
        public List<Customer> lstProducts { get; set; }
        public List<Customer> lstMainCategory { get; set; }
        public List<Customer> lstCategory { get; set; }
        public List<Customer> lstCart { get; set; }
        public List<Customer> lstSubCategory { get; set; }
        public List<SelectListItem> lstSize { get; set; }
        public List<SelectListItem> lstColor { get; set; }
        public List<SelectListItem> lstMaterial { get; set; }
        public List<SelectListItem> lstFlavor { get; set; }
        public string ShortDescription { get; set; }
        public string AddressType { get; set; }
        public DataTable dtCustomerOrderDetails { get; set; }
        public string PaymentModeID { get; set; }
        public string FK_AddressID { get; set; }
        public List<Customer> lstAddress { get; set; }
        #endregion

        public DataSet CheckStock()
        {
            SqlParameter[] para = { new SqlParameter("@ProductInfoCode", ProductCode),
            new SqlParameter("@FK_VendorID", Fk_vendorId) };
            DataSet ds = Connection.ExecuteQuery("CheckProductStock", para);
            return ds;
        }
        public DataSet TotalProductItems()
        {
            DataSet ds = Connection.ExecuteQuery("TotalProductItems");
            return ds;
        }
        public DataSet CheckProductStock()
        {
            SqlParameter[] para = { new SqlParameter("@ProductInfoCode", ProductInfoCode),
            new SqlParameter("@Quantity", ProductQuantity) ,
             new SqlParameter("@FK_VendorID", Fk_vendorId) };
            DataSet ds = Connection.ExecuteQuery("CheckStock", para);
            return ds;
        }

        #region CustomerHomePage

        public DataSet loadProducts()
        {
            //SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };

            DataSet ds = Connection.ExecuteQuery("ProductList");
            return ds;
        }

        public DataSet loadHeaderMenu()
        {
            //SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };

            DataSet ds = Connection.ExecuteQuery("GetHeader");
            return ds;
        }

        public DataSet BindTrendingProducts()
        {
            DataSet ds = Connection.ExecuteQuery("GetTrendingProducts");
            return ds;
        }
        public DataSet BindFeaturedProducts()
        {

            DataSet ds = Connection.ExecuteQuery("GetFeaturedProducts");
            return ds;
        }
        public DataSet GetNewArrivalProducts()
        {

            DataSet ds = Connection.ExecuteQuery("GetNewArrivalProducts");
            return ds;
        }
        public DataSet GetSellerReview()
        {

            DataSet ds = Connection.ExecuteQuery("GetSellerReview");
            return ds;
        }
        public DataSet OfferProductList()
        {

            DataSet ds = Connection.ExecuteQuery("OfferProductList");
            return ds;
        }
        #endregion

        #region Registration

        public DataSet CustomerRegistration()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerName", CustomerName),
                                      new SqlParameter("@Contact", Contact),
                                      new SqlParameter("@ProfilePic", ProfilePic),
                                      new SqlParameter("@Password", Password),
                                      new SqlParameter("@Email", Email)};
            DataSet ds = Connection.ExecuteQuery("CustomerRegistration", para);
            return ds;
        }

        public DataSet CustomerLogin()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("ValidateCustomerLogin", para);
            return ds;
        }


        #endregion

        #region ProductList

        public DataSet ProductList()
        {
            DataSet ds = Connection.ExecuteQuery("ProductListForCustomerDashboard");
            return ds;
        }

        public DataSet AddToCart()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID),
                                    new SqlParameter("@FK_ProductID", ProductID),
                                    new SqlParameter("@ProductInfoCode", ProductCode),
                                    new SqlParameter("@FK_SizeID", SizeID),
                                    new SqlParameter("@FK_UnitID", UnitID),
                                    new SqlParameter("@Rate", OfferedPrice),
                                    new SqlParameter("@FK_ColorID", ColorID),
                                    new SqlParameter("@FK_FlavorID", FlavorID),
                                    new SqlParameter("@FK_MaterialID", MaterialID),
                                    new SqlParameter("@FK_RamID", RamID),
                                    new SqlParameter("@FK_StorageID", StorageID),
                                    new SqlParameter("@FK_StarRatingID", StarRatingID),
                                    new SqlParameter("@Quantity", ProductQuantity),
                                    new SqlParameter("@FK_VendorID", Fk_vendorId),
                                    new SqlParameter("@DeliveryCharge", DeliveryCharge) };
            DataSet ds = Connection.ExecuteQuery("AddToCart", para);
            return ds;
        }

        public DataSet BindBanner()
        {
            DataSet ds = Connection.ExecuteQuery("BannerList");
            return ds;
        }
        public DataSet BindOffer()
        {
            DataSet ds = Connection.ExecuteQuery("OfferList");
            return ds;
        }
        public DataSet BindBrand()
        {
            DataSet ds = Connection.ExecuteQuery("BrandList");
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
        #endregion

        #region ProductDetailsIndividual

        public DataSet ProductDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_ProductID", ProductID),
                                      new SqlParameter("@FK_SizeID", SizeID),
                                      new SqlParameter("@FK_UnitID", UnitID),
                                      new SqlParameter("@Fk_ColorId", ColorID),
            };
            DataSet ds = Connection.ExecuteQuery("ProductDetails", para);
            return ds;
        }
        public DataSet QuickView()

        {
            SqlParameter[] para = { new SqlParameter("@PK_ProductID", ProductID),
                                      new SqlParameter("@Fk_ColorId", ColorID),
                                       new SqlParameter("@Fk_SizeId", SizeID),
                                       new SqlParameter("@Fk_FlavorID", FlavorID),
                                       new SqlParameter("@FK_MaterialID", MaterialID),
                                       new SqlParameter("@FK_RamID", RamID),
                                       new SqlParameter("@FK_Storage", StorageID),
                                       new SqlParameter("@FK_StarRating", StarRatingID), };
            DataSet ds = Connection.ExecuteQuery("QuickView", para);
            return ds;
        }
        public DataSet QuickViewNew()

        {
            SqlParameter[] para = { new SqlParameter("@FK_ProductID", ProductID),
                                      new SqlParameter("@Fk_ColorId", ColorID),
                                       new SqlParameter("@Fk_SizeId", SizeID),
                                       new SqlParameter("@FK_RamId", RamID),
                                       new SqlParameter("@FK_StorageId", StorageID),
                                       new SqlParameter("@FK_FlavourId", FlavorID),
                                       new SqlParameter("@Fk_MaterialId", MaterialID),
            new SqlParameter("@LastSelected", Landmark),
                new SqlParameter("@FK_UserId", Fk_UserId),
            };
            DataSet ds = Connection.ExecuteQuery("QuickViewNew", para);
            return ds;
        }
        #endregion

        #region CustomerCart

        public DataSet loadCustomerCart()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID) };
            DataSet ds = Connection.ExecuteQuery("ShowCart", para);
            return ds;
        }

        #endregion

        #region PlaceCustomerOrder

        public DataSet PlaceCustomerOrder()
        {
            SqlParameter[] para = { new SqlParameter("@dtCustomerOrderDetails", dtCustomerOrderDetails),
            new SqlParameter("@PK_CustomerID", CustomerID),
            new SqlParameter("@FK_PaymentModeID", PaymentModeID),
            new SqlParameter("@HouseNo", HouseNo),
            new SqlParameter("@Locality", Locality),
            new SqlParameter("@Landmark", Landmark),
            new SqlParameter("@Pincode", Pincode),
            new SqlParameter("@AddressType", AddressType),
            new SqlParameter("@FK_AddressID", FK_AddressID),
            new SqlParameter("@OrderTotal", TotalAmount) ,
            new SqlParameter("@IsShoopingRedeem", IsShoopingRedeem) ,
              new SqlParameter("@IsBecomePrime", IsBecomePrime) ,
             new SqlParameter("@CouponID", CouponID),};

            DataSet ds = Connection.ExecuteQuery("PlaceCustomerOrder", para);
            return ds;
        }

        #endregion

        #region CancelOrder

        public DataSet CancelOrder()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID),
                                    new SqlParameter("@FK_OrderDetailsID", OrderDetailsID),};
            DataSet ds = Connection.ExecuteQuery("CancelOrder", para);
            return ds;
        }
        public DataSet ReturnOrder()
        {
            SqlParameter[] para = { new SqlParameter("@OrderID", PK_OrderID),
                                    new SqlParameter("@OrderDetailsID", OrderDetailsID),
                                    new SqlParameter("@CustomerID", CustomerID),
                                    new SqlParameter("@Remark", Description),};
            DataSet ds = Connection.ExecuteQuery("ReturnRequest", para);
            return ds;
        }

        #endregion

        #region ReturnRequest
        public DataSet GetReturnRequestList()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerID", CustomerID),
                                    new SqlParameter("@OrderNo", OrderNo),
                                    new SqlParameter("@VendorID", Fk_vendorId) };
            DataSet ds = Connection.ExecuteQuery("ReturnRequestList", para);
            return ds;
        }
        public DataSet ApproveReturnRequest()
        {
            SqlParameter[] para = { new SqlParameter("@RequestID", RequestID),
                                    new SqlParameter("@OrderID", PK_OrderID),
                                    new SqlParameter("@OrderdetailsID", OrderDetailsID),
                                    new SqlParameter("@CustomerID", CustomerID),
                                    new SqlParameter("@RefundAmount", TotalAmount),
                                    new SqlParameter("@ApprovedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("ApproveReturn", para);
            return ds;
        }
        #endregion

        #region ExchangeRequest
        public DataSet GetExchangeRequestList()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerID", CustomerID),
                                    new SqlParameter("@OrderNo", OrderNo),
                                    new SqlParameter("@VendorID", Fk_vendorId) };
            DataSet ds = Connection.ExecuteQuery("ExchangeRequestList", para);
            return ds;
        }
        public DataSet ApproveExchangeRequest()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerID", CustomerID),
                                    new SqlParameter("@OrderID", PK_OrderID),
                                    new SqlParameter("@OrderDetailsID", OrderDetailsID),
                                    new SqlParameter("@ProductInfoCode", ProductInfoCode),
                                    new SqlParameter("@Quantity", ProductQuantity),
                                    new SqlParameter("@Rate", Rate),
                                    new SqlParameter("@VendorID", Fk_vendorId),
                                    new SqlParameter("@ApprovedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("ApproveExchange", para);
            return ds;
        }
        #endregion

        #region TrackOrder
        public DataSet TrackOrder()
        {
            SqlParameter[] para = { new SqlParameter("@PK_OrderID", PK_OrderID),
                                    new SqlParameter("@FK_CustomerID", CustomerID),};
            DataSet ds = Connection.ExecuteQuery("TrackOrder", para);
            return ds;
        }

        #endregion

        public DataSet ApplyDiscount()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID),
                                    new SqlParameter("@CouponCode", CouponCode) };
            DataSet ds = Connection.ExecuteQuery("ApplyDiscountCoupon", para);
            return ds;
        }

        public DataSet GetCustomerBalance()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID), };
            DataSet ds = Connection.ExecuteQuery("GetWalletBalance", para);
            return ds;
        }
        public string hdColorID { get; set; }
        public string ColorID { get; set; }
        public string MaterialID { get; set; }
        public string FlavorID { get; set; }
        public string StarRatingID { get; set; }
        public string StorageID { get; set; }
        public string StorageName { get; set; }
        public string RamID { get; set; }
        public string RamName { get; set; }
        public string Rate { get; set; }
        public string SubTotal { get; set; }
        public string BannerName { get; set; }
        public string BannerImage { get; set; }
        public string Status { get; set; }
        public string ProductInfoCode { get; set; }
        public string ShortValue { get; set; }
        public string OtherStatus { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string IsDefault { get; set; }
        public DataSet ClearCart()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID),
                new SqlParameter("@FK_ProductID", ProductID),
                new SqlParameter("@DeletedBy", CustomerID),
                 new SqlParameter("@CartID", CartID),

            };
            DataSet ds = Connection.ExecuteQuery("ClearShoppingCart", para);
            return ds;
        }
        public DataSet GetProductSubcategoryWise()
        {
            SqlParameter[] para = { new SqlParameter("@SubCategoryID", SubCategoryID),
                                    new SqlParameter("@Status", Status),

            };
            DataSet ds = Connection.ExecuteQuery("GetProductSubcategoryWise", para);
            return ds;
        }
        public DataSet GetGlobalData()
        {
            SqlParameter[] para = { new SqlParameter("@ProductName", DisplayName), };
            DataSet ds = Connection.ExecuteQuery("SearchProductFromDashboard", para);
            return ds;
        }
        public DataSet AutoSearchProduct()
        {
            SqlParameter[] para = { new SqlParameter("@SearchText", DisplayName), };
            DataSet ds = Connection.ExecuteQuery("AutoSearchProduct", para);
            return ds;
        }

        public static Customer GetMenus()
        {
            Customer model = new Customer();
            List<Customer> lstMainCategory = new List<Customer>();
            List<Customer> lstCategory = new List<Customer>();
            List<Customer> lstSubCategory = new List<Customer>();

            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Customer obj = new Customer();

                        obj.MainCategoryID = r["PK_MainCategoryID"].ToString();
                        obj.MainCategoryName = r["MainCategoryName"].ToString();

                        lstMainCategory.Add(obj);
                    }

                    model.lstMainCategory = lstMainCategory;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Customer obj = new Customer();

                        obj.CategoryID = r["PK_CategoryID"].ToString();
                        obj.CategoryName = r["CategoryName"].ToString();
                        obj.MainCategoryID = r["FK_MainCategory"].ToString();
                        lstCategory.Add(obj);
                    }

                    model.lstCategory = lstCategory;
                    foreach (DataRow r in dsHeader.Tables[2].Rows)
                    {
                        Customer obj = new Customer();

                        obj.MainCategoryID = r["FK_MainCategory"].ToString();
                        obj.CategoryID = r["FK_CategoryID"].ToString();
                        obj.SubCategoryName = r["SubCategoryName"].ToString();
                        obj.SubCategoryID = r["PK_SubCategoryID"].ToString();
                        lstSubCategory.Add(obj);
                    }

                    model.lstSubCategory = lstSubCategory;
                }


            }
            return model;

        }
        public DataSet GetProductByOffer()
        {
            SqlParameter[] para = { new SqlParameter("@OfferID", OfferID), };
            DataSet ds = Connection.ExecuteQuery("GetProductByOffer", para);
            return ds;
        }
        public DataSet LoadCustomerOrders()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID), };
            DataSet ds = Connection.ExecuteQuery("MyOrders", para);
            return ds;
        }

        public DataSet Invoice()
        {
            SqlParameter[] para = { new SqlParameter("@OrderNo", OrderNo), };
            DataSet ds = Connection.ExecuteQuery("InvoiceNew", para);
            return ds;
        }
        public DataSet GetProfileDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_CustomerID", CustomerID), };
            DataSet ds = Connection.ExecuteQuery("GetCustomerProfile", para);
            return ds;
        }
        public DataSet AddressList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID), };
            DataSet ds = Connection.ExecuteQuery("GetCustomerAddress", para);
            return ds;
        }
        public DataSet SaveAddress()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", AddedBy),
                new SqlParameter("@Locality", Locality),
                new SqlParameter("@Landmark", Landmark),
                new SqlParameter("@HouseNo", HouseNo),
                new SqlParameter("@Pincode", Pincode),
                new SqlParameter("@AddedBy", AddedBy),
                  new SqlParameter("@AddressType", AddressType),
                   new SqlParameter("@IsDefault", IsDefault),
                   new SqlParameter("@Name", DisplayName),
                   new SqlParameter("@ContatNo", Contact),

            };
            DataSet ds = Connection.ExecuteQuery("SaveCustomerAddress", para);
            return ds;
        }
        public DataSet UpdateAddress()
        {
            SqlParameter[] para = { new SqlParameter("@pk", FK_AddressID),
                 new SqlParameter("@Locality", Locality),
                new SqlParameter("@Landmark", Landmark),
                new SqlParameter("@HouseNo", HouseNo),
                new SqlParameter("@Pincode", Pincode),
                new SqlParameter("@AddedBy", UpdatedBy),
                     new SqlParameter("@AddressType", AddressType),
                       new SqlParameter("@IsDefault", IsDefault),
                   new SqlParameter("@Name", DisplayName),
                   new SqlParameter("@ContatNo", Contact),

            };
            DataSet ds = Connection.ExecuteQuery("UpdateCustomerAddress", para);
            return ds;
        }
        public DataSet DeleteAddress()
        {
            SqlParameter[] para = { new SqlParameter("@ID", FK_AddressID),
            new SqlParameter("@DeletedBy", AddedBy),};
            DataSet ds = Connection.ExecuteQuery("DeleteAddress", para);
            return ds;
        }
        public DataSet UpdateInfo()
        {
            SqlParameter[] para = { new SqlParameter("@PK_CustomerID", CustomerID),
                                    new SqlParameter("@CustomerName", CustomerName),
                                    new SqlParameter("@Contact", Contact),
                                    new SqlParameter("@Email", Email),
                                     new SqlParameter("@updatedby", UpdatedBy),
            };
            DataSet ds = Connection.ExecuteQuery("UpdateCustomerInfo", para);
            return ds;
        }
        public DataSet MyCoupons()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerID", CustomerID),

            };
            DataSet ds = Connection.ExecuteQuery("CouponListForCustomer", para);
            return ds;
        }
        public DataSet BindFiltersForProductList()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_CategoryId", CategoryID),
                new SqlParameter("@Fk_MainCategoryId", MainCategoryID),
                new SqlParameter("@FK_SubCategoryId", SubCategoryID),
                new SqlParameter("@Status", OtherStatus),

            };
            DataSet ds = Connection.ExecuteQuery("BindFiltersForProductList", para);
            return ds;
        }
        public DataSet GetFilterData()
        {
            SqlParameter[] para = { new SqlParameter("@ColorDetails", ColorDetails),
                new SqlParameter("@SizeDetails", SizeDetails),
                new SqlParameter("@FlavourDetails", FlavourDetails),
                new SqlParameter("@MaterialDetails", MaterialDetails),
                new SqlParameter("@RamDetails", RamDetails),
                new SqlParameter("@StorageDetails", StorageDetails),
                new SqlParameter("@Status", Status),
                new SqlParameter("@Id", Id),

            };
            DataSet ds = Connection.ExecuteQuery("FilterProduct", para);
            return ds;
        }


        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                 new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy) };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordCustomer", para);
            return ds;
        }

        #region Complain
        public string EncrptNo { get; set; }
        // public string FK_UserID { get; set; }

        public string Issue { get; set; }
        public string PK_ComplainID { get; set; }
        public string ComplainID { get; set; }
        public string ComplainDate { get; set; }
        public string ComplainStatus1 { get; set; }

        public string Reply { get; set; }
        public string ReplyPerson { get; set; }
        public string ReplyDate { get; set; }
        public List<Customer> lstComplains { get; set; }
        public List<Customer> lstComplainResponse { get; set; }
        public string AdharNo { get; set; }
        public string PAN { get; set; }
        public string GSTNo { get; set; }
        public string Address { get; set; }
        public string AmtInwords { get; set; }
        public List<Customer> lstofferproduct { get; set; }
        public string RedeemPrice { get; set; }
        public string IsShoopingRedeem { get;  set; }
        public string IsBecomePrime { get;  set; }
        public string IsAvailable { get;  set; }
        public string TemPermanent { get;  set; }
        public string FinalAmount { get;  set; }
        public string TaxableAmt { get;  set; }
        public string CustomerState { get;  set; }
        public string InvoiceNo { get; set; }
        public string ShippingCGST { get;  set; }
        public string ShippingSGST { get; set; }
        public string ShippingIGST { get; set; }
        public string ShippingTaxable { get;  set; }

        public DataSet InsertComplain()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",     Fk_UserId),
                                    new SqlParameter("@LoginID", LoginID),
                                    new SqlParameter("@Issue", Issue) };
            DataSet ds = Connection.ExecuteQuery("InsertComplain", para);
            return ds;
        }

        public DataSet GetAllComplains()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",     Fk_UserId),
                                    new SqlParameter("@Status", ComplainStatus1), };
            DataSet ds = Connection.ExecuteQuery("GetAllComplains", para);
            return ds;
        }

        public DataSet GetAllComplainsAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@Mobile", Contact),
                 new SqlParameter("@CustomerName", CustomerName),
                                    new SqlParameter("@Status", ComplainStatus1),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate), };
            DataSet ds = Connection.ExecuteQuery("GetAllComplainsAdmin", para);
            return ds;
        }

        public DataSet GetComplainResponseAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@PK_ComplainID", PK_ComplainID), };
            DataSet ds = Connection.ExecuteQuery("GetComplainResponse", para);
            return ds;
        }

        public DataSet ReplyAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ComplainID", PK_ComplainID),
                                    new SqlParameter("@Reply", Reply),
                                    new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = Connection.ExecuteQuery("ComplainReplyAdmin", para);
            return ds;
        }

        public DataSet ReplyAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ComplainID", PK_ComplainID),
                                    new SqlParameter("@Reply", Reply),
                                    new SqlParameter("@FK_UserID",     Fk_UserId), };
            DataSet ds = Connection.ExecuteQuery("ComplainReplyAssociate", para);
            return ds;
        }
        #endregion

        public DataSet GetCustomerList()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerCode", LoginID),
                                    new SqlParameter("@CustomerName", CustomerName),
                                      new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),
                                          new SqlParameter("@Mobile", Contact),


            };
            DataSet ds = Connection.ExecuteQuery("getCustomerList", para);
            return ds;
        }
        public DataSet GetCustomerListName()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerCode", LoginID),
                                    new SqlParameter("@CustomerName", CustomerName), };
            DataSet ds = Connection.ExecuteQuery("GetCustomerForOrders", para);
            return ds;
        }
        
        public DataSet ValidateData()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@Contact", Contact)};
            DataSet ds = Connection.ExecuteQuery("ForgotPasswordCustomer", para);
            return ds;
        }
        public DataSet DeleteCustomer()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@PK_UserId", CustomerID),
            new SqlParameter("@DeletedBy", AddedBy)};
            DataSet ds = Connection.ExecuteQuery("DeleteCustomer", para);
            return ds;
        }

        public DataSet GetVendorForProduct()
        {
            SqlParameter[] para = { new SqlParameter("@FK_ProductID", ProductID),
                                    new SqlParameter("@Fk_RamId", RamID),
                                    new SqlParameter("@Fk_StorageId", StorageID),
                                    new SqlParameter("@Fk_ColorId", ColorID),
                                    new SqlParameter("@Fk_SizeId", SizeID),
                                    new SqlParameter("@FK_FlavourId", FlavorID), };
            DataSet ds = Connection.ExecuteQuery("GetVendorForProduct", para);
            return ds;
        }

        public DataSet SaveProductRating()
        {
            SqlParameter[] para = { new SqlParameter("@ProductInfoCode", ProductInfoCode),
                                    new SqlParameter("@StarRating", StarRating),
                                    new SqlParameter("@Comment", Comment),
                                    new SqlParameter("@AddedBy", AddedBy),
                                  };
            DataSet ds = Connection.ExecuteQuery("ProductRating", para);
            return ds;
        }
        public DataSet SaveVendorRating()
        {
            SqlParameter[] para = { new SqlParameter("@VendorId", Fk_vendorId),
                                    new SqlParameter("@StarRating", StarRating),
                                    new SqlParameter("@Comment", Comment),
                                    new SqlParameter("@AddedBy", AddedBy),
                                  };
            DataSet ds = Connection.ExecuteQuery("VendorRating", para);
            return ds;
        }
        public DataSet CheckMobileNo()
        {
            SqlParameter[] para = { new SqlParameter("@MobileNo", Contact),

                                  };
            DataSet ds = Connection.ExecuteQuery("CheckvendorMobileNo", para);
            return ds;
        }

        public DataSet GetDetailedOrderList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerID),
            new SqlParameter("@OrderNo", OrderNo),
            new SqlParameter("@FromDate", FromDate),
            new SqlParameter("@ToDate", ToDate) };
            DataSet ds = Connection.ExecuteQuery("DetailedOrderList", para);
            return ds;
        }

        public DataSet SaveExchangeDetails()
        {
            SqlParameter[] para = { new SqlParameter("@CustomerID", CustomerID),
                                    new SqlParameter("@OrderID", PK_OrderID),
                                    new SqlParameter("@OrderDetailsID", OrderDetailsID),
                                    new SqlParameter("@ProductInfoCode", ProductInfoCode),
                                    new SqlParameter("@Quantity", ProductQuantity),
                                    new SqlParameter("@Rate", Rate),
                                    new SqlParameter("@ShippingCharge", DeliveryCharge),
                                    new SqlParameter("@VendorID", Fk_vendorId) };
            DataSet ds = Connection.ExecuteQuery("SaveExchangeDetails", para);
            return ds;
        }
        public DataSet BindDashBannerImage()
        {
            
            DataSet ds = Connection.ExecuteQuery("GetDashboardBanner");
            return ds;
        }

        public DataSet UpdateProfilePic()
        {
            SqlParameter[] para = { new SqlParameter("@PK_CustomerID",CustomerID ) ,
                                      new SqlParameter("@ProfilePic", ProfilePic)
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateCustomerProfilePic", para);
            return ds;
        }


    }
}