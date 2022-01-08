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
             var lstProduct = _context.Products.Include(i=>i.ProductType);
            ViewData["ProductType"] = new SelectList(_context.ProductTypes, "Id", "TypeName");
            return View( await lstProduct.ToListAsync());
        }
      

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
