using AppStore.Data;
using AppStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers
{
    public class AppController : Controller
    {
        private readonly AppStoreDbContext _db;

        public AppController(AppStoreDbContext db)
        {
            _db = db;
        }

        // Display all apps
        // GET
        public IActionResult Index()
        {
            IEnumerable<App> appList = _db.Apps
                .Take(20)
                .ToList();
            ViewBag.apps = appList;
            return View();
        }

        // Create a new app
        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(App obj)
        {
            // do not let name and category to be the same
            if (obj.Category != null && obj.Name != null)
            {
                if (obj.Name.ToString().ToLower() == obj.Category.ToString().ToLower())
                {
                    ModelState.AddModelError(
                        "NameCategorySameError",
                        "The Name and the Category of an app should not be the same."
                        );
                }
            }

            // if model is valid, save it to database
            if (ModelState.IsValid)
            {
                // add object to database
                _db.Apps.Add(obj);
                _db.SaveChanges();
                TempData["success"] = string.Format(
                    "App with the name '{0}' is created successfully",
                    obj.Name
                    );

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // Update an app
        // GET
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var appFromDb = _db.Apps.Find(id);

            if (appFromDb == null)
            {
                return NotFound();
            }

            return View(appFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(App obj)
        {
            // do not let name and category to be the same
            if (obj.Category != null && obj.Name != null)
            {
                if (obj.Name.ToString().ToLower() == obj.Category.ToString().ToLower())
                {
                    ModelState.AddModelError(
                        "NameCategorySameError",
                        "The Name and the Category of an app should not be the same."
                        );
                }
            }

            // if model is valid, save it to database
            if (ModelState.IsValid)
            {
                // update object in database
                _db.Apps.Update(obj);
                _db.SaveChanges();
                TempData["success"] = string.Format(
                    "App with the name '{0}' is updated successfully",
                    obj.Name
                    );

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // Delete an app
        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var appFromDb = _db.Apps.Find(id);

            if (appFromDb == null)
            {
                return NotFound();
            }

            return View(appFromDb);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            // get object with the given id
            var obj = _db.Apps.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            // delete object from database
            _db.Apps.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = string.Format(
                    "App with the name '{0}' is deleted successfully",
                    obj.Name
                    );

            return RedirectToAction("Index");
        }
    }
}
