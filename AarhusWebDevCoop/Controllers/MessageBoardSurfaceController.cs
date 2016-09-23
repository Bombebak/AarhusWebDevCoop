using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AarhusWebDevCoop.Models.ViewModels;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    public class MessageBoardSurfaceController : SurfaceController
    {
        // GET: MessageBoardSurface
        [HttpGet]
        public ActionResult CreateMessage()
        {
            return PartialView("MessageForm", new MessageBoardViewModel());
        }

        [HttpPost]
        public ActionResult CreateMessage(MessageBoardViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            IContent msg = Services.ContentService.CreateContent(viewModel.Name, CurrentPage.Id, "message");

            var dt = DateTime.Now;
            String strDate = "";
            strDate = dt.ToString("dddd, dd MMMM yyyy HH:mm");

            msg.SetValue("msgbName", viewModel.Name);
            msg.SetValue("msgbMessage", viewModel.Message);
            msg.SetValue("msgbCreateDate", strDate);

            
            try
            {
                //Saves but doesnt publish IContent
                //Services.ContentService.Save(msg);
                //Saves and publish IContent
                Services.ContentService.SaveAndPublishWithStatus(msg);
            }
            catch (Exception e)
            {
                TempData["TempMsgBoard"] = "<div class='alert alert-danger'><p>Message was NOT created</p>" +
                                                        "<p>Something went wrong. Try again later or contact your administrator.</p></div>";
                ModelState.AddModelError(string.Empty, "" + e.InnerException.ToString());
            }
            TempData["TempMsgBoard"] = "<div class='alert alert-success'><p>Message created</p>" +
                                                        "<p>You have succesfully created a message.</p></div>";

            return RedirectToCurrentUmbracoPage();
        }

        [HttpPost]
        public ActionResult DeleteMessage(int contentId, int redirectId)
        {
            var cs = Services.ContentService;
            var msg = cs.GetById(contentId);
            if (msg != null)
            {
                try
                {
                    cs.Delete(msg);
                }
                catch (Exception e)
                {
                    TempData["TempMsgBoard"] = "<div class='alert alert-danger'><p>Message was NOT created</p>" +
                                                        "<p>Something went wrong. Try again later or contact your administrator.</p></div>";
                    ModelState.AddModelError(string.Empty, "" + e.InnerException.ToString());
                }
                
            }
            TempData["TempMsgBoard"] = "<div class='alert alert-success'><p>Message deleted</p>" +
                                                        "<p>You have succesfully deleted a message.</p></div>";
            return RedirectToUmbracoPage(redirectId);
            //return RedirectToCurrentUmbracoPage();
        }
    }
}