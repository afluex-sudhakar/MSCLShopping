﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace MSCLShopping.Models
{
    public class Home
    {
        public List<Home> lstmainmenu { get; set; }
        public string Password { get; set; }
        public string Pk_AdminId { get; set; }
        public string UserType { get; set; }
        public string LoginId { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Commitment { get; set; }
        public string PaymentMethod { get; set; }
        public string PK_UserId { get; set; }
        public string Menu { get; set; }
        public string SubMenu { get; set; }
        public string RegistrationBy { get; set; }
        public string Response { get; set; }
        public string ProfilePic { get; set; }
        public string DecriptPass { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string Url { get; set; }
        public List<Home> lstMenu { get; set; }
        public List<Home> lstsubmenu { get; set; }

        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }


        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = Connection.ExecuteQuery("ValidateAdminLogin", para);
            return ds;
        }
        public DataSet LoginMLM()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = Connection.ExecuteQuery("Login", para);
            return ds;
        }
        public DataSet GetMainMenu()
        {

            DataSet ds = Connection.ExecuteQuery("MainCategoryList");
            return ds;
        }
        public DataSet Registration()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@Email",Email),
                                   new SqlParameter("@MobileNo",MobileNo),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@Commitment",Commitment),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@PaymentMethod",PaymentMethod)

                                   };
            DataSet ds = Connection.ExecuteQuery("Registration", para);
            return ds;
        }

        public DataSet GetDetails()
        {
            SqlParameter[] para = {


                                     new SqlParameter("@FK_UserID",PK_UserId)

                                   };
            DataSet ds = Connection.ExecuteQuery("SelectMenu", para);
            return ds;
        }
        public DataSet SaveContactUs()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@Name",Name),
                                     new SqlParameter("@Email",Email),
                                     new SqlParameter("@Subject",Subject),
                                     new SqlParameter("@Message",Message),
                                     new SqlParameter("@CreatedBy",CreatedBy),

                                   };
            DataSet ds = Connection.ExecuteQuery("SaveContactUs", para);
            return ds;
        }

        #region ChangePassword

        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string UpdatedBy { get; set; }
        public string PK_MainCategoryID { get; set; }
        public string MainCategoryName { get; set; }

        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangePassword", para);
            return ds;
        }
        public DataSet UpdateAssociatePassword()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@OldPassword", Password) ,
                                          new SqlParameter("@LoginId", LoginId) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangeAssociatePasswordByAdmin", para);
            return ds;
        }

        #endregion
        public class SingleImagePayload
        {
            public string number { get; set; }
            public string caption { get; set; }
            public string image { get; set; }
        }
        public DataSet ValidateData()
        {
            SqlParameter[] para = {


                                     new SqlParameter("@LoginID",LoginId),
                                     new SqlParameter("@Mobile",MobileNo)

                                   };
            DataSet ds = Connection.ExecuteQuery("ForgotPasswordShoppingAdmin", para);
            return ds;
        }
        public DataSet loadHeaderMenu()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = Connection.ExecuteQuery("GetMenuUserWiseShopping", para);
            return ds;
        }
        //public List<Home> lstsubmenu { get; set; }
        //public string Pk_AdminId { get; set; }
        //public string UserType { get; set; }
        //public List<Home> lstMenu { get; set; }
        //public string MenuId { get; set; }
        //public string MenuName { get; set; }
        //public string SubMenuId { get; set; }
        //public string SubMenuName { get; set; }
        //public string Url { get; set; }
        public static Home GetMenus(string Pk_AdminId, string UserType)
        {
            Home model = new Home();
            List<Home> lstmenu = new List<Home>();
            List<Home> lstsubmenu = new List<Home>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Home obj = new Home();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Url = r["Url"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Home obj = new Home();
                        obj.Url = r["Url"].ToString();
                        obj.MenuId = r["FK_FormTypeId"].ToString();
                        obj.SubMenuId = r["PK_FormId"].ToString();
                        obj.SubMenuName = r["FormName"].ToString();
                        lstsubmenu.Add(obj);
                    }

                    model.lstsubmenu = lstsubmenu;

                }


            }
            return model;

        }

        public static Home GetMenusShopping(string Pk_AdminId, string UserType)
        {
            Home model = new Home();
            List<Home> lstmenu = new List<Home>();
            List<Home> lstsubmenu = new List<Home>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenuShopping();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Home obj = new Home();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Url = r["Url"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Home obj = new Home();
                        obj.Url = r["Url"].ToString();
                        obj.MenuId = r["FK_FormTypeId"].ToString();
                        obj.SubMenuId = r["PK_FormId"].ToString();
                        obj.SubMenuName = r["FormName"].ToString();
                        lstsubmenu.Add(obj);
                    }

                    model.lstsubmenu = lstsubmenu;

                }


            }
            return model;

        }

        public DataSet loadHeaderMenuShopping()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = Connection.ExecuteQuery("GetMenuUserWiseShopping", para);
            return ds;
        }

    }
}