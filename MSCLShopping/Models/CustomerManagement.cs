using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace MSCLShopping.Models
{
    public class CustomerManagement : Common
    {

        public DataSet OrderList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@CustomerName", CustomerName),
                                    new SqlParameter("@OrderNo", OrderNO),
                                    new SqlParameter("@OrderStatus", OrderStatus),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@AddedBy", AddedBy),
                                      };
            DataSet ds = Connection.ExecuteQuery("CustomerOrderDetails", para);
            return ds;
        }

        public DataSet FillDetails()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@PK_OrderID", OrderID),
                                       new SqlParameter("@OrderNo", OrderNO),
                                       new SqlParameter("@Status", OrderStatus),
                                       new SqlParameter("@ProductName", ProductName),
                                       new SqlParameter("@CustomerName", CustomerName),
                                       new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate),
                                      };
            DataSet ds = Connection.ExecuteQuery("OrderDetails", para);
            return ds;
        }
        public DataSet GetDetails()
        {
            SqlParameter[] para = {    new SqlParameter("@OrderNo", OrderNO),
                                       new SqlParameter("@Status", OrderStatus),
                                       new SqlParameter("@ProductName", ProductName),
                                       new SqlParameter("@CustomerName", CustomerName),
                                       new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate),
                                       new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = Connection.ExecuteQuery("OrderReportDetails", para);
            return ds;
        }

         public string FinalAmount { get; set; }
        public string RedeemPrice { get; set; }
        public string Mobile { get; set; }
        public List<CustomerManagement> List { get; set; }
        public string CustomerName { get; set; }
        public string OrderNO { get; set; }
        public string OrderStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrderID { get; set; }
        public string OrderDate { get; set; }
        public string TotalAmount { get; set; }
        //public string Result { get; set; }
        public string ProductID { get; set; }
        public string SizeID { get; set; }
        public string UnitID { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public string UnitName { get; set; }
        public string ColorID { get; set; }
        public string ColorName { get; set; }
        public string OrderDetailsID { get; set; }   
        public string CustomerID { get; set; }
        public string OrderNumber { get; set; }
        public string PaymentMode { get;  set; }
        public string Address { get;  set; }

        public DataSet UpdateOrder()
        {
            SqlParameter[] para = { new SqlParameter("@PK_OrderDetailsID", OrderDetailsID),
                                    new SqlParameter("@OrderStatus", OrderStatus),
                                    new SqlParameter("@UpdatedBy", AddedBy), };
            DataSet ds = Connection.ExecuteQuery("UpdateOrderStatus", para);
            return ds;
        }





        #region CancelledList

        public DataSet CancelledList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@CustomerName",CustomerName),
                                    new SqlParameter("@OrderNo",OrderNO),
                                     new SqlParameter("@AddedBy",AddedBy)

                                       };
            DataSet ds = Connection.ExecuteQuery("CancelledOrderDetails", para);
            return ds;

        }
        public DataSet ApproveRequest()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@PK_OrderDetailsID",OrderDetailsID),
                                    new SqlParameter("@FK_CustomerID",CustomerID),
                                     new SqlParameter("@CRAmount",TotalAmount),
                                    new SqlParameter("@AddedBy",AddedBy)
                                       };
            DataSet ds = Connection.ExecuteQuery("ApproveCancelledOrder", para);
            return ds;
        }

        #endregion
    }
}




