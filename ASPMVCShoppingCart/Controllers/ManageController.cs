﻿using ASPMVCShoppingCart.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ASPMVCShoppingCart.Controllers
{
    public class ManageController : Controller
    {
        private DemoDBEntities db = new DemoDBEntities();

        //
        // GET: /Manage/Login

        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Manage/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(mLogin login)
        {
            if (login != null &&
                login.User != null &&
                login.Password != null)
            {
                if (login.User.Equals("admin") ||
                   login.Password.Equals("admin"))
                {
                    login.setLoginOk();
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        //
        // GET: /Manage/

        public ActionResult Index(int? page)
        {
            if (ASPMVCShoppingCart.Models.mLogin.IsLogged())
            {
                return View(daoAccess.getInstance().getList().ToPagedList(page ?? 1, 15));
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //
        // GET: /Manage/Create

        public ActionResult Create()
        {
            if (ASPMVCShoppingCart.Models.mLogin.IsLogged())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //
        // POST: /Manage/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblProduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                daoAccess.getInstance().createData(tblproduct);
                return RedirectToAction("Index");
            }

            return View(tblproduct);
        }

        //
        // GET: /Manage/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tblProduct tblproduct = daoAccess.getInstance().Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        //
        // POST: /Manage/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblProduct tblproduct)
        {
            if (ModelState.IsValid)
            {
                daoAccess.getInstance().Edit(tblproduct);
                return RedirectToAction("Index");
            }
            return View(tblproduct);
        }

        //
        // GET: /Manage/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblProduct tblproduct = daoAccess.getInstance().Find(id);
            if (tblproduct == null)
            {
                return HttpNotFound();
            }
            return View(tblproduct);
        }

        //
        // POST: /Manage/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct tblproduct = daoAccess.getInstance().Find(id);
            daoAccess.getInstance().Remove(tblproduct);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}