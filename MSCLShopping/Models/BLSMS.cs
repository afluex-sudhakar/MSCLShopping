using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace MSCLShopping
{
    static public class BLSMS
    {
        //static public void SendSMS(string Mobile, string Message)
        //{
        //    try
        //    {
        //        string SMSAPI = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=MSCLShopping[AND]password=aapka123[AND]sender=APKBIZ[AND]to=[MOBILE][AND]message=[MESSAGE][AND]reqid=1[AND]format={json}[AND]route_id=39[AND]callback=#[AND]unique=0[AND]sendondate=" + DateTime.Now.ToString();

        //        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
        //        HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public static string SendSMSNew(string mobile, string msg)
        {
            string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=dwpe&password=dw@2020pe&sender=DWPEFU&to=" + mobile + "&message=" + msg + "& reqid = 1 & format ={ json}&route_id = 39 & callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";

            WebRequest request = HttpWebRequest.Create(strUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }

        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }

        static public string OTP(string MemberName, string OTP)
        {
            string Message = ConfigurationSettings.AppSettings["OTP"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[OTP]", OTP);

            return Message;
        }

    }
}
