using GUI.Abtractions;
using Microsoft.AspNetCore.Mvc;

namespace GUI.Areas.Admin.Controllers
{
    public class ProductController : BaseShopOwnerController
    {
        [ActionName("Add")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [ActionName("Index")]
        public IActionResult ListProduct()
        {
            return View();
        }

        [ActionName("Edit")]
        public IActionResult EditProduct()
        {
            return View();
        }
    }
}