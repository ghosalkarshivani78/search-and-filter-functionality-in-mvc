using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using createform.Models;

namespace createform.Controllers
{ 
    public class Default1Controller : Controller
    {
        private testEntities db = new testEntities();

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            return View(db.countries.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ViewResult Details(int id)
        {
            country country = db.countries.Single(c => c.countryid == id);
            return View(country);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(country country)
        {
            if (ModelState.IsValid)
            {
                db.countries.AddObject(country);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(country);
        }
        
        //
        // GET: /Default1/Edit/5
 
        public ActionResult Edit(int id)
        {
            country country = db.countries.Single(c => c.countryid == id);
            return View(country);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(country country)
        {
            if (ModelState.IsValid)
            {
                db.countries.Attach(country);
                db.ObjectStateManager.ChangeObjectState(country, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        //
        // GET: /Default1/Delete/5
 
        public ActionResult Delete(int id)
        {
            country country = db.countries.Single(c => c.countryid == id);
            return View(country);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            country country = db.countries.Single(c => c.countryid == id);
            db.countries.DeleteObject(country);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}