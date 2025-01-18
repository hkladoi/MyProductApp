using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData.DbContexts;
using AppData.Models;
using Newtonsoft.Json;
using AppData.Dto;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.SignalR;

namespace AppView.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private HttpClient _client;

        public ProductsController(AppDbContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid productId,Guid categoryId)
        {
            ViewData["ProductId"] = productId;
            ViewData["CategoryId"] = categoryId;
            return View();
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id, Guid categoryId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
