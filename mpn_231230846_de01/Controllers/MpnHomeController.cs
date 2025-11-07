using Microsoft.AspNetCore.Mvc;

namespace mpn_231230846_de01
{
    public class MpnHomeController : Controller
    {
        public IActionResult MpnIndex()
        {
            return View();
        }
        public IActionResult MpnContact()
        {
            ViewBag.Name = "Mai Phuong Nam";
            ViewBag.MSV = "231230846";
            ViewBag.Email = "mai.p.nam@example.com";
            ViewBag.Phone = "0123456789";
            ViewBag.Address = "Hanoi, Vietnam";
            return View();
        }
    }
}
