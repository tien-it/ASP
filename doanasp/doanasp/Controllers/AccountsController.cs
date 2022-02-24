
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doanasp.Data;
using doanasp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace doanasp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger,ShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        //GET: Register

        public ActionResult Register()
        {
            return RedirectToAction("Login", "Accounts");
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string Username,string Password,string Email,string Phone,string Address,string Fullname)
        {
            Account acc = _context.Accounts.FirstOrDefault(c => c.Username == Username && c.Password ==Password) ;
                if (acc == null)
                {
                    acc = new Account();
                    acc.Username = Username;
                    acc.Password = Password;
                    acc.Email = Email;
                    acc.Phone = Phone;
                    acc.Address = Address;
                    acc.Fullname = Fullname;
                    acc.Status = true;
                _context.Add(acc);
                HttpContext.Session.SetInt32("id", acc.id);
                HttpContext.Session.SetString("Password", acc.Password);
                HttpContext.Session.SetString("Username", acc.Username);
            }
                else
                {
                    ViewBag.error = "Error";
                return RedirectToAction("Login", "Accounts");
            }
           
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Accounts
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
            ViewBag.Invoice = _context.Invoides.Include(p => p.Account).ToList();
            var shopContext = _context.Accounts.Include(p => p.Invoices);
            return View(await shopContext.ToListAsync());
        }
        public async Task<IActionResult> iaccount()
        {
            
            if (HttpContext.Session.Keys.Contains("id"))
            {
                var id = HttpContext.Session.GetInt32("id");
              
                var account = await _context.Accounts.FirstOrDefaultAsync(m => m.id == id);
                if (account == null)
                {
                    return NotFound();
                }

                return View(account);
            }
          
                return NotFound();
        }
        // GET: Accounts/Details/5
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

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
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
        public async Task<IActionResult> EditProfile()
        {
            var idUser = HttpContext.Session.GetInt32("id");
            if (idUser == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.id == idUser);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile([Bind("id,Username,Password,Email,Phone,Address,Fullname,IsAdmin,Avatar,Status")] Account account)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
   
        public async Task<IActionResult> Login(string Username, string Password)
        {
            var account =   _context.Accounts.Where(a => a.Username == Username && a.Password == Password).FirstOrDefault();
            if (account != null)
            {
                HttpContext.Session.SetInt32("id", account.id);
                HttpContext.Session.SetString("Password", account.Password);
                HttpContext.Session.SetString("Username", account.Username);
                if (account.IsAdmin == false && account.Status==true)
                {
                    return RedirectToAction("index", "Home");
                }
                else
                if(account.Status==true)
                {
                    return RedirectToAction("index", "Accounts");
                }
            }
                ViewBag.ErrorMessage = "Đăng nhập thất bại";
                return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Username,Password,Email,Phone,Address,Fullname,IsAdmin,Avatar,Status")] Account account)
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
                _context.Add(account);
                await _context.SaveChangesAsync();

                if (account.ImageFile != null)
                {
                    var filename = Guid.NewGuid() + Path.GetExtension(account.ImageFile.FileName);
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "user_images");
                    var filePath = Path.Combine(uploadPath, filename);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        account.ImageFile.CopyTo(fs);
                        fs.Flush();
                    }
                    account.Avatar = filename;

                    _context.Accounts.Update(account);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
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

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Username,Password,Email,Phone,Address,Fullname,IsAdmin,ImageFile,Avatar,Status")] Account account)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            if (id != account.id)
            {
                return NotFound();
            }

            if (account.ImageFile != null)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(account.ImageFile.FileName);
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "user");
                var filePath = Path.Combine(uploadPath, filename);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    account.ImageFile.CopyTo(fs);
                    fs.Flush();
                }
                account.Avatar = filename;
            }
            
            if (ModelState.IsValid)
            {
                

                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.id))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
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

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
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
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            return _context.Accounts.Any(e => e.id == id);
        }
    }
}
