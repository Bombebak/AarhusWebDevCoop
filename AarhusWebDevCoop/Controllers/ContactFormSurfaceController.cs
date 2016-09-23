using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AarhusWebDevCoop.Models.ViewModels;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface

        //[ChildActionOnly]
        public ActionResult Index()
        {

            return PartialView("ContactForm", new ContactFormViewModel());
        }

        [HttpPost]
        public ActionResult ContactUs(ContactFormViewModel viewModel)
        {
            IContent comment = Services.ContentService.CreateContent(viewModel.Subject, CurrentPage.Id, "Comment");

            comment.SetValue("ctfName", viewModel.Name);
            comment.SetValue("ctfEmail", viewModel.Email);
            comment.SetValue("ctfSubject", viewModel.Subject);
            comment.SetValue("ctfmessage", viewModel.Message);

            //Save
            Services.ContentService.Save(comment);

            return RedirectToCurrentUmbracoPage();

        }

        [HttpPost]
        public ActionResult ContactUs2(ContactFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                //Passes data back to the view
                TempData["TempContactMsg"] = null;
                return CurrentUmbracoPage();
            }

            MailMessage msg = new MailMessage();
            msg.To.Add("bombebak@gmail.com");
            msg.Subject = viewModel.Subject;
            msg.From = new MailAddress(viewModel.Email, viewModel.Name);
            msg.Body = viewModel.Message + "\n my email is: ";
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("bombebak@gmail.com", "Snartferie1");
                try
                {
                    smtp.Send(msg);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message + " ");
                    //throw;
                }
            }
            TempData["TempContactMsg"] = true;
            //Redirects to the same page without the data
            return RedirectToCurrentUmbracoPage();


        }
    }
}