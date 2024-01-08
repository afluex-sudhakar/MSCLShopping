using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class Employee : Common
    {
        public string Status { get; set; } 
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string JoiningDate { get; set; }
        public string Fk_UserTypeId { get; set; }
        public string EncryptKey { get; set; }
        public string Password { get; set; }
        public string UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Employee> EmpList { get; set; }
        public string Salary { get; set; }
        public string LoginId { get; set; }
        public string ProfilePic { get;  set; }
        public string Present { get; set; }
        public string Absent { get; set; }
        public DataSet UserTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("UserTypeList");
            return ds;
        }
        
        #region ShoppingEmployees
        public DataSet EmployeeList()
        {
            SqlParameter[] para = {  new SqlParameter("@PK_AdminId", UserID) ,
                                     new SqlParameter("@Name", Name) ,
                                     new SqlParameter("@Fk_UserTypeId ", UserTypeID),
                                     new SqlParameter("@FromDate ", FromDate),
                                     new SqlParameter("@ToDate ", ToDate) };
            DataSet ds = Connection.ExecuteQuery("EmployeeListShopping", para);
            return ds;
        }
        public DataSet EmployeeRegistration()
        {
            SqlParameter[] para = { new SqlParameter("@Name", Name) ,
                                    new SqlParameter("@Mobile", Mobile) ,
                                    new SqlParameter("@Email", Email) ,
                                    new SqlParameter("@JoiningDate", JoiningDate) ,
                                    new SqlParameter("@UserType", UserTypeID) ,
                                    new SqlParameter("@CreatedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("EmployeeRegistrationShopping", para);
            return ds;
        }
        public DataSet UpdateShoppingEmployee()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@ID", UserID) ,
                                      new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@JoiningDate", JoiningDate) ,
                                      new SqlParameter("@Fk_UserTypeId", UserTypeID) ,
                                      new SqlParameter("@UpdatedBy", AddedBy) ,
                                        new SqlParameter("@Salary", Salary) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateEmployee", para);
            return ds;
        }
        public DataSet DeleteShoppingEmployee()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID) ,
                                    new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteEmployeeShopping", para);
            return ds;
        }
        #endregion

    }
}
 