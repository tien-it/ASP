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

namespace doanasp.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly ShopContext _context;
        public async Task<IActionResult> Detail(int? id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }
       
        public ProductTypesController(ShopContext context)
        {
            _context = context;
        }

        // GET: ProductTypes
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
            if (HttpContext.Request.Cookies.ContainsKey("Username"))
            {
                ViewBag.UserName = HttpContext.Request.Cookies["Username"].ToString();
            }
            return View(await _context.ProductTypes.ToListAsync());
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName,Status")] ProductType productType)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName,Status")] ProductType productType)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.Id))
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
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            var productType = await _context.ProductTypes.FindAsync(id);
            _context.ProductTypes.Remove(productType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            return _context.ProductTypes.Any(e => e.Id == id);
        }
    }
}
