using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models.RazpayModels
{
    public class CreateOrders
    {
        public string amount { get; set; }
        public string Pk_UserId { get; set; }
        public string Type { get; set; }
        public string TransactionType { get; set; }
        public string OrderId { get; set; }
        public string CardNo { get; set; }
        public string CardHolder { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }

        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        

        public DataSet SaveOrderDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@amount", amount),
                                      new SqlParameter("@Pk_UserId", Pk_UserId),
                                      new SqlParameter("@Type", Type),
                                      new SqlParameter("@TransactionType", TransactionType),
                                      new SqlParameter("@OrderId", OrderId),

                                  };
            DataSet ds = Connection.ExecuteQuery("SaveOrderDetails", para);
            return ds;
        }


    }
    public class CreateOrderResponses
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}