using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class Admin
    {

        public DataSet GetDetails()
        {

            DataSet ds = Connection.ExecuteQuery("DetailsForDashboard");
            return ds;
        }
        public DataSet GetDetailsVendor()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@AddedBy", AddedBy),
                                

                                      };
            DataSet ds = Connection.ExecuteQuery("DetailsForVenorDashboard",para);
            return ds;
        }
        
        public DataSet GetList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@OfferName", OfferName),
	                                new SqlParameter("@FromDate", FromDate),
	                                new SqlParameter("@ToDate", ToDate),
	                              
                                      };
            DataSet ds = Connection.ExecuteQuery("OfferList", para);
            return ds;
        }
        
        public string AddedBy { get; set; }
        public string OfferID { get; set; }

        public string OfferName { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string OfferStatus { get; set; }
        public List<Admin> List { get; set; } 
    }
}