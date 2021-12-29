﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doanasp.Data;
using doanasp.Models;
using System.Collections.Generic;
namespace doanasp.Controllers
{
    public class accountsController : Controller
    {
        private readonly ShopContext _context;

        public accountsController(ShopContext context)
        {
            _context = context;
        }

        // GET: accounts
        public async Task<IActionResult> Index()
        {
            //var accountName = from acc in _context.Accounts
            //                   where acc.trangthai == 2
            //                   select acc ;
            return View(await _context.Accounts.ToListAsync());
            //return View(await accountName.ToListAsync());
        }

        // GET: accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.makhachhang == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        //1
        public async Task<IActionResult> ByAddress()
        {

            var account = from acc in _context.Accounts
                          where acc.diachi.Contains("Tp.HCM")
                          select acc;

            return View(await account.ToListAsync());

        }
        //2
        public async Task<IActionResult> ByEmail()
        {

            var account = from acc in _context.Accounts
                          where acc.email.Contains("@gmail.com")
                          select acc;

            return View(await account.ToListAsync());

        }
        //3 
        public async Task<IActionResult> Search()
        {

            var account = from acc in _context.Accounts
                          where acc.email.Contains("vantien.11122001@gmail.com")
                          where acc.tendangnhap == "tiendeptrai"
                          where acc.sodienthoai == "065423451"
                          where acc.diachi.Contains("Tp.HCM")
                          select acc;   

            return View(await account.ToListAsync());

        }
        // GET: accounts/Create
        public IActionResult Create()
        {
            return View();
        }
   

        // POST: accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("makhachhang,tendangnhap,matkhau,email,hovaten,diachi,sodienthoai,phuongthucdangnhap,trangthai")] account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: accounts/Edit/5
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

        // POST: accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("makhachhang,tendangnhap,matkhau,email,hovaten,diachi,sodienthoai,phuongthucdangnhap,trangthai")] account account)
        {
            if (id != account.makhachhang)
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
                    if (!accountExists(account.makhachhang))
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

        // GET: accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.makhachhang == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool accountExists(int id)
        {
            return _context.Accounts.Any(e => e.makhachhang == id);
        }
    }
}
