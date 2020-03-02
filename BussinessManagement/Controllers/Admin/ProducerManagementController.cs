using BussinessManagement.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BussinessManagement.Controllers.Admin
{
    public class ProducerManagementController : Controller
    {
        private BussinessEntities db = new BussinessEntities();

        // GET: ProducerManagement
        public ActionResult Index()
        {
            var lstProducer = db.Producers;
            return View(lstProducer);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Producer producer, HttpPostedFileBase logo)
        {
            if (logo.ContentLength > 0)
            {
                if (logo.ContentType != "image/jpeg" && logo.ContentType != "image/png" && logo.ContentType != "image/gif" && logo.ContentType != "image/jpg")
                {
                    ViewBag.Upload += "Logo is not valid";
                }
                else
                {
                    var fileName = Path.GetFileName(logo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/ProductImage"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Error = "logo is already exist";
                        return View();
                    }
                    else
                    {
                        logo.SaveAs(path);
                        producer.Logo = fileName;
                    }
                }
            }

            db.Producers.Add(producer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var producer = db.Producers.SingleOrDefault(n => n.ID == id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        [HttpPost]
        public ActionResult Edit(Producer producer, HttpPostedFileBase logo)
        {
            if (logo.ContentLength > 0)
            {
                if (logo.ContentType != "image/jpeg" && logo.ContentType != "image/png" && logo.ContentType != "image/gif" && logo.ContentType != "image/jpg")
                {
                    ViewBag.Upload += "Logo is not valid";
                }
                else
                {
                    var fileName = Path.GetFileName(logo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/ProductImage"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Error = "logo is already exist";
                        return View();
                    }
                    else
                    {
                        logo.SaveAs(path);
                        producer.Logo = fileName;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(producer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producer);
        }
    }
}