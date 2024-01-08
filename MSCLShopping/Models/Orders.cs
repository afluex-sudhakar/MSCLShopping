using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class Orders
    {
        public string CustomerId { get; set; }

        public string Status { get; set; }
        public string ErrorMessage { get; set; }


        public List<orderdata> lstorder { get; set; }

        public DataSet MyOrders()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_CustomerID", CustomerId),


                                    };
            DataSet ds = Connection.ExecuteQuery("MyOrders", para);
            return ds;
        }
    }
    public class orderdata
    {

        public string Title { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }



    }

    public class OrderDetails
    {

        public string OrderDate { get; set; }
        public string OrderAmount { get; set; }
        public string OrderNo { get; set; }
        public string PK_OrderID { get; set; }

        public List<OrderSummary> OrderSummary { get; set; }

    }
    public class OrderSummary
    {
        public string ImagePath { get; set; }

        public string ProductName { get; set; }
        public string ProductInfo { get; set; }
        public string ExpectedDelivery { get; set; }

        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string OrderStatus { get; set; }
        public string PK_OrderDetailsID { get; set; }
        public string RedeemPrice { get; set; }
        public string DeliveryCharge { get; set; }
    }

    public class PlaceOrder
    {
        public string CustomerId { get; set; }
        public string Fk_AddressId { get; set; }
        public string PaymentMode { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string IsShoopingRedeem { get; set; }

        public string OrderNo { get; set; }

        public string IsBecomePrime { get; set; }

        public DataSet OrderPlace()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_CustomerId", CustomerId),
                                       new SqlParameter("@Fk_AddressId", Fk_AddressId),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                         new SqlParameter("@IsShoopingRedeem", IsShoopingRedeem),
                                          new SqlParameter("@IsBecomePrime", IsBecomePrime),
                                    };
            DataSet ds = Connection.ExecuteQuery("PlaceCustomerOrderMobile", para);
            return ds;
        }
    }
    public class CancelOrder
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string PK_OrderDetailsID { get; set; }

        public string CustomerId { get; set; }

        public DataSet Cancel()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_OrderDetailsID", PK_OrderDetailsID),
                                      new SqlParameter("@FK_CustomerID", CustomerId),
                                    };
            DataSet ds = Connection.ExecuteQuery("CancelOrder", para);
            return ds;
        }
    }
    public class CartCount
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Count { get; set; }

        public string CustomerId { get; set; }

        public DataSet CountCart()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@FK_CustomerID", CustomerId),
                                    };
            DataSet ds = Connection.ExecuteQuery("ShowCart", para);
            return ds;
        }
    }
}