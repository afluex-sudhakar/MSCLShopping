using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static MSCLShopping.Models.Home;

namespace MSCLShopping.Controllers
{
    public class WhatsUpShareController : Controller
    {
        private static string INSTANCE_ID = "YOUR_INSTANCE_ID";
        private static string CLIENT_ID = "YOUR_CLIENT_ID_HERE";
        private static string CLIENT_SECRET = "YOUR_CLIENT_SECRET_HERE";
        private static string IMAGE_SINGLE_API_URL = "http://api.whatsmate.net/v3/whatsapp/single/image/message/" + INSTANCE_ID;
        public ActionResult Index()
        {
            string l= GetBase64StringForImage("D:\\Repositories\\MSCLShopping\\MSCLShopping\\MSCLShopping\\images\\BrandImage\\5332dfd6-6c6d-401a-b2b7-26aeba52a886.jpg");
            bool result = sendImage("7310000413", l, "Product Share");
            return View();
        }
       
        protected static string GetBase64StringForImage(string imgPath)
        {
            string imagePath = @"MSCLShopping.com/images/SubCategoryImage/ed9dd235-39e6-4b78-b8a8-528c12d3dd40.jpg";
           
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        public bool sendImage(string number, string base64Content, string caption)
        {
            bool success = true;

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                    client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                    SingleImagePayload payloadObj = new SingleImagePayload() { number = number, caption = caption, image = base64Content };
                    string postData = (new JavaScriptSerializer()).Serialize(payloadObj);

                    client.Encoding = Encoding.UTF8;
                    string response = client.UploadString(IMAGE_SINGLE_API_URL, postData);
                    Console.WriteLine(response);
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
                Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                String body = reader.ReadToEnd();
                Console.WriteLine(body);
                success = false;
            }

            return success;
        }
    }
}