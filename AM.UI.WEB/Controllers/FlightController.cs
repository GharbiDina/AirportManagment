using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Service;
using AM.Infrastucture.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AM.UI.WEB.Controllers
{
    public class FlightController : Controller
    {
        // GET: FlightController
        IFlightService fs;
        IPlaneService ps;
        #region other functions
        public ActionResult Sort()
        {
            return View("Index", fs.SortFlights());
        }
        #endregion
        public FlightController(IFlightService fs, IPlaneService ps)
        {
            this.fs = fs;
            this.ps = ps;
        }

        public ActionResult Index(DateTime? dateDepart)
        {
            if(dateDepart == null)
                return View(fs.GetAll());
            else
                return View(fs.GetMany(f => f.FlightDate.Date.Equals(dateDepart)).ToList());
        }

        // GET: FlightController/Details/5
        public ActionResult Details(int id)
        {
            return View(fs.GetById(id));
        }
        public ActionResult PlaneDetails(int id)
        {
            return View(ps.GetById(id));
        }
        // GET: FlightController/Create
        public ActionResult Create()
        {
            ViewBag.planeList = new SelectList(ps.GetAll(), "PlaneId", "information");
            return View();
        }

        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight f, IFormFile PilotImage)
        {
            try
            {
                if (PilotImage != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", PilotImage.FileName);
                    Stream stream = new FileStream(path, FileMode.Create);
                    PilotImage.CopyTo(stream);
                    f.Pilot = PilotImage.FileName;
                }
                fs.Add(f);
                fs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.planeList = new SelectList(ps.GetAll(), "PlaneId", "information");
            return View(fs.GetById(id));
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight f, IFormFile PilotImage)
        {
            try
            {
                //Flight oldF = fs.GetById(id);
                f.FlightId = id;
                if (PilotImage != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", PilotImage.FileName);
                    Stream stream = new FileStream(path, FileMode.Create);
                    PilotImage.CopyTo(stream);
                    f.Pilot = PilotImage.FileName;
                }
                //oldF.Departure = f.Departure;
                //oldF.Destination = f.Destination;
                //oldF.EffectiveArrival = f.EffectiveArrival;
                //oldF.EstimatedDuration = f.EstimatedDuration;
                //oldF.FlightDate = f.FlightDate;
                //oldF.PlaneFK = f.PlaneFK;
                fs.Update(f);
                fs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(fs.GetById(id));
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Flight f = fs.GetById(id);
                fs.Delete(f);
                fs.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
