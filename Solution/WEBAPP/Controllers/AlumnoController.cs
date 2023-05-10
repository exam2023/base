using Core.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WEBAPP.Models;

namespace WEBAPP.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly IAlumnoRepository _repository;


        public AlumnoController(IAlumnoRepository repository)
        {
            _repository = repository;
        }



        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: Alumno/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var entidad = await _repository.GetById(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return View(entidad);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAlumno,Nombres,ApellidoPaterno,DNI,ApellidoMaterno,Direccion,Telefono")] Alumno entidad)
        {
            await _repository.Post(entidad);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Alumno/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entidad = await _repository.GetById(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAlumno,Nombres,ApellidoPaterno,DNI,ApellidoMaterno,Direccion,Telefono")] Alumno entidad)
        {
            if (id != entidad.IdAlumno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.Update(entidad);
                return RedirectToAction(nameof(Index));
            }
            return View(entidad);
        }

        // GET: Alumno/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var entidad = await _repository.GetById(id);
            if (entidad == null)
            {
                return NotFound();
            }
            return View(entidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var entidad = await _repository.GetById(id);
            if (entidad != null)
            {
                await _repository.Delete(entidad.IdAlumno);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
