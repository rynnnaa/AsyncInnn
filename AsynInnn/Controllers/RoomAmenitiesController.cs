﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AsynInnn.Data;
using AsynInnn.Models;

namespace AsynInnn.Controllers
{
    public class RoomAmenitiesController : Controller
    {
        private readonly AsyncInnDbContext _context;

        public RoomAmenitiesController(AsyncInnDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Gets list of all room amenities and sends to view
        /// </summary>
        /// <returns>view</returns>
        // GET: RoomAmenities
        public async Task<IActionResult> Index()
        {
            var asyncInnDbContext = _context.RoomAmenities.Include(r => r.Amenities).Include(r => r.Room);
            return View(await asyncInnDbContext.ToListAsync());
        }

        /// <summary>
        /// Show details of one room amenity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>detail view</returns>
        // GET: RoomAmenities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAmenities = await _context.RoomAmenities
                .Include(r => r.Amenities)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.AmenitiesID == id);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            return View(roomAmenities);
        }


        /// <summary>
        /// Creates new instance of room amenities
        /// </summary>
        /// <returns>List of all room amenities</returns>
        // GET: RoomAmenities/Create
        public IActionResult Create()
        {
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "ID");
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            return View();
        }

        // POST: RoomAmenities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AmenitiesID,RoomID")] RoomAmenities roomAmenities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomAmenities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "ID", roomAmenities.AmenitiesID);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID", roomAmenities.RoomID);
            return View(roomAmenities);
        }


        /// <summary>
        /// Edit details for room amenities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: RoomAmenities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAmenities = await _context.RoomAmenities.FindAsync(id);
            if (roomAmenities == null)
            {
                return NotFound();
            }
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "ID", roomAmenities.AmenitiesID);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID", roomAmenities.RoomID);
            return View(roomAmenities);
        }

        // POST: RoomAmenities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AmenitiesID,RoomID")] RoomAmenities roomAmenities)
        {
            if (id != roomAmenities.AmenitiesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomAmenities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomAmenitiesExists(roomAmenities.AmenitiesID))
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
            ViewData["AmenitiesID"] = new SelectList(_context.Amenities, "ID", "ID", roomAmenities.AmenitiesID);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID", roomAmenities.RoomID);
            return View(roomAmenities);
        }
        /// <summary>
        /// shows delete page
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="hotelID"></param>
        /// <returns></returns>
        // GET: RoomAmenities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAmenities = await _context.RoomAmenities
                .Include(r => r.Amenities)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.AmenitiesID == id);
            if (roomAmenities == null)
            {
                return NotFound();
            }

            return View(roomAmenities);
        }


        /// <summary>
        /// Deletes a room amenity instance
        /// </summary>
        /// <param name="amenitiesID"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        // POST: RoomAmenities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomAmenities = await _context.RoomAmenities.FindAsync(id);
            _context.RoomAmenities.Remove(roomAmenities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Verifies room amenities exitis
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false</returns>
        private bool RoomAmenitiesExists(int id)
        {
            return _context.RoomAmenities.Any(e => e.AmenitiesID == id);
        }
    }
}
