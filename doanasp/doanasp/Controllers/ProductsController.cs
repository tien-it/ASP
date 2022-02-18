using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doanasp.Data;
using doanasp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace doanasp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(ShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> SellProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p=>p.InvoiceDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Price = _context.InvoiceDetails.Include(c => c.Product).Where(inv => inv.Product.Id == id).Sum(inv => inv.Product.Price * inv.Quantity);
            ViewBag.Quantity = _context.InvoiceDetails.Include(c=>c.Product).Where(inv => inv.Product.Id == id).Sum(inv => inv.Quantity);
            return View(product);
        }
        //tìm kiếm
        public async Task<IActionResult> SearchProducts(string keyword = "")
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var productList = _context.Products.Where(prod => prod.Name.Contains(keyword) || prod.ProductType.TypeName.Contains(keyword));
            if (productList is null)
            {
                return RedirectToAction("PD", "Products");
            }
            return View(await productList.ToListAsync());
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            //
            var shopContext = _context.Products.Include(p => p.ProductType);
            return View(await shopContext.ToListAsync());
            //
        }
        public async Task<IActionResult> Detail(int? id)
        {

            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (id == null)
            {
                return NotFound();
            }
            var productDetail = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            var product = from prd in _context.Products
                          select prd;
            if (productDetail == null)
            {
                return NotFound();
            }
            return View(productDetail);
        }
        public async Task<IActionResult> PD()
        {
            var shopContext = _context.Products.Include(p => p.ProductType);
            return View(await shopContext.ToListAsync());
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "TypeName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SKU,Name,Author,Description,Price,Stock,ProductTypeId,ImageFile,Image,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                if (product.ImageFile != null)
                {
                    var filename = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "product");
                    var filePath = Path.Combine(uploadPath, filename);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        product.ImageFile.CopyTo(fs);
                        fs.Flush();
                    }
                    product.Image = filename;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "TypeName", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "TypeName", product.ProductTypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SKU,Name,Author,Description,Price,Stock,ProductTypeId,ImageFile,Image,Status")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (product.ImageFile != null)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "product");
                var filePath = Path.Combine(uploadPath, filename);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    product.ImageFile.CopyTo(fs);
                    fs.Flush();
                }
                product.Image = filename;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "Id", "TypeName", product.ProductTypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }


        [HttpGet]
        public async Task<IActionResult> Search(string keyword = "")
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var productList = _context.Products.Where(prod => prod.Name.Contains(keyword) || prod.ProductType.TypeName.Contains(keyword));
            if( productList is null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await productList.ToListAsync());
        }
    }
}
