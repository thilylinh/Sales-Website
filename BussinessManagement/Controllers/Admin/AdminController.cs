using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BussinessManagement.Models;

namespace BussinessManagement.Controllers
{
    public class AdminController : Controller
    {
        BussinessEntities db = new BussinessEntities();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Member member)
        {
            var ojectMember = db.Members.Where(m => m.Account == member.Account && m.Password == member.Password).SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (ojectMember != null)
                {
                    IEnumerable<MemberType_Role> lstRole = db.MemberType_Role.Where(n => n.MemberTypeID == ojectMember.IDTypeMember);
                    string role = "";
                    foreach (var item in lstRole)
                    {
                        role += item.Role.ID + ",";
                    }
                    role = role.Substring(0, role.Length - 1);
                    Session["Member"] = ojectMember;
                    Authorization(ojectMember.Account, role);
                    //FormsAuthentication.SetAuthCookie(member.Account, false);
                    return RedirectToAction("Index", "Statistics");
                }
            }
            //ModelState.AddModelError("", "login fail");
            return View();
        }
        public void Authorization(string account, string role)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                          account,
                                          DateTime.Now,
                                          DateTime.Now.AddHours(3),
                                          false,
                                          role,
                                          FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        public ActionResult AuthorizationError()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Member"] = null;
            return RedirectToAction("Login", "Admin");
        }
    }
}