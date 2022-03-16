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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace doanasp.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CommentsController(ShopContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            string username = HttpContext.Session.GetString("Username");
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = username;
            }
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            var shopContext = _context.Comments.Include(c => c.Account).Include(c => c.Product);
            return View(await shopContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string username = HttpContext.Session.GetString("Username");
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = username;
            }
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }

            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Account)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public async Task<IActionResult> Create(int? id)
        {
            string username = HttpContext.Session.GetString("Username");
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = username;
            }
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            Account accuntx = await _context.Accounts.FirstOrDefaultAsync(i => i.id == HttpContext.Session.GetInt32("id"));
            ViewBag.account = accuntx;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string Content , IFormFile ImageFile, string Image)
        {
           
            if (ModelState.IsValid)
            {
                string username = HttpContext.Session.GetString("Username");
                Comment cmt = new Comment();
                cmt.AccountId = _context.Accounts.FirstOrDefault(a => a.Username == username).id;
                cmt.ProductId = id;
                cmt.IssueDate = DateTime.Now;
                cmt.Content = Content;

                if ( ImageFile != null)
                {
                    var filename = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    var uploadPath = Path.Combine( _webHostEnvironment.WebRootPath, "img", "comment_images");
                    var filePath = Path.Combine(uploadPath, filename);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        ImageFile.CopyTo(fs);
                        fs.Flush();
                    }
                    cmt.Image = filename;
                }
                cmt.status = false;
                _context.Add(cmt);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        // GET: Comments/Edit/5
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
            var cmt = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
                cmt.status = true;
            _context.SaveChanges();
            return RedirectToAction("Index", "Comments");
        }
        public async Task<IActionResult> Edit1(int? id)
        {
            if (HttpContext.Session.Keys.Contains("id"))
            {
                ViewBag.id = HttpContext.Session.GetInt32("id");
            }
            if (HttpContext.Session.Keys.Contains("Username"))
            {
                ViewBag.UserName = HttpContext.Session.GetString("Username");
            }
            var cmt = await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
            cmt.status = false;
            _context.SaveChanges();
            return RedirectToAction("Index", "Comments");
        }
        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: Comments/Delete/5
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

            var comment = await _context.Comments
                .Include(c => c.Account)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
