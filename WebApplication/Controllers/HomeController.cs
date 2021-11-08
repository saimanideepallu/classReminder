using Microsoft.Ajax.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.DBContext;
using WebApplication.Models;

namespace WebApplication.Controllers
{

    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            var isLogin = Session["LoginStatus"] == null ? false : Convert.ToBoolean(Session["LoginStatus"]);

            if (isLogin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult AddEvents()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvents(AddEventsModel events)
        {
            bool status = false;

            if (events != null && Session["UserId"] != null)
            {
                try
                {

                    var e = new EventsModel();
                    string[] days = { };
                    string imgFileName = Path.GetFileNameWithoutExtension(events.ImageFile.FileName);
                    string extension = Path.GetExtension(events.ImageFile.FileName);
                    imgFileName = imgFileName + DateTime.Now.ToString("yyMMddhhmmss") + extension;
                    events.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/App_Data/Upload"), imgFileName));
                    e.ImageName = imgFileName;
                    e.EventName = events.EventName;
                    e.Location = events.Location;
                    e.UserID = Session["UserId"].ToString();

                    if (events.Recarsive == "on")
                    {
                        List<string> list = new List<string>();
                        if (events.M == "on")
                        {
                            list.Add(DayOfWeek.Monday.ToString());
                        }
                        if (events.T == "on")
                        {
                            list.Add(DayOfWeek.Tuesday.ToString());
                        }
                        if (events.W == "on")
                        {
                            list.Add(DayOfWeek.Wednesday.ToString());
                        }
                        if (events.TH == "on")
                        {
                            list.Add(DayOfWeek.Thursday.ToString());
                        }
                        if (events.F == "on")
                        {
                            list.Add(DayOfWeek.Friday.ToString());
                        }
                        if (list.Count > 0)
                        {
                            e.Days = list.ToArray();
                        }
                        else
                        {
                            e.Days = days;
                        }
                        e.StartDate = events.StartDate;
                        e.EndDate = events.EndDate;
                        e.IsRecarsive = true;
                        e.Date = null;
                    }
                    else
                    {
                        e.IsRecarsive = false;
                        e.Date = events.Date;
                        e.StartDate = null;
                        e.EndDate = null;
                    }

                    if (e != null)
                    {
                        MongoHelper.ConnectToMongoService();
                        MongoHelper.Events = MongoHelper.database.GetCollection<EventsModel>("Events");
                        MongoHelper.Events.InsertOneAsync(new EventsModel
                        {

                            EventName = e.EventName,
                            Location = e.Location,
                            Date = e.Date,
                            IsRecarsive = e.IsRecarsive,
                            Days = e.Days,
                            StartDate = e.StartDate,
                            EndDate = e.EndDate,
                            ImageName = e.ImageName,
                            UserID = e.UserID

                        });
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex.InnerException);
                    status = false;
                }
            }

            return Json(new { Status = status });
        }

        public ActionResult EditEvent(string id)
        {
            if (Session["UserId"] != null)
            {
                MongoHelper.ConnectToMongoService();
                var ev = MongoHelper.database.GetCollection<EventsViewModel>("Events");
                var result = ev.Find<EventsViewModel>(x => x._id == ObjectId.Parse(id)).FirstOrDefault();

                if (result != null)
                {
                    var e = new EventsModel
                    {
                        EventName = result.EventName,
                        Location = result.Location,
                        Date = result.Date,
                        Days = result.Days,
                        StartDate = result.StartDate,
                        EndDate = result.EndDate,
                        ImageName = "/App_Data/Upload/" + result.ImageName,
                        IsRecarsive = result.IsRecarsive
                    };

                    return Json(new { Event = e });
                    //ViewBag.EventsDetails = JsonConvert.SerializeObject(result);
                }
                else
                {
                    return Json(new { Event = "" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEventDetails(AddEventsModel events)
        {
            bool status = false;

            if (events != null && Session["UserId"] != null)
            {
                try
                {

                    var e = new EventsModel();
                    string[] days = { };
                    string imgFileName = Path.GetFileNameWithoutExtension(events.ImageFile.FileName);
                    string extension = Path.GetExtension(events.ImageFile.FileName);
                    imgFileName = imgFileName + DateTime.Now.ToString("yyMMddhhmmss") + extension;
                    events.ImageFile.SaveAs(Path.Combine(Server.MapPath("~/App_Data/Upload"), imgFileName));
                    e.ImageName = imgFileName;
                    e.EventName = events.EventName;
                    e.Location = events.Location;
                    e.UserID = Session["UserId"].ToString();

                    if (events.Recarsive == "on")
                    {
                        List<string> list = new List<string>();
                        if (events.M == "on")
                        {
                            list.Add(DayOfWeek.Monday.ToString());
                        }
                        if (events.T == "on")
                        {
                            list.Add(DayOfWeek.Tuesday.ToString());
                        }
                        if (events.W == "on")
                        {
                            list.Add(DayOfWeek.Wednesday.ToString());
                        }
                        if (events.TH == "on")
                        {
                            list.Add(DayOfWeek.Thursday.ToString());
                        }
                        if (events.F == "on")
                        {
                            list.Add(DayOfWeek.Friday.ToString());
                        }
                        if (list.Count > 0)
                        {
                            e.Days = list.ToArray();
                        }
                        else
                        {
                            e.Days = days;
                        }
                        e.StartDate = events.StartDate;
                        e.EndDate = events.EndDate;
                        e.IsRecarsive = true;
                        e.Date = null;
                    }
                    else
                    {
                        e.IsRecarsive = false;
                        e.Date = events.Date;
                        e.StartDate = null;
                        e.EndDate = null;
                    }

                    if (e != null)
                    {
                        //MongoHelper.ConnectToMongoService();
                        //MongoHelper.Events = MongoHelper.database.GetCollection<EventsModel>("Events");
                        //MongoHelper.Events.InsertOneAsync(new EventsModel
                        //{

                        //    EventName = e.EventName,
                        //    Loacation = e.Loacation,
                        //    Date = e.Date,
                        //    IsRecarsive = e.IsRecarsive,
                        //    Days = e.Days,
                        //    StartDate = e.StartDate,
                        //    EndDate = e.EndDate,
                        //    ImageName = e.ImageName,
                        //    UserID = e.UserID

                        //});
                        //status = true;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex.InnerException);
                    status = false;
                }
            }

            return Json(new { Status = status });
        }

        public ActionResult ViewEvents()
        {
            if (Session["UserId"] != null)
            {
                var uId = Session["UserId"].ToString();
                MongoHelper.ConnectToMongoService();
                var ev = MongoHelper.database.GetCollection<EventsViewModel>("Events");
                var result = ev.Find<EventsViewModel>(x => x.UserID == uId).ToList();

                if (result.Count > 0)
                {
                    ViewBag.EventsDetails = JsonConvert.SerializeObject(result);
                }
                else
                {
                    ViewBag.EventsDetails = "";
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel Reg, bool IsModel)
        {
            var status = 0;

            try
            {
                if (Reg != null)
                {
                    MongoHelper.ConnectToMongoService();
                    MongoHelper.User = MongoHelper.database.GetCollection<UserModel>("User");
                    MongoHelper.User.InsertOneAsync(new UserModel
                    {
                        FirstName = Reg.FirstName,
                        LastName = Reg.LastName,
                        Password = Reg.Password,
                        Email = Reg.Email
                    });

                    status = 1;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex.InnerException);
                status = 0;
            }

            return Json(new { Status = status, IsModel = IsModel });
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session["LoginStatus"] = null;
            Session["UserId"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Login()
        {
            try
            {
                MongoHelper.ConnectToMongoService();
                MongoHelper.User = MongoHelper.database.GetCollection<UserModel>("User");
                // var filter = Builders<UserModel>.Filter.Ne("_Id", "");
                var result = await MongoHelper.User.Find(_ => true).ToListAsync();

                if (result.Count == 0)
                {
                    DefaultUser();
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex.InnerException);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel info)
        {
            try
            {
                Session["LoginStatus"] = null;
                Session["UserId"] = null;
                MongoHelper.ConnectToMongoService();
                MongoHelper.User = MongoHelper.database.GetCollection<UserModel>("User");
                var result = MongoHelper.User.Find<UserModel>(x => x.Email == info.Email && x.Password == info.Password).FirstOrDefault();

                if (result != null)
                {
                    Session["LoginStatus"] = true;
                    Session["UserId"] = result._id;
                    FormsAuthentication.SetAuthCookie(result.FirstName, false);
                }
                else
                {
                    Session["LoginStatus"] = false;
                    Session["UserId"] = null;
                    ViewBag.LoginInfo = "Please check the credential \n information and try again.!";
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex.InnerException);
                ViewBag.LoginInfo = "Login requests cannot be completed.!";
            }
            return RedirectToAction("Index", "Home");
        }
        private void DefaultUser()
        {
            try
            {
                MongoHelper.ConnectToMongoService();
                MongoHelper.User = MongoHelper.database.GetCollection<UserModel>("User");
                MongoHelper.User.InsertOneAsync(new UserModel
                {
                    FirstName = "System",
                    LastName = "Admin",
                    Password = "admin",
                    Email = "info@email.com"
                });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex.InnerException);
            }
        }



    }
}