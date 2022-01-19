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
    public class WishesController : Controller
    {
        private readonly ShopContext _context;

        public WishesController(ShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> WishUser()
        {
            var id = HttpContext.Session.GetInt32("id");
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            var shopContext = _context.wishes.Include(c => c.Account).Include(c => c.Product).Where(i => i.AccountId == id);
            return View(await shopContext.ToListAsync());
        }
        public IActionResult Add(int id)
        {
            return Add(id, 1);
        }
        [HttpPost]
        public IActionResult Add(int ProductId, int Quantity)
        {
            int price = _context.Products.FirstOrDefault(c => c.Id == ProductId).Price;
            string username = HttpContext.Session.GetString("Username");
            int id = _context.Accounts.FirstOrDefault(c => c.Username == username).id;
            Wish wish = _context.wishes.FirstOrDefault(c => c.AccountId == id && c.ProductId == ProductId);
            if (wish == null)
            {
                wish = new Wish();
                wish.AccountId = id;
                wish.ProductId = ProductId;
                wish.Quantity = Quantity;
                wish.price = price;
                _context.Add(wish);

            }
            else
            {
                wish.Quantity += Quantity;
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(WishUser));
        }
        // GET: Wishes
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.wishes.Include(c => c.Account).Include(c => c.Product);
            return View(await shopContext.ToListAsync());
        }

        // GET: Wishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wish = await _context.wishes
                .Include(w => w.Account)
                .Include(w => w.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wish == null)
            {
                return NotFound();
            }

            return View(wish);
        }

        // GET: Wishes/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Password");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Author");
            return View();
        }

        // POST: Wishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,ProductId,price,Quantity")] Wish wish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Password", wish.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Author", wish.ProductId);
            return View(wish);
        }

        // GET: Wishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wish = await _context.wishes.FindAsync(id);
            if (wish == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Password", wish.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Author", wish.ProductId);
            return View(wish);
        }

        // POST: Wishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountId,ProductId,price,Quantity")] Wish wish)
        {
            if (id != wish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishExists(wish.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Password", wish.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Author", wish.ProductId);
            return View(wish);
        }

        // GET: Wishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wish = await _context.wishes
                .Include(w => w.Account)
                .Include(w => w.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wish == null)
            {
                return NotFound();
            }

            return View(wish);
        }

        // POST: Wishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wish = await _context.wishes.FindAsync(id);
            _context.wishes.Remove(wish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishExists(int id)
        {
            return _context.wishes.Any(e => e.Id == id);
        }
    }
}
