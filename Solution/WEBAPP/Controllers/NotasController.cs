using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entity;

namespace WEBAPP.Controllers
{
    public class NotasController : Controller
    {
        private readonly InLearningContext _context;

        public NotasController(InLearningContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var inLearningContext = _context.Nota.Include(n => n.IdAlumnoNavigation).Include(n => n.IdCursoNavigation);
            return View(await inLearningContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notas = await _context.Nota
                .Include(n => n.IdAlumnoNavigation)
                .Include(n => n.IdCursoNavigation)
                .FirstOrDefaultAsync(m => m.IdNota == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno");
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Docente");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNota,IdAlumno,IdCurso,Nota")] Notas notas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", notas.IdAlumno);
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Docente", notas.IdCurso);
            return View(notas);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notas = await _context.Nota.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", notas.IdAlumno);
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Docente", notas.IdCurso);
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNota,IdAlumno,IdCurso,Nota")] Notas notas)
        {
            if (id != notas.IdNota)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotasExists(notas.IdNota))
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
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "IdAlumno", notas.IdAlumno);
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Docente", notas.IdCurso);
            return View(notas);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notas = await _context.Nota
                .Include(n => n.IdAlumnoNavigation)
                .Include(n => n.IdCursoNavigation)
                .FirstOrDefaultAsync(m => m.IdNota == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nota == null)
            {
                return Problem("Entity set 'InLearningContext.Nota'  is null.");
            }
            var notas = await _context.Nota.FindAsync(id);
            if (notas != null)
            {
                _context.Nota.Remove(notas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotasExists(int id)
        {
          return (_context.Nota?.Any(e => e.IdNota == id)).GetValueOrDefault();
        }
    }
}
