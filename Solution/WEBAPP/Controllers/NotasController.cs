using Core.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEBAPP.Controllers
{
    public class NotasController : Controller
    {
        private readonly INotasRepository _repository;
        private readonly IAlumnoRepository _Alumno;
        private readonly ICursoRepository _Curso;

        public NotasController(INotasRepository repository, IAlumnoRepository Alumno, ICursoRepository Curso)
        {
            _repository = repository;
            _Alumno = Alumno;
            _Curso = Curso; 
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            
            return View(await _repository.GetAll());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int id)
        {
           var entity = await _repository.GetById(id);
           if(entity == null) return NotFound();

           return View(entity);
        }

        // GET: Notas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Alumnos"] = new SelectList(await _Alumno.GetAll(), "IdAlumno", "Nombres");
            ViewData["Cursos"] = new SelectList(await _Curso.GetAll(), "IdCurso", "Nombre");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNota,IdAlumno,IdCurso,Nota")] Notas entity)
        {

            await _repository.Post(entity);
            return RedirectToAction(nameof(Index));
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
             var entity = await _repository.GetById(id);
            if (entity == null) return NotFound();

            ViewData["Alumnos"] = new SelectList(await _Alumno.GetAll(), "IdAlumno", "Nombres", entity.IdAlumno);
            ViewData["Cursos"] = new SelectList(await _Curso.GetAll(), "IdCurso", "Nombre", entity.IdCurso);

            return View(entity);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNota,IdAlumno,IdCurso,Nota")] Notas entity)
        {
            if (id != entity.IdNota) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.Update(entity);
                return RedirectToAction(nameof(Index));
            }

            return View(entity);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetById(id);
           if(entity == null) return NotFound();

           return View(entity);

        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
