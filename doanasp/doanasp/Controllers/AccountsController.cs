
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doanasp.Data;
using doanasp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace doanasp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ShopContext _context;

        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger,ShopContext context)
        {
            _context = context;
            _logger = logger;
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
            ViewBag.Invoice = _context.Invoides;
            return View(await _context.Accounts.Include(i=>i.Invoices).ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            return View();
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
                if (account.IsAdmin == false)
                {
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    return RedirectToAction("index", "Accounts");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Đăng nhập thất bại";
                return View();
            }

        }
        //public async Task<IActionResult> LoginSesstion()
        //{
        //    var password = HttpContext.Session.GetString("Password");
        //    var username = HttpContext.Session.GetString("Username");
        //    var account = _context.Accounts.Where(a => a.Username == username && a.Password == password).FirstOrDefault();
        //    if (account != null)
        //    {
        //        return RedirectToAction("index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Đăng nhập thất bại";
        //        return View();
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Username,Password,Email,Phone,Address,Fullname,IsAdmin,Avatar,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("id,Username,Password,Email,Phone,Address,Fullname,IsAdmin,Avatar,Status")] Account account)
        {
            if (id != account.id)
            {
                return NotFound();
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
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.id == id);
        }
    }
}
