using MSCLShopping.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ShoppingPortal.Models
{
    public class Menu
    {
        public List<Main> Main { get; set; }

        public string Status { get; set; }

        public List<menuDetails> menu { get; set; }

        public DataSet GetServicesDetailsForApplication()
        {


            DataSet dsResult = Connection.ExecuteQuery("GetServicesDetailsForApplication");
            return dsResult;
        }
        public DataSet GetMenu()
        {


            DataSet dsResult = Connection.ExecuteQuery("GetMenuForMobile");
            return dsResult;
        }

    }
    public class Main
    {
        public string MainCategory { get; set; }
        public string MainCategoryId { get; set; }
    }


    public class MenuDetails
    {


        public List<menuDetails> menuDetails { get; set; }



    }

    public class Data1
    {


        public List<menuDetails> menu { get; set; }




    }
    public class menuDetails
    {

        public string MainServiceType { get; set; }
        public List<menuData> menuData { get; set; }
    }
    public class menuData
    {
        public string ServiceIcon { get; set; }
        public string Service { get; set; }

        public string ServiceUrl { get; set; }
    }
}