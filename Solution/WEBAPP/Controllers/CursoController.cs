using Core.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPP.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoRepository _repository;

        public CursoController(ICursoRepository repository)
        {
            _repository = repository;
        }

        // GET: Cursoes
        public async Task<IActionResult> Index()
        {
             return View(await _repository.GetAll());
        }

        // GET: Curso/Details/5
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return NotFound();

            return View(entity);
        }

        // GET: Curso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCurso,Nombre,Docente")] Curso entity)
        {
           
            await _repository.Post(entity);
            return RedirectToAction(nameof(Index));
           
        }

        // GET: Curso/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return NotFound();

            return View(entity);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCurso,Nombre,Docente")] Curso entity)
        {
           if (id != entity.IdCurso) return NotFound();

           if (ModelState.IsValid)
            {
                await _repository.Update(entity);
                return RedirectToAction(nameof(Index));
            }

           return View(entity);
        }

        // GET: Curso/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null) return NotFound();

            return View(entity);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
