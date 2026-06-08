using Microsoft.AspNetCore.Mvc;
using CrudEf.Models;
using CrudEf.Repositories;

namespace CrudEf.Controllers
{
    // El controller es el director de orquesta. Recibe el repositorio por
    // inyeccion de dependencias y expone una accion por cada cosa que el
    // usuario puede hacer. Su tarea clave es TRADUCIR: del DTO del formulario
    // a la entidad Animal, y de la entidad a las vistas.
    public class AnimalesController : Controller
    {
        private readonly AnimalRepository _repositorio;

        public AnimalesController(AnimalRepository repositorio)
        {
            _repositorio = repositorio;
        }

        // GET: /Animales  -> lista todos
        public IActionResult Index()
        {
            List<Animal> animales = _repositorio.ObtenerTodos();
            return View(animales);
        }

        // GET: /Animales/Details/5 -> muestra uno
        public IActionResult Details(int id)
        {
            Animal? animal = _repositorio.ObtenerPorId(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // GET: /Animales/Create -> formulario vacio
        [HttpGet]
        public IActionResult Create()
        {
            return View(new AnimalFormDto());
        }

        // POST: /Animales/Create -> valida el DTO, lo traduce a Animal, lo crea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AnimalFormDto form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            Animal animal = new Animal();
            animal.Especie = form.Especie;
            animal.Sexo = form.Sexo[0];
            animal.Reserva = form.Reserva;
            animal.Energia = form.Energia;

            _repositorio.Crear(animal);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Animales/Edit/5 -> formulario con los datos actuales
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Animal? animal = _repositorio.ObtenerPorId(id);
            if (animal == null)
            {
                return NotFound();
            }

            AnimalFormDto form = new AnimalFormDto();
            form.Especie = animal.Especie;
            form.Sexo = animal.Sexo.ToString();
            form.Reserva = animal.Reserva;
            form.Energia = animal.Energia;

            ViewBag.Id = animal.Id;
            return View(form);
        }

        // POST: /Animales/Edit/5 -> valida, arma el Animal, lo actualiza
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AnimalFormDto form)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(form);
            }

            Animal animal = new Animal();
            animal.Id = id;
            animal.Especie = form.Especie;
            animal.Sexo = form.Sexo[0];
            animal.Reserva = form.Reserva;
            animal.Energia = form.Energia;

            _repositorio.Actualizar(animal);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Animales/Delete/5 -> pide confirmacion
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Animal? animal = _repositorio.ObtenerPorId(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: /Animales/Delete/5 -> borra
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmado(int id)
        {
            _repositorio.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
