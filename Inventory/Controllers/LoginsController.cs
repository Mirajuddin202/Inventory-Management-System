using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class LoginsController : Controller
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: Logins
        public ActionResult Index()
        {
            return View(new Login());
        }

        // POST: Logins
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login u)
        {
            var user = db.Logins.FirstOrDefault(l => l.UserName == u.UserName && l.Password == u.Password);
            if (user != null)
            {
                Session["name"] = u.UserName;
                return RedirectToAction("Main");
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(u);
        }

        public ActionResult Logout()
        {
            Session.Remove("name");
            return RedirectToAction("Index");
        }

        public ActionResult Main()
        {
            if (Session["name"] == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}