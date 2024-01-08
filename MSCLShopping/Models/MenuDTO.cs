using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class CategoryData
    {
        public string id { get; set; }
        public string SubCategory { get; set; }

    }

    public class MainData1
    {
        public string Category { get; set; }
        public string PK_CategoryID { get; set; }
        public List<CategoryData> CategoryData { get; set; }

    }

    public class Menu1
    {
        public string image { get; set; }
        public string MainCategoryId { get; set; }
        public string MainCategory { get; set; }
        public List<MainData1> MainData { get; set; }

    }

    public class MenuDTO
    {
        public List<Menu1> menu { get; set; }
        public string Status { get; set; }
    }

}