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
    public class InvoicesController : Controller
    {
        private readonly ShopContext _context;

        public InvoicesController(ShopContext context)
        {
            _context = context;
        }

        // GET: Invoices
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
            var shopContext = _context.Invoides.Include(i => i.Account);
            return View(await shopContext.ToListAsync());
        }

        // GET: Invoices/Details/5
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

            var invoice = await _context.Invoides
                .Include(i => i.Account)
                .FirstOrDefaultAsync(m => m.id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Username");

            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Code,IssueDate,ShippingAddress,ShippingPhone,Total,AccountId,Status")] Invoice invoice)
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
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Username", invoice.AccountId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
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

            var invoice = await _context.Invoides.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Username", invoice.AccountId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Code,IssueDate,ShippingAddress,ShippingPhone,Total,AccountId,Status")] Invoice invoice)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id != invoice.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Username", invoice.AccountId);
            return View(invoice);
        }
        public async Task<IActionResult> Accept(int? id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            var invoice = await _context.Invoides
                .Include(i => i.Account)
                .FirstOrDefaultAsync(m => m.id == id);
            invoice.Status = true;
            _context.SaveChanges();
            return RedirectToAction("Index","Invoices");
        }
        // GET: Invoices/Delete/5
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

            var invoice = await _context.Invoides
                .Include(i => i.Account)
                .FirstOrDefaultAsync(m => m.id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
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
            var invoice = await _context.Invoides.FindAsync(id);
            _context.Invoides.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoides.Any(e => e.id == id);
        }
    }
}
