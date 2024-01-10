using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bloomify.Data;
using Bloomify.Models;

namespace Bloomify.Controllers
{
    public class CartProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CartProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CartProduct.Include(c => c.Cart).Include(c => c.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CartProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartProduct == null)
            {
                return NotFound();
            }

            var cartProduct = await _context.CartProduct
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartProduct == null)
            {
                return NotFound();
            }

            return View(cartProduct);
        }

        // GET: CartProducts/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "UserId");
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ImageName");
            return View();
        }

        // POST: CartProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,ProductQuantity,ProductId")] CartProduct cartProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "UserId", cartProduct.CartId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ImageName", cartProduct.ProductId);
            return View(cartProduct);
        }

        // GET: CartProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartProduct == null)
            {
                return NotFound();
            }

            var cartProduct = await _context.CartProduct.FindAsync(id);
            if (cartProduct == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "UserId", cartProduct.CartId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ImageName", cartProduct.ProductId);
            return View(cartProduct);
        }

        // POST: CartProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,ProductQuantity,ProductId")] CartProduct cartProduct)
        {
            if (id != cartProduct.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartProductExists(cartProduct.CartId))
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
            ViewData["CartId"] = new SelectList(_context.Cart, "CartId", "UserId", cartProduct.CartId);
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ImageName", cartProduct.ProductId);
            return View(cartProduct);
        }

        // GET: CartProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartProduct == null)
            {
                return NotFound();
            }

            var cartProduct = await _context.CartProduct
                .Include(c => c.Cart)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cartProduct == null)
            {
                return NotFound();
            }

            return View(cartProduct);
        }

        // POST: CartProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartProduct == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CartProduct'  is null.");
            }
            var cartProduct = await _context.CartProduct.FindAsync(id);
            if (cartProduct != null)
            {
                _context.CartProduct.Remove(cartProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartProductExists(int id)
        {
          return (_context.CartProduct?.Any(e => e.CartId == id)).GetValueOrDefault();
        }
    }
}
