using MSCLShopping.Models;
using Newtonsoft.Json;
using Razorpay.Api;
using ShoppingPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using static MSCLShopping.Models.Common;

namespace MSCLShopping.Controllers
{
    public class APIBaseController : Controller
    {
        #region Login
        public ActionResult Login1(Request model)
        {
            Login para = new Login();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<Login>(dcdata);

            if (para.MobileNo == "" || para.MobileNo == null)
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter MobileNo";

            }
            if (para.Password == "" || para.Password == null)
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter Password";

            }
            if (para.DeviceId == "" || para.DeviceId == null)
            {
                para.Status = "1";
                para.ErrorMessage = "Please Pass DeviceId";

            }
            if (para.FireBaseId == "" || para.FireBaseId == null)
            {
                para.Status = "1";
                para.ErrorMessage = "Please Pass FireBaseId";

            }
            try
            {
                para.Password = Crypto.Encrypt(para.Password);
                DataSet dsResult = para.LoginAction();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "2")
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();

                        try
                        {
                            string str2 = "Dear " + dsResult.Tables[0].Rows[0]["FirstName"].ToString() + ",Your OTP for Login is " + dsResult.Tables[0].Rows[0]["OTP"].ToString() + ", OTP is valid for 5 minute only";
                            BLSMS.SendSMSNew(para.MobileNo, str2);
                        }
                        catch (Exception ex) { }


                    }
                    else if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "3")
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();



                    }
                    else if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "4")
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();



                    }
                    else if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        if ((dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                        {

                            if (para.Password == (dsResult.Tables[0].Rows[0]["Password"].ToString()))
                            {
                                para.MobileNo = dsResult.Tables[0].Rows[0]["MobileNo"].ToString();
                                para.Pk_UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                                para.FirstName = dsResult.Tables[0].Rows[0]["FirstName"].ToString();
                                para.LastName = dsResult.Tables[0].Rows[0]["LastName"].ToString();
                                para.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                                para.Email = dsResult.Tables[0].Rows[0]["Email"].ToString();
                                para.IsPinCreated = dsResult.Tables[0].Rows[0]["IsPinCreated"].ToString();
                                para.ProfilePic = EncKey.ProfilePicURL + dsResult.Tables[0].Rows[0]["Profile"].ToString();
                                para.AssociateStatus = dsResult.Tables[0].Rows[0]["Status"].ToString();
                                para.Status = "0";
                            }
                            else
                            {
                                para.Status = "1";
                                para.ErrorMessage = "Invalid Password.";

                            }
                        }
                        else
                        {
                            para.Status = "1";
                            para.ErrorMessage = "Invalid LoginId and Password.";
                        }
                    }

                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "Invalid LoginId and Password.";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(Login));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion Login
        #region Registration
        public ActionResult GetStateCity(Request model)
        {
            GetStateCity para = new GetStateCity();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<GetStateCity>(dcdata);

            if (string.IsNullOrEmpty(para.PinCode))
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter Pincode";

            }

            try
            {

                DataSet dsResult = para.GetStateCityName();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.State = dsResult.Tables[0].Rows[0]["State"].ToString();
                        para.City = dsResult.Tables[0].Rows[0]["City"].ToString();
                        para.Status = "0";

                    }
                    else
                    {
                        //var url = "https://api.postalpincode.in/pincode/";
                        //url = url + para.PinCode;

                        //var CityResponse = _download_serialized_json_data<GetStateCityAPI>(url);
                        para.Status = "1";
                        para.ErrorMessage = "Invalid PinCode.";

                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "Invalid PinCode.";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetStateCity));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
        public ActionResult GetSponsorName(Request model)
        {
            GetSponsorName para = new GetSponsorName();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<GetSponsorName>(dcdata);

            if (string.IsNullOrEmpty(para.SponsorId))
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter SponsorId";

            }

            try
            {

                DataSet dsResult = para.GetSponsorDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.SponsorName = dsResult.Tables[0].Rows[0]["FullName"].ToString();

                        para.Status = "0";

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "Invalid Sponsor Id.";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetSponsorName));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Registration1(Request model)
        {
            Registration para = new Registration();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<Registration>(dcdata);

            if (string.IsNullOrEmpty(para.SponsorId))
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter SponsorId";

            }
            if (string.IsNullOrEmpty(para.FirstName))
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter First Name";

            }
            if (string.IsNullOrEmpty(para.MobileNo))
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter Mobile No";

            }
            try
            {
                para.Password = Crypto.Encrypt(para.Password);
                DataSet dsResult = para.SaveRegistration();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {



                        para.MobileNo = dsResult.Tables[0].Rows[0]["MobileNo"].ToString();
                        para.Pk_UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                        para.FirstName = dsResult.Tables[0].Rows[0]["FirstName"].ToString();
                        para.LastName = dsResult.Tables[0].Rows[0]["LastName"].ToString();
                        para.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                        para.Email = dsResult.Tables[0].Rows[0]["Email"].ToString();
                        para.IsPinCreated = dsResult.Tables[0].Rows[0]["IsPinCreated"].ToString();
                        para.ProfilePic = EncKey.ProfilePicURL + dsResult.Tables[0].Rows[0]["Profile"].ToString();
                        para.AssociateStatus = "Inactive";
                        para.Status = "0";
                        try
                        {
                            string str2 = BLSMS.Registration(para.FirstName + ' ' + para.LastName, para.MobileNo, Crypto.Decrypt(para.Password));
                            BLSMS.SendSMSNew(para.MobileNo, str2);
                        }
                        catch (Exception ex) { }

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Registration));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion Registration
        #region ChangePassord
        public ActionResult ChangePassword(Request model)
        {
            ChangePassword para = new ChangePassword();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<ChangePassword>(dcdata);

            if (string.IsNullOrEmpty(para.OldPassword))
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter Old Password";

            }
            if (string.IsNullOrEmpty(para.NewPassword))
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter New Password";

            }
            if (string.IsNullOrEmpty(para.PasswordType))
            {
                para.Status = "1";
                para.ErrorMessage = "Please select Password Type";

            }
            try
            {
                para.OldPassword = Crypto.Encrypt(para.OldPassword);
                para.NewPassword = Crypto.Encrypt(para.NewPassword);
                DataSet dsResult = para.ChngPass();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ChangePassword));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion ChangePassword
        #region Terms&Condition
        public ActionResult TermsCondition(Request model)
        {
            TermsCondition obj = new TermsCondition();
            List<TermsData> datalist = new List<TermsData>();
            APIResponse objApiResponse = new APIResponse();


            try
            {

                DataSet dsResult = obj.GetTermsCondition();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstTermsList = datalist;


                        }
                        List<TermsListDetails> objterms = new List<TermsListDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new TermsListDetails

                                {


                                    Termscondition = row1["Termscondition"].ToString(),



                                });
                            }
                            datalist.Add(new TermsData
                            {
                                Title = "Terms & Conditions Details",
                                TermsListDetails = objterms

                            });

                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TermsCondition));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion Terms&Condition
        #region Pay
        public ActionResult GetDWPayBalance(Request model)
        {
            GetBalance para = new GetBalance();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<GetBalance>(dcdata);


            try
            {

                DataSet dsResult = para.GetBalanceDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";
                        para.Balance = dsResult.Tables[0].Rows[0]["Balance"].ToString();

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetBalance));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckMobileNo(Request model)
        {
            CheckMobile para = new CheckMobile();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<CheckMobile>(dcdata);


            try
            {

                DataSet dsResult = para.GetDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";
                        para.Name = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                        para.Pk_UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CheckMobile));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pay(Request model)
        {
            Pay para = new Pay();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<Pay>(dcdata);


            try
            {

                DataSet dsResult = para.MSCLShoppingAPITransfer();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";


                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Pay));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion Pay
        #region ShoppingPoint
        public ActionResult ShoppingLedger(Request model)
        {
            ShoppingLedger obj = new ShoppingLedger();
            List<ShoppingLedger> datalist = new List<ShoppingLedger>();

            List<LedgerDetails> datalist1 = new List<LedgerDetails>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<ShoppingLedger>(dcdata);

            try
            {
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");


                DataSet dsResult = obj.GetShoppingLedger();
                if (dsResult != null && dsResult.Tables[1].Rows.Count > 0)
                {
                    if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        obj.TotalCredit = dsResult.Tables[0].Rows[0]["TotalCredited"].ToString();
                        obj.TotalDebit = dsResult.Tables[0].Rows[0]["TotalDebited"].ToString();
                        obj.TotalBalance = dsResult.Tables[0].Rows[0]["Balance"].ToString();
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {


                            obj.lstledger = datalist1;


                        }
                        List<LedgerDetails> objterms = new List<LedgerDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objterms.Add(new LedgerDetails

                                {


                                    TransactionDate = row1["CurrentDate"].ToString(),
                                    Narration = row1["Narration"].ToString(),
                                    CrAmount = row1["CrAmount"].ToString(),
                                    DrAmount = row1["DrAmount"].ToString(),
                                    Balance = row1["Balance"].ToString(),



                                });
                            }
                            datalist.Add(new ShoppingLedger
                            {

                                lstledger = objterms

                            });
                            obj.lstledger = datalist[0].lstledger;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ShoppingLedger));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion ShoppingPoint
        #region DirectList
        public ActionResult DirectList(Request model)
        {
            DirectList obj = new DirectList();
            List<DirectData> datalist = new List<DirectData>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<DirectList>(dcdata);

            try
            {

                DataSet dsResult = obj.GetDirectDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstdirectlist = datalist;


                        }
                        List<DircetDetails> objterms = new List<DircetDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new DircetDetails

                                {


                                    LoginId = row1["LoginId"].ToString(),
                                    AssociateName = row1["AssociateName"].ToString(),
                                    Mobile = row1["Mobile"].ToString(),
                                    ParentLoginId = row1["ParentLoginId"].ToString(),
                                    ParentName = row1["ParentName"].ToString(),
                                    Status = row1["Status"].ToString(),
                                    JoiningDate = row1["JoiningDate"].ToString(),
                                    Level = row1["Level"].ToString(),
                                    RefferBy = row1["RefferBy"].ToString(),
                                    RefferByName = row1["RefferByName"].ToString(),


                                });
                            }
                            datalist.Add(new DirectData
                            {
                                Title = "Direct Member",
                                DircetDetails = objterms

                            });

                        }
                    }
                    else
                    {
                        obj.Status = "1";
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DirectList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion DirectList
        #region DownlineList
        public ActionResult DownlineList(Request model)
        {
            DirectList obj = new DirectList();
            List<DirectData> datalist = new List<DirectData>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<DirectList>(dcdata);

            try
            {

                DataSet dsResult = obj.GetDownlineDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstdirectlist = datalist;


                        }
                        List<DircetDetails> objterms = new List<DircetDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new DircetDetails

                                {


                                    LoginId = row1["LoginId"].ToString(),
                                    AssociateName = row1["AssociateName"].ToString(),
                                    Mobile = row1["Mobile"].ToString(),
                                    ParentLoginId = row1["ParentLoginId"].ToString(),
                                    ParentName = row1["ParentName"].ToString(),
                                    Status = row1["Status"].ToString(),
                                    RefferBy = row1["RefferBy"].ToString(),
                                    RefferByName = row1["RefferByName"].ToString(),
                                    JoiningDate = row1["JoiningDate"].ToString(),

                                });
                            }
                            datalist.Add(new DirectData
                            {
                                Title = "Downline Member",
                                DircetDetails = objterms

                            });

                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(DirectList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion DownlineList
        #region SendOTP
        public ActionResult SendOTP(Request model)
        {
            SendOTP para = new SendOTP();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<SendOTP>(dcdata);

            if (para.MobileNo == "" || para.MobileNo == null)
            {
                para.Status = "1";
                para.ErrorMessage = "Please Enter MobileNo";

            }

            try
            {

                DataSet dsResult = para.GetSendOTP();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        try
                        {
                            string str2 = "Dear User,Your OTP is " + dsResult.Tables[0].Rows[0]["OTP"].ToString() + ", OTP is valid for 5 minute only";
                            BLSMS.SendSMSNew(para.MobileNo, str2);
                        }
                        catch (Exception ex) { }
                        para.Status = "0";
                        para.OTP = dsResult.Tables[0].Rows[0]["OTP"].ToString();

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";

                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(SendOTP));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion SendOTP
        #region DashBoard
        public ActionResult DashBoard(Request model)
        {
            DashBoardData para = new DashBoardData();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<DashBoardData>(dcdata);


            try
            {

                DataSet dsResult = para.GetBusinessDashboard();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                        para.Name = dsResult.Tables[0].Rows[0]["Name"].ToString();
                        para.DOJ = dsResult.Tables[0].Rows[0]["DOJ"].ToString();
                        para.Mobile = dsResult.Tables[0].Rows[0]["Mobile"].ToString();
                        para.UnclearedBalance = dsResult.Tables[0].Rows[0]["UnclearedBalance"].ToString();
                        para.TeamSize = dsResult.Tables[0].Rows[0]["TeamSize"].ToString();
                        para.Commission = dsResult.Tables[0].Rows[0]["Commission"].ToString();
                        para.KYCStatus = dsResult.Tables[0].Rows[0]["KYCStatus"].ToString();
                        para.LastLogin = dsResult.Tables[0].Rows[0]["LastLogin"].ToString();
                        para.DOA = dsResult.Tables[0].Rows[0]["DOA"].ToString();
                        para.ProfilePic = EncKey.ProfilePicURL + dsResult.Tables[0].Rows[0]["ProfilePic"].ToString();
                        para.TodaysPrimeId = dsResult.Tables[0].Rows[0]["TodaysPrimeId"].ToString();
                        para.TodaysId = dsResult.Tables[0].Rows[0]["TodaysId"].ToString();
                        para.Status = "0";
                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = "Invalid.";
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(DashBoardData));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion DashBoard
        #region ViewProfile
        public ActionResult ViewProfile(Request model)
        {
            ViewProfile para = new ViewProfile();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<ViewProfile>(dcdata);


            try
            {

                DataSet dsResult = para.GetProfileData();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.FirstName = dsResult.Tables[0].Rows[0]["FirstName"].ToString();
                        para.LastName = dsResult.Tables[0].Rows[0]["LastName"].ToString();
                        para.DOB = dsResult.Tables[0].Rows[0]["DOB"].ToString();
                        para.Sex = dsResult.Tables[0].Rows[0]["Sex"].ToString();
                        para.MarritalStatus = dsResult.Tables[0].Rows[0]["MarritalStatus"].ToString();
                        para.Email = dsResult.Tables[0].Rows[0]["Email"].ToString();
                        para.NomineName = dsResult.Tables[0].Rows[0]["NomineName"].ToString();
                        para.NomineRealation = dsResult.Tables[0].Rows[0]["NomineRealation"].ToString();
                        para.AccountHolderName = dsResult.Tables[0].Rows[0]["BankHolderName"].ToString();
                        para.MemberAccNo = dsResult.Tables[0].Rows[0]["MemberAccNo"].ToString();
                        para.MemberBankName = dsResult.Tables[0].Rows[0]["MemberBankName"].ToString();
                        para.MemberBranch = dsResult.Tables[0].Rows[0]["MemberBranch"].ToString();
                        para.IFSCCode = dsResult.Tables[0].Rows[0]["IFSCCode"].ToString();

                        para.InsuranceNumber = dsResult.Tables[0].Rows[0]["InsuranceNumber"].ToString();

                        para.PolicyHolderName = dsResult.Tables[0].Rows[0]["PolicyHolderName"].ToString();
                        para.Premium = dsResult.Tables[0].Rows[0]["Premium"].ToString();
                        para.CompanyName = dsResult.Tables[0].Rows[0]["CompanyName"].ToString();
                        para.NomineeName = dsResult.Tables[0].Rows[0]["NomineName"].ToString();
                        para.InsuranceType = dsResult.Tables[0].Rows[0]["InsuranceType"].ToString();
                        para.PolicyNumber = dsResult.Tables[0].Rows[0]["PolicyNumber"].ToString();
                        para.VechicleNumber = dsResult.Tables[0].Rows[0]["VechicleNumber"].ToString();
                        para.PurchaedYear = dsResult.Tables[0].Rows[0]["PurchaedYear"].ToString();
                        para.GeneralInsPremium = dsResult.Tables[0].Rows[0]["GeneralInsPremium"].ToString();
                        para.ExpiryDate = dsResult.Tables[0].Rows[0]["ExpiryDate"].ToString();
                        para.GeneralCompanyName = dsResult.Tables[0].Rows[0]["GeneralCompanyName"].ToString();
                        para.FathersName = dsResult.Tables[0].Rows[0]["FathersName"].ToString();
                        para.MothersName = dsResult.Tables[0].Rows[0]["MothersName"].ToString();
                        para.Address = dsResult.Tables[0].Rows[0]["Address"].ToString();
                        para.PinCode = dsResult.Tables[0].Rows[0]["Pincode"].ToString();
                        para.State = dsResult.Tables[0].Rows[0]["statename"].ToString();
                        para.City = dsResult.Tables[0].Rows[0]["Districtname"].ToString();
                        para.PanNo = dsResult.Tables[0].Rows[0]["PanNumber"].ToString();
                        para.InsNomineeName = dsResult.Tables[0].Rows[0]["NomineeName"].ToString();
                        para.cancelchequepic = dsResult.Tables[0].Rows[0]["CanceledChequepic"].ToString();
                        para.profilepic = dsResult.Tables[0].Rows[0]["NomineeName"].ToString();
                        para.Status = "0";
                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = "Invalid.";
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(ViewProfile));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion ViewProfile
        #region Logout
        public ActionResult Logout(Request model)
        {
            Logout para = new Logout();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<Logout>(dcdata);


            try
            {

                DataSet dsResult = para.LogoutUser();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.ErrorMessage = "Logout Successfully.";

                        para.Status = "0";
                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = "Invalid.";
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(Logout));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion Logout
        #region TeamStatus
        public ActionResult TeamStatus(Request model)
        {
            TeamStatus obj = new TeamStatus();
            List<TeamStatus> datalist = new List<TeamStatus>();
            List<TeamStatusData> datalist1 = new List<TeamStatusData>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<TeamStatus>(dcdata);

            try
            {

                DataSet dsResult = obj.GetTeamStatusDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        obj.TotalUpgradedApp = dsResult.Tables[0].Rows[0]["TotalUpgradedApp"].ToString();
                        obj.TotalNonUpgradedApp = dsResult.Tables[0].Rows[0]["TotalNonUpgradedApp"].ToString();
                        obj.TotalApp = dsResult.Tables[0].Rows[0]["TotalApp"].ToString();
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {


                            obj.lstteamstatus = datalist1;


                        }
                        List<TeamStatusData> objterms = new List<TeamStatusData>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objterms.Add(new TeamStatusData

                                {


                                    Level = row1["Lvl"].ToString(),
                                    UpgradedApp = row1["UpgradedApp"].ToString(),
                                    NonUpgradedApp = row1["NonUpgradedApp"].ToString(),



                                });
                            }
                            datalist.Add(new TeamStatus
                            {
                                Title = "Team Status",
                                lstteamstatus = objterms

                            });

                        }
                        obj.lstteamstatus = datalist[0].lstteamstatus;
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TeamStatus));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion TeamStatus
        #region AutoLogin
        public ActionResult AutoLogin(Request model)
        {
            Login para = new Login();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<Login>(dcdata);


            if (para.DeviceId == "" || para.DeviceId == null)
            {
                para.Status = "1";
                para.ErrorMessage = "Please Pass DeviceId";

            }
            if (para.FireBaseId == "" || para.FireBaseId == null)
            {
                para.Status = "1";
                para.ErrorMessage = "Please Pass FireBaseId";

            }
            try
            {

                DataSet dsResult = para.AutoLoginAction();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {

                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        if ((dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                        {

                            para.MobileNo = dsResult.Tables[0].Rows[0]["MobileNo"].ToString();
                            para.Pk_UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                            para.FirstName = dsResult.Tables[0].Rows[0]["FirstName"].ToString();
                            para.LastName = dsResult.Tables[0].Rows[0]["LastName"].ToString();
                            para.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                            para.Email = dsResult.Tables[0].Rows[0]["Email"].ToString();
                            para.IsPinCreated = dsResult.Tables[0].Rows[0]["IsPinCreated"].ToString();
                            para.ProfilePic = EncKey.ProfilePicURL + dsResult.Tables[0].Rows[0]["Profile"].ToString();
                            para.AssociateStatus = dsResult.Tables[0].Rows[0]["Status"].ToString();
                            para.Status = "0";

                        }
                        else
                        {
                            para.Status = "1";
                            para.ErrorMessage = "Invalid LoginId and Password.";
                        }
                    }

                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "Invalid LoginId and Password.";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(Login));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion Login
        #region ForgetPass
        public ActionResult ForgetPass(Request model)
        {
            ForgetPass para = new ForgetPass();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<ForgetPass>(dcdata);



            try
            {
                para.Password = Crypto.Encrypt(para.Password);
                DataSet dsResult = para.ForgetPassword();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {

                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.Status = "0";

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else
                {
                    para.Status = "1";

                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(ForgetPass));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion ForgetPass
        #region GetLevelWiseList 
        public ActionResult GetLevelWiseList(Request model)
        {
            LevelList obj = new LevelList();
            List<LevelList> datalist = new List<LevelList>();
            List<LevelListData> datalist1 = new List<LevelListData>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<LevelList>(dcdata);

            try
            {

                DataSet dsResult = obj.GetLevelWiseList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";

                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstlevellist = datalist1;


                        }
                        List<LevelListData> objterms = new List<LevelListData>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new LevelListData

                                {


                                    Pk_UserId = row1["Fk_UserId"].ToString(),
                                    Name = row1["Name"].ToString(),
                                    TotalTeam = row1["TotalTeam"].ToString(),
                                    LoginId = row1["RefferalCode"].ToString(),


                                });
                            }
                            datalist.Add(new LevelList
                            {

                                lstlevellist = objterms

                            });

                        }
                        obj.lstlevellist = datalist[0].lstlevellist;
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(LevelList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion GetLevelWiseList 
        #region UpdateProfile
        public ActionResult UpdateProfile(Request model)
        {
            ViewProfile para = new ViewProfile();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<ViewProfile>(dcdata);


            try
            {
                para.DOB = string.IsNullOrEmpty(para.DOB) ? null : Common.ConvertToSystemDate(para.DOB, "dd/MM/yyyy");
                para.ExpiryDate = string.IsNullOrEmpty(para.ExpiryDate) ? null : Common.ConvertToSystemDate(para.ExpiryDate, "dd/MM/yyyy");
                DataSet dsResult = para.UpdateProfile();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ViewProfile));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        #endregion UpdateProfile
        #region PassBook
        public ActionResult PassBook(Request model)
        {
            PassBook para = new PassBook();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<PassBook>(dcdata);


            try
            {

                DataSet dsResult = para.PassBookDetails();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.Status = "0";
                        para.MSCLShoppingAPIBalance = dsResult.Tables[0].Rows[0]["MSCLShoppingAPIBalance"].ToString();
                        para.ShoppingPoint = dsResult.Tables[0].Rows[0]["ShoppingPoint"].ToString();
                        para.Commission = dsResult.Tables[0].Rows[0]["Commission"].ToString();
                        para.Hold = dsResult.Tables[0].Rows[0]["Hold"].ToString();
                        para.Unclear = dsResult.Tables[0].Rows[0]["Unclear"].ToString();
                        para.PIN = Crypto.Decrypt(dsResult.Tables[0].Rows[0]["PIN"].ToString());

                    }
                    else
                    {
                        para.Status = "1";

                    }

                }
                else
                {
                    para.Status = "1";

                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(PassBook));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion PassBook
        #region DWPayLedger
        public ActionResult DWPayLedger(Request model)
        {
            ShoppingLedger obj = new ShoppingLedger();
            List<ShoppingLedger> datalist = new List<ShoppingLedger>();

            List<LedgerDetails> datalist1 = new List<LedgerDetails>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<ShoppingLedger>(dcdata);

            try
            {
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");

                DataSet dsResult = obj.GetDWPayLedger();
                if (dsResult != null && dsResult.Tables[1].Rows.Count > 0)
                {
                    if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        obj.TotalCredit = dsResult.Tables[0].Rows[0]["TotalCredited"].ToString();
                        obj.TotalDebit = dsResult.Tables[0].Rows[0]["TotalDebited"].ToString();
                        obj.TotalBalance = dsResult.Tables[0].Rows[0]["Balance"].ToString();
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {


                            obj.lstledger = datalist1;


                        }
                        List<LedgerDetails> objterms = new List<LedgerDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objterms.Add(new LedgerDetails

                                {


                                    TransactionDate = row1["CurrentDate"].ToString(),
                                    Narration = row1["Narration"].ToString(),
                                    CrAmount = row1["CrAmount"].ToString(),
                                    DrAmount = row1["DrAmount"].ToString(),
                                    Status = row1["Status"].ToString(),
                                    TransactionType = row1["TransactionType"].ToString(),
                                    TransactionNo = row1["TransactionNo"].ToString(),
                                    Balance = row1["Balance"].ToString(),



                                });
                            }
                            datalist.Add(new ShoppingLedger
                            {

                                lstledger = objterms

                            });
                            obj.lstledger = datalist[0].lstledger;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ShoppingLedger));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion DWPayLedger
        #region CommissionLedger
        public ActionResult CommissionLedger(Request model)
        {
            CommissionLedger obj = new CommissionLedger();
            List<CommissionLedger> datalist = new List<CommissionLedger>();

            List<CommissionDetails> datalist1 = new List<CommissionDetails>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<CommissionLedger>(dcdata);

            try
            {
                obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");
                obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");

                DataSet dsResult = obj.GetCommissionLedger();
                if (dsResult != null && dsResult.Tables[1].Rows.Count > 0)
                {
                    if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.TotalCredit = dsResult.Tables[0].Rows[0]["TotalCredited"].ToString();
                        obj.TotalDebit = dsResult.Tables[0].Rows[0]["TotalDebited"].ToString();
                        obj.TotalBalance = dsResult.Tables[0].Rows[0]["Balance"].ToString();
                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {


                            obj.lstledger = datalist1;


                        }
                        List<CommissionDetails> objterms = new List<CommissionDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objterms.Add(new CommissionDetails

                                {


                                    TransactionDate = row1["CurrentDate"].ToString(),
                                    Narration = row1["Narration"].ToString(),
                                    CrAmount = row1["CrAmount"].ToString(),
                                    DrAmount = row1["DrAmount"].ToString(),
                                    Balance = row1["Balance"].ToString(),




                                });
                            }
                            datalist.Add(new CommissionLedger
                            {

                                lstledger = objterms

                            });
                            obj.lstledger = datalist[0].lstledger;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CommissionLedger));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion CommissionLedger
        #region GetUnclearWallet

        public ActionResult GetUnclearWallet(Request model)
        {
            Wallet para = new Wallet();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<Wallet>(dcdata);


            try
            {

                DataSet dsResult = para.GetUnclearWallet();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.Status = "0";
                        para.SelfIncome = dsResult.Tables[0].Rows[0]["SelfIncome"].ToString();
                        para.FirstLevelIncome = dsResult.Tables[0].Rows[0]["FirstLevelIncome"].ToString();
                        para.SecondLevelIncome = dsResult.Tables[0].Rows[0]["SecondLevelIncome"].ToString();
                        para.ThirdLevelIncome = dsResult.Tables[0].Rows[0]["thirdLevelIncome"].ToString();
                        para.FourthLevelIncome = dsResult.Tables[0].Rows[0]["fourthLevelIncome"].ToString();
                        para.FifthLevelIncome = dsResult.Tables[0].Rows[0]["fifthLevelIncome"].ToString();
                        para.SixthLevelIncome = dsResult.Tables[0].Rows[0]["sixthLevelIncome"].ToString();
                        para.SeventhLevelIncome = dsResult.Tables[0].Rows[0]["seventhLevelIncome"].ToString();
                        para.EightLevelIncome = dsResult.Tables[0].Rows[0]["EightLevelIncome"].ToString();

                        para.SelfOrder = dsResult.Tables[0].Rows[0]["SelfOrder"].ToString();
                        para.FirstLevelOrder = dsResult.Tables[0].Rows[0]["FirstLevelOrder"].ToString();
                        para.SecondLevelOrder = dsResult.Tables[0].Rows[0]["secondLevelOrder"].ToString();
                        para.ThirdLevelOrder = dsResult.Tables[0].Rows[0]["thirdLevelOrder"].ToString();
                        para.FourthLevelOrder = dsResult.Tables[0].Rows[0]["fourthLevelOrder"].ToString();
                        para.FifthLevelOrder = dsResult.Tables[0].Rows[0]["fifthLevelOrder"].ToString();
                        para.SixthLevelOrder = dsResult.Tables[0].Rows[0]["sixthLevelOrder"].ToString();
                        para.SeventhLeveOrder = dsResult.Tables[0].Rows[0]["seventhLeveOrder"].ToString();
                        para.EightLeveOrder = dsResult.Tables[0].Rows[0]["EightLevelOrder"].ToString();

                        para.SelfOrdervalue = dsResult.Tables[0].Rows[0]["SelfOrdervalue"].ToString();
                        para.FirstLevelOrdervalue = dsResult.Tables[0].Rows[0]["FirstLevelOrdervalue"].ToString();
                        para.SecondLevelOrdervalue = dsResult.Tables[0].Rows[0]["secondLevelOrdervalue"].ToString();
                        para.ThirdLevelOrdervalue = dsResult.Tables[0].Rows[0]["thirdLevelOrdervalue"].ToString();
                        para.FourthLevelOrdervalue = dsResult.Tables[0].Rows[0]["fourthLevelOrdervalue"].ToString();
                        para.FifthLevelOrdervalue = dsResult.Tables[0].Rows[0]["fifthLevelOrdervalue"].ToString();
                        para.SixthLevelOrdervalue = dsResult.Tables[0].Rows[0]["sixthLevelOrdervalue"].ToString();
                        para.SeventhLeveOrdervalue = dsResult.Tables[0].Rows[0]["seventhLeveOrdervalue"].ToString();
                        para.RefferIncome = dsResult.Tables[0].Rows[0]["RefferIncome"].ToString();
                        para.EightLeveOrdervalue = dsResult.Tables[0].Rows[0]["EightLeveOrdervalue"].ToString();

                    }
                    else
                    {
                        para.Status = "1";

                    }

                }
                else
                {
                    para.Status = "1";

                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(Wallet));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        #endregion GetUnclearWallet
        #region GetCommissionWallet

        public ActionResult GetCommissionWallet(Request model)
        {
            Wallet para = new Wallet();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<Wallet>(dcdata);


            try
            {

                DataSet dsResult = para.GetCommissionWallet();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.Status = "0";
                        para.SelfIncome = dsResult.Tables[0].Rows[0]["SelfIncome"].ToString();
                        para.FirstLevelIncome = dsResult.Tables[0].Rows[0]["FirstLevelIncome"].ToString();
                        para.SecondLevelIncome = dsResult.Tables[0].Rows[0]["SecondLevelIncome"].ToString();
                        para.ThirdLevelIncome = dsResult.Tables[0].Rows[0]["thirdLevelIncome"].ToString();
                        para.FourthLevelIncome = dsResult.Tables[0].Rows[0]["fourthLevelIncome"].ToString();
                        para.FifthLevelIncome = dsResult.Tables[0].Rows[0]["fifthLevelIncome"].ToString();
                        para.SixthLevelIncome = dsResult.Tables[0].Rows[0]["sixthLevelIncome"].ToString();
                        para.SeventhLevelIncome = dsResult.Tables[0].Rows[0]["seventhLevelIncome"].ToString();
                        para.EightLevelIncome = dsResult.Tables[0].Rows[0]["EightLevelIncome"].ToString();

                        para.SelfOrder = dsResult.Tables[0].Rows[0]["SelfOrder"].ToString();
                        para.FirstLevelOrder = dsResult.Tables[0].Rows[0]["FirstLevelOrder"].ToString();
                        para.SecondLevelOrder = dsResult.Tables[0].Rows[0]["secondLevelOrder"].ToString();
                        para.ThirdLevelOrder = dsResult.Tables[0].Rows[0]["thirdLevelOrder"].ToString();
                        para.FourthLevelOrder = dsResult.Tables[0].Rows[0]["fourthLevelOrder"].ToString();
                        para.FifthLevelOrder = dsResult.Tables[0].Rows[0]["fifthLevelOrder"].ToString();
                        para.SixthLevelOrder = dsResult.Tables[0].Rows[0]["sixthLevelOrder"].ToString();
                        para.SeventhLeveOrder = dsResult.Tables[0].Rows[0]["seventhLeveOrder"].ToString();
                        para.EightLeveOrder = dsResult.Tables[0].Rows[0]["EightLeveOrder"].ToString();

                        para.SelfOrdervalue = dsResult.Tables[0].Rows[0]["SelfOrdervalue"].ToString();
                        para.FirstLevelOrdervalue = dsResult.Tables[0].Rows[0]["FirstLevelOrdervalue"].ToString();
                        para.SecondLevelOrdervalue = dsResult.Tables[0].Rows[0]["secondLevelOrdervalue"].ToString();
                        para.ThirdLevelOrdervalue = dsResult.Tables[0].Rows[0]["thirdLevelOrdervalue"].ToString();
                        para.FourthLevelOrdervalue = dsResult.Tables[0].Rows[0]["fourthLevelOrdervalue"].ToString();
                        para.FifthLevelOrdervalue = dsResult.Tables[0].Rows[0]["fifthLevelOrdervalue"].ToString();
                        para.SixthLevelOrdervalue = dsResult.Tables[0].Rows[0]["sixthLevelOrdervalue"].ToString();
                        para.SeventhLeveOrdervalue = dsResult.Tables[0].Rows[0]["seventhLeveOrdervalue"].ToString();
                        para.EightLeveOrdervalue = dsResult.Tables[0].Rows[0]["EightLeveOrdervalue"].ToString();

                        para.TotalCleared = dsResult.Tables[0].Rows[0]["TotalCleared"].ToString();
                        para.TotalUncleared = dsResult.Tables[0].Rows[0]["TotalUncleared"].ToString();
                        para.ClearedRelease = dsResult.Tables[0].Rows[0]["ClearedRelease"].ToString();
                        para.ClearedHold = dsResult.Tables[0].Rows[0]["ClearedHold"].ToString();
                        para.Hold = dsResult.Tables[0].Rows[0]["Hold"].ToString();
                        para.TDS = dsResult.Tables[0].Rows[0]["TDS"].ToString();
                        para.TransferToBank = dsResult.Tables[0].Rows[0]["TransferToBank"].ToString();

                        para.RefferIncome = dsResult.Tables[0].Rows[0]["RefferIncome"].ToString();

                    }
                    else
                    {
                        para.Status = "1";

                    }

                }
                else
                {
                    para.Status = "1";

                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;

            js = new DataContractJsonSerializer(typeof(Wallet));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        #endregion GetCommissionWallet
        #region SupportList
        public ActionResult SupportList(Request model)
        {
            SupportList obj = new SupportList();
            List<SupportList> datalist = new List<SupportList>();

            List<SupportDetails> datalist1 = new List<SupportDetails>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<SupportList>(dcdata);

            try
            {

                DataSet dsResult = obj.GetSupportList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstsupport = datalist1;


                        }
                        List<SupportDetails> objterms = new List<SupportDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new SupportDetails

                                {


                                    TicketNo = row1["TicketNo"].ToString(),
                                    Subject = row1["Subject"].ToString(),
                                    Message = row1["Message"].ToString(),
                                    Attachmenturl = row1["Attachmenturl"].ToString(),
                                    TicketStatus = row1["TicketStatus"].ToString(),

                                    Reply = row1["Reply"].ToString(),
                                    ReplyDate = row1["ReplyDate"].ToString(),

                                });
                            }
                            datalist.Add(new SupportList
                            {

                                lstsupport = objterms

                            });
                            obj.lstsupport = datalist[0].lstsupport;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(SupportList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion SupportList
        #region CreatePin
        public ActionResult CreatePin(Request model)
        {
            CreatePin para = new CreatePin();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<CreatePin>(dcdata);


            try
            {
                para.PIN = Crypto.Encrypt(para.PIN);
                DataSet dsResult = para.GeneratePin();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        para.PIN = Crypto.Decrypt(para.PIN);
                        para.Status = "0";

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CreatePin));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        #endregion CreatePin
        #region CreateOrder
        public ActionResult CreateOrder(Request model)
        {
            CreateOrder obj = new CreateOrder();

            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<CreateOrder>(dcdata);

            CreateOrderResponse obj1 = new CreateOrderResponse();
            string random = Common.GenerateRandom();
            try
            {

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", obj.amount); // amount in the smallest currency unit
                options.Add("receipt", random);
                options.Add("currency", "INR");
                options.Add("payment_capture", "1");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                Razorpay.Api.Order order = client.Order.Create(options);
                obj1.OrderId = order["id"].ToString();
                obj1.Status = "0";
                obj.OrderId = order["id"].ToString();
                DataSet ds = obj.SaveOrderDetails();

            }
            catch (Exception ex)
            {
                obj1.Status = "1";
                obj1.ErrorMessage = ex.Message;
            }



            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CreateOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, obj1);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        public static HttpWebRequest GetCreateOrderURL()
        {
            var url = PaymentGateWayDetails.CreateOrder;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion CreateOrder
        #region FetchPaymentByOrder
        public ActionResult FetchPaymentByOrder(Request model)
        {
            APIResponse objApiResponse = new APIResponse();
            FetchPaymnetByOrder obj = new FetchPaymnetByOrder();


            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<FetchPaymnetByOrder>(dcdata);

            FetchPaymentByOrderResponse obj1 = new FetchPaymentByOrderResponse();
            string random = Common.GenerateRandom();
            try
            {


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);

                List<Razorpay.Api.Payment> orderdetails = client.Order.Payments(obj.OrderId);
                if (orderdetails.Count > 0)
                {
                    for (int i = 0; i <= orderdetails.Count - 1; i++)
                    {
                        dynamic rr = orderdetails[i].Attributes;
                        obj1.PaymentId = rr["id"];
                        obj1.entity = rr["entity"];
                        obj1.amount = rr["amount"];
                        obj1.currency = rr["currency"];
                        obj1.status = rr["status"];
                        obj1.OrderId = rr["order_id"];
                        obj1.invoice_id = rr["invoice_id"];
                        obj1.international = rr["international"];
                        obj1.method = rr["method"];
                        obj1.amount_refunded = rr["amount_refunded"];
                        obj1.refund_status = rr["refund_status"];
                        obj1.captured = rr["captured"];
                        obj1.description = rr["description"];
                        obj1.card_id = rr["card_id"];
                        obj1.bank = rr["bank"];
                        obj1.wallet = rr["wallet"];
                        obj1.vpa = rr["vpa"];
                        obj1.email = rr["email"];
                        obj1.contact = rr["contact"];
                        obj1.fee = rr["fee"];
                        obj1.tax = rr["tax"];
                        obj1.error_code = rr["error_code"];
                        obj1.error_description = rr["error_description"];
                        obj1.error_source = rr["error_source"];
                        obj1.error_step = rr["error_step"];
                        obj1.error_reason = rr["error_reason"];
                        obj1.created_at = rr["created_at"];
                        obj1.Pk_UserId = obj.Pk_UserId;
                        DataSet ds = obj1.SaveFetchPaymentResponse();



                    }
                }

                else
                {
                    obj1.OrderId = obj.OrderId;
                    obj1.captured = "Failed";
                    obj1.Pk_UserId = obj.Pk_UserId;
                    DataSet ds = obj1.SaveFetchPaymentResponse();

                }



            }
            catch (Exception ex)
            {
                obj1.OrderId = obj.OrderId;
                obj1.captured = ex.Message;
                obj1.Pk_UserId = obj.Pk_UserId;
                DataSet ds = obj1.SaveFetchPaymentResponse();

            }



            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(FetchPaymentByOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, obj1);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }

        public static HttpWebRequest FetchPaymentByOrderURL(string orderid)
        {
            var url = PaymentGateWayDetails.FetchPaymentByOrderURL + "" + orderid + "/payments";
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion FetchPaymentByOrder
        #region FetchPayment
        public ActionResult CapturePayment(Request model)
        {
            FetchPaymnet obj = new FetchPaymnet();

            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<FetchPaymnet>(dcdata);

            CreateOrderResponse obj1 = new CreateOrderResponse();

            try
            {

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", obj.amount); // amount in the smallest currency unit
                options.Add("currency", obj.currency);


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                Razorpay.Api.Payment payment = client.Payment.Capture(options);
                obj1.Status = "0";
            }
            catch (Exception ex)
            {
                obj1.Status = "1";
            }



            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CreateOrderResponse));
            ms = new MemoryStream();
            js.WriteObject(ms, obj1);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        public static HttpWebRequest GetCapturePaymentURL(string PaymentId)
        {
            var url = PaymentGateWayDetails.CapturePayment + "" + PaymentId + "/capture";
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        #endregion FetchPayment
        #region UpdateId
        public ActionResult ActivateId(Request model)
        {
            UpgradeId para = new UpgradeId();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<UpgradeId>(dcdata);


            try
            {

                DataSet dsResult = para.ActivateId();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";

                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UpgradeId));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion UpdateId
        #region ProductList
        public ActionResult ProductList(Request model)
        {
            ProductList obj = new ProductList();
            List<ProductList> datalist = new List<ProductList>();

            List<ProductDetails> datalist1 = new List<ProductDetails>();
            List<BannerDetails> datalist2 = new List<BannerDetails>();
            APIResponse objApiResponse = new APIResponse();

            try
            {

                DataSet dsResult = obj.GetProductList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstproduct = datalist1;
                            obj.lstbanner = datalist2;


                        }
                        List<ProductDetails> objterms = new List<ProductDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new ProductDetails

                                {


                                    ProductName = row1["ProductName"].ToString(),
                                    Pk_ProductId = row1["Pk_ProductId"].ToString(),
                                    MRP = row1["MRP"].ToString(),
                                    OfferedPrice = row1["OfferedPrice"].ToString(),
                                    Images = EncKey.ShoppingProfilePicURL + row1["Images"].ToString(),
                                    Discount = row1["Discount"].ToString(),



                                });
                            }
                            datalist.Add(new ProductList
                            {

                                lstproduct = objterms

                            });
                            List<BannerDetails> objterms1 = new List<BannerDetails>();
                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objterms1.Add(new BannerDetails

                                {


                                    BannerImage = EncKey.ShoppingProfilePicURL + row1["BannerImage"].ToString(),



                                });
                            }
                            datalist.Add(new ProductList
                            {

                                lstbanner = objterms1

                            });

                            obj.lstproduct = datalist[0].lstproduct;
                            obj.lstbanner = datalist[1].lstbanner;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion ProductList
        #region GetOrderValue
        public ActionResult GetOrderValue(Request model)
        {
            OrderValue obj = new OrderValue();
            List<OrderValue> datalist = new List<OrderValue>();

            List<OrderValueDetails> datalist1 = new List<OrderValueDetails>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<OrderValue>(dcdata);

            try
            {


                DataSet dsResult = obj.GetOrderValue();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.Orders = dsResult.Tables[0].Rows[0]["Orders"].ToString();
                        obj.Value = dsResult.Tables[0].Rows[0]["Value"].ToString();
                        obj.TotalBusiness = dsResult.Tables[0].Rows[0]["TotalBusiness"].ToString();
                        obj.TotalEarnings = dsResult.Tables[0].Rows[0]["TotalEarnings"].ToString();
                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {


                            obj.lstdetails = datalist1;


                        }
                        List<OrderValueDetails> objterms = new List<OrderValueDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objterms.Add(new OrderValueDetails

                                {


                                    IncomeType = row1["IncomeType"].ToString(),
                                    Business = row1["Business"].ToString(),
                                    Earnings = row1["Earnings"].ToString(),




                                });
                            }
                            datalist.Add(new OrderValue
                            {

                                lstdetails = objterms

                            });
                            obj.lstdetails = datalist[0].lstdetails;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(OrderValue));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion GetOrderValue
        #region GetServicesDetailsForApplication
        public ActionResult GetServicesDetailsForApplication(Menu obj1)
        {

            Menu obj = new Menu();
            List<Data1> datalist = new List<Data1>();
            List<menuDetails> datalist1 = new List<menuDetails>();
            APIResponse objApiResponse = new APIResponse();
            DataSet dsResult = obj1.GetServicesDetailsForApplication();

            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                obj.Status = "0";
                foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                {


                    obj.menu = datalist1;


                }

                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    List<menuDetails> objrecentjoined = new List<menuDetails>();

                    {
                        #region menuDetails
                        for (int i = 0; i <= dsResult.Tables[0].Rows.Count - 1; i++)
                        {
                            List<menuData> objsub = new List<menuData>();
                            objrecentjoined.Add(new menuDetails

                            {

                                MainServiceType = dsResult.Tables[0].Rows[i]["MainServiceType"].ToString(),



                            });
                            for (int j = 0; j <= dsResult.Tables[1].Rows.Count - 1; j++)
                            {

                                if (dsResult.Tables[0].Rows[i]["Pk_MainServiceTypeId"].ToString() == dsResult.Tables[1].Rows[j]["Fk_MainServiceTypeId"].ToString())
                                {
                                    objsub.Add(new menuData

                                    {
                                        Service = dsResult.Tables[1].Rows[j]["Service"].ToString(),
                                        ServiceUrl = dsResult.Tables[1].Rows[j]["ServiceUrl"].ToString(),
                                        ServiceIcon = dsResult.Tables[1].Rows[j]["ServiceIcon"].ToString(),


                                    });
                                }
                            }
                            objrecentjoined[i].menuData = objsub;
                        }
                        datalist.Add(new Data1
                        {

                            menu = objrecentjoined,


                        });
                        #endregion
                    }

                }
                obj.menu = datalist[0].menu;

            }

            else
            {

                obj.Status = "1";


                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Menu));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);


        }
        #endregion GetServicesDetailsForApplication
        #region MSCLShoppingAPICalculator
        public ActionResult MSCLShoppingAPICalculator(Request model)
        {
            MSCLShoppingAPICalculator obj = new MSCLShoppingAPICalculator();


            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<MSCLShoppingAPICalculator>(dcdata);

            try
            {


                DataSet dsResult = obj.GetMSCLShoppingAPICalculator();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Userprofit = dsResult.Tables[0].Rows[0]["Userprofit"].ToString();
                        obj.Status = "0";


                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(MSCLShoppingAPICalculator));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion MSCLShoppingAPICalculator
        #region StateList
        public ActionResult StateList(Request model)
        {
            StateList obj = new StateList();
            List<StateList> datalist = new List<StateList>();

            List<StateDetails> datalist1 = new List<StateDetails>();

            APIResponse objApiResponse = new APIResponse();

            try
            {

                DataSet dsResult = obj.GetState();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lststate = datalist1;


                        }
                        List<StateDetails> objterms = new List<StateDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new StateDetails

                                {


                                    StateName = row1["StateName"].ToString(),
                                    Fk_StateId = row1["Fk_StateId"].ToString(),



                                });
                            }
                            datalist.Add(new StateList
                            {

                                lststate = objterms

                            });



                            obj.lststate = datalist[0].lststate;

                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(StateList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion StateList
        #region RecentRecharges
        public ActionResult RecentRecharges(Request model)
        {
            RecentRecharges obj = new RecentRecharges();
            List<RecentRecharges> datalist = new List<RecentRecharges>();

            List<RecharegeDetails> datalist1 = new List<RecharegeDetails>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<RecentRecharges>(dcdata);

            try
            {

                DataSet dsResult = obj.GetRecentRecharges();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";

                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstdetails = datalist1;


                        }
                        List<RecharegeDetails> objterms = new List<RecharegeDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new RecharegeDetails

                                {


                                    Number = row1["Number"].ToString(),
                                    Narration = row1["Narration"].ToString(),
                                    Amount = row1["Amount"].ToString(),
                                    Operator = row1["Operator"].ToString(),
                                    PaymentDate = row1["PaymentDate"].ToString(),
                                    TransactionId = row1["TransactionId"].ToString(),
                                    Image = row1["Image"].ToString(),


                                });
                            }
                            datalist.Add(new RecentRecharges
                            {

                                lstdetails = objterms

                            });
                            obj.lstdetails = datalist[0].lstdetails;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RecentRecharges));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion RecentRecharges
        #region BannerList
        public ActionResult BannerList(Request model)
        {
            ProductList obj = new ProductList();
            List<ProductList> datalist = new List<ProductList>();


            List<BannerDetails> datalist2 = new List<BannerDetails>();
            APIResponse objApiResponse = new APIResponse();

            try
            {

                DataSet dsResult = obj.GetProductList();
                if (dsResult != null && dsResult.Tables[2].Rows.Count > 0)
                {
                    if (dsResult.Tables[2].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[2].Rows))
                        {



                            obj.lstbanner = datalist2;


                        }
                        List<ProductDetails> objterms = new List<ProductDetails>();

                        {


                            List<BannerDetails> objterms1 = new List<BannerDetails>();
                            foreach (DataRow row1 in (dsResult.Tables[2].Rows))
                            {
                                objterms1.Add(new BannerDetails

                                {


                                    BannerImage = EncKey.ProfilePicURL + row1["BannerImage"].ToString(),



                                });
                            }
                            datalist.Add(new ProductList
                            {

                                lstbanner = objterms1

                            });


                            obj.lstbanner = datalist[0].lstbanner;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion BannerList
        #region INRREPORTS
        public ActionResult GetShoppingDetails(Request model)
        {
            RechargeHistory obj = new RechargeHistory();
            List<RechargeHistory> datalist = new List<RechargeHistory>();

            List<Header> datalist1 = new List<Header>();
            List<Recharge> datalist2 = new List<Recharge>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<RechargeHistory>(dcdata);
            obj.FromDate = string.IsNullOrEmpty(obj.FromDate) ? null : Common.ConvertToSystemDate(obj.FromDate, "dd/MM/yyyy");
            obj.ToDate = string.IsNullOrEmpty(obj.ToDate) ? null : Common.ConvertToSystemDate(obj.ToDate, "dd/MM/yyyy");

            try
            {

                DataSet dsResult = obj.GetINRReports();
                if (dsResult != null && dsResult.Tables[1].Rows.Count > 0)
                {

                    if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[1].Rows))
                        {



                            obj.lstdetails = datalist2;
                            obj.lstheader = datalist1;


                        }
                        List<Recharge> objterms = new List<Recharge>();

                        {


                            List<Recharge> objterms1 = new List<Recharge>();
                            List<Header> objterms2 = new List<Header>();
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms1.Add(new Recharge

                                {


                                    TransactionId = row1["TransactionId"].ToString(),
                                    Amount = row1["Amount"].ToString(),
                                    Status = row1["Status"].ToString(),
                                    StorName = row1["StorName"].ToString(),
                                    SaleDate = row1["SaleDate"].ToString(),
                                    Icon = row1["Icon"].ToString(),
                                    Number = row1["Number"].ToString(),



                                });
                            }

                            datalist.Add(new RechargeHistory
                            {

                                lstdetails = objterms1

                            });
                            foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                            {
                                objterms2.Add(new Header

                                {


                                    HeaderName = row1["HeaderName"].ToString(),



                                });
                            }

                            datalist.Add(new RechargeHistory
                            {

                                lstheader = objterms2

                            });

                            obj.lstdetails = datalist[0].lstdetails;
                            obj.lstheader = datalist[1].lstheader;
                        }
                    }
                    else
                    {
                        obj.Status = "1";

                    }

                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RechargeHistory));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion INRREPORTS
        #region BusinessBannerList
        public ActionResult BusinessDashBoardBanner(Request model)
        {
            ProductList obj = new ProductList();
            List<ProductList> datalist = new List<ProductList>();


            List<BannerDetails> datalist2 = new List<BannerDetails>();
            APIResponse objApiResponse = new APIResponse();

            try
            {

                DataSet dsResult = obj.GetProductList();
                if (dsResult != null && dsResult.Tables[3].Rows.Count > 0)
                {
                    if (dsResult.Tables[3].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[3].Rows))
                        {



                            obj.lstbanner = datalist2;


                        }
                        List<ProductDetails> objterms = new List<ProductDetails>();

                        {


                            List<BannerDetails> objterms1 = new List<BannerDetails>();
                            foreach (DataRow row1 in (dsResult.Tables[3].Rows))
                            {
                                objterms1.Add(new BannerDetails

                                {


                                    BannerImage = EncKey.ProfilePicURL + row1["BannerImage"].ToString(),



                                });
                            }
                            datalist.Add(new ProductList
                            {

                                lstbanner = objterms1

                            });


                            obj.lstbanner = datalist[0].lstbanner;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion BusinessBannerList
        #region UpdateNotification
        public ActionResult UpdateNotification(Request model)
        {
            UpdateNotification obj = new UpdateNotification();
            List<UpdateNotification> datalist = new List<UpdateNotification>();


            List<NotificationDetails> datalist2 = new List<NotificationDetails>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj = JsonConvert.DeserializeObject<UpdateNotification>(dcdata);
            try
            {

                DataSet dsResult = obj.UpdatenotificationData();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {



                            obj.lstnotification = datalist2;


                        }
                        List<NotificationDetails> objterms = new List<NotificationDetails>();

                        {


                            List<NotificationDetails> objterms1 = new List<NotificationDetails>();
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms1.Add(new NotificationDetails

                                {


                                    Title = row1["Title"].ToString(),
                                    Notification = row1["Notification"].ToString(),
                                    IsRead = row1["Status"].ToString(),
                                    NotificationDate = row1["NotificationDate"].ToString(),



                                });
                            }
                            datalist.Add(new UpdateNotification
                            {

                                lstnotification = objterms1

                            });


                            obj.lstnotification = datalist[0].lstnotification;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(UpdateNotification));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion UpdateNotification
        #region QuickView

        public ActionResult QuickView(Request model)
        {
            QuickView obj1 = new QuickView();
            APIResponse objApiResponse = new APIResponse();
            QuickView obj = new QuickView();
            List<QuickView> lst = new List<QuickView>();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj1 = JsonConvert.DeserializeObject<QuickView>(dcdata);
            obj1.ColorID = string.IsNullOrEmpty(obj1.ColorID) ? null : obj1.ColorID;
            obj1.SizeID = string.IsNullOrEmpty(obj1.SizeID) ? null : obj1.SizeID;
            obj1.RamID = string.IsNullOrEmpty(obj1.RamID) ? null : obj1.RamID;
            obj1.StorageID = string.IsNullOrEmpty(obj1.StorageID) ? null : obj1.StorageID;
            obj1.FlavorID = string.IsNullOrEmpty(obj1.FlavorID) ? null : obj1.FlavorID;
            obj1.MaterialID = string.IsNullOrEmpty(obj1.MaterialID) ? null : obj1.MaterialID;
            DataSet ds = obj1.GetQuickView();
            List<Varients> datalist = new List<Varients>();
            List<sizedata> sizedata = new List<sizedata>();
            List<Colordata> Colordata = new List<Colordata>();
            List<Flavourdata> flavordata = new List<Flavourdata>();
            List<Ramdata> Ramdata = new List<Ramdata>();
            List<Materialdata> MaterialData = new List<Materialdata>();
            List<Storagedata> Storagedata = new List<Storagedata>();
            List<Imagesdata> Imagesdata = new List<Imagesdata>();
            if (ds != null && ds.Tables.Count > 0)
            {
                obj.Status = "0";
                foreach (DataRow row0 in (ds.Tables[0].Rows))
                {


                    obj.Varients = datalist;

                    obj.lstimages = Imagesdata;


                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region ProductDetails
                    if (ds != null && ds.Tables.Count > 8)
                    {
                        obj.ProductID = (ds.Tables[8].Rows[0]["PK_ProductID"].ToString());
                        obj.ProductName = ds.Tables[8].Rows[0]["ProductName"].ToString();
                        obj.DeliveryCharge = ds.Tables[8].Rows[0]["DeliveryCharge"].ToString();
                        obj.Description = ds.Tables[8].Rows[0]["Description"].ToString();
                        obj.MRP = ds.Tables[8].Rows[0]["MRP"].ToString();
                        obj.OfferedPrice = ds.Tables[8].Rows[0]["OfferedPrice"].ToString();
                        obj.RedeemPrice = ds.Tables[8].Rows[0]["RedeemPrice"].ToString();
                        obj.IsAvailable = ds.Tables[8].Rows[0]["IsAvailable"].ToString();
                        obj.ShortDescription = ds.Tables[8].Rows[0]["ShortDescription"].ToString();
                        obj.IsCart = ds.Tables[8].Rows[0]["IsCart"].ToString();
                        obj.ProductInfoCode = ds.Tables[8].Rows[0]["ProductInfoCode"].ToString();
                        obj.ShareURL = "https://www.MSCLShoppingAPI.in/Customer/QuickView?pid=" + Crypto.Encrypt(ds.Tables[8].Rows[0]["PK_ProductID"].ToString());

                    }
                    else
                    {
                        obj.ProductID = (ds.Tables[7].Rows[0]["PK_ProductID"].ToString());

                        obj.ProductName = ds.Tables[7].Rows[0]["ProductName"].ToString();
                        obj.DeliveryCharge = ds.Tables[7].Rows[0]["DeliveryCharge"].ToString();
                        obj.Description = ds.Tables[7].Rows[0]["Description"].ToString();
                        obj.MRP = ds.Tables[7].Rows[0]["MRP"].ToString();
                        obj.OfferedPrice = ds.Tables[7].Rows[0]["OfferedPrice"].ToString();
                        obj.RedeemPrice = ds.Tables[7].Rows[0]["RedeemPrice"].ToString();
                        obj.IsAvailable = ds.Tables[7].Rows[0]["IsAvailable"].ToString();
                        obj.ShortDescription = ds.Tables[7].Rows[0]["ShortDescription"].ToString();
                        obj.IsCart = ds.Tables[7].Rows[0]["IsCart"].ToString();
                        obj.ProductInfoCode = ds.Tables[7].Rows[0]["ProductInfoCode"].ToString();
                        obj.ShareURL = "http://www.aapkabiz.com/Customer/QuickView?pid=" + Crypto.Encrypt(ds.Tables[7].Rows[0]["PK_ProductID"].ToString());
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {


                        //obj.UnitID = ds.Tables[0].Rows[0]["FK_UnitID"].ToString();
                        obj.ColorID = ds.Tables[0].Rows[0]["FK_ColorID"].ToString();
                        obj.SizeID = ds.Tables[0].Rows[0]["FK_SizeID"].ToString();
                        obj.FlavorID = ds.Tables[0].Rows[0]["FK_FlavorID"].ToString();
                        obj.RamID = ds.Tables[0].Rows[0]["FK_RAM_ID"].ToString();
                        obj.StorageID = ds.Tables[0].Rows[0]["FK_StorageID"].ToString();
                        obj.SizeID = ds.Tables[0].Rows[0]["FK_SizeID"].ToString();
                        obj.MaterialID = ds.Tables[0].Rows[0]["Fk_MaterialId"].ToString();
                    }
                    else
                    {

                        //obj.UnitID = ds.Tables[7].Rows[0]["FK_UnitID"].ToString();
                        obj.ColorID = ds.Tables[7].Rows[0]["FK_ColorID"].ToString();
                        obj.SizeID = ds.Tables[7].Rows[0]["FK_SizeID"].ToString();
                        obj.FlavorID = ds.Tables[7].Rows[0]["FK_FlavorID"].ToString();
                        obj.RamID = ds.Tables[7].Rows[0]["FK_RAM_ID"].ToString();
                        obj.StorageID = ds.Tables[7].Rows[0]["FK_StorageID"].ToString();
                        obj.SizeID = ds.Tables[7].Rows[0]["FK_SizeID"].ToString();
                        obj.MaterialID = ds.Tables[7].Rows[0]["Fk_MaterialId"].ToString();
                    }
                    #endregion ProductDetails


                }


                #region SizeDetails
                List<SizeDetails> objsizedetails = new List<SizeDetails>();
                {

                    foreach (DataRow row1 in (ds.Tables[1].Rows))
                    {
                        objsizedetails.Add(new SizeDetails

                        {

                            SizeID = row1["FK_SizeID"].ToString(),
                            SizeName = row1["SizeName"].ToString(),
                            Status = obj.SizeID == row1["FK_SizeID"].ToString() ? "selectedSize" : "",

                        });
                    }
                    datalist.Add(new Varients
                    {
                        Title = "Size",
                        lstsize = objsizedetails

                    });

                }
                #endregion Sizedetails

                #region ColorDetails
                List<ColorDetails> objColordetails = new List<ColorDetails>();
                {

                    foreach (DataRow row1 in (ds.Tables[2].Rows))
                    {
                        objColordetails.Add(new ColorDetails

                        {

                            ColorID = row1["FK_ColorID"].ToString(),
                            ColorCode = row1["ColorCode"].ToString(),
                            ColorName = row1["ColorName"].ToString(),
                            Status = obj.ColorID == row1["FK_ColorID"].ToString() ? "selectedColor" : "",

                        });
                    }
                    datalist.Add(new Varients
                    {
                        Title = "Color",
                        lstcolor = objColordetails

                    });

                }
                #endregion Colordetails


                #region FlavourDetails
                List<FlavourDetails> objflavourdetails = new List<FlavourDetails>();
                {

                    foreach (DataRow row1 in (ds.Tables[3].Rows))
                    {
                        objflavourdetails.Add(new FlavourDetails

                        {

                            FlavorID = row1["FK_FlavorID"].ToString(),
                            FlavorName = row1["FlavorName"].ToString(),
                            Status = obj.FlavorID == row1["FK_FlavorID"].ToString() ? "selectedFlavour" : "",
                        });
                    }
                    datalist.Add(new Varients
                    {
                        Title = "Flavour",
                        lstflavour = objflavourdetails

                    });

                }
                #endregion FlavourDetails

                #region RamDetails
                List<RamDetails> objramdetails = new List<RamDetails>();
                {

                    foreach (DataRow row1 in (ds.Tables[4].Rows))
                    {
                        objramdetails.Add(new RamDetails

                        {

                            RamID = row1["FK_RAM_ID"].ToString(),
                            RamName = row1["RAM"].ToString(),
                            Status = obj.RamID == row1["FK_RAM_ID"].ToString() ? "selectedRam" : "",
                        });
                    }
                    datalist.Add(new Varients
                    {
                        Title = "Ram",
                        lstram = objramdetails

                    });

                }
                #endregion RamDetails

                #region StorageDetails
                List<StorageDetails> objstoragedetails = new List<StorageDetails>();
                {

                    foreach (DataRow row1 in (ds.Tables[5].Rows))
                    {
                        objstoragedetails.Add(new StorageDetails

                        {

                            StorageID = row1["FK_StorageID"].ToString(),
                            StorageName = row1["Storage"].ToString(),
                            Status = obj.StorageID == row1["FK_StorageID"].ToString() ? "selectedStorage" : "",
                        });
                    }
                    datalist.Add(new Varients
                    {
                        Title = "Storage",
                        lststorage = objstoragedetails

                    });

                }
                #endregion StorageDetails

                #region MaterialDetails
                List<MaterialDetails> objMaterialDetails = new List<MaterialDetails>();
                {

                    foreach (DataRow row1 in (ds.Tables[6].Rows))
                    {
                        objMaterialDetails.Add(new MaterialDetails

                        {

                            MaterialID = row1["Fk_MaterialId"].ToString(),
                            MaterialName = row1["MaterialName"].ToString(),
                            Status = obj.MaterialID == row1["Fk_MaterialId"].ToString() ? "selectedMaterial" : "",
                        });
                    }
                    datalist.Add(new Varients
                    {
                        Title = "Material",
                        lstmaterial = objMaterialDetails

                    });

                }
                #endregion MaterialDetails
                if (ds.Tables.Count > 8)
                {
                    #region ImagesDetails
                    List<ImagesDetails> objimagedetails = new List<ImagesDetails>();
                    {

                        foreach (DataRow row1 in (ds.Tables[8].Rows))
                        {
                            objimagedetails.Add(new ImagesDetails

                            {

                                ImagePath = EncKey.ShoppingProfilePicURL + row1["ImagePath"].ToString().Replace("../../", "")

                            });
                        }
                        datalist.Add(new Varients
                        {
                            Title = "Images",
                            lstimages = objimagedetails

                        });

                    }
                    #endregion ImagesDetails
                }
                else
                {
                    #region ImagesDetails
                    List<ImagesDetails> objimagedetails = new List<ImagesDetails>();
                    {

                        foreach (DataRow row1 in (ds.Tables[7].Rows))
                        {
                            objimagedetails.Add(new ImagesDetails

                            {

                                ImagePath = EncKey.ShoppingProfilePicURL + row1["ImagePath"].ToString().Replace("../../", "")

                            });
                        }
                        datalist.Add(new Varients
                        {
                            Title = "Images",
                            lstimages = objimagedetails

                        });

                    }
                    #endregion ImagesDetails
                }
                DataSet dsVendor = obj.GetVendorForProduct();
                if (dsVendor != null && dsVendor.Tables[0].Rows.Count > 0)
                {
                    obj.VendorName = dsVendor.Tables[0].Rows[0]["VendorName"].ToString();
                    obj.Fk_vendorId = dsVendor.Tables[0].Rows[0]["Fk_vendorId"].ToString();


                }
            }

            else
            {

                obj.Status = "1";




            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(QuickView));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);


        }

        #endregion QuickView
        #region AddToCart
        public ActionResult AddToCart(Request model)
        {
            AddToCart obj = new AddToCart();
            AddToCart obj1 = new AddToCart();
            APIResponse objApiResponse = new APIResponse();


            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
                obj = JsonConvert.DeserializeObject<AddToCart>(dcdata);
                if (obj.ProductInfoCode == "" || obj.ProductInfoCode == null)
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Please Enter ProductInfoCode";

                }
                else
                {
                    DataSet ds = obj.SaveAddToCart();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            obj.Status = "0";
                            obj.ErrorMessage = "Product Added To Cart";

                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            obj.Status = "1";
                            obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(AddToCart));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion AddToCart
        #region ShowCart
        public ActionResult ShowCart(Request model)
        {
            ShowCart obj = new ShowCart();
            ShowCart obj1 = new ShowCart();
            List<Cartdata> CartData = new List<Cartdata>();

            APIResponse objApiResponse = new APIResponse();
            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
                obj = JsonConvert.DeserializeObject<ShowCart>(dcdata);
                DataSet ds = obj.loadCustomerCart();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    obj.Status = "0";
                    foreach (DataRow row0 in (ds.Tables[0].Rows))
                    {


                        obj.lstcart = CartData;


                    }
                    #region ShowCart
                    List<CartDetails> objimagedetails = new List<CartDetails>();
                    {

                        foreach (DataRow row1 in (ds.Tables[0].Rows))
                        {
                            objimagedetails.Add(new CartDetails

                            {

                                PK_CartID = row1["PK_CartID"].ToString(),
                                ProductInfoCode = row1["ProductInfoCode"].ToString(),
                                ProductName = row1["ProductName"].ToString(),
                                DeliveryCharge = row1["DeliveryCharge"].ToString(),
                                RedeemPrice = row1["RedeemPrice"].ToString(),
                                VendorName = row1["VendorName"].ToString(),
                                VendorId = row1["VendorId"].ToString(),
                                ProductInfo = row1["ProductInfo"].ToString(),
                                Rate = row1["Rate"].ToString(),
                                Quantity = row1["Quantity"].ToString(),
                                SubTotal = row1["SubTotal"].ToString(),
                                ImagePath = row1["PrimaryImage"].ToString(),
                                IsAvailable = row1["IsAvailable"].ToString(),

                            });
                        }
                        CartData.Add(new Cartdata
                        {
                            Title = "Show Cart",
                            CartDetails = objimagedetails

                        });

                    }
                    #endregion ShowCart
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "No Record Found.";
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ShowCart));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion ShowCart
        #region RemoveItemFromCart
        public ActionResult RemoveItemFromCart(Request model)
        {
            RemoveCart obj = new RemoveCart();
            RemoveCart obj1 = new RemoveCart();
            APIResponse objApiResponse = new APIResponse();

            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
                obj = JsonConvert.DeserializeObject<RemoveCart>(dcdata);
                DataSet ds = obj.ClearCart();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.ErrorMessage = "product Removed From Cart";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }

                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Problem In Connection.";
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(RemoveCart));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion RemoveItemFromCart
        #region CartCount
        public ActionResult CartCount(Request model)
        {
            CartCount obj = new CartCount();
            CartCount objParameters = new CartCount();
            APIResponse objApiResponse = new APIResponse();
            try
            {
                string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
                objParameters = JsonConvert.DeserializeObject<CartCount>(dcdata);
                DataSet dsResult = objParameters.CountCart();
                if (dsResult != null && dsResult.Tables[3].Rows.Count > 0)
                {

                    obj.Status = "0";
                    obj.Count = dsResult.Tables[3].Rows[0]["TotalCount"].ToString();



                }
                else
                {
                    obj.Status = "1";
                    obj.Count = "0";
                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CartCount));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion CartCount
        #region SaveAddress
        public ActionResult SaveAddress(Request model)
        {
            Address objParameters = new Address();
            Address obj = new Address();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            objParameters = JsonConvert.DeserializeObject<Address>(dcdata);
            if (objParameters.HouseNo == "" || objParameters.HouseNo == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter HouseNo";

            }
            else if (objParameters.Locality == "" || objParameters.Locality == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Locality";

            }
            else if (objParameters.LandMark == "" || objParameters.LandMark == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter LandMark";

            }
            else if (objParameters.PinCode == "" || objParameters.PinCode == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter PinCode";

            }
            else if (objParameters.AddressType == "" || objParameters.AddressType == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please select AddressType";

            }
            else if (objParameters.Name == "" || objParameters.Name == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Name";

            }
            else if (objParameters.ContatNo == "" || objParameters.ContatNo == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter ContatNo";

            }

            else
            {
                try
                {

                    DataSet dsResult = objParameters.InsertAddress();
                    if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            obj.Status = "0";
                            obj.ErrorMessage = "Address Saved Successfully.";

                        }
                        else
                        {
                            obj.Status = "1";
                            obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = "Invalid LoginId and Password.";
                    }

                }
                catch (Exception ex)
                {
                    obj.Status = "1";
                    obj.ErrorMessage = ex.Message;

                }
            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Address));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion Saveaddress
        #region GetAddress
        public ActionResult GetAddress(Request model)
        {
            GetAddress objParameters = new GetAddress();
            GetAddress obj = new GetAddress();
            APIResponse objApiResponse = new APIResponse();
            List<adressdata> datalist = new List<adressdata>();
            try
            {

                string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
                objParameters = JsonConvert.DeserializeObject<GetAddress>(dcdata);
                DataSet dsResult = objParameters.GetAddressData();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                    {


                        obj.lstaddress = datalist;


                    }
                    List<AddressDetails> objaddress = new List<AddressDetails>();

                    {
                        #region Address
                        foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                        {
                            objaddress.Add(new AddressDetails

                            {
                                Pk_AddressId = row1["PK_CustomerOtherInfoID"].ToString(),
                                HouseNo = row1["HouseNo"].ToString(),
                                Locality = row1["Locality"].ToString(),
                                LandMark = row1["LandMark"].ToString(),
                                PinCode = row1["PinCode"].ToString(),
                                AddressType = row1["AddressType"].ToString(),
                                IsDefault = row1["IsDefault"].ToString(),
                                Name = row1["Name"].ToString(),
                                ContatNo = row1["ContatNo"].ToString(),

                            });
                        }
                        datalist.Add(new adressdata
                        {
                            Title = "Address",
                            Address = objaddress

                        });
                        #endregion
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GetAddress));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion GetAddress
        #region PlaceOrder
        public ActionResult PlaceOrder(Request model)
        {
            PlaceOrder objParameters = new PlaceOrder();
            PlaceOrder obj = new PlaceOrder();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            objParameters = JsonConvert.DeserializeObject<PlaceOrder>(dcdata);

            if (objParameters.Fk_AddressId == "" || objParameters.Fk_AddressId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Address";

            }
            else if (objParameters.PaymentMode == "" || objParameters.PaymentMode == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter PaymentMode";

            }
            else
            {
                try
                {

                    DataSet dsResult = objParameters.OrderPlace();
                    if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            obj.Status = "0";
                            obj.OrderNo = dsResult.Tables[0].Rows[0]["OrderNo"].ToString();


                        }
                        else
                        {
                            obj.Status = "1";
                            obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = "";
                    }

                }
                catch (Exception ex)
                {
                    obj.Status = "1";
                    obj.ErrorMessage = ex.Message;

                }
            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(PlaceOrder));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion PlaceOrder
        #region MyOrders
        public ActionResult MyOrders(Request model)
        {
            Orders obj1 = new Orders();
            Orders obj = new Orders();
            List<orderdata> datalist = new List<orderdata>();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj1 = JsonConvert.DeserializeObject<Orders>(dcdata);
            DataSet dsResult = obj1.MyOrders();

            if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                {


                    obj.lstorder = datalist;


                }

                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    List<OrderDetails> objrecentjoined = new List<OrderDetails>();

                    {
                        #region OrderDetails
                        for (int i = 0; i <= dsResult.Tables[0].Rows.Count - 1; i++)
                        {
                            List<OrderSummary> objsub = new List<OrderSummary>();
                            objrecentjoined.Add(new OrderDetails

                            {
                                OrderDate = dsResult.Tables[0].Rows[i]["OrderDate"].ToString(),
                                OrderAmount = dsResult.Tables[0].Rows[i]["OrderTotal"].ToString(),
                                OrderNo = dsResult.Tables[0].Rows[i]["OrderNo"].ToString(),
                                PK_OrderID = dsResult.Tables[0].Rows[i]["PK_OrderID"].ToString(),


                            });
                            for (int j = 0; j <= dsResult.Tables[1].Rows.Count - 1; j++)
                            {

                                if (dsResult.Tables[0].Rows[i]["PK_OrderID"].ToString() == dsResult.Tables[1].Rows[j]["PK_OrderID"].ToString())
                                {
                                    objsub.Add(new OrderSummary

                                    {
                                        ImagePath = EncKey.ShoppingProfilePicURL + dsResult.Tables[1].Rows[j]["ProductImage"].ToString().Replace("../../", ""),
                                        ProductName = dsResult.Tables[1].Rows[j]["ProductName"].ToString(),
                                        ProductInfo = dsResult.Tables[1].Rows[j]["ProductInfo"].ToString(),
                                        ExpectedDelivery = dsResult.Tables[1].Rows[j]["ExpectedDelivery"].ToString(),
                                        Quantity = dsResult.Tables[1].Rows[j]["Quantity"].ToString(),
                                        Rate = dsResult.Tables[1].Rows[j]["Rate"].ToString(),
                                        Amount = dsResult.Tables[1].Rows[j]["Amount"].ToString(),
                                        OrderStatus = dsResult.Tables[1].Rows[j]["OrderStatus"].ToString(),
                                        PK_OrderDetailsID = dsResult.Tables[1].Rows[j]["PK_OrderDetailsID"].ToString(),
                                        RedeemPrice = dsResult.Tables[1].Rows[j]["RedeemPrice"].ToString(),
                                        DeliveryCharge = dsResult.Tables[1].Rows[j]["ShippingCharges"].ToString(),

                                    });
                                }
                            }
                            objrecentjoined[i].OrderSummary = objsub;
                        }
                        datalist.Add(new orderdata
                        {

                            OrderDetails = objrecentjoined,


                        });
                        #endregion
                    }

                }

            }

            else
            {

                obj.Status = "1";
                obj.ErrorMessage = "No Record Found.";



            }


            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Orders));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion MyOrders
        #region CancelOrder
        public ActionResult CancelOrder(Request model)
        {
            CancelOrder obj = new CancelOrder();
            CancelOrder objParameters = new CancelOrder();
            APIResponse objApiResponse = new APIResponse();
            try
            {

                string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
                objParameters = JsonConvert.DeserializeObject<CancelOrder>(dcdata);
                DataSet dsResult = objParameters.Cancel();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.ErrorMessage = "Order Cancelled Successfully.";


                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Problem In Connection";
                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;

            }

            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(CancelOrder));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion CancelOrder
        #region GlobalSearch
        public ActionResult GlobalSearch(Request model)
        {
            GlobalSearch obj = new GlobalSearch();
            List<Data1> datalist = new List<Data1>();
            List<Data> datalist1 = new List<Data>();
            GlobalSearch obj1 = new GlobalSearch();
            APIResponse objApiResponse = new APIResponse();
            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            obj1 = JsonConvert.DeserializeObject<GlobalSearch>(dcdata);
            DataSet dsResult = obj1.GlobalSearchData();

            if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                {



                    obj.ProductList = datalist1;


                }


                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    List<Newarrivals> lstnewarrivals = new List<Newarrivals>();

                    {
                        #region New Arrivals
                        foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                        {
                            lstnewarrivals.Add(new Newarrivals

                            {
                                Pk_ProductId = row1["Pk_ProductId"].ToString(),
                                Images = EncKey.ShoppingProfilePicURL + row1["Images"].ToString().Replace("../../", ""),
                                ProductName = row1["ProductName"].ToString(),
                                OfferedPrice = row1["OfferedPrice"].ToString(),
                                MRP = row1["MRP"].ToString(),
                                SoldOutCss = row1["SoldOutCss"].ToString(),

                            });
                        }
                        datalist1.Add(new Data
                        {
                            Title = "Products",
                            Newarrivals = lstnewarrivals

                        });
                        #endregion
                    }

                }

            }

            else
            {

                obj.Status = "1";




            }


            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(GlobalSearch));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion GlobalSearch
        #region Popup
        public ActionResult Popup(Request model)
        {
            Popup obj = new Popup();
            List<Popup> datalist = new List<Popup>();

            List<PopupDetails> datalist1 = new List<PopupDetails>();
            APIResponse objApiResponse = new APIResponse();

            try
            {

                DataSet dsResult = obj.GetPopupData();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";

                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstdetails = datalist1;


                        }
                        List<PopupDetails> objterms = new List<PopupDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new PopupDetails

                                {


                                    PopUpImage = EncKey.ProfilePicURL + row1["PopUpImage"].ToString().Replace("../../", ""),
                                    PopUpName = row1["PopUpName"].ToString()



                                });
                            }
                            datalist.Add(new Popup
                            {

                                lstdetails = objterms

                            });
                            obj.lstdetails = datalist[0].lstdetails;
                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(Popup));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion Popup
        #region PrimeProductList
        public ActionResult PrimeProductList(Request model)
        {
            ProductList obj = new ProductList();
            List<ProductList> datalist = new List<ProductList>();

            List<ProductDetails> datalist1 = new List<ProductDetails>();
            List<BannerDetails> datalist2 = new List<BannerDetails>();
            APIResponse objApiResponse = new APIResponse();

            try
            {

                DataSet dsResult = obj.GetPrimeUserOfferProducts();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        obj.Description = dsResult.Tables[1].Rows[0]["Description"].ToString();
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstproduct = datalist1;


                        }
                        List<ProductDetails> objterms = new List<ProductDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new ProductDetails

                                {


                                    ProductName = row1["ProductName"].ToString(),
                                    Pk_ProductId = row1["PK_PrimeUserProductID"].ToString(),
                                    MRP = row1["MRP"].ToString(),
                                    OfferedPrice = row1["MaxShoppingPointRedeemption"].ToString(),
                                    DP = row1["DP"].ToString(),
                                    Images = EncKey.ShoppingProfilePicURL + row1["ProductUrl"].ToString().Replace("../../", ""),
                                    Discount = "0",



                                });
                            }
                            datalist.Add(new ProductList
                            {

                                lstproduct = objterms

                            });

                            obj.lstproduct = datalist[0].lstproduct;

                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(ProductList));
            ms = new MemoryStream();
            js.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);

        }
        #endregion ProductList
        #region TransferToBank
        public ActionResult TransferToBank(Request model)
        {
            TransferToBank para = new TransferToBank();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<TransferToBank>(dcdata);

            try
            {

                DataSet dsResult = para.TransferBank();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";
                        para.ErrorMessage = "Request Saved Successfully.";
                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TransferToBank));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion TransferToBank
        #region TransferCommissionToMSCLShoppingAPIwallet
        public ActionResult TransferCommissionToMSCLShoppingAPIwallet(Request model)
        {
            TransferCommissionToMSCLShoppingAPIwallet para = new TransferCommissionToMSCLShoppingAPIwallet();
            APIResponse objApiResponse = new APIResponse();

            string dcdata = ApiEncrypt_Decrypt.DecryptString(EncKey.KeyName, model.Body);
            para = JsonConvert.DeserializeObject<TransferCommissionToMSCLShoppingAPIwallet>(dcdata);

            try
            {

                DataSet dsResult = para.TransferCommission();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        para.Status = "0";
                        para.ErrorMessage = "Comission Wallet Successfully fransfer to MSCLShoppingAPI Wallet";
                    }
                    else
                    {
                        para.Status = "1";
                        para.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    para.Status = "1";
                    para.ErrorMessage = "";
                }

            }
            catch (Exception ex)
            {
                para.Status = "1";
                para.ErrorMessage = ex.Message;

            }
            string EncryptedText = "";
            string CustData = "";
            DataContractJsonSerializer js;
            MemoryStream ms;
            js = new DataContractJsonSerializer(typeof(TransferCommissionToMSCLShoppingAPIwallet));
            ms = new MemoryStream();
            js.WriteObject(ms, para);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            CustData = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            objApiResponse.Body = EncryptedText;
            return Json(objApiResponse, JsonRequestBehavior.AllowGet);
        }
        #endregion TransferCommissionToMSCLShoppingAPIwallet


        //public ActionResult GetMenu(MobileMenu obj1)
        //{

        //    MobileMenu obj = new MobileMenu();
        //    List<MainData> datalist = new List<MainData>();

        //    DataSet dsResult = obj1.GetMenu();

        //    if (dsResult != null && dsResult.Tables.Count > 0)
        //    {
        //        obj.Status = "0";
        //        int m = 0;
        //        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
        //        {


        //            obj.lstnmain = datalist;



        //        }

        //        if (dsResult.Tables[0].Rows.Count > 0)
        //        {
        //            List<MainCategoryDetails> objrecentjoined = new List<MainCategoryDetails>();
        //            List<CategoryDetails> objsub = new List<CategoryDetails>();

        //            {

        //                for (int i = 0; i <= dsResult.Tables[0].Rows.Count - 1; i++)
        //                {
        //                    #region MainCategoryDetails


        //                    objrecentjoined.Add(new MainCategoryDetails

        //                    {
        //                        FK_MainCategory = dsResult.Tables[0].Rows[i]["PK_MainCategoryID"].ToString(),
        //                        MainCategoryName = dsResult.Tables[0].Rows[i]["MainCategoryName"].ToString(),
        //                        Image = dsResult.Tables[0].Rows[i]["Image"].ToString(),


        //                    });
        //                    #endregion MainCategoryDetails
        //                    for (int j = 0; j <= dsResult.Tables[1].Rows.Count - 1; j++)
        //                    {
        //                        #region CategoryDetails

        //                        List<SubCategoryDetails> objsub1 = new List<SubCategoryDetails>();

        //                        if (dsResult.Tables[0].Rows[i]["PK_MainCategoryID"].ToString() == dsResult.Tables[1].Rows[j]["FK_MainCategory"].ToString())
        //                        {

        //                            objsub.Add(new CategoryDetails

        //                            {
        //                                CategoryName = dsResult.Tables[1].Rows[j]["CategoryName"].ToString(),
        //                                FK_MainCategory = dsResult.Tables[1].Rows[j]["FK_MainCategory"].ToString(),
        //                                FK_CategoryID = dsResult.Tables[1].Rows[j]["PK_CategoryID"].ToString(),


        //                            });
        //                            for (int k = 0; k <= dsResult.Tables[2].Rows.Count - 1; k++)
        //                            {
        //                                #region SUbCategory Detals
        //                                if ((dsResult.Tables[1].Rows[j]["PK_CategoryID"].ToString() == dsResult.Tables[2].Rows[k]["FK_CategoryID"].ToString()))
        //                                {
        //                                    objsub1.Add(new SubCategoryDetails

        //                                    {
        //                                        PK_SubCategoryID = dsResult.Tables[2].Rows[k]["PK_SubCategoryID"].ToString(),
        //                                        SubCategoryName = dsResult.Tables[2].Rows[k]["SubCategoryName"].ToString(),
        //                                        FK_CategoryID = dsResult.Tables[2].Rows[k]["FK_CategoryID"].ToString(),
        //                                        FK_MainCategory = dsResult.Tables[2].Rows[k]["FK_MainCategory"].ToString(),

        //                                    });
        //                                }
        //                                #endregion SUbCategory Detals

        //                            }


        //                        }
        //                        #endregion CategoryDetails

        //                        objrecentjoined[i].LstCategory = objsub;

        //                        objrecentjoined[i].LstCategory[j].LstsubCategory = objsub1;

        //                    }



        //                }
        //                datalist.Add(new MainData
        //                {

        //                    MainCategoryDetails = objrecentjoined,


        //                });
        //            }

        //        }


        //    }

        //    else
        //    {

        //        obj.Status = "1";


        //        return Json(obj, JsonRequestBehavior.AllowGet);

        //    }


        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetMenu(MobileMenu obj1)
        {

            MenuDTO obj = new MenuDTO();
            DataSet dsResult = obj1.GetMenu();
            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    List<Menu1> ml = new List<Menu1>();
                    for (int i = 0; i <= dsResult.Tables[0].Rows.Count - 1; i++)
                    {
                        //Main category
                        Menu1 m = new Menu1();
                        m.image = dsResult.Tables[0].Rows[i]["Image"].ToString();
                        m.MainCategoryId = dsResult.Tables[0].Rows[i]["PK_MainCategoryID"].ToString();
                        m.MainCategory = dsResult.Tables[0].Rows[i]["MainCategoryName"].ToString();

                        List<MainData1> mdl = new List<MainData1>();
                        for (int j = 0; j <= dsResult.Tables[1].Rows.Count - 1; j++)
                        {
                            //category
                            if (dsResult.Tables[0].Rows[i]["PK_MainCategoryID"].ToString() == dsResult.Tables[1].Rows[j]["FK_MainCategory"].ToString())
                            {
                                MainData1 md = new MainData1();
                                md.Category = dsResult.Tables[1].Rows[j]["CategoryName"].ToString();
                                md.PK_CategoryID = dsResult.Tables[1].Rows[j]["PK_CategoryID"].ToString();
                                List<CategoryData> cl = new List<CategoryData>();
                                for (int k = 0; k <= dsResult.Tables[2].Rows.Count - 1; k++)
                                {
                                    //subcategory
                                    if ((dsResult.Tables[1].Rows[j]["PK_CategoryID"].ToString() == dsResult.Tables[2].Rows[k]["FK_CategoryID"].ToString()))
                                    {
                                        CategoryData c = new CategoryData();
                                        c.id = dsResult.Tables[2].Rows[k]["PK_SubCategoryID"].ToString();
                                        c.SubCategory = dsResult.Tables[2].Rows[k]["SubCategoryName"].ToString();
                                        cl.Add(c);
                                    }
                                }
                                md.CategoryData = cl;
                                mdl.Add(md);
                            }
                        }
                        m.MainData = mdl;
                        ml.Add(m);
                    }
                    obj.menu = ml;
                }
            }
            else
            {
                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDashBoardData(ShoppingDashBoardData obj)
        {


            List<ShoppingDashBoardData> datalist = new List<ShoppingDashBoardData>();
            List<BannerDetails> datalist2 = new List<BannerDetails>();
            List<MainCategoryDetailsForDash> datalist3 = new List<MainCategoryDetailsForDash>();
            List<OtherDetails> datalist4 = new List<OtherDetails>();

            try
            {

                DataSet dsResult = obj.GetDashBoardDataForMobile();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        #region Banner & MainCategory

                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstmaincategory = datalist3;
                            obj.lstbanner = datalist2;

                        }

                        List<BannerDetails> objterms1 = new List<BannerDetails>();
                        foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                        {
                            objterms1.Add(new BannerDetails

                            {


                                BannerImage = EncKey.ShoppingProfilePicURL + row1["BannerImage"].ToString().Replace("../", ""),



                            });
                        }
                        datalist.Add(new ShoppingDashBoardData
                        {

                            lstbanner = objterms1

                        });

                        List<MainCategoryDetailsForDash> objterms2 = new List<MainCategoryDetailsForDash>();
                        foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                        {
                            objterms2.Add(new MainCategoryDetailsForDash

                            {


                                PK_MainCategoryID = row1["PK_MainCategoryID"].ToString(),
                                MainCategoryName = row1["MainCategoryName"].ToString(),
                                Image = row1["Image"].ToString(),


                            });
                        }
                        datalist.Add(new ShoppingDashBoardData
                        {

                            lstmaincategory = objterms2

                        });

                        obj.lstbanner = datalist[0].lstbanner;
                        obj.lstmaincategory = datalist[1].lstmaincategory;
                        #endregion
                    }
                    if (dsResult != null && dsResult.Tables.Count > 0)
                    {

                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[2].Rows))
                        {


                            obj.lstotherdetails = datalist4;



                        }

                        if (dsResult.Tables[2].Rows.Count > 0)
                        {
                            List<OtherDetails> objrecentjoined = new List<OtherDetails>();

                            {
                                #region OtherDetails
                                for (int i = 0; i <= dsResult.Tables[2].Rows.Count - 1; i++)
                                {

                                    List<OtherData> objsub = new List<OtherData>();

                                    objrecentjoined.Add(new OtherDetails

                                    {
                                        Name = dsResult.Tables[2].Rows[i]["Name"].ToString(),
                                        Pk_Id = dsResult.Tables[2].Rows[i]["Pk_Id"].ToString(),
                                        Image = EncKey.ShoppingProfilePicURL + dsResult.Tables[2].Rows[i]["Images"].ToString().Replace("../", ""),



                                    });
                                    for (int j = 0; j <= dsResult.Tables[3].Rows.Count - 1; j++)
                                    {

                                        if (dsResult.Tables[2].Rows[i]["Pk_Id"].ToString() == dsResult.Tables[3].Rows[j]["Fk_ProductTypeId"].ToString())
                                        {

                                            objsub.Add(new OtherData

                                            {
                                                Pk_ProductId = dsResult.Tables[3].Rows[j]["Pk_ProductId"].ToString(),
                                                ProductName = dsResult.Tables[3].Rows[j]["ProductName"].ToString(),
                                                MRP = dsResult.Tables[3].Rows[j]["MRP"].ToString(),
                                                OfferedPrice = dsResult.Tables[3].Rows[j]["OfferedPrice"].ToString(),
                                                Images = EncKey.ShoppingProfilePicURL + dsResult.Tables[3].Rows[j]["Images"].ToString().Replace("../../", ""),


                                            });




                                        }

                                    }

                                    objrecentjoined[i].OtherData = objsub;


                                }
                                datalist.Add(new ShoppingDashBoardData
                                {

                                    lstotherdetails = objrecentjoined,


                                });
                                #endregion
                            }
                            obj.lstotherdetails = datalist[2].lstotherdetails;
                        }


                    }
                }

                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCategoryWiseProduct(ProductList obj)
        {

            List<ProductList> datalist = new List<ProductList>();

            List<ProductDetails> datalist1 = new List<ProductDetails>();
            try
            {
                obj.Fk_MaincategoryId = string.IsNullOrEmpty(obj.Fk_MaincategoryId) ? null : obj.Fk_MaincategoryId;
                obj.Fk_CategoryId = string.IsNullOrEmpty(obj.Fk_CategoryId) ? null : obj.Fk_CategoryId;
                obj.Fk_SubCategoryId = string.IsNullOrEmpty(obj.Fk_SubCategoryId) ? null : obj.Fk_SubCategoryId;
                obj.Pk_Id = string.IsNullOrEmpty(obj.Pk_Id) ? null : obj.Pk_Id;
                DataSet dsResult = obj.GetProductList();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstproduct = datalist1;


                        }
                        List<ProductDetails> objterms = new List<ProductDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new ProductDetails

                                {


                                    ProductName = row1["ProductName"].ToString(),
                                    Pk_ProductId = row1["Pk_ProductId"].ToString(),
                                    MRP = row1["MRP"].ToString(),
                                    OfferedPrice = row1["OfferedPrice"].ToString(),
                                    Images = EncKey.ShoppingProfilePicURL + row1["Images"].ToString(),
                                    Discount = row1["Discount"].ToString(),



                                });
                            }
                            datalist.Add(new ProductList
                            {

                                lstproduct = objterms

                            });

                            obj.lstproduct = datalist[0].lstproduct;

                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDashBoardDataForMobile(ProductList obj)
        {

            List<ProductList> datalist = new List<ProductList>();

            List<ProductDetails> datalist1 = new List<ProductDetails>();
            try
            {

                DataSet dsResult = obj.GetAllDashBoardDataForMobile();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {


                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {


                            obj.lstproduct = datalist1;


                        }
                        List<ProductDetails> objterms = new List<ProductDetails>();

                        {

                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                objterms.Add(new ProductDetails

                                {

                                    ProductName = row1["ProductName"].ToString(),
                                    Pk_ProductId = row1["Pk_ProductId"].ToString(),
                                    MRP = row1["MRP"].ToString(),
                                    OfferedPrice = row1["OfferedPrice"].ToString(),
                                    Images = EncKey.ShoppingProfilePicURL + row1["Images"].ToString(),
                                    Discount = row1["Discount"].ToString(),



                                });
                            }
                            datalist.Add(new ProductList
                            {

                                lstproduct = objterms

                            });

                            obj.lstproduct = datalist[0].lstproduct;

                        }
                    }
                }
                else
                {
                    obj.Status = "1";

                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";


            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        #region GetPackageList 
        public ActionResult GetPackageList(PackageRoot req)
        {
            //APIResponse objApiResponse = new APIResponse();
            List<Package> packages = new List<Package>();
            PackageRoot obj = new PackageRoot();
            try
            {

                DataSet dsResult = req.GetPackages();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {

                    obj.Status = "0";
                    obj.ItemImageBaseUrl = "https://www.MSCLShoppingAPI.in/PackageImages/";
                    var check = "";
                    foreach (DataRow row in (dsResult.Tables[0].Rows))
                    {
                        if (check != row["PackageId"].ToString())
                        {
                            var p = new Package();
                            p.Id = row["PackageId"].ToString();
                            p.Name = row["Package"].ToString();
                            p.Image = row["PackageImage"].ToString();
                            p.Type = row["PackageType"].ToString();
                            p.IsPackageClaimed = row["IsPackageClaimed"].ToString();
                            p.PackageClaimedStatus = row["PackageClaimedStatus"].ToString();
                            List<PackageItem> items = new List<PackageItem>();
                            foreach (DataRow row1 in (dsResult.Tables[0].Rows))
                            {
                                if (row["PackageId"].ToString() == row1["PackageId"].ToString())
                                {
                                    items.Add(new PackageItem
                                    {
                                        Name = row1["Item"].ToString(),
                                        Image = row1["PackageItemImage"].ToString()
                                    });
                                }
                            }
                            p.PackageItems = items;
                            packages.Add(p);

                            check = row["PackageId"].ToString();
                        }
                    }

                    obj.Packages = packages;
                }
                else
                {
                    obj.Status = "1";
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
            }
            //string EncryptedText = "";
            //string CustData = "";
            //DataContractJsonSerializer js;
            //MemoryStream ms;
            //js = new DataContractJsonSerializer(typeof(LevelList));
            //ms = new MemoryStream();
            //js.WriteObject(ms, obj);
            //ms.Position = 0;
            //StreamReader sr = new StreamReader(ms);
            //CustData = sr.ReadToEnd();
            //sr.Close();
            //ms.Close();
            //EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            //objApiResponse.Body = EncryptedText;
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        #endregion GetPackageList


        #region GetDownlineWeeklyReport
        public ActionResult GetDownlineWeeklyReport(DownlineRequest req)
        {
            DownlineResponse obj = new DownlineResponse();
            try
            {
                List<DownlineItem> downlines = new List<DownlineItem>();
                List<ClubItem> Clubs = new List<ClubItem>();
                DataSet dsResult = req.GetDownlineWeekly();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {

                    obj.Status = "0";

                    foreach (DataRow row in (dsResult.Tables[0].Rows))
                    {
                        downlines.Add(new DownlineItem
                        {
                            Leg = row["Leg"].ToString(),
                            LoginId = row["LoginId"].ToString(),
                            Downline = row["Downline"].ToString()
                        });

                    }

                    foreach (DataRow row1 in (dsResult.Tables[1].Rows))
                    {
                        Clubs.Add(new ClubItem
                        {
                            Name = row1["ClubName"].ToString(),
                            Status = row1["Status"].ToString(),
                        });

                    }
                    obj.Downlines = downlines;
                    obj.Clubs = Clubs;
                }
                else
                {
                    obj.Status = "1";
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
            }
            //string EncryptedText = "";
            //string CustData = "";
            //DataContractJsonSerializer js;
            //MemoryStream ms;
            //js = new DataContractJsonSerializer(typeof(LevelList));
            //ms = new MemoryStream();
            //js.WriteObject(ms, obj);
            //ms.Position = 0;
            //StreamReader sr = new StreamReader(ms);
            //CustData = sr.ReadToEnd();
            //sr.Close();
            //ms.Close();
            //EncryptedText = ApiEncrypt_Decrypt.EncryptString(EncKey.KeyName, CustData);
            //objApiResponse.Body = EncryptedText;
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        #endregion GetDownlineWeeklyReport 

        #region PackageClaim
        public ActionResult PackageClaim(PackageClaim req)
        {
            try
            {

                DataSet dsResult = req.Claim();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0 && dsResult.Tables[0].Rows[0]["msg"].ToString() == "1")
                {

                    req.Status = "0";
                }
                else
                {
                    req.Status = "1";
                }
            }
            catch (Exception ex)
            {
                req.Status = "1";
            }
            return Json(req, JsonRequestBehavior.AllowGet);

        }
        #endregion PackageClaim
    }
}