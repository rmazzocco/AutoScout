
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
using AutoScout.Services;

namespace AutoScout.Controllers
{
    public class VehiclesController : Controller
    {
        private AutoScoutDBContext db = new AutoScoutDBContext();

        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
            var vehicles = db.Vehicles.Include(v => v.Dealership);
            return View(await vehicles.ToListAsync());
        }
        
        // GET: Vehicles
        public ActionResult IndexPhoto()
        {
            var db = new AutoScoutDBContext();
            var photo = (from d in db.VehicleImages
                         select d).ToList();
            return View(photo);
        }

        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            var vehicleImage = db.VehicleImages.FirstOrDefault(x => x.VehicleId == vehicle.Id);
            ViewBag.VehicleImage = vehicleImage;
        
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.DealershipId = new SelectList(db.Dealerships, "Id", "CompanyName");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,VIN,StockNumber,Mileage,Transmission,Style,ExteriorColor,InteriorColor,Make,Model,Year,Price,DealershipId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DealershipId = new SelectList(db.Dealerships, "Id", "CompanyName", vehicle.DealershipId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.DealershipId = new SelectList(db.Dealerships, "Id", "CompanyName", vehicle.DealershipId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,VIN,StockNumber,Mileage,Transmission,Style,ExteriorColor,InteriorColor,Make,Model,Year,Price,DealershipId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DealershipId = new SelectList(db.Dealerships, "Id", "CompanyName", vehicle.DealershipId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        public ActionResult AddVehiclePhoto()
        {
            var vehicleList = db.Vehicles.Where(x => x.Id != -1).ToList();
            
            VehicleImage image = new VehicleImage();
            return View(image);
        }

        [HttpPost]
        public ActionResult AddVehiclePhoto(VehicleImage model, HttpPostedFileBase imageFile)
        {
            var db = new AutoScoutDBContext();
            
            if(imageFile != null)
            {
                var service = new ImageManagementService(db);
                service.AssignImageToVehicle(model.Id, imageFile);
            }
            return View(model);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vehicle vehicle = await db.Vehicles.FindAsync(id);
            db.Vehicles.Remove(vehicle);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
