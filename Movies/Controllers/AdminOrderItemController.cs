﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminOrderItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminOrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminOrderItem
        public async Task<IActionResult> Index(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index", "AdminOrder");
            }
            var orderItems = await _context.OrderItem.Where(o=>o.OrderId==id).ToListAsync();
            foreach (var item in orderItems)
            {
                item.ProductTitle = (from product in _context.Product
                                    where product.Id == item.ProductId
                                    select product.Title).FirstOrDefault();
            }
            return View(orderItems);
        }

        // GET: AdminOrderItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            orderItem.ProductTitle = (from product in _context.Product
                                      where product.Id == orderItem.ProductId
                                      select product.Title).FirstOrDefault();

            return View(orderItem);
        }

        // GET: AdminOrderItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminOrderItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,Quantity,Price")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderItem);
        }

        // GET: AdminOrderItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            orderItem.ProductTitle = (from product in _context.Product
                                      where product.Id == orderItem.ProductId
                                      select product.Title).FirstOrDefault();
            return View(orderItem);
        }

        // POST: AdminOrderItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,ProductId,Quantity,Price")] OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return NotFound();
            }
            if(orderItem.Price<=0)
            {
                ModelState.AddModelError("Price", "Price must be greater than 0");
            }
            if(orderItem.Quantity<=0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be greater than 0");
            }
            ModelState.Remove("ProductTitle");
            if (ModelState.IsValid)
            {
                try
                {
                    var old_orderItem = await _context.OrderItem.FindAsync(id);
                    var quantitity_diff= orderItem.Quantity - old_orderItem.Quantity;
                    var price_diff = orderItem.Price - old_orderItem.Price;
                    if (quantitity_diff < 0)
                    {
                        _context.Product.Find(orderItem.ProductId).Quantity += Math.Abs(quantitity_diff);
                    }
                    if (quantitity_diff > 0)
                    {
                        var available_quantity = _context.Product.Find(orderItem.ProductId).Quantity;
                        if (available_quantity < quantitity_diff)
                        {
                            ModelState.AddModelError("Quantity", $"Only {available_quantity} items are available," +
                                $" you tried to add {quantitity_diff}");
                            orderItem.ProductTitle = (from product in _context.Product
                                                      where product.Id == orderItem.ProductId
                                                      select product.Title).FirstOrDefault();
                            return View(orderItem);
                        }
                        _context.Product.Find(orderItem.ProductId).Quantity -= quantitity_diff;
                    }
                    if (price_diff != 0 || quantitity_diff!=0)
                    {
                        var old_price=old_orderItem.Price*old_orderItem.Quantity;
                        var new_price=orderItem.Price*orderItem.Quantity;
                        _context.Order.Find(orderItem.OrderId).Total += new_price - old_price;
                    }
                    old_orderItem.Quantity = orderItem.Quantity;
                    old_orderItem.Price = orderItem.Price;
                    //_context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new {id=orderItem.OrderId});
            }
            orderItem.ProductTitle = (from product in _context.Product
                                      where product.Id == orderItem.ProductId
                                      select product.Title).FirstOrDefault();
            return View(orderItem);
        }

        // GET: AdminOrderItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            orderItem.ProductTitle = (from product in _context.Product
                                      where product.Id == orderItem.ProductId
                                      select product.Title).FirstOrDefault();
            return View(orderItem);
        }

        // POST: AdminOrderItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderItem'  is null.");
            }
            var orderItem = await _context.OrderItem.FindAsync(id);
            int? orderid= orderItem.OrderId;
            if (orderItem != null)
            {
                _context.Product.Find(orderItem.ProductId).Quantity += orderItem.Quantity;
                _context.Order.Find(orderItem.OrderId).Total -= orderItem.Price * orderItem.Quantity;
                _context.OrderItem.Remove(orderItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new {id=orderid});
        }

        private bool OrderItemExists(int id)
        {
          return (_context.OrderItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
