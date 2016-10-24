using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoScout.Models;

namespace AutoScout.Controllers
{
    public class DealershipsController : Controller
    {
        private AutoScoutDBContext db = new AutoScoutDBContext();

        // GET: Dealerships
        public async Task<ActionResult> Index()
        {
            var dealerships = db.Dealerships.Include(d => d.AutoScoutIdentityUser);
            return View(await dealerships.ToListAsync());
        }

        // GET: Dealerships/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = await db.Dealerships.FindAsync(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            return View(dealership);
        }

        // GET: Dealerships/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = await db.Dealerships.FindAsync(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutoScoutIdentityUserId = new SelectList(db.Dealerships, "Id", "Email", dealership.AutoScoutIdentityUserId);
            return View(dealership);
        }

        // POST: Dealerships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyName,Email,City,State,ZipCode,DealershipLicenseNumber,Certified,Active,PhoneNumber,Icon,BackgroundGraphic,AutoScoutIdentityUserId")] Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealership).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.AutoScoutIdentityUserId = new SelectList(db.Dealerships, "Id", "Email", dealership.AutoScoutIdentityUserId);
            return View(dealership);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
