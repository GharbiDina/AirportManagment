using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Service;
using AM.Infrastructure;
using AM.Infrastucture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AM.UI.WEB.Controllers
{
    public class PlaneController : Controller
    {
        // GET: PlaneController
        IPlaneService sp;

        public PlaneController(IPlaneService sp)
        {
            this.sp = sp;
        }

        public ActionResult Index()
        {
            return View(sp.GetAll());
        }

        // GET: PlaneController/Details/5
        public ActionResult Details(int id)
        {
            return View(sp.GetById(id));
        }

        // GET: PlaneController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plane p)
        {
            try
            {
                sp.Add(p);
                sp.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaneController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlaneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaneController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlaneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
