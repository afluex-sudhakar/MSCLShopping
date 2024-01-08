using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class Vendor : Common
    {

        #region Properties
        public string isAdharVerified { get; set; }
        public string isPANVerified { get; set; }
        public string isGSTINVerified { get; set; }
        public string isAccountVerified { get; set; }
        public string isSignatureVerified { get; set; }
        public string isDocumentVerified { get; set; }
        public string VerificationStatus { get; set; }
        public string BrandName { get; set; }
        public string StoreName { get; set; }
        public string Review { get; set; }
        public string ProfilePic { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string OTP { get; set; }
        public string VendorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Signature { get; set; }
        public string Address { get; set; }
        public string EncryptKey { get; set; }
        public List<Vendor> List { get; set; }
        public string AdharNo { get; set; }
        public string PAN { get; set; }
        public string GSTNo { get; set; }
        public string Password { get; set; }
        public string LoginId { get; set; }
        public string BrandLogo { get; set; }
        public string WebsiteLink { get; set; }
        public string BrandOwner { get; set; }
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public string RequestCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Vendor> lstRequests { get; set; }
        #endregion

        #region Dashboard
        public DataSet Dashboard()
        {
            SqlParameter[] para = { new SqlParameter("@PK_VendorID", VendorID) };
            DataSet ds = Connection.ExecuteQuery("VendorDashboard", para);
            return ds;
        }
        #endregion

        #region VendorProfile
        public DataSet VendorProfile()
        {
            SqlParameter[] para = { new SqlParameter("@PK_VendorID", VendorID) };
            DataSet ds = Connection.ExecuteQuery("VendorDashboard", para);
            return ds;
        }
        #endregion

        #region Vendor-Register-Update-Delete
        public DataSet VendorList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Mobile", Mobile)
                                  };
            DataSet ds = Connection.ExecuteQuery("VendorList", para);
            return ds;

        }

        public DataSet EditVendorList()
        {
            SqlParameter[] para = {
                  new SqlParameter("@PK_UserID", Fk_UserId),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Mobile", Mobile)
                                  };
            DataSet ds = Connection.ExecuteQuery("EditVendorList", para);
            return ds;
        }
        
        public DataSet SaveVendor()
        {
            SqlParameter[] para = {   new SqlParameter("@FirstName", DisplayName),
                                      new SqlParameter("@PinCode", Pincode),
                                      new SqlParameter("@Address", Address),
                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@AddedBy", AddedBy),
                                      new SqlParameter("@GSTNo", GSTNo),
                                      new SqlParameter("@AdharNo", AdharNo),
                                      new SqlParameter("@PAN", PAN),
                                      new SqlParameter("@Password", Password),
                                      new SqlParameter("@AccountNo", AccountNumber),
                                      new SqlParameter("@AccountHolderName", AccountHolderName) };
            DataSet ds = Connection.ExecuteQuery("AddVendor", para);
            return ds;
        }
        public DataSet UpdateVendor()
        {
            SqlParameter[] para ={
                                      new SqlParameter("@PK_VendorID", VendorID),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@PinCode", Pincode),
                                      new SqlParameter("@Address", Address),
                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@State", State),
                                      new SqlParameter("@City", City),
                                      new SqlParameter("@Mobile", Mobile),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@AdharNo", AdharNo),
                                      new SqlParameter("@PAN", PAN),
                                      new SqlParameter("@GSTNo", GSTNo),
                                      new SqlParameter("@AccountNumber", AccountNumber),
                                      new SqlParameter("@AccountHolderName", AccountHolderName),
                                      new SqlParameter("@Signature", Signature),
                                 };
            DataSet ds = Connection.ExecuteQuery("UpdateVendorProfile", para);
            return ds;
        }
        public DataSet DeleteVendor()
        {
            SqlParameter[] para ={
                                     new SqlParameter("@PK_VendorID", VendorID),
                                      new SqlParameter("@DeletedBy", AddedBy),

                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteVendor", para);
            return ds;
        }
        public DataSet VerifyVendorEmail()
        {
            SqlParameter[] para = { new SqlParameter("@MobileNo", Mobile) };
            DataSet ds = Connection.ExecuteQuery("VerifyVendorEmail", para);
            return ds;
        }

        #endregion

        #region VerifyVendorDocuments
        public DataSet VerifyVendorDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@DocumentType",DocumentType),
                                    new SqlParameter("@VerificationStatus", VerificationStatus),
                                    new SqlParameter("@VendorID", VendorID),
                                    new SqlParameter("@ApprovedBy", UpdatedBy) };
            DataSet ds = Connection.ExecuteQuery("VerifyVendorDocument", para);
            return ds;
        }
        #endregion

        #region BrandRequest
        public DataSet CheckBrandName()
        {
            SqlParameter[] para = { new SqlParameter("@BrandName", BrandName) };
            DataSet ds = Connection.ExecuteQuery("BrandList", para);
            return ds;
        }
        public DataSet SaveBrandRequest()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_VendorId", VendorID),
            new SqlParameter("@BrandName", BrandName),
            new SqlParameter("@BrandLogo", BrandLogo),
            new SqlParameter("@WebsiteLink", WebsiteLink),
            new SqlParameter("@DocumentType", DocumentType),
            new SqlParameter("@DocumentPath", DocumentPath),
            new SqlParameter("@BrandOwner", BrandOwner)};
            DataSet ds = Connection.ExecuteQuery("RequestBrand", para);
            return ds;
        }
        public DataSet TrackRequest()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Status", Status),
            new SqlParameter("@FromDate", FromDate),
            new SqlParameter("@ToDate", ToDate) };
            DataSet ds = Connection.ExecuteQuery("GetBrandRequest", para);
            return ds;
        }
        public DataSet ApproveBrandRequest()
        {
            SqlParameter[] para = { new SqlParameter("@RequestCode", RequestCode),
                                    new SqlParameter("@ApprovedBy", UpdatedBy) };
            DataSet ds = Connection.ExecuteQuery("ApproveBrandRequest", para);
            return ds;
        }
        #endregion

        public DataSet CheckStoreName()
        {
            SqlParameter[] para = { new SqlParameter("@StoreName", StoreName) };
            DataSet ds = Connection.ExecuteQuery("CheckStoreName", para);
            return ds;
        }
        public DataSet SaveStoreName()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_VendorId", VendorID),
                                    new SqlParameter("@StoreName", StoreName)};
            DataSet ds = Connection.ExecuteQuery("SaveVendorStoreName", para);
            return ds;
        }
        public DataSet SaveReview() 
        {
            SqlParameter[] para = { new SqlParameter("@Fk_SellerId", VendorID),
                                    new SqlParameter("@Review", Review)};
            DataSet ds = Connection.ExecuteQuery("SellerReview", para);
            return ds;
        }
        public DataSet GetSellerReview()
        {
            DataSet ds = Connection.ExecuteQuery("GetSellerReview");
            return ds;
        }

        public DataSet UpdateProfilePic()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_SellerId", VendorID),
                new SqlParameter("@FileName", ProfilePic)
                                    };
            DataSet ds = Connection.ExecuteQuery("UpdateProfilePic", para);
            return ds;
        }

    }
}