using Microsoft.AspNetCore.Mvc;

namespace WebScraper.Api.Controllers
{
    public class CurrencyRateController : Controller
    {
        // GET: CurrencyRateController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CurrencyRateController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CurrencyRateController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrencyRateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CurrencyRateController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CurrencyRateController/Edit/5
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

        // GET: CurrencyRateController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CurrencyRateController/Delete/5
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
