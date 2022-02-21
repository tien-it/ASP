using doanasp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using doanasp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace doanasp.Controllers

{
    public class HomeController : Controller
    {
        private readonly ShopContext   _context;
  
       

        public HomeController( ShopContext context)
        {
          
            _context = context;
        }

        public async Task<IActionResult> Index()
        {   
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            var inv = _context.InvoiceDetails.Include(i => i.Product).Where(i=>i.Product.Id==i.ProductId).Sum(i => i.Quantity);
            var lstProduct = _context.Products.Include(i=>i.InvoiceDetails).Include(i => i.ProductType).OrderByDescending(prd=>inv).Take(5);
     
            return View( await lstProduct.ToListAsync());
        }
        public async Task<IActionResult> HighPrice()
        {
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            var lstProduct = _context.Products.Include(i => i.ProductType).Take(5).OrderByDescending(prd=>prd.Price);

            return View(await lstProduct.ToListAsync());
        }
        // tìm kiếm theo tên và loại


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Checkout()
        {
            return View();
        }
        
    }
}
