using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using SD = System.Drawing;

namespace MSCLShopping.Models
{
    public class Common
    {
        public string DeliveryCharge { get; set; }
        //public List<Common> List { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string DisplayName { get; set; }
        public string AddedOn { get; set; }
        public string FK_DesignationId { get; set; }

        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;
            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];
                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }
            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }



        public static List<SelectListItem> lstPayment()
        {
            List<SelectListItem> Paymode = new List<SelectListItem>();
            Paymode.Add(new SelectListItem { Text = "Select", Value = null });
            Paymode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            Paymode.Add(new SelectListItem { Text = "Wallet", Value = "Wallet" });

            return Paymode;
        }

        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;

        }
        public class PaymentGateWayDetails
        {
            public static string CreateOrder = "https://api.razorpay.com/v1/orders";
            public static string CapturePayment = "https://api.razorpay.com/v1/payments/";
            public static string FetchPaymentByOrderURL = "https://api.razorpay.com/v1/orders/";
            public static string KeyName = "rzp_live_IsKhZhtWWOAVUx";
            public static string SecretKey = "nZMn2wBc4m6odf21XD5Wyt1y";
        }
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        public DataSet GetTradMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetTradMemberName", para);

            return ds;
        }
        public DataSet BindProduct()
        {

            DataSet ds = Connection.ExecuteQuery("GetProductList");
            return ds;
        }
        public DataSet BindDesignation(string FK_DesignationId)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_DesignationId", FK_DesignationId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignation");
            return ds;
        }

        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode) };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }
        public static List<SelectListItem> lstBrandOwner()
        {
            List<SelectListItem> BrandOwner = new List<SelectListItem>();
            BrandOwner.Add(new SelectListItem { Text = "Yes", Value = "1" });
            BrandOwner.Add(new SelectListItem { Text = "No", Value = "0" });
            return BrandOwner;
        }
        public static List<SelectListItem> lstBrandStatus()
        {
            List<SelectListItem> BrandStatus = new List<SelectListItem>();
            BrandStatus.Add(new SelectListItem { Text = "All", Value = null });
            BrandStatus.Add(new SelectListItem { Text = "Pending", Value = "0" });
            BrandStatus.Add(new SelectListItem { Text = "Approved", Value = "1" });

            return BrandStatus;
        }
        public static List<SelectListItem> BindPaymentMode()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Cheque", Value = "Cheque" });
            PaymentMode.Add(new SelectListItem { Text = "NEFT", Value = "NEFT" });
            PaymentMode.Add(new SelectListItem { Text = "RTGS", Value = "RTGS" });
            PaymentMode.Add(new SelectListItem { Text = "Demand Draft", Value = "DD" });
            return PaymentMode;
        }
        public static List<SelectListItem> BindPasswordType()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Profile Password", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Transaction Password", Value = "T" });

            return PasswordType;
        }
        public static List<SelectListItem> TransactionType()
        {
            List<SelectListItem> TransactionType = new List<SelectListItem>();
            TransactionType.Add(new SelectListItem { Text = "Select", Value = "0" });
            TransactionType.Add(new SelectListItem { Text = "Credit", Value = "Credit" });
            TransactionType.Add(new SelectListItem { Text = "Debit", Value = "Debit" });

            return TransactionType;
        }
        public static List<SelectListItem> BindKYCStatus()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Not Uploaded", Value = "N" });
            PasswordType.Add(new SelectListItem { Text = "Pending", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Approved", Value = "A" });

            return PasswordType;
        }

        public static List<SelectListItem> AddressType()
        {
            List<SelectListItem> TypeAddress = new List<SelectListItem>();
            TypeAddress.Add(new SelectListItem { Text = "Select", Value = null });
            TypeAddress.Add(new SelectListItem { Text = "Home", Value = "Home" });
            TypeAddress.Add(new SelectListItem { Text = "Work", Value = "Work" });


            return TypeAddress;
        }
        public string Fk_UserId { get; set; }


        public static List<SelectListItem> BindPaymentStatus()
        {
            List<SelectListItem> PaymentStatus = new List<SelectListItem>();
            PaymentStatus.Add(new SelectListItem { Text = "All", Value = null });
            PaymentStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            PaymentStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            PaymentStatus.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });

            return PaymentStatus;
        }

        public DataSet BindUserTypeForRegistration()
        {

            DataSet ds = Connection.ExecuteQuery("GetUserTypeForRegistration");

            return ds;

        }
        public DataSet BindFormMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManageShopping", para);

            return ds;

        }
        #region Form Permissions By User
        public DataSet FormPermissions(string FormName, string AdminId)
        {
            try
            {
                SqlParameter[] para = {
                                          new SqlParameter("@FormName", FormName) ,
                                          new SqlParameter("@AdminId", AdminId)
                                      };

                DataSet ds = Connection.ExecuteQuery("PermissionsOfForm", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion




        public static List<SelectListItem> BindOfferStatus()
        {
            List<SelectListItem> OfferStatus = new List<SelectListItem>();
            OfferStatus.Add(new SelectListItem { Text = "Select", Value = null });
            OfferStatus.Add(new SelectListItem { Text = "Yes", Value = "Yes" });
            OfferStatus.Add(new SelectListItem { Text = "No", Value = "No" });

            return OfferStatus;
        }
        public static List<SelectListItem> ComplainStatus()
        {
            List<SelectListItem> ComplainStatus = new List<SelectListItem>();
            ComplainStatus.Add(new SelectListItem { Text = "Select", Value = null });
            ComplainStatus.Add(new SelectListItem { Text = "Replied", Value = "Replied" });
            ComplainStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });

            return ComplainStatus;
        }
        public class EncKey
        {
            public static string KeyName = "ALMKOLKJ12345ANB";
            public static string ProfilePicURL = "/";
            public static string ShoppingProfilePicURL = "/";

        }
        static public void BindDropDown(DropDownList ddl, string dataValueField, string dataTextField, DataTable dtab)
        {
            try
            {

                if (dtab.Rows.Count == 0)
                {
                    ddl.Items.Clear();
                    ddl.Items.Insert(0, new ListItem("----Select----", "0"));
                    return;
                }
                else
                {

                    ddl.DataSource = dtab;
                    ddl.DataTextField = dataTextField;
                    ddl.DataValueField = dataValueField;
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("----Select----", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string UpdateMetaDetails()
        {
            StringBuilder sbMetaTags = new StringBuilder();
            if (HttpContext.Current.Session["Description"] != null)
            {

                sbMetaTags.Append("<meta name='description' content='" + HttpContext.Current.Session["Description"].ToString() + "'/>");

                sbMetaTags.Append(Environment.NewLine);
            }
            return sbMetaTags.ToString();

        }
        static public void ClearControls(ControlCollection page)
        {
            foreach (Control c in page)
            {
                if (c.ID != null)
                {

                    if (c.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                    {
                        TextBox txt = (TextBox)c;
                        txt.Text = "";
                    }
                    if (c.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                    {
                        DropDownList ddl = (DropDownList)c;
                        ddl.SelectedIndex = 0;
                    }
                    if (c.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                    {
                        CheckBox chk = (CheckBox)c;
                        chk.Checked = false;
                    }
                    if (c.GetType().ToString() == "System.Web.UI.WebControls.RadioButton")
                    {
                        RadioButton Rad = (RadioButton)c;
                        Rad.Checked = false;
                    }
                    if (c.GetType().ToString() == "System.Web.UI.WebControls.CheckBoxList")
                    {
                        CheckBoxList chkList = (CheckBoxList)c;
                        chkList.Items.Clear();
                    }
                    if (c.GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
                    {
                        RadioButtonList RadList = (RadioButtonList)c;
                        RadList.Items.Clear();
                    }
                }

                if (c.HasControls())
                {
                    ClearControls(c.Controls);
                }
            }
        }

      

    }

  
}