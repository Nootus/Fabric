using Nootus.Fabric.Web.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nootus.Fabric.Web.Core.Notification
{
    public static class SmsManager
    {
        public static string SendSms(string mobileNumber, string message)
        {
            if (!SmsSettings.Enabled)
            {
                return null;
            }

            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", SmsSettings.AuthKey);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", HttpUtility.UrlEncode(message));
            sbPostData.AppendFormat("&sender={0}", SmsSettings.SenderId);
            sbPostData.AppendFormat("&route={0}", SmsSettings.Route);

            //Call Send SMS API
            Uri sendSMSUri = new Uri("http://api.msg91.com/api/sendhttp.php");
            //Create HTTPWebrequest
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //Prepare and Add URL Encoded data
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            //Specify post method
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            //Get the response
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            //Close the response
            reader.Close();
            response.Close();

            return responseString;
        }
    }
}
