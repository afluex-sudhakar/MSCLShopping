using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MSCLShopping.Filter;
using MSCLShopping.Models;

namespace MSCLShopping.Controllers
{
    public class CustomerManagementController : AdminBaseController
    {

        #region OrderList
        public ActionResult CustomerOrders(CustomerManagement model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("CustomerOrders")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(CustomerManagement model)
        {
            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.OrderList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.OrderID = r["PK_OrderID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.OrderNO = r["OrderNO"].ToString();
                    obj.OrderDate = r["OrderDate"].ToString();
                    obj.TotalAmount = r["TotalAmount"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.Address = r["Address"].ToString();

                    //obj.RedeemPrice = r["Address"].ToString();
                    //obj.DeliveryCharge = r["Address"].ToString();
                    //obj.TotalAmt = r["Address"].ToString();
                    //obj.FinalAmount = r["Address"].ToString();



                    obj.Mobile = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.List = lst;
            }

            return View(model);
        }
        
        public ActionResult Details(string id)
        {

            CustomerManagement model = new CustomerManagement();
            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderID = id;
            DataSet dsblock = model.FillDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "yes";
                    model.OrderID = dsblock.Tables[0].Rows[0]["PK_OrderID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                    model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                    model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                    model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();
                    model.Mobile = dsblock.Tables[0].Rows[0]["Contact"].ToString();

                    {
                        foreach (DataRow r in dsblock.Tables[1].Rows)
                        {
                            CustomerManagement obj = new CustomerManagement();
                            obj.ProductID = r["FK_ProductID"].ToString();
                            obj.Result = r["SubCategoryName"].ToString();
                            obj.SizeID = r["FK_SizeID"].ToString();
                            obj.UnitID = r["FK_UnitID"].ToString();
                            obj.Quantity = r["Quantity"].ToString();
                            obj.Rate = r["Rate"].ToString();
                            obj.TotalAmount = r["Amount"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.SizeName = r["SizeName"].ToString();
                            obj.UnitName = r["UnitName"].ToString();
                            obj.ColorID = r["FK_ColorID"].ToString();
                            obj.ColorName = r["ColorName"].ToString();
                            obj.OrderStatus = r["OrderStatus"].ToString();
                            obj.DeliveryCharge = r["ShippingCharges"].ToString();
                            obj.RedeemPrice = r["RedeemPrice"].ToString();
                             obj.FinalAmount = r["FinalAMount"].ToString();
                            lst.Add(obj);
                        }
                        model.List = lst;
                    }
                }
                else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return View(model);
        }

        #endregion

        #region UpdateOrderStatus-Packed

        public ActionResult UpdateOrderStatus(CustomerManagement model)
        {
            //#region ddlOrderStatus
            //List<SelectListItem> ddlOrderStatus = Common.BindOrderStatus();
            //ViewBag.ddlOrderStatus = ddlOrderStatus;
            //#endregion ddlOrderStatus

            return View(model);
        }

        [HttpPost]
        [ActionName("UpdateOrderStatus")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetOrderList(CustomerManagement model)
        {

            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderStatus = "Placed";
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet dsblock = model.GetDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();

                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.ProductID = r["FK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.TotalAmount = r["Amount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();

                    lst.Add(obj);
                }
                model.List = lst;
            }
            else
            {
                model.Result = "No record found !";
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("UpdateOrderStatus")]
        [OnAction(ButtonName = "Update")]
        public ActionResult Update(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();
                string chk = "";
                string id = "";
                string status = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("OrderDetailsID", typeof(string));
                dtst.Columns.Add("OrderStatus", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        obj.OrderStatus = "Packed";
                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.AddedBy = Session["Pk_AdminId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.UpdateOrder();

                        try
                        {
                            string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", Your Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " containing " + ds.Tables[0].Rows[0]["ProductName"].ToString() +
                                " has been " + ds.Tables[0].Rows[0]["OrderStatus"].ToString() + ". Expected Delivery : " + ds.Tables[0].Rows[0]["ExpectedDelivery"].ToString() + ".";
                            string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                            BLSMS.SendSMSNew(mobile, message);
                        }
                        catch { }

                    }
                }
                TempData["Class"] = "alert alert-success";
                TempData["Offer"] = "Order Status updated successfully";
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Offer"] = ex.Message;
            }
            return RedirectToAction("UpdateOrderStatus", "CustomerManagement");
        }
        #endregion

        #region Shipped
        public ActionResult ShipOrder(CustomerManagement model)
        {
            //#region ddlOrderStatus
            //List<SelectListItem> ddlOrderStatus = Common.BindOrderStatus();
            //ViewBag.ddlOrderStatus = ddlOrderStatus;
            //#endregion ddlOrderStatus

            return View(model);
        }

        [HttpPost]
        [ActionName("ShipOrder")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetOrderListforship(CustomerManagement model)
        {

            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderStatus = "Packed";
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet dsblock = model.GetDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();

                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.ProductID = r["FK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.TotalAmount = r["Amount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.List = lst;
            }
            else
            {
                model.Result = "No record found !";
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ShipOrder")]
        [OnAction(ButtonName = "Update")]
        public ActionResult shipUpdate(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string id = "";
                string status = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("OrderDetailsID", typeof(string));
                dtst.Columns.Add("OrderStatus", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        obj.OrderStatus = "Shipped";
                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.AddedBy = Session["Pk_AdminId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.UpdateOrder();

                        try
                        {
                            string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", Your Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " containing " + ds.Tables[0].Rows[0]["ProductName"].ToString() +
                                " has been " + ds.Tables[0].Rows[0]["OrderStatus"].ToString() + ". Expected Delivery : " + ds.Tables[0].Rows[0]["ExpectedDelivery"].ToString() + ".";
                            string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                            BLSMS.SendSMSNew(mobile, message);
                        }
                        catch { }
                    }
                }
                TempData["Class"] = "alert alert-success";
                TempData["ShipOrder"] = "Order shipped successfully";
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["ShipOrder"] = ex.Message;
            }
            return RedirectToAction("ShipOrder", "CustomerManagement");
        }
        #endregion 

        #region Deliver
        public ActionResult DeliverOrder(CustomerManagement model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("DeliverOrder")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetOrderListfordeliver(CustomerManagement model)
        {

            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderStatus = "Shipped";
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet dsblock = model.GetDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();

                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.ProductID = r["FK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.TotalAmount = r["Amount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.List = lst;
            }
            else
            {
                model.Result = "No record found !";
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("DeliverOrder")]
        [OnAction(ButtonName = "Update")]
        public ActionResult DeliverUpdate(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string id = "";
                string status = "";

                DataTable dtst = new DataTable();
                dtst.Columns.Add("OrderDetailsID", typeof(string));
                dtst.Columns.Add("OrderStatus", typeof(string));

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {
                        obj.OrderStatus = "Delivered";
                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.AddedBy = Session["Pk_AdminId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.UpdateOrder();
                        if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                        {
                            try
                            {
                                string message = "Dear " + ds.Tables[0].Rows[0]["CustomerName"].ToString() + ", Your Order : " + ds.Tables[0].Rows[0]["OrderNo"].ToString() + " containing " + ds.Tables[0].Rows[0]["ProductName"].ToString() +
                                    " has been " + ds.Tables[0].Rows[0]["OrderStatus"].ToString() + ". Expected Delivery : " + ds.Tables[0].Rows[0]["ExpectedDelivery"].ToString() + ".";
                                string mobile = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();

                                BLSMS.SendSMSNew(mobile, message);
                            }
                            catch { }
                            TempData["Class"] = "alert alert-success";
                            TempData["DeliverOrder"] = "Order Delivered successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                        {
                            TempData["Class"] = "alert alert-danger";
                            TempData["DeliverOrder"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["DeliverOrder"] = ex.Message;
            }
            return RedirectToAction("DeliverOrder", "CustomerManagement");
        }
        #endregion

        #region CancelledOrderList
        public ActionResult CancelledOrderList(CustomerManagement model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("CancelledOrderList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ListOfOrders(CustomerManagement model)
        {
            try
            {
                List<CustomerManagement> lst = new List<CustomerManagement>();

                // model.SiteID = model.SiteID == "0" ? null : model.SiteID;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.CancelledList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        CustomerManagement obj = new CustomerManagement();
                        obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                        obj.OrderID = r["FK_OrderID"].ToString();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.Quantity = r["Quantity"].ToString();
                        obj.TotalAmount = r["Amount"].ToString();
                        obj.CustomerID = r["FK_CustomerID"].ToString();
                        obj.OrderNumber = r["OrderNo"].ToString();
                        obj.OrderDate = r["OrderDate"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Mobile = r["Contact"].ToString();
                        lst.Add(obj);
                    }
                    model.List = lst;
                }
            }
            catch (Exception ex)
            {


            }

            return View(model);
        }


        [HttpPost]
        [ActionName("CancelledOrderList")]
        [OnAction(ButtonName = "Update")]
        public ActionResult ApproveCancelledOrder(CustomerManagement obj)
        {
            try
            {
                string noofrows = Request["hdRows"].ToString();

                string chk = "";
                string id = "";
                string customer = "";
                string amt = "";

                for (int i = 1; i <= int.Parse(noofrows) - 1; i++)
                {
                    chk = Request["Chk_ " + i];
                    if (chk == "on")
                    {

                        obj.OrderDetailsID = Request["orderdetailid_ " + i].ToString();
                        obj.CustomerID = Request["custid_ " + i].ToString();
                        obj.TotalAmount = Request["amt_ " + i].ToString();
                        obj.AddedBy = Session["Pk_AdminId"].ToString();
                        DataSet ds = new DataSet();
                        ds = obj.ApproveRequest();

                        try
                        {

                        }
                        catch { }

                    }

                }


                TempData["Offer"] = "Cancelled Order Request Is Approved ";
            }
            catch (Exception ex)
            {
                TempData["Offer"] = ex.Message;
            }
            return RedirectToAction("CancelledOrderList", "CustomerManagement");
        }
        #endregion

        #region ReturnRequestList
        public ActionResult ReturnRequests()
        {
            #region CustomersDDL
            Customer model = new Customer();
            int count = 0;
            List<SelectListItem> ddlCustomers = new List<SelectListItem>();
            DataSet ds = model.GetCustomerList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCustomers.Add(new SelectListItem { Text = "Select Customer", Value = null });
                    }
                    ddlCustomers.Add(new SelectListItem { Text = r["CustomerName"].ToString() + " (" + r["CustomerCode"].ToString() + ")", Value = r["PK_CustomerID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlCustomers = ddlCustomers;
            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("ReturnRequests")]
        [OnAction(ButtonName = "btnGetList")]
        public ActionResult GetReturnRequestList(Customer model)
        {
            try
            {
                if (model.CustomerID == "0")
                {
                    model.CustomerID = null;
                }
                model.Fk_vendorId = Session["Pk_AdminId"].ToString();
                DataSet ds = model.GetReturnRequestList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Customer> lstRequest = new List<Customer>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.CustomerID = r["PK_CustomerID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.Contact = r["Contact"].ToString();
                        obj.ProductName = r["ProductsName"].ToString();
                        obj.TotalAmount = r["Amount"].ToString();
                        obj.OrderNo = r["OrderNo"].ToString();
                        obj.OrderDate = r["OrderDate"].ToString();
                        obj.ComplainDate = r["RequestDate"].ToString();
                        obj.Description = r["CustomerRemark"].ToString();
                        obj.PK_OrderID = r["PK_OrderID"].ToString();
                        obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                        obj.RequestID = r["PK_ReturnRequestID"].ToString();
                        lstRequest.Add(obj);
                    }
                    model.lstOrders = lstRequest;
                }
            }
            catch (Exception ex)
            {

            }
            #region CustomersDDL
            int count = 0;
            List<SelectListItem> ddlCustomers = new List<SelectListItem>();
            DataSet dsCustomer = model.GetCustomerList();
            if (dsCustomer != null && dsCustomer.Tables.Count > 0 && dsCustomer.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsCustomer.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCustomers.Add(new SelectListItem { Text = "Select Customer", Value = null });
                    }
                    ddlCustomers.Add(new SelectListItem { Text = r["CustomerName"].ToString() + " (" + r["CustomerCode"].ToString() + ")", Value = r["PK_CustomerID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlCustomers = ddlCustomers;
            #endregion
            return View(model);
        }
        public ActionResult Approverequest(string rid, string oid, string did, string cid, string amt)
        {
            Customer model = new Customer();
            model.RequestID = rid;
            model.PK_OrderID = oid;
            model.OrderDetailsID = did;
            model.CustomerID = cid;
            model.TotalAmount = amt;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.ApproveReturnRequest();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    model.Result = "1";
                    model.Description = "Return Approved";
                }
                else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "0";
                    model.Description = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ExchangeRequestList
        public ActionResult ExchangeRequests()
        {
            #region CustomersDDL
            Customer model = new Customer();
            int count = 0;
            List<SelectListItem> ddlCustomers = new List<SelectListItem>();
            DataSet ds = model.GetCustomerList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCustomers.Add(new SelectListItem { Text = "Select Customer", Value = null });
                    }
                    ddlCustomers.Add(new SelectListItem { Text = r["CustomerName"].ToString() + " (" + r["CustomerCode"].ToString() + ")", Value = r["PK_CustomerID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlCustomers = ddlCustomers;
            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("ExchangeRequests")]
        [OnAction(ButtonName = "btnGetList")]
        public ActionResult GetExchangeRequestList(Customer model)
        {
            try
            {
                if (model.CustomerID == "0")
                {
                    model.CustomerID = null;
                }
                model.Fk_vendorId = Session["Pk_AdminId"].ToString();
                DataSet ds = model.GetExchangeRequestList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Customer> lstRequest = new List<Customer>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.CustomerID = r["PK_CustomerID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.Contact = r["Contact"].ToString();
                        obj.ProductName = r["ProductsName"].ToString();
                        obj.TotalAmount = r["Amount"].ToString();
                        obj.OrderNo = r["OrderNo"].ToString();
                        obj.OrderDate = r["OrderDate"].ToString();
                        //obj.Description = r["CustomerRemark"].ToString();
                        obj.PK_OrderID = r["PK_OrderID"].ToString();
                        obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                        obj.ProductInfoCode = r["ProductInfoCode"].ToString();
                        obj.ProductQuantity = r["Quantity"].ToString();
                        obj.Rate = r["Rate"].ToString();
                        obj.DeliveryCharge = r["ShippingCharges"].ToString();
                        obj.Fk_vendorId = r["Fk_vendorId"].ToString();
                        lstRequest.Add(obj);
                    }
                    model.lstOrders = lstRequest;
                }
            }
            catch (Exception ex)
            {

            }
            #region CustomersDDL
            int count = 0;
            List<SelectListItem> ddlCustomers = new List<SelectListItem>();
            DataSet dsCustomer = model.GetCustomerList();
            if (dsCustomer != null && dsCustomer.Tables.Count > 0 && dsCustomer.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsCustomer.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCustomers.Add(new SelectListItem { Text = "Select Customer", Value = null });
                    }
                    ddlCustomers.Add(new SelectListItem { Text = r["CustomerName"].ToString() + " (" + r["CustomerCode"].ToString() + ")", Value = r["PK_CustomerID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlCustomers = ddlCustomers;
            #endregion
            return View(model);
        }
        public ActionResult ApproveExchangeRequest(string rid, string oid, string did, string cid, string amt, string pd, string rt, string vid, string pq)
        {
            Customer model = new Customer();
            model.RequestID = rid;
            model.PK_OrderID = oid;
            model.OrderDetailsID = did;
            model.CustomerID = cid;
            model.TotalAmount = amt;
            model.ProductInfoCode = pd;
            model.Rate = rt;
            model.Fk_vendorId = vid;
            model.ProductQuantity = pq;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.ApproveExchangeRequest();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    model.Result = "1";
                    model.Description = "Return Approved";
                }
                else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "0";
                    model.Description = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CustomerOrdersDetailedView
        public ActionResult DetailedCustomerOrders()
        {
            #region CustomersDDL
            Customer model = new Customer();
            int count = 0;
            List<SelectListItem> ddlCustomers = new List<SelectListItem>();
            DataSet ds = model.GetCustomerListName();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCustomers.Add(new SelectListItem { Text = "Select Customer", Value = null });
                    }
                    ddlCustomers.Add(new SelectListItem { Text = r["CustomerName"].ToString() + " (" + r["CustomerCode"].ToString() + ")", Value = r["PK_CustomerID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlCustomers = ddlCustomers;
            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("DetailedCustomerOrders")]
        [OnAction(ButtonName= "getOrderList")]
        public ActionResult GetDetailedOrderList(Customer model)
        {
            try
            {
                if (model.CustomerID == "0")
                {
                    model.CustomerID = null;
                }
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.GetDetailedOrderList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Customer> lstCustomerOrders = new List<Customer>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Customer obj = new Customer();
                        obj.PK_OrderID = r["PK_OrderID"].ToString();
                        obj.CustomerID = r["PK_CustomerID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.Contact = r["Contact"].ToString();
                        obj.OrderNo = r["OrderNo"].ToString();
                        obj.OrderDate = r["OrderDate"].ToString();
                        obj.TotalAmount = r["OrderTotal"].ToString();
                        lstCustomerOrders.Add(obj);
                    }
                    model.lstcustomer = lstCustomerOrders;

                    List<Customer> lstCustomerOrderDetails = new List<Customer>();
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Customer obj = new Customer();
                        obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                        obj.PK_OrderID = r["PK_OrderID"].ToString();
                        obj.ProductID = r["FK_ProductID"].ToString();
                        obj.SizeID = r["SizeID"].ToString();
                        obj.UnitID = r["UnitID"].ToString();
                        obj.ColorID = r["ColorID"].ToString();
                        obj.SizeName = r["SizeName"].ToString();
                        obj.UnitName = r["UnitName"].ToString();
                        obj.ColorName = r["ColorName"].ToString();
                        obj.ProductQuantity = r["Quantity"].ToString();
                        obj.Rate = r["Rate"].ToString();
                        obj.TotalAmount = r["Amount"].ToString();
                        obj.ProductName = r["ProductName"].ToString();
                        obj.Status = r["OrderStatus"].ToString();
                        obj.PrimaryImage = r["ProductImage"].ToString();
                        obj.DeliveryDate = r["ExpectedDelivery"].ToString();
                        lstCustomerOrderDetails.Add(obj);
                    }
                    model.lstOrders = lstCustomerOrderDetails;
                }
            }
            catch (Exception ex)
            {

            }
            #region CustomersDDL
            int count = 0;
            List<SelectListItem> ddlCustomers = new List<SelectListItem>();
            DataSet dsCustomer = model.GetCustomerListName();
            if (dsCustomer != null && dsCustomer.Tables.Count > 0 && dsCustomer.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsCustomer.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCustomers.Add(new SelectListItem { Text = "Select Customer", Value = "0" });
                    }
                    ddlCustomers.Add(new SelectListItem { Text = r["CustomerName"].ToString() + " (" + r["CustomerCode"].ToString() + ")", Value = r["PK_CustomerID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlCustomers = ddlCustomers;
            #endregion
            return View(model);
        }
        public ActionResult OrderSlip(string cn, string con)
        {
            ViewBag.CustomerName = cn;
            ViewBag.CustomerContact = con;
            ViewBag.CustomerAddress = "";
            ViewBag.CompanyName = "";
            ViewBag.CompanyContact = "";
            ViewBag.CompanyEmail = "";
            ViewBag.Website = "";
            return View();
        }
        #endregion

        public ActionResult OrderReport(CustomerManagement model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("OrderReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult SearchOrderReport(CustomerManagement model)
        {

            List<CustomerManagement> lst = new List<CustomerManagement>();
            model.OrderStatus = null;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet dsblock = model.GetDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                model.OrderNO = dsblock.Tables[0].Rows[0]["OrderNO"].ToString();
                model.OrderDate = dsblock.Tables[0].Rows[0]["OrderDate"].ToString();
                model.TotalAmount = dsblock.Tables[0].Rows[0]["TotalAmount"].ToString();
                model.OrderStatus = dsblock.Tables[0].Rows[0]["OrderStatus"].ToString();

                foreach (DataRow r in dsblock.Tables[0].Rows)
                {
                    CustomerManagement obj = new CustomerManagement();
                    obj.ProductID = r["FK_ProductID"].ToString();
                    obj.SizeID = r["FK_SizeID"].ToString();
                    obj.UnitID = r["FK_UnitID"].ToString();
                    obj.Quantity = r["Quantity"].ToString();
                    obj.Rate = r["Rate"].ToString();
                    obj.TotalAmount = r["Amount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.SizeName = r["SizeName"].ToString();
                    obj.UnitName = r["UnitName"].ToString();
                    obj.ColorID = r["FK_ColorID"].ToString();
                    obj.ColorName = r["ColorName"].ToString();
                    obj.OrderDetailsID = r["PK_OrderDetailsID"].ToString();
                    obj.OrderStatus = r["OrderStatus"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.OrderDate = r["OrderDate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.List = lst;
            }
            else
            {
                model.Result = "No record found !";
            }

            return View(model);
        }
    }
}
