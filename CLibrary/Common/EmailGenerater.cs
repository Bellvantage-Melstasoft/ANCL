using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Principal;
using System.Text;

namespace CLibrary.Common
{
    public class EmailGenerater
    {


        public static void SendEmail(string to,string subject, string message , System.Web.HttpContext Current)
        {
            try
            {
                //MailMessage mail = new MailMessage("NoReply@ezbidlanka.com", to);
                //SmtpClient client = new SmtpClient();
                //client.Port = 25;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("NoReply@ezbidlanka.com", "ezbidlive@123");
                //client.Host = "mail.ezbidlanka.com";
                //mail.Subject = subject;
                //mail.Body = message;
                ////               
                //string x = Current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
                //string mediaType = MediaTypeNames.Image.Jpeg;
                //LinkedResource logo = new LinkedResource(x, mediaType);
                //logo.ContentId = Guid.NewGuid().ToString();
                //logo.ContentId = "companylogo";
                //logo.ContentType.MediaType = mediaType;
                //logo.ContentType.Name = logo.ContentId;
                //logo.TransferEncoding = TransferEncoding.Base64;
                //AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
                //av1.LinkedResources.Add(logo);
                //mail.AlternateViews.Add(av1);
                //mail.IsBodyHtml = true;

                //client.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void SendEmailWithAttachement(string to, string subject, string message, System.Web.HttpContext Current , List<PR_Details> prdetails , string createdUserEmail)
        {
            try
            {
                //MailMessage mail = new MailMessage("NoReply@ezbidlanka.com", to);
                //SmtpClient client = new SmtpClient();
                //client.Port = 25;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("NoReply@ezbidlanka.com", "ezbidlive@123");
                //mail.CC.Add(new MailAddress(createdUserEmail));
                //client.Host = "mail.ezbidlanka.com";
                //mail.Subject = subject;
                //mail.Body = message;
                ////               
                //string imagePath = Current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
                //string mediaType = MediaTypeNames.Image.Jpeg;
                //LinkedResource logo = new LinkedResource(imagePath, mediaType);
                //logo.ContentId = Guid.NewGuid().ToString();
                //logo.ContentId = "companylogo";
                //logo.ContentType.MediaType = mediaType;
                //logo.ContentType.Name = logo.ContentId;
                //logo.TransferEncoding = TransferEncoding.Base64;
                //AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
                //av1.LinkedResources.Add(logo);
                //mail.AlternateViews.Add(av1);
                //mail.IsBodyHtml = true;
                ////
                //System.Net.Mail.Attachment attachment;
                //for (int t = 0; t < prdetails.Count; ++t)
                //{
                //    for (int r = 0; r < prdetails[t].SupportiveDocs.Count; ++r) {
                //        string x = prdetails[t].SupportiveDocs[r].FilePath;
                //        string extension = Path.GetExtension(x);
                //        string p = Current.Server.MapPath(prdetails[t].SupportiveDocs[r].FilePath);
                //        attachment = new System.Net.Mail.Attachment(p);
                //        attachment.Name = prdetails[t].ItemName + extension;
                //        mail.Attachments.Add(attachment);
                //    }
                //}
                

                //client.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
