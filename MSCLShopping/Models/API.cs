using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MSCLShopping.Models
{
    public class Login
    {
        public string ErrorMessage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Pk_UserId { get; set; }
        public string ProfilePic { get; set; }
        public string Status { get; set; }
        public string DeviceId { get; set; }

        public string FireBaseId { get; set; }
        public string LoginId { get; set; }
        public string OTP { get; set; }
        public string Email { get; set; }
        public string IsPinCreated { get; set; }
        public string AssociateStatus { get; set; }

        public DataSet AutoLoginAction()
        {
            SqlParameter[] param = {

                                       new SqlParameter("@FireBaseId",FireBaseId),
                                       new SqlParameter("@DeviceId",DeviceId)


            };
            DataSet ds = Connection.ExecuteQuery("AutoLogin", param);
            return ds;
        }
        public DataSet LoginAction()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@MobileNo",MobileNo),
                                       new SqlParameter("@Password",Password),
                                       new SqlParameter("@FireBaseId",FireBaseId),
                                       new SqlParameter("@DeviceId",DeviceId),
                                       new SqlParameter("@OTP",OTP),

            };
            DataSet ds = Connection.ExecuteQuery("LoginforMobile", param);
            return ds;
        }
    }
    public class Request
    {
        public string Body { get; set; }
    }
    public class APIResponse
    {
        public string Body { get; set; }
        public string Response { get; set; }
    }
    public class GetStateCity
    {
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public DataSet GetStateCityName()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PinCode", PinCode),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);

            return ds;
        }
    }
    public class GetStateCityAPI
    {
        public string Status { get; set; }

        public PostOffice PostOffice { get; set; }
    }
    public class PostOffice
    {
        public string State { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
    }
    public class GetSponsorName
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public string SponsorName { get; set; }
        public string SponsorId { get; set; }

        public DataSet GetSponsorDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", SponsorId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetMemberName", para);

            return ds;
        }
    }

    public class Registration : Login
    {


        public string SponsorId { get; set; }


        public string PinCode { get; set; }


        public DataSet SaveRegistration()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SponsorId", SponsorId),
                                      new SqlParameter("@Email", Email),
                                      new SqlParameter("@MobileNo", MobileNo),
                                      new SqlParameter("@FirstName", FirstName),
                                      new SqlParameter("@LastName", LastName),
                                      new SqlParameter("@RegistrationBy", "Mobile"),
                                      new SqlParameter("@PinCode", PinCode),
                                      new SqlParameter("@Password", Password),
                                      new SqlParameter("@DeviceId", DeviceId),
                                      new SqlParameter("@FireBaseId", FireBaseId),
                                       new SqlParameter("@OTP", OTP),

                                  };
            DataSet ds = Connection.ExecuteQuery("Registration", para);

            return ds;
        }
    }

    public class ChangePassword
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Pk_UserId { get; set; }
        public string PasswordType { get; set; }

        public DataSet ChngPass()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OldPassword", OldPassword),
                                      new SqlParameter("@NewPassword", NewPassword),
                                      new SqlParameter("@UpdatedBy", Pk_UserId),
                                      new SqlParameter("@PasswordType", PasswordType)

                                  };
            DataSet ds = Connection.ExecuteQuery("ChangePassword", para);

            return ds;
        }
    }
    public class TransferToBank
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Amount { get; set; }

        public string Pk_UserId { get; set; }


        public DataSet TransferBank()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@AddedBy", Pk_UserId),


                                  };
            DataSet ds = Connection.ExecuteQuery("TransferToBank", para);

            return ds;
        }
    }
    public class MainMenu
    {
        public string Status { get; set; }

        public List<menudata> lstmenu { get; set; }

        public DataSet GetMainMenu()
        {

            DataSet ds = Connection.ExecuteQuery("GetMenuForMobile");
            return ds;
        }
    }
    public class MainMenuDetails
    {
        public string Menu { get; set; }
        public string FK_MainCategory { get; set; }

    }
    public class menudata
    {

        public string Title { get; set; }

        public List<MainMenuDetails> MainMenu { get; set; }



    }
    public class TransferCommissionToMSCLShoppingAPIwallet
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Amount { get; set; }

        public string Pk_UserId { get; set; }


        public DataSet TransferCommission()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@AddedBy", Pk_UserId),


                                  };
            DataSet ds = Connection.ExecuteQuery("TransferCommissionToMSCLShoppingAPIwallet", para);

            return ds;
        }
    }

    public class TermsCondition
    {
        public List<TermsData> lstTermsList { get; set; }

        public string Status { get; set; }

        public DataSet GetTermsCondition()
        {

            DataSet ds = Connection.ExecuteQuery("GetTermsCondition");
            return ds;
        }

    }
    public class TermsData
    {
        public string Title { get; set; }
        public List<TermsListDetails> TermsListDetails { get; set; }


    }
    public class TermsListDetails
    {

        public string Termscondition { get; set; }

    }

    public class GetBalance
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Balance { get; set; }
        public string LoginId { get; set; }

        public DataSet GetBalanceDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),


                                  };
            DataSet ds = Connection.ExecuteQuery("GetDWPayLedger", para);

            return ds;
        }
    }

    public class CheckMobile
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Name { get; set; }
        public string Pk_UserId { get; set; }
        public string MobileNo { get; set; }

        public DataSet GetDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@MobileNo", MobileNo),


                                  };
            DataSet ds = Connection.ExecuteQuery("CheckMobileNo", para);

            return ds;
        }
    }

    public class ShoppingLedger
    {
        public List<LedgerDetails> lstledger { get; set; }

        public string Status { get; set; }
        public string LoginId { get; set; }
        public string PageNo { get; set; }
        public string TotalDebit { get; set; }
        public string TotalCredit { get; set; }
        public string TotalBalance { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }

        public DataSet GetShoppingLedger()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PageNo", PageNo),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetShoppingWalletLedger", para);
            return ds;
        }

        public DataSet GetDWPayLedger()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PageNo", PageNo),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDWPayLedger", para);
            return ds;
        }
        public DataSet GetCommissionLedger()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PageNo", PageNo),
                                       new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetCommissionwalletLedger", para);
            return ds;
        }

    }

    public class LedgerDetails
    {

        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }

        public string Balance { get; set; }
        public string TransactionType { get; set; }
        public string TransactionNo { get; set; }
        public string Status { get; set; }

    }
    public class Pay
    {
        public string Fk_FromId { get; set; }
        public string Fk_ToId { get; set; }
        public string PayAmount { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public DataSet MSCLShoppingAPITransfer()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_fromId", Fk_FromId),
                                      new SqlParameter("@Fk_ToId", Fk_ToId),
                                      new SqlParameter("@PayAmount", PayAmount),


                                  };
            DataSet ds = Connection.ExecuteQuery("MSCLShoppingAPITransfer", para);

            return ds;
        }
    }

    public class DirectList
    {
        public List<DirectData> lstdirectlist { get; set; }
        public string Fk_RootId { get; set; }
        public string Status { get; set; }
        public string LoginId { get; set; }
        public string PageNo { get; set; }
        public DataSet GetDirectDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PageNo", PageNo),
                                      new SqlParameter("@Fk_RootId", Fk_RootId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDirectDetails", para);
            return ds;
        }
        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PageNo", PageNo),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDownlineDetails", para);
            return ds;
        }
    }
    public class DirectData
    {
        public string Title { get; set; }
        public List<DircetDetails> DircetDetails { get; set; }


    }
    public class DircetDetails
    {

        public string LoginId { get; set; }
        public string AssociateName { get; set; }
        public string Mobile { get; set; }
        public string ParentLoginId { get; set; }

        public string ParentName { get; set; }

        public string Status { get; set; }
        public string RefferBy { get; set; }
        public string RefferByName { get; set; }
        public string JoiningDate { get; set; }
        public string Level { get; set; }
    }

    public class SendOTP
    {
        public string Status { get; set; }
        public string OTP { get; set; }
        public string MobileNo { get; set; }
        public string ErrorMessage { get; set; }

        public DataSet GetSendOTP()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@MobileNo", MobileNo),

                                  };
            DataSet ds = Connection.ExecuteQuery("SendOTP", para);

            return ds;
        }
    }

    public class DashBoardData
    {
        public string ErrorMessage { get; set; }
        public string LoginId { get; set; }
        public string Pk_UserId { get; set; }
        public string Name { get; set; }
        public string DOJ { get; set; }
        public string Mobile { get; set; }
        public string ProfilePic { get; set; }
        public string UnclearedBalance { get; set; }
        public string TeamSize { get; set; }

        public string Commission { get; set; }
        public string KYCStatus { get; set; }
        public string Status { get; set; }
        public string DOA { get; set; }
        public string LastLogin { get; set; }
        public string TodaysPrimeId { get; set; }
        public string TodaysId { get; set; }

        public DataSet GetBusinessDashboard()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_UserId",Pk_UserId),
                                   };
            DataSet ds = Connection.ExecuteQuery("GetBusinessDashboard", param);
            return ds;
        }
    }

    public class ViewProfile
    {
        public string ErrorMessage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Sex { get; set; }
        public string MarritalStatus { get; set; }
        public string Email { get; set; }
        public string NomineName { get; set; }
        public string NomineRealation { get; set; }

        public string MemberAccNo { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string IFSCCode { get; set; }
        public string InsuranceNumber { get; set; }
        public string PolicyHolderName { get; set; }
        public string Premium { get; set; }
        public string CompanyName { get; set; }
        public string NomineeName { get; set; }
        public string InsuranceType { get; set; }
        public string PolicyNumber { get; set; }

        public string VechicleNumber { get; set; }
        public string PurchaedYear { get; set; }
        public string GeneralInsPremium { get; set; }
        public string ExpiryDate { get; set; }
        public string UpdateType { get; set; }
        public string GeneralCompanyName { get; set; }

        public string Status { get; set; }
        public string Pk_UserId { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string PanNo { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AccountHolderName { get; set; }
        public string InsNomineeName { get; set; }
        public string cancelchequepic { get; set; }
        public string profilepic { get; set; }

        public DataSet GetProfileData()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@PK_UserId",Pk_UserId),
                                   };
            DataSet ds = Connection.ExecuteQuery("ViewProfile", param);
            return ds;
        }
        public DataSet UpdateProfile()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@PK_UserID",Pk_UserId),
                                       new SqlParameter("@DOB",DOB),
                                       new SqlParameter("@Gender",Sex),
                                       new SqlParameter("@FathersName",FathersName),
                                       new SqlParameter("@MothersName",MothersName),
                                       new SqlParameter("@NomineName",NomineName),
                                       new SqlParameter("@NomineeRealaton",NomineRealation),
                                       new SqlParameter("@MaritalStatus",MarritalStatus),
                                       new SqlParameter("@PanNo",PanNo),
                                       new SqlParameter("@Address",Address),
                                       new SqlParameter("@PinCode",PinCode),
                                       new SqlParameter("@AccountHolderName",AccountHolderName),
                                       new SqlParameter("@AccountNo",MemberAccNo),
                                       new SqlParameter("@IFSC",IFSCCode),
                                       new SqlParameter("@BankName",MemberBankName),
                                       new SqlParameter("@MemberBranch",MemberBranch),
                                       new SqlParameter("@InsuranceNumber",InsuranceNumber),
                                       new SqlParameter("@PolicyHolderName",PolicyHolderName),
                                       new SqlParameter("@Premium",Premium),
                                       new SqlParameter("@CompanyName",CompanyName),
                                       new SqlParameter("@InsNomineeName",InsNomineeName),
                                       new SqlParameter("@InsuranceType",InsuranceType),
                                       new SqlParameter("@PolicyNumber",PolicyNumber),
                                       new SqlParameter("@VechicleNumber",VechicleNumber),
                                       new SqlParameter("@PurchaedYear",PurchaedYear),
                                       new SqlParameter("@GeneralInsPremium",GeneralInsPremium),
                                       new SqlParameter("@ExpiryDate",ExpiryDate),
                                       new SqlParameter("@Type",UpdateType),
                                       new SqlParameter("@GeneralCompanyName",GeneralCompanyName),
                                   };
            DataSet ds = Connection.ExecuteQuery("UpdateProfile", param);
            return ds;
        }
    }

    public class CreatePin
    {
        public string ErrorMessage { get; set; }

        public string Status { get; set; }
        public string Pk_UserId { get; set; }

        public string PIN { get; set; }
        public string OTP { get; set; }

        public DataSet GeneratePin()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_UserId",Pk_UserId),
                                       new SqlParameter("@PIN",PIN),
                                       new SqlParameter("@OTP",OTP),
                                   };
            DataSet ds = Connection.ExecuteQuery("CreatePIN", param);
            return ds;
        }

    }
    public class FetchPaymnetByOrder
    {
        public List<FetchPaymnetByOrder> lstdetails { get; set; }

        public string Pk_UserId { get; set; }
        public string OrderId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }

        public DataSet GetDataForUpdateRazorPayStatus()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@FromDate",FromDate),
                                       new SqlParameter("@ToDate",ToDate),
                                       new SqlParameter("@Mobile",Mobile),
                                   };
            DataSet ds = Connection.ExecuteQuery("GetDataForUpdateRazorPayStatus", param);
            return ds;
        }
    }
    public class FetchPaymnet
    {
        public string amount { get; set; }
        public string currency { get; set; }
    }
    public class Logout
    {
        public string ErrorMessage { get; set; }
        public string DeviceId { get; set; }
        public string Status { get; set; }

        public DataSet LogoutUser()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@DeviceId",DeviceId),
                                   };
            DataSet ds = Connection.ExecuteQuery("Logout", param);
            return ds;
        }
    }

    public class TeamStatus
    {
        public List<TeamStatusData> lstteamstatus { get; set; }

        public string Status { get; set; }
        public string Pk_UserId { get; set; }
        public string TotalUpgradedApp { get; set; }
        public string TotalNonUpgradedApp { get; set; }
        public string TotalApp { get; set; }

        public string Title { get; set; }


        public DataSet GetTeamStatusDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", Pk_UserId),


                                  };
            DataSet ds = Connection.ExecuteQuery("GetTeamStatusDetails", para);
            return ds;
        }

    }
    public class TeamStatusData
    {


        public string Level { get; set; }
        public string UpgradedApp { get; set; }
        public string NonUpgradedApp { get; set; }

    }
    public class AutoLogin
    {
        public string ErrorMessage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string Pk_UserId { get; set; }
        public string ProfilePic { get; set; }
        public string Status { get; set; }
        public string DeviceId { get; set; }

        public string FireBaseId { get; set; }
        public string LoginId { get; set; }
        public string OTP { get; set; }
        public DataSet AutoLoginAction()
        {
            SqlParameter[] param = {

                                       new SqlParameter("@FireBaseId",FireBaseId),
                                       new SqlParameter("@DeviceId",DeviceId)


            };
            DataSet ds = Connection.ExecuteQuery("AutoLogin", param);
            return ds;
        }
    }

    public class ForgetPass
    {
        public string ErrorMessage { get; set; }

        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
        public string Status { get; set; }


        public DataSet ForgetPassword()
        {
            SqlParameter[] param = {

                                       new SqlParameter("@MobileNo",MobileNo),
                                        new SqlParameter("@OTP",OTP),
                                         new SqlParameter("@Password",Password),
            };
            DataSet ds = Connection.ExecuteQuery("ForgetPass", param);
            return ds;
        }
    }

    public class LevelList
    {
        public List<LevelListData> lstlevellist { get; set; }

        public string Status { get; set; }
        public string Pk_UserId { get; set; }
        public string PageNo { get; set; }
        public string Level { get; set; }

        public string TempStatus { get; set; }

        public DataSet GetLevelWiseList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", Pk_UserId),
                                      new SqlParameter("@Lvl", Level),
                                      new SqlParameter("@PageNo", PageNo),
                                      new SqlParameter("@Status", TempStatus),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetLevelWiseList", para);
            return ds;
        }

    }
    public class LevelListData
    {


        public string Pk_UserId { get; set; }
        public string Name { get; set; }
        public string TotalTeam { get; set; }

        public string LoginId { get; set; }

    }
    public class PassBook
    {
        public string ErrorMessage { get; set; }

        public string Pk_UserId { get; set; }
        public string Status { get; set; }
        public string MSCLShoppingAPIBalance { get; set; }
        public string ShoppingPoint { get; set; }
        public string Commission { get; set; }
        public string Hold { get; set; }
        public string Unclear { get; set; }
        public string PIN { get; set; }

        public DataSet PassBookDetails()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_UserId",Pk_UserId)
            };
            DataSet ds = Connection.ExecuteQuery("GetPassbookBalance", param);
            return ds;
        }
    }

    public class Wallet
    {
        public string AddedBy1 { get; set; }
        public decimal DeductPoint { get; set; }
        public string Balance { get; set; }
        public string Result1 { get; set; }
        public string MobileNo { get; set; }
        public string ErrorMessage { get; set; }

        public string Pk_UserId { get; set; }
        public string Status { get; set; }
        public string SelfIncome { get; set; }
        public string FirstLevelIncome { get; set; }
        public string SecondLevelIncome { get; set; }
        public string ThirdLevelIncome { get; set; }
        public string FourthLevelIncome { get; set; }
        public string FifthLevelIncome { get; set; }
        public string SixthLevelIncome { get; set; }
        public string SeventhLevelIncome { get; set; }
        public string SelfOrder { get; set; }
        public string FirstLevelOrder { get; set; }
        public string SecondLevelOrder { get; set; }
        public string ThirdLevelOrder { get; set; }
        public string FourthLevelOrder { get; set; }
        public string FifthLevelOrder { get; set; }
        public string SixthLevelOrder { get; set; }

        public string SeventhLeveOrder { get; set; }
        public string TotalCleared { get; set; }
        public string TotalUncleared { get; set; }
        public string ClearedRelease { get; set; }
        public string ClearedHold { get; set; }
        public string Hold { get; set; }
        public string TDS { get; set; }
        public string TransferToBank { get; set; }

        public string SelfOrdervalue { get; set; }
        public string FirstLevelOrdervalue { get; set; }
        public string SecondLevelOrdervalue { get; set; }
        public string ThirdLevelOrdervalue { get; set; }
        public string FourthLevelOrdervalue { get; set; }
        public string FifthLevelOrdervalue { get; set; }
        public string SixthLevelOrdervalue { get; set; }
        public string SeventhLeveOrdervalue { get; set; }
        public string RefferIncome { get; set; }
        public string EightLeveOrdervalue { get; set; }
        public string EightLeveOrder { get; set; }
        public string EightLevelIncome { get; set; }

        public DataSet GetUnclearWallet()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_UserId",Pk_UserId)
            };
            DataSet ds = Connection.ExecuteQuery("GetUnclearWallet", param);
            return ds;
        }
        public DataSet GetCommissionWallet()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_UserId",Pk_UserId)
            };
            DataSet ds = Connection.ExecuteQuery("GetCommissionWallet", param);
            return ds;
        }


        public DataSet GettingShoppingPoint()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@MobileNo",MobileNo)
            };
            DataSet ds = Connection.ExecuteQuery("GetShoppingPointDeduction", param);
            return ds;
        }

        public DataSet DeductingShoppingPoint()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@MobileNo",MobileNo),
                                       new SqlParameter("@AddedBy",AddedBy1),
                                       new SqlParameter("@DeductPoint",DeductPoint)
            };
            DataSet ds = Connection.ExecuteQuery("DeductShoppingPoint", param);
            return ds;
        }
    }
    public class SupportList
    {
        public List<SupportDetails> lstsupport { get; set; }

        public string Status { get; set; }
        public string Pk_UserId { get; set; }
        public string PageNo { get; set; }
        public DataSet GetSupportList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Pk_UserId", Pk_UserId),
                                      new SqlParameter("@PageNo", PageNo),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetSupportList", para);
            return ds;
        }



    }

    public class SupportDetails
    {

        public string TicketNo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Attachmenturl { get; set; }

        public string TicketStatus { get; set; }
        public string Reply { get; set; }
        public string ReplyDate { get; set; }
    }
    public class CreateOrder
    {
        public string amount { get; set; }
        public string Pk_UserId { get; set; }
        public string Type { get; set; }
        public string TransactionType { get; set; }
        public string OrderId { get; set; }

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
    public class CreateOrderResponse
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class FetchPaymentByOrderResponse
    {
        public string PaymentId { get; set; }
        public string entity { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }

        public string OrderId { get; set; }
        public string invoice_id { get; set; }
        public string international { get; set; }
        public string method { get; set; }
        public string amount_refunded { get; set; }
        public string refund_status { get; set; }
        public string captured { get; set; }
        public string description { get; set; }
        public string card_id { get; set; }
        public string bank { get; set; }
        public string wallet { get; set; }
        public string vpa { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public notes notes { get; set; }
        public string fee { get; set; }
        public string tax { get; set; }
        public string error_code { get; set; }

        public string error_description { get; set; }
        public string error_source { get; set; }
        public string error_step { get; set; }
        public string error_reason { get; set; }
        public string created_at { get; set; }
        public string Pk_UserId { get; set; }
        public DataSet GetDataForUpdateRazorPayStatus()
        {

            DataSet ds = Connection.ExecuteQuery("GetDataForUpdateRazorPayStatus");
            return ds;
        }
        public DataSet SaveFetchPaymentResponse()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PaymentId", PaymentId),
                                      new SqlParameter("@entity", entity),
                                      new SqlParameter("@amount", amount),
                                      new SqlParameter("@currency", currency),
                                      new SqlParameter("@OrderId", OrderId),
                                      new SqlParameter("@status", status),
                                      new SqlParameter("@invoice_id", invoice_id),
                                      new SqlParameter("@international", international),
                                      new SqlParameter("@method", method),
                                      new SqlParameter("@amount_refunded", amount_refunded),
                                      new SqlParameter("@refund_status", refund_status),
                                      new SqlParameter("@captured", captured),
                                      new SqlParameter("@description", description),
                                      new SqlParameter("@card_id", card_id),
                                      new SqlParameter("@bank", bank),
                                      new SqlParameter("@wallet", wallet),
                                      new SqlParameter("@vpa", vpa),
                                      new SqlParameter("@email", email),
                                      new SqlParameter("@contact", contact),
                                      new SqlParameter("@fee", fee),
                                      new SqlParameter("@tax", tax),
                                      new SqlParameter("@error_code", error_code),
                                      new SqlParameter("@error_description", error_description),
                                      new SqlParameter("@error_source", error_source),
                                      new SqlParameter("@error_step", error_step),
                                      new SqlParameter("@error_reason", error_reason),
                                      new SqlParameter("@created_at", created_at),
                                      new SqlParameter("@Pk_UserId", Pk_UserId),

                                  };
            DataSet ds = Connection.ExecuteQuery("SaveFetchPaymentResponse", para);
            return ds;
        }

        public DataSet UpdateRazorayStatus()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PaymentId", PaymentId),
                                      new SqlParameter("@entity", entity),
                                      new SqlParameter("@amount", amount),
                                      new SqlParameter("@currency", currency),
                                      new SqlParameter("@OrderId", OrderId),
                                      new SqlParameter("@status", status),
                                      new SqlParameter("@invoice_id", invoice_id),
                                      new SqlParameter("@international", international),
                                      new SqlParameter("@method", method),
                                      new SqlParameter("@amount_refunded", amount_refunded),
                                      new SqlParameter("@refund_status", refund_status),
                                      new SqlParameter("@captured", captured),
                                      new SqlParameter("@description", description),
                                      new SqlParameter("@card_id", card_id),
                                      new SqlParameter("@bank", bank),
                                      new SqlParameter("@wallet", wallet),
                                      new SqlParameter("@vpa", vpa),
                                      new SqlParameter("@email", email),
                                      new SqlParameter("@contact", contact),
                                      new SqlParameter("@fee", fee),
                                      new SqlParameter("@tax", tax),
                                      new SqlParameter("@error_code", error_code),
                                      new SqlParameter("@error_description", error_description),
                                      new SqlParameter("@error_source", error_source),
                                      new SqlParameter("@error_step", error_step),
                                      new SqlParameter("@error_reason", error_reason),
                                      new SqlParameter("@created_at", created_at),
                                      new SqlParameter("@Pk_UserId", Pk_UserId),

                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateRazorayStatus", para);
            return ds;
        }
    }

    public class notes
    {
        public string notes_key_1 { get; set; }
        public string notes_key_2 { get; set; }
    }

    public class UpgradeId
    {
        public string ErrorMessage { get; set; }

        public string Status { get; set; }
        public string Pk_UserId { get; set; }

        public string Type { get; set; }



        public DataSet ActivateId()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_UserId",Pk_UserId),
                                       new SqlParameter("@Type",Type),

                                   };
            DataSet ds = Connection.ExecuteQuery("ActivateId", param);
            return ds;
        }

    }
    public class StateList
    {
        public List<StateDetails> lststate { get; set; }



        public string Status { get; set; }

        public DataSet GetState()
        {

            DataSet ds = Connection.ExecuteQuery("GetState");
            return ds;
        }



    }
    public class StateDetails
    {
        public string Fk_StateId { get; set; }
        public string StateName { get; set; }
    }
    public class ProductList
    {
        public List<ProductDetails> lstproduct { get; set; }

        public List<BannerDetails> lstbanner { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }

        public string Fk_MaincategoryId { get; set; }
        public string Fk_CategoryId { get; set; }
        public string Fk_SubCategoryId { get; set; }

        public string Pk_Id { get; set; }
        public string PageNo { get; set; }

        public DataSet GetProductList()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_MaincategoryId",Fk_MaincategoryId),
                                       new SqlParameter("@Fk_CategoryId",Fk_CategoryId),
                                        new SqlParameter("@Fk_SubCategoryId",Fk_SubCategoryId),
                new SqlParameter("@Pk_Id",Pk_Id),
                                          new SqlParameter("@PageNo",PageNo),

                                   };
            DataSet ds = Connection.ExecuteQuery("GetProductList", param);
            return ds;
        }
        public DataSet GetAllDashBoardDataForMobile()
        {
            SqlParameter[] param = {


                                          new SqlParameter("@PageNo",PageNo),

                                   };
            DataSet ds = Connection.ExecuteQuery("GetAllDashBoardDataForMobile", param);
            return ds;
        }
        public DataSet GetPrimeUserOfferProducts()
        {

            DataSet ds = Connection.ExecuteQuery("GetPrimeUserOfferProducts");
            return ds;
        }

    }
    public class BannerDetails
    {
        public string BannerImage { get; set; }
    }
    public class MainCategoryDetailsForDash
    {
        public string PK_MainCategoryID { get; set; }
        public string MainCategoryName { get; set; }
        public string Image { get; set; }
    }
    public class ProductDetails
    {

        public string ProductName { get; set; }
        public string Pk_ProductId { get; set; }
        public string MRP { get; set; }
        public string OfferedPrice { get; set; }

        public string Images { get; set; }
        public string Discount { get; set; }
        public string DP { get; set; }
    }
    public class OrderValue
    {
        public List<OrderValueDetails> lstdetails { get; set; }

        public string Status { get; set; }
        public string Pk_UserId { get; set; }
        public string Level { get; set; }
        public string OrderType { get; set; }
        public string PageNo { get; set; }
        public string Orders { get; set; }
        public string Value { get; set; }
        public string TotalBusiness { get; set; }
        public string TotalEarnings { get; set; }

        public DataSet GetOrderValue()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PageNo", PageNo),
                                      new SqlParameter("@Type", OrderType),
                                      new SqlParameter("@Level", Level),
                                      new SqlParameter("@Fk_UserId", Pk_UserId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetOrderValue", para);
            return ds;
        }



    }

    public class MSCLShoppingAPICalculator
    {
        public string Brand { get; set; }
        public string Shopping { get; set; }
        public string Insurance { get; set; }
        public string Medicine { get; set; }
        public string Travel { get; set; }
        public string Other { get; set; }
        public string Status { get; set; }
        public string Utilities { get; set; }
        public string Level7 { get; set; }
        public string Level6 { get; set; }
        public string Level5 { get; set; }
        public string Level4 { get; set; }
        public string Level3 { get; set; }
        public string Level2 { get; set; }
        public string Level1 { get; set; }
        public string Userprofit { get; set; }

        public DataSet GetMSCLShoppingAPICalculator()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Level1", Level1),
                                      new SqlParameter("@Level2", Level2),
                                      new SqlParameter("@Level3", Level3),
                                      new SqlParameter("@Level4", Level4),
                                      new SqlParameter("@Level5", Level5),
                                      new SqlParameter("@Level6", Level6),
                                      new SqlParameter("@Level7", Level7),
                                      new SqlParameter("@Utilities", Utilities),
                                      new SqlParameter("@Brand", Brand),
                                      new SqlParameter("@Shopping", Shopping),
                                      new SqlParameter("@Insurance", Insurance),
                                      new SqlParameter("@Medicine", Medicine),
                                      new SqlParameter("@Travel", Travel),
                                      new SqlParameter("@Other", Other),

                                  };
            DataSet ds = Connection.ExecuteQuery("MSCLShoppingAPICalculator", para);
            return ds;
        }



    }
    public class OrderValueDetails
    {

        public string IncomeType { get; set; }
        public string Business { get; set; }
        public string Earnings { get; set; }


    }


    public class CommissionLedger
    {
        public List<CommissionDetails> lstledger { get; set; }

        public string Status { get; set; }
        public string LoginId { get; set; }
        public string PageNo { get; set; }
        public string TotalDebit { get; set; }
        public string TotalCredit { get; set; }
        public string TotalBalance { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }


        public DataSet GetCommissionLedger()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PageNo", PageNo),
                                       new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetCommissionwalletLedger", para);
            return ds;
        }

    }

    public class CommissionDetails
    {

        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }

        public string Balance { get; set; }



    }
    public class RecentRecharges
    {
        public List<RecharegeDetails> lstdetails { get; set; }

        public string Status { get; set; }
        public string Fk_UserId { get; set; }
        public string Type { get; set; }

        public DataSet GetRecentRecharges()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@Type", Type),


                                  };
            DataSet ds = Connection.ExecuteQuery("GetRecentRecharges", para);
            return ds;
        }


    }

    public class RecharegeDetails
    {

        public string Amount { get; set; }
        public string Number { get; set; }
        public string Narration { get; set; }
        public string Operator { get; set; }

        public string PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public string Image { get; set; }
    }

    public class RechargeHistory
    {
        public List<Recharge> lstdetails { get; set; }
        public List<Header> lstheader { get; set; }

        public string Fk_UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PageNo { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public DataSet GetINRReports()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@Type", Type),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@PageNo", PageNo),


                                  };
            DataSet ds = Connection.ExecuteQuery("GetINRReports", para);
            return ds;
        }



    }
    public class Recharge
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string StorName { get; set; }
        public string SaleDate { get; set; }
        public string Icon { get; set; }
        public string Number { get; set; }
    }
    public class Header
    {
        public string HeaderName { get; set; }
    }
    public class UpdateNotification
    {
        public List<NotificationDetails> lstnotification { get; set; }

        public string Fk_UserId { get; set; }
        public string IsRead { get; set; }

        public string Status { get; set; }
        public string PageNo { get; set; }

        public DataSet UpdatenotificationData()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@Status", IsRead),
                                    new SqlParameter("@PageNo", PageNo),


                                  };
            DataSet ds = Connection.ExecuteQuery("NotificationStatus", para);
            return ds;
        }



    }
    public class NotificationDetails
    {
        public string Title { get; set; }
        public string Notification { get; set; }

        public string IsRead { get; set; }

        public string NotificationDate { get; set; }
    }
    public class OffLinePayment
    {

        public string Fk_UserId { get; set; }
        public string PaymentMode { get; set; }
        public string ImageUrl { get; set; }

        public string Status { get; set; }
        public string Amount { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string ErrorMessage { get; set; }
        public string Narration { get; set; }

        public DataSet SaveOffLinePayment()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@PaymnetMode", PaymentMode),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@TransactionNo", TransactionNo),
                                      new SqlParameter("@TransactionDate", TransactionDate),
                                      new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BankBranch", BranchName),

                                      new SqlParameter("@ImageUrl", ImageUrl),


                                  };
            DataSet ds = Connection.ExecuteQuery("AddFundOffline", para);
            return ds;
        }



    }
    public class Popup
    {
        public List<PopupDetails> lstdetails { get; set; }

        public string Status { get; set; }

        public DataSet GetPopupData()
        {

            DataSet ds = Connection.ExecuteQuery("GetAndroidPopUpList");
            return ds;
        }


    }
    public class PopupDetails
    {

        public string PopUpName { get; set; }
        public string PopUpImage { get; set; }

    }
    public class OtherDetails
    {
        public List<OtherData> OtherData { get; set; }
        public string Name { get; set; }
        public string Pk_Id { get; set; }
        public string Image { get; set; }
    }
    public class OtherData
    {
        public string Pk_ProductId { get; set; }
        public string ProductName { get; set; }
        public string MRP { get; set; }
        public string OfferedPrice { get; set; }
        public string Images { get; set; }
    }
    public class ShoppingDashBoardData
    {


        public List<BannerDetails> lstbanner { get; set; }

        public List<MainCategoryDetailsForDash> lstmaincategory { get; set; }

        public List<OtherDetails> lstotherdetails { get; set; }

        public string Status { get; set; }


        public DataSet GetDashBoardDataForMobile()
        {

            DataSet ds = Connection.ExecuteQuery("GetDashBoardDataForMobile");
            return ds;
        }



    }

    public class PackageRoot
    {
        public string Status { get; set; }
        public string PK_UserId { get; set; }
        public string ItemImageBaseUrl { get; set; }
        public List<Package> Packages { get; set; }
        public DataSet GetPackages()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_UserId", PK_UserId)
                                  };
            DataSet ds = Connection.ExecuteQuery("PackageList", para);
            return ds;
        }
    }

    public class Package
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public List<PackageItem> PackageItems { get; set; }
        public string PackageClaimedStatus { get; set; }
        public string IsPackageClaimed { get; set; }
    }
    public class PackageItem
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class DownlineRequest
    {
        public string LoginId { get; set; }
        public DataSet GetDownlineWeekly()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDownlineWeeklyReport", para);
            return ds;
        }
    }
    public class DownlineResponse
    {
        public string Status { get; set; }
        public List<DownlineItem> Downlines { get; set; }
        public List<ClubItem> Clubs { get; set; }
    }
    public class DownlineItem
    {
        public string LoginId { get; set; }
        public string Leg { get; set; }
        public string Downline { get; set; }
    }
    public class ClubItem
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }


    public class PackageClaim
    {
        public string PK_UserId { get; set; }
        public string Status { get; set; }
        public string FK_PackageId { get; set; }
        public DataSet Claim()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_UserId", PK_UserId),
                                      new SqlParameter("@FK_PackageId", FK_PackageId)
                                  };
            DataSet ds = Connection.ExecuteQuery("ClaimPackage", para);
            return ds;
        }
    }
}