using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using contactosProyecto.Models;
using contactosProyecto.Models.ViewModels;

namespace contactosProyecto.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index(string buscar)
        {
            List<ContactViewModel> list;
            using (PhonebookEntities db = new PhonebookEntities())
            {
                list = (from d in db.Contacts
                        select new ContactViewModel
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Number = d.Number,
                            Detail = d.Detail,
                            Favorite = d.Favorite,
                            Emergency = d.Emergency
                        }).Where(x => x.Name.Contains(buscar) || buscar == null).OrderBy(x => x.Name).ToList();


            }
            return View(list);
        }

        public ActionResult Favorite(string buscar)
        {
            List<ContactViewModel> list;
            using (PhonebookEntities db = new PhonebookEntities())
            {
                list = (from d in db.Contacts
                        select new ContactViewModel
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Number = d.Number,
                            Detail = d.Detail,
                            Favorite = d.Favorite,
                            Emergency = d.Emergency
                        }).Where(x => x.Favorite == true).ToList();


            }
            return View(list);
        }

        public ActionResult Emergency(string buscar)
        {
            List<ContactViewModel> list;
            using (PhonebookEntities db = new PhonebookEntities())
            {
                list = (from d in db.Contacts
                        select new ContactViewModel
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Number = d.Number,
                            Detail = d.Detail,
                            Favorite = d.Favorite,
                            Emergency = d.Emergency
                        }).Where(x => x.Emergency == true).ToList();


            }
            return View(list);
        }

        public JsonResult BuscarNombre(string term)
        {
            using (PhonebookEntities db = new PhonebookEntities())
            {
                var resultado = db.Contacts.Where(x => x.Name.Contains(term))
                                .Select(x => x.Name).Take(5).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult New()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult New(ContactFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PhonebookEntities db = new PhonebookEntities())
                    {
                        var c = new Contact();
                        c.Name = model.Name;
                        c.Number = model.Number;
                        c.Detail = model.Detail;
                        c.Favorite = model.Favorite;
                        c.Emergency = model.Emergency;
                        db.Contacts.Add(c);
                        db.SaveChanges();
                        //Json(new { success = true });
                    }
                    return Redirect("/");
                }
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Edit(int Id)
        {
            ContactFormModel m = new ContactFormModel();

            using (PhonebookEntities db = new PhonebookEntities())
            {
                var editData = db.Contacts.Find(Id);
                m.Id = editData.Id;
                m.Name = editData.Name;
                m.Number = editData.Number;
                m.Detail = editData.Detail;
                m.Favorite = editData.Favorite;
                m.Emergency = editData.Emergency;
            }

            return PartialView(m);
        }

        [HttpPost]
        public ActionResult Edit(ContactFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PhonebookEntities db = new PhonebookEntities())
                    {
                        var c = db.Contacts.Find(model.Id);
                        c.Id = model.Id;
                        c.Name = model.Name;
                        c.Number = model.Number;
                        c.Detail = model.Detail;
                        c.Favorite = model.Favorite;
                        c.Emergency = model.Emergency;
                        db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Contact/");
                }
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Remove(int Id)
        {

            using (PhonebookEntities db = new PhonebookEntities())
            {
                var Data = db.Contacts.Find(Id);
                db.Contacts.Remove(Data);
                db.SaveChanges();
            }

            return Redirect("~/Contact/");
        }

    }
}