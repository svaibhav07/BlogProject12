using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace BlogProject12.Utilities
{
    public class EmailConfig
    {

        public IWebHostEnvironment _Host;


        //first method
        /*public static async Task SendMail(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress("blogworldproject12@gmail.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "blogworldproject12@gmail.com",
                    Password = "Vaibhav@18"
                };

                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                smtp.Send(message);
            }
      
System.Web.HttpContext  


        }*/


        //second method

  

        public static string GetBody(string type,string name)
        {
            string str = null;
            string[] paths = { @"C:\Users", "repos", "SchoolManagement.Utilities", "EmailTemplates","Welcome.html" };

            switch (type)
            {
                case "welcome":
                    //using (StreamReader reader = new StreamReader(Path.Combine(@"C:\Users\vaibhav.singh\source\repos\BlogProject12\BlogProject.Utilities\Email\EmailTemplates", "Welcome.html")))
                    using (StreamReader reader = new StreamReader(@"/EmailTemplates/Welcome.html"))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{name}", "vaibhav");

                   
                  
                break;

                case "notice":
                    using (StreamReader reader = new StreamReader(Path.Combine(@"C:\Users\vaibhav.singh\source\repos\BlogProject12\BlogProject.Utilities\Email\EmailTemplates", "Notice.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{name}", "vaibhav");

                    
                break;

                case "feepayment":
                    using (StreamReader reader = new StreamReader(Path.Combine(@"C:\Users\vaibhav.singh\source\repos\BlogProject12\BlogProject.Utilities\Email\EmailTemplates", "Feepayment.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{name}", "vaibhav");

                    
                    break;

                case "feereminder":
                    using (StreamReader reader = new StreamReader(Path.Combine(@"C:\Users\vaibhav.singh\source\repos\BlogProject12\BlogProject.Utilities\Email\EmailTemplates", "Welcome.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{name}", "vaibhav");

                    
                    break;

                case "assignment":
                    using (StreamReader reader = new StreamReader(Path.Combine(@"C:\Users\vaibhav.singh\source\repos\BlogProject12\BlogProject.Utilities\Email\EmailTemplates", "Welcome.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{name}", "vaibhav");

                   
                    break;


            }
            return str;
        
        }


       /* public static void SendMail(string to, string subject, string body)
        {

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("blogworldproject18@gmail.com");
                message.To.Add(to);
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("blogworldproject18@gmail.com", "Vaibhav@18");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e) 
           {

                


            }

        }*/


        public static void SendMail(string to, string subject,string body)
        {
            

            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("blogworldproject18@gmail.com");
                message.To.Add(to);
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html 
                message.Body = GetBody("welcome","vaibhav");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("blogworldproject18@gmail.com", "Vaibhav@18");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {




            }

        }
    }
}
