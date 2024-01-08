using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class MobileMenu
    {
        public string Status { get; set; }
        public List<MainData> lstnmain { get; set; }
        public DataSet GetMenu()
        {
            DataSet ds = Connection.ExecuteQuery("GetMenuForMobile");
            return ds;
        }
    }
    public class MainData
    {


        public List<MainCategoryDetails> MainCategoryDetails { get; set; }




    }
    public class MainCategoryDetails
    {
        public string FK_MainCategory { get; set; }
        public string MainCategoryName { get; set; }
        public List<CategoryDetails> LstCategory { get; set; }
        public string Image { get; set; }
    }
    public class CategoryDetails
    {

        public string FK_CategoryID { get; set; }
        public string FK_MainCategory { get; set; }

        public string CategoryName { get; set; }

        public List<SubCategoryDetails> LstsubCategory { get; set; }
    }
    public class SubCategoryDetails
    {
        public string PK_SubCategoryID { get; set; }
        public string FK_MainCategory { get; set; }

        public string SubCategoryName { get; set; }
        public string FK_CategoryID { get; set; }

    }
}