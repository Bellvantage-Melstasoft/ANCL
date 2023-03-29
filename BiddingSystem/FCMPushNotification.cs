using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;

namespace BiddingSystem
{
    public class FCMPushNotification
    {
        public FCMPushNotification()
        {
            // TODO: Add constructor logic here
        }

        public bool Successful
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }


        public FCMPushNotification SendNotification(string _title, string _message, string _topic)
        {
            FCMPushNotification result = new FCMPushNotification();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;
                var requestUri = "https://fcm.googleapis.com/fcm/send";

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAeWW40X8:APA91bEUvYR1d4XRubQlf84a9SpaHWOjSXzsZpuJ6eAjse9SUmpQYdcNSii0XWsrCw-32CO5QGqIQoyUefoiUu1FJ2AQ-h2tFBrw61vq0VIogq_uBWgzaKRL9L7gBruXY-KajQQ8AOWk"));
                webRequest.Headers.Add(string.Format("Sender: id={0}", "521397653887"));
                webRequest.ContentType = "application/json";

                var data = new
                {
                    // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                    to = "dBCeu1Fqgeo:APA91bG6xVI4X2nbG0d-UEaMCr1wHsn2MgSuZ6th3i-KpHUkt4eJzKWSFEAhjYus0WKMYYWQER0k45DJklxmKBiDqXy6k6tgfvgmc7ipOUXdVPvF1aSXzoQ90l4s6nUO2286Uu8XCRGi", // Uncoment this if you want to test for single device
                    //to = "/topics/" + _topic, // this is for topic 
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        //icon="myicon"
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }
    }
}