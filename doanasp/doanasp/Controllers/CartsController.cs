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
    public class CartsController : Controller
    {
        private readonly ShopContext _context;

        public CartsController(ShopContext context)
        {
            _context = context;
        }

        //THANH TOÁN 
        public async Task<IActionResult> Pay()
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
                
            }
  
            Account accuntx = await _context.Accounts.FirstOrDefaultAsync(i=>i.id == HttpContext.Session.GetInt32("id"));

            ViewBag.account = accuntx; 

            ViewBag.total = _context.Carts.Include(c => c.Account).Include(c => c.Product)
                          .Where(c => c.Account.Username == HttpContext.Session.GetString("Username"))
                          .Sum(c => c.Quantity * c.Product.Price);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Pay(string ShippingAddress, String ShippingPhone)
        {
            Invoice invoice = new Invoice();
            string username = HttpContext.Session.GetString("Username");
            if (!checkStock(username))
            {
                ViewBag.ErrorMessage = "Có Sản Phẩm Hết Hàng!";
                ViewBag.Account = _context.Accounts.Where(c=>c.Username==username).FirstOrDefault();
                ViewBag.CartsTotal = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                                .Where(c => c.Account.Username == username)
                                .Sum(c => c.Quantity * c.Product.Price);
            }
                Account acc = _context.Accounts.FirstOrDefault(c=>c.Username==username);
            //Hoa don

            DateTime now = DateTime.Now;
            invoice.Code = now.ToString("yyMMddhhmmss");
            invoice.AccountId = _context.Accounts.FirstOrDefault(a => a.Username == username).id;
            invoice.IssueDate = now;
            invoice.Status = false;
            invoice.ShippingPhone = ShippingPhone;
            invoice.ShippingAddress = ShippingAddress;
            invoice.Total = _context.Carts.Include(c => c.Account).Include(c => c.Product)
                          .Where(c => c.Account.Username == username)
                          .Sum(c => c.Quantity * c.Product.Price);
            _context.Add(invoice);
            _context.SaveChanges();

            //Chi Tiet Hoa Don
            List<Cart> carts = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                             .Where(c => c.Account.Username == username).ToList();
            foreach (Cart item in carts)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.InvoiceId = invoice.id;
                invoiceDetail.ProductId = item.ProductId;
                invoiceDetail.Quantity = item.Quantity;
                invoiceDetail.UnitPrice = item.Product.Price;
                _context.Add(invoiceDetail);
            }
            _context.SaveChanges();
            foreach (Cart c in carts)
            {
                c.Product.Stock -= c.Quantity;
                _context.Carts.Remove(c);
            }
            _context.SaveChanges();
            return RedirectToAction("CartUser", "Carts");
        }
        public IActionResult clearCart()
        {
            string username = HttpContext.Session.GetString("Username");
            List<Cart> carts = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                             .Where(c => c.Account.Username == username).ToList();
            foreach (Cart c in carts)
            {
                _context.Carts.Remove(c);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(CartUser));
        }
        public IActionResult Tang(int id)
        {
            string username = HttpContext.Session.GetString("Username");
            int ids = _context.Accounts.FirstOrDefault(c => c.Username == username).id;
            Cart cart = _context.Carts.Include(c=>c.Product).FirstOrDefault(c => c.Id == id&& c.AccountId==ids);
            if (cart.Product.Stock<=cart.Quantity)
            {
                cart.Quantity += 0;
            }
            else
            {
                cart.Quantity+=1;
            }
            _context.SaveChanges();
            return RedirectToAction("CartUser", "Carts");
        }
        public IActionResult Giam(int id)
        {
            string username = HttpContext.Session.GetString("Username");
            int ids = _context.Accounts.FirstOrDefault(c => c.Username == username).id;
            Cart cart = _context.Carts.FirstOrDefault(c => c.Id == id && c.AccountId == ids);
            if (cart.Quantity == 1)
            {
                cart.Quantity = 1;
            }
            else
            {
                cart.Quantity -= 1;
            }
            _context.SaveChanges();
            return RedirectToAction("CartUser", "Carts");
        }
            // GET: Carts
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
            var shopContext = _context.Carts.Include(c => c.Account).Include(c => c.Product);
            return View(await shopContext.ToListAsync());
        }
        //Cart User ----------------------------------------
        public async Task<IActionResult> CartUser()
        {
            string username = HttpContext.Session.GetString("Username");
            ViewBag.CartsTotal = _context.Carts.Include(c => c.Product).Include(c => c.Account)
                                .Where(c => c.Account.Username == username)
                                .Sum(c => c.Quantity * c.Product.Price);
            var id = HttpContext.Session.GetInt32("id");
            if ( HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = username;
            }
            if ( HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }

            var shopContext = _context.Carts.Include(c => c.Account).Include(c => c.Product).Where (i => i.AccountId==id);
            return View(await shopContext.ToListAsync());
        }

        // GET: Carts/Details/5
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

            var cart = await _context.Carts
                .Include(c => c.Account)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }
        // thêm giỏ hàng
        public  IActionResult Add(int id)
        {
            return Add(id, 1);
        }
        public IActionResult Added(int id,int quantity)
        {
            return Add(id, quantity);
        }
        [HttpPost]
        public  IActionResult Add(int ProductId,int Quantity)
        {
            
            string username = HttpContext.Session.GetString("Username");

            if (username is null) {
                return RedirectToAction("index","Home");
            } ;
            int id = _context.Accounts.FirstOrDefault(c => c.Username == username).id;

            Cart cart = _context.Carts.FirstOrDefault(c => c.AccountId == id && c.ProductId == ProductId);
            int qtt = _context.Products.Find(ProductId).Stock;
            if ( cart == null )
            {
                cart = new Cart();
                cart.AccountId = id;
                cart.ProductId = ProductId;
                cart.Quantity = Quantity;
                _context.Add(cart);

            }
            else
            {
                if (Quantity + cart.Quantity > qtt)
                {
                    cart.Quantity = qtt;
                }
                else
                {
                    cart.Quantity += Quantity;
                }

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(CartUser));
        }
        // <<< end thêm giỏ hàng
        // kiểm tra số lượng giỏ hàng
        private bool checkStock(string username)
        {
            List <Cart>  cart = _context.Carts.Include(c => c.Product).Include(c => c.Account).Where(c => c.Account.Username == username).ToList();
            foreach (Cart c in cart)
            {
                if(c.Product.Stock < c.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
        //<<<end kiểm tra số lượng
        // GET: Carts/Create
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,ProductId,Quantity")] Cart cart)
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
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Username", cart.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Edit/5
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

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Username", cart.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", cart.ProductId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountId,ProductId,Quantity")] Cart cart)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "id", "Username", cart.AccountId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> DeleteUser(int? id)
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

            var cart = await _context.Carts
                .Include(c => c.Account)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CartUser));
        }




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

            var cart = await _context.Carts
                .Include(c => c.Account)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }





        // POST: Carts/Delete/5
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
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
