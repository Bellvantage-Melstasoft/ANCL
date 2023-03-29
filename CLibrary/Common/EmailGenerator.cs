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
using System.Web;
using System.Web.UI.WebControls;

namespace CLibrary.Common
{
    public class EmailGenerator
    {

        public static void SendEmail(List<string> to, string subject, string message, bool IsHtml) {
            try {

                MailAddress from = new MailAddress("admin@ezbidlanka.com", "EzBidLanka");
                MailAddress admin = new MailAddress("salman.primary@gmail.com");
                MailMessage mail = new MailMessage(from, admin);
                mail.Subject = subject + " " + LocalTime.Now.ToString("dd/MMM/yyyy") + " UAT";
                mail.Body = message;
                mail.IsBodyHtml = IsHtml;
                for (int i = 0; i < to.Count; i++) {
                    mail.CC.Add(new MailAddress(to[i]));
                }
                using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 587)) {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential("admin@ezbidlanka.com", "Bell@1234");
                    sc.Send(mail);
                }
            }
            catch (Exception ex) {
            }
        }
        public static void SendEmail(string to,string subject, string message , System.Web.HttpContext Current)
        {
            try
            {
                MailMessage mail = new MailMessage("noreply@melstasoft.com", to);
                SmtpClient client = new SmtpClient();
                client.Port = 465;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("noreply@melstasoft.com", "PMN@Bell#321");
                client.Host = "maxcluster1.pvtwebs.com";
                mail.Subject = subject;
                mail.Body = message;
                //               
                string x = Current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
                string mediaType = MediaTypeNames.Image.Jpeg;
                LinkedResource logo = new LinkedResource(x, mediaType);
                logo.ContentId = Guid.NewGuid().ToString();
                logo.ContentId = "companylogo";
                logo.ContentType.MediaType = mediaType;
                logo.ContentType.Name = logo.ContentId;
                logo.TransferEncoding = TransferEncoding.Base64;
                AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
                av1.LinkedResources.Add(logo);
                mail.AlternateViews.Add(av1);
                mail.IsBodyHtml = true;

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static int SendEmailWithAttachement(string to, string subject, string message, System.Web.HttpContext Current , List<PrDetailsV2> prdetails , string createdUserEmail)
        {
            try
            {
                MailMessage mail = new MailMessage("NoReply@ezbidlanka.com", to);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("NoReply@ezbidlanka.com", "ezbidlive@123");
                mail.CC.Add(new MailAddress(createdUserEmail));
                client.Host = "mail.ezbidlanka.com";
                mail.Subject = subject;
                mail.Body = message;
                //               
                string imagePath = Current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
                string mediaType = MediaTypeNames.Image.Jpeg;
                LinkedResource logo = new LinkedResource(imagePath, mediaType);
                logo.ContentId = Guid.NewGuid().ToString();
                logo.ContentId = "companylogo";
                logo.ContentType.MediaType = mediaType;
                logo.ContentType.Name = logo.ContentId;
                logo.TransferEncoding = TransferEncoding.Base64;
                AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
                av1.LinkedResources.Add(logo);
                mail.AlternateViews.Add(av1);
                mail.IsBodyHtml = true;
                //
                System.Net.Mail.Attachment attachment;
                for (int t = 0; t < prdetails.Count; ++t)
                {
                    for (int r = 0; r < prdetails[t].PrSupportiveDocuments.Count; ++r) {
                        string x = prdetails[t].PrSupportiveDocuments[r].FilePath;
                        string extension = Path.GetExtension(x);
                        string p = Current.Server.MapPath(prdetails[t].PrSupportiveDocuments[r].FilePath);
                        attachment = new System.Net.Mail.Attachment(p);
                        attachment.Name = prdetails[t].ItemName + extension;
                        mail.Attachments.Add(attachment);
                    }
                }
                

                client.Send(mail);
                return 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void SendEmailV2(List<string> to, string subject, string message, bool IsHtml)
        {
            try
            {
                MailMessage mail = new MailMessage("NoReply@ezbidlanka.com", "ezbidlive@123");
                SmtpClient client = new SmtpClient();
                client.Port = 8889;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("NoReply@ezbidlanka.com", "ezbidlive@123");
                client.Host = "mail.ezbidlanka.com";
                mail.Subject = subject + " " + LocalTime.Now.ToString("dd/MMM/yyyy") + " UAT";
                mail.Body = message;
                mail.IsBodyHtml = IsHtml;
                for (int i = 0; i < to.Count; i++)
                {
                    mail.CC.Add(new MailAddress(to[i]));
                }
                client.Send(mail);
            }
            catch (Exception)
            {
            }
        }



        public static void SendEmailWithAttachement(string to, string subject, string message, HttpContext current, GridView gvStandardImageAttachment, GridView gvSupportiveDocumentAttachment, GridView gvReplacementImageAttachment, string createdUserEmail) {
            try {


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.UseDefaultCredentials = false;

                mail.From = new MailAddress("procurement@lakehouse.lk");
                mail.To.Add(to);
                mail.CC.Add(new MailAddress(createdUserEmail));
                mail.Subject = subject;
                mail.Body = message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("procurement@lakehouse.lk", "user@123lh");
                SmtpServer.EnableSsl = true;
                
          
                    string imagePath = current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
                    string mediaType = MediaTypeNames.Image.Jpeg;
                    LinkedResource logo = new LinkedResource(imagePath, mediaType);
                    logo.ContentId = Guid.NewGuid().ToString();
                    logo.ContentId = "companylogo";
                    logo.ContentType.MediaType = mediaType;
                    logo.ContentType.Name = logo.ContentId;
                    logo.TransferEncoding = TransferEncoding.Base64;
                    AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
                    av1.LinkedResources.Add(logo);
                    mail.AlternateViews.Add(av1);
                    mail.IsBodyHtml = true;
                    //
                    System.Net.Mail.Attachment attachment;
                    for (int t = 0; t < gvStandardImageAttachment.Rows.Count; ++t) {
                        string x = gvStandardImageAttachment.Rows[t].Cells[2].Text;
                        string extension = Path.GetExtension(x);
                        string p = current.Server.MapPath(gvStandardImageAttachment.Rows[t].Cells[2].Text); //filepath
                        attachment = new System.Net.Mail.Attachment(p);
                        attachment.Name = gvStandardImageAttachment.Rows[t].Cells[3].Text;
                        mail.Attachments.Add(attachment);
                    }

                    for (int t = 0; t < gvSupportiveDocumentAttachment.Rows.Count; ++t) {
                        string x = gvSupportiveDocumentAttachment.Rows[t].Cells[2].Text;
                        string extension = Path.GetExtension(x);
                        string p = current.Server.MapPath(gvSupportiveDocumentAttachment.Rows[t].Cells[2].Text); //filepath
                        attachment = new System.Net.Mail.Attachment(p);
                        attachment.Name = gvSupportiveDocumentAttachment.Rows[t].Cells[3].Text;
                        mail.Attachments.Add(attachment);
                    }

                    for (int t = 0; t < gvReplacementImageAttachment.Rows.Count; ++t) {
                        string x = gvReplacementImageAttachment.Rows[t].Cells[2].Text;
                        string extension = Path.GetExtension(x);
                        string p = current.Server.MapPath(gvReplacementImageAttachment.Rows[t].Cells[2].Text); //filepath
                        attachment = new System.Net.Mail.Attachment(p);
                        attachment.Name = gvReplacementImageAttachment.Rows[t].Cells[3].Text;
                        mail.Attachments.Add(attachment);
                    }

                SmtpServer.Send(mail);
                //}
            }
            catch (Exception ex) {
                throw;
            }
        }


        //public static void SendEmailWithAttachement(string to, string subject, string message, HttpContext current, GridView gvStandardImageAttachment, GridView gvSupportiveDocumentAttachment, GridView gvReplacementImageAttachment, string createdUserEmail) {
        //    try {
        //        MailMessage mail = new MailMessage("admin@ezbidlanka.com", to);
        //        mail.CC.Add(new MailAddress(createdUserEmail));
        //        mail.Subject = subject;
        //        mail.Body = message;

        //        //SmtpClient sc = new SmtpClient();
        //        using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 587)) {
        //            sc.EnableSsl = true;
        //            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            sc.UseDefaultCredentials = false;
        //            sc.Credentials = new NetworkCredential("admin@ezbidlanka.com", "Bell@1234");


        //            //SmtpClient client = new SmtpClient();
        //            //client.Port = 587;


        //            //client.Host = "smtp.yandex.ru";
        //            //               
        //            string imagePath = current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
        //            string mediaType = MediaTypeNames.Image.Jpeg;
        //            LinkedResource logo = new LinkedResource(imagePath, mediaType);
        //            logo.ContentId = Guid.NewGuid().ToString();
        //            logo.ContentId = "companylogo";
        //            logo.ContentType.MediaType = mediaType;
        //            logo.ContentType.Name = logo.ContentId;
        //            logo.TransferEncoding = TransferEncoding.Base64;
        //            AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
        //            av1.LinkedResources.Add(logo);
        //            mail.AlternateViews.Add(av1);
        //            mail.IsBodyHtml = true;
        //            //
        //            System.Net.Mail.Attachment attachment;
        //            for (int t = 0; t < gvStandardImageAttachment.Rows.Count; ++t) {
        //                string x = gvStandardImageAttachment.Rows[t].Cells[2].Text;
        //                string extension = Path.GetExtension(x);
        //                string p = current.Server.MapPath(gvStandardImageAttachment.Rows[t].Cells[2].Text); //filepath
        //                attachment = new System.Net.Mail.Attachment(p);
        //                attachment.Name = gvStandardImageAttachment.Rows[t].Cells[3].Text;
        //                mail.Attachments.Add(attachment);
        //            }

        //            for (int t = 0; t < gvSupportiveDocumentAttachment.Rows.Count; ++t) {
        //                string x = gvSupportiveDocumentAttachment.Rows[t].Cells[2].Text;
        //                string extension = Path.GetExtension(x);
        //                string p = current.Server.MapPath(gvSupportiveDocumentAttachment.Rows[t].Cells[2].Text); //filepath
        //                attachment = new System.Net.Mail.Attachment(p);
        //                attachment.Name = gvSupportiveDocumentAttachment.Rows[t].Cells[3].Text;
        //                mail.Attachments.Add(attachment);
        //            }

        //            for (int t = 0; t < gvReplacementImageAttachment.Rows.Count; ++t) {
        //                string x = gvReplacementImageAttachment.Rows[t].Cells[2].Text;
        //                string extension = Path.GetExtension(x);
        //                string p = current.Server.MapPath(gvReplacementImageAttachment.Rows[t].Cells[2].Text); //filepath
        //                attachment = new System.Net.Mail.Attachment(p);
        //                attachment.Name = gvReplacementImageAttachment.Rows[t].Cells[3].Text;
        //                mail.Attachments.Add(attachment);
        //            }

        //            sc.Send(mail);
        //        }
        //    }
        //    catch (Exception ex) {
        //        throw;
        //    }
        //}
        //public static void SendEmailWithAttachement(string to, string subject, string message, HttpContext current, GridView gvStandardImageAttachment, GridView gvSupportiveDocumentAttachment, GridView gvReplacementImageAttachment, string createdUserEmail)
        //{
        //    try
        //    {
        //        MailMessage mail = new MailMessage("admin@ezbidlanka.com", to);
        //        mail.CC.Add(new MailAddress(createdUserEmail));
        //        mail.Subject = subject;
        //        mail.Body = message;

        //        //SmtpClient sc = new SmtpClient();
        //        using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 587)) {
        //            sc.EnableSsl = true;
        //            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            sc.UseDefaultCredentials = false;
        //            sc.Credentials = new NetworkCredential("admin@ezbidlanka.com", "Bell@1234");


        //            //SmtpClient client = new SmtpClient();
        //            //client.Port = 587;


        //            //client.Host = "smtp.yandex.ru";
        //            //               
        //            string imagePath = current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
        //            string mediaType = MediaTypeNames.Image.Jpeg;
        //            LinkedResource logo = new LinkedResource(imagePath, mediaType);
        //            logo.ContentId = Guid.NewGuid().ToString();
        //            logo.ContentId = "companylogo";
        //            logo.ContentType.MediaType = mediaType;
        //            logo.ContentType.Name = logo.ContentId;
        //            logo.TransferEncoding = TransferEncoding.Base64;
        //            AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
        //            av1.LinkedResources.Add(logo);
        //            mail.AlternateViews.Add(av1);
        //            mail.IsBodyHtml = true;
        //            //
        //            System.Net.Mail.Attachment attachment;
        //            for (int t = 0; t < gvStandardImageAttachment.Rows.Count; ++t) {
        //                string x = gvStandardImageAttachment.Rows[t].Cells[2].Text;
        //                string extension = Path.GetExtension(x);
        //                string p = current.Server.MapPath(gvStandardImageAttachment.Rows[t].Cells[2].Text); //filepath
        //                attachment = new System.Net.Mail.Attachment(p);
        //                attachment.Name = gvStandardImageAttachment.Rows[t].Cells[3].Text;
        //                mail.Attachments.Add(attachment);
        //            }

        //            for (int t = 0; t < gvSupportiveDocumentAttachment.Rows.Count; ++t) {
        //                string x = gvSupportiveDocumentAttachment.Rows[t].Cells[2].Text;
        //                string extension = Path.GetExtension(x);
        //                string p = current.Server.MapPath(gvSupportiveDocumentAttachment.Rows[t].Cells[2].Text); //filepath
        //                attachment = new System.Net.Mail.Attachment(p);
        //                attachment.Name = gvSupportiveDocumentAttachment.Rows[t].Cells[3].Text;
        //                mail.Attachments.Add(attachment);
        //            }

        //            for (int t = 0; t < gvReplacementImageAttachment.Rows.Count; ++t) {
        //                string x = gvReplacementImageAttachment.Rows[t].Cells[2].Text;
        //                string extension = Path.GetExtension(x);
        //                string p = current.Server.MapPath(gvReplacementImageAttachment.Rows[t].Cells[2].Text); //filepath
        //                attachment = new System.Net.Mail.Attachment(p);
        //                attachment.Name = gvReplacementImageAttachment.Rows[t].Cells[3].Text;
        //                mail.Attachments.Add(attachment);
        //            }

        //            sc.Send(mail);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
