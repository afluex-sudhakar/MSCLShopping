using MSCLShopping.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class GlobalSearch
    {

        public string Status { get; set; }
        public string ProductName { get; set; }

        public List<Data> ProductList { get; set; }
        public DataSet GlobalSearchData()
        {
            SqlParameter[] para ={new SqlParameter ("@ProductName",ProductName),
                                };
            DataSet ds = Connection.ExecuteQuery("SearchProductFromDashboard", para);
            return ds;

        }


    }

    public class Data
    {

        public string Title { get; set; }

        public List<FeaturedProduct> FeaturedProduct { get; set; }

        public List<Offers> OffersList { get; set; }

        public List<Newarrivals> Newarrivals { get; set; }

        public List<BannerDetails> Banner { get; set; }

        public List<Products> AllProducts { get; set; }



    }
    public class FeaturedProduct
    {
        public string SoldOutCss { get; set; }

        public string ProductName { get; set; }

        public string Images { get; set; }

        public string OfferedPrice { get; set; }

        public string MRP { get; set; }
        public string ColorName { get; set; }
        public string Pk_ProductId { get; set; }

    }


    public class Products
    {


        public string ProductName { get; set; }

        public string Images { get; set; }

        public string OfferedPrice { get; set; }

        public string MRP { get; set; }
        public string ColorName { get; set; }
        public string Pk_ProductId { get; set; }
        public string SoldOutCss { get; set; }
    }
    public class Newarrivals
    {


        public string ProductName { get; set; }

        public string Images { get; set; }

        public string OfferedPrice { get; set; }

        public string MRP { get; set; }
        public string ColorName { get; set; }
        public string Pk_ProductId { get; set; }
        public string SoldOutCss { get; set; }
    }
    public class Offers
    {

        public string PK_OfferID { get; set; }

        public string OfferImage { get; set; }


    }
}