using MSCLShopping.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSCLShopping.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            Vendor Modal = new Vendor();
            List<Vendor> lstreview = new List<Vendor>();
            DataSet ds = Modal.GetSellerReview();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Vendor obj = new Vendor();
                    obj.Review = r["Review"].ToString();
                    obj.AddedBy = r["Fullname"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    lstreview.Add(obj);
                }
                Modal.lstRequests = lstreview;
            }
            return View(Modal);
        }

        public ActionResult FeeStructure()
        {
            return View();
        }
    }
}