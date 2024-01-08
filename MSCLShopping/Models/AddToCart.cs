using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class AddToCart
    {

        public string CustomerId { get; set; }

        public string ErrorMessage { get; set; }
        public string Fk_vendorId { get; set; }

        public string ProductInfoCode { get; set; }
        public string ProductQuantity { get; set; }

        public string Status { get; set; }
        public string Type { get; set; }


        public DataSet SaveAddToCart()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerId),

                                    new SqlParameter("@ProductInfoCode", ProductInfoCode),

                                    new SqlParameter("@Quantity", ProductQuantity),
                                    new SqlParameter("@FK_VendorID", Fk_vendorId),
                                    new SqlParameter("@Type", Type),
                                    };
            DataSet ds = Connection.ExecuteQuery("AddToCartMobile", para);
            return ds;
        }
    }

    public class ShowCart
    {
        public List<Cartdata> lstcart { get; set; }

        public string CustomerId { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public DataSet loadCustomerCart()
        {
            SqlParameter[] para = { new SqlParameter("@FK_CustomerID", CustomerId) };
            DataSet ds = Connection.ExecuteQuery("ShowCart", para);
            return ds;
        }

    }
    public class CartDetails
    {
        public string PK_CartID { get; set; }

        public string ProductInfoCode { get; set; }
        public string ProductName { get; set; }
        public string DeliveryCharge { get; set; }
        public string VendorName { get; set; }
        public string VendorId { get; set; }

        public string ProductInfo { get; set; }

        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string SubTotal { get; set; }

        public string ImagePath { get; set; }
        public string SoldOutCss { get; set; }
        public string IsAvailable { get; set; }
        public string RedeemPrice { get; set; }
    }
    public class Cartdata
    {
        public string Title { get; set; }

        public List<CartDetails> CartDetails { get; set; }

    }
    public class RemoveCart
    {
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string CartId { get; set; }
        public DataSet ClearCart()
        {
            SqlParameter[] para = {
                    new SqlParameter("@FK_CustomerID", CustomerId),
                    new SqlParameter("@DeletedBy", CustomerId),
                    new SqlParameter("@CartID", CartId),

            };
            DataSet ds = Connection.ExecuteQuery("ClearShoppingCart", para);
            return ds;
        }
    }

}