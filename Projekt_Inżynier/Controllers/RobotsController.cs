using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekcik.application.Robots;
using Projekcik.Entities;
using Projekcik.Infrastructure.Persistance;

namespace Projekcik.Controllers
{
    public class RobotsController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public RobotsController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Robots
        public async Task<IActionResult> Index()
        {
            var robots = await _context.Roboty
                .Include(r => r.Team)       // Ważne! Dołącz tabelę Team
                .Include(r => r.Categories) // Ważne! Dołącz tabelę Categories
            .ToListAsync();

            var dtos = _mapper.Map<List<RobotsDto>>(robots);

            return View(dtos);
        }

        // GET: Robots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var robots = await _context.Roboty
                .FirstOrDefaultAsync(m => m.IdRobota == id);
            if (robots == null)
            {
                return NotFound();
            }

            return View(robots);
        }

        // GET: Robots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Robots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRobota,IdKategorii,NazwaRobota,IdDruzyny")] Robots robots)
        {
            if (ModelState.IsValid)
            {
                _context.Add(robots);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(robots);
        }

        // GET: Robots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var robots = await _context.Roboty.FindAsync(id);
            if (robots == null)
            {
                return NotFound();
            }
            return View(robots);
        }

        // POST: Robots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRobota,IdKategorii,NazwaRobota,IdDruzyny")] Robots robots)
        {
            if (id != robots.IdRobota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(robots);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RobotsExists(robots.IdRobota))
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
            return View(robots);
        }

        // GET: Robots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var robots = await _context.Roboty
                .FirstOrDefaultAsync(m => m.IdRobota == id);
            if (robots == null)
            {
                return NotFound();
            }

            return View(robots);
        }

        // POST: Robots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var robots = await _context.Roboty.FindAsync(id);
            if (robots != null)
            {
                _context.Roboty.Remove(robots);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RobotsExists(int id)
        {
            return _context.Roboty.Any(e => e.IdRobota == id);
        }
    }
}
