using Microsoft.AspNetCore.Mvc;
using CrudEf.Data;
using CrudEf.Models;

namespace CrudEf.Controllers
{
    // El controller habla DIRECTO con el DbContext de EF. No hay repositorio,
    // y no hace falta: el DbContext YA es un repositorio + unidad de trabajo.
    // DbSet<Animal> (la propiedad Animales) te da Add, Remove, Find y consultas;
    // SaveChanges() confirma todo junto en una transaccion. Compara esto con
    // crud_ado, donde el repositorio existe para esconder la danza de ADO.NET:
    // EF nos ahorra esa capa entera.
    public class AnimalesController : Controller
    {
        private readonly SafariContext _context;

        public AnimalesController(SafariContext context)
        {
            _context = context;
        }

        // GET: /Animales  -> lista todos
        public IActionResult Index()
        {
            List<Animal> animales = _context.Animales.OrderBy(a => a.Id).ToList();
            return View(animales);
        }

        // GET: /Animales/Details/5 -> muestra uno
        public IActionResult Details(int id)
        {
            Animal? animal = _context.Animales.Find(id);
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

            _context.Animales.Add(animal);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Animales/Edit/5 -> formulario con los datos actuales
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Animal? animal = _context.Animales.Find(id);
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

        // POST: /Animales/Edit/5 -> valida y actualiza
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AnimalFormDto form)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(form);
            }

            // Patron idiomatico de EF: cargar la entidad, modificar lo que
            // cambio, y guardar. EF rastrea el objeto (change tracking) y en el
            // UPDATE toca SOLO las columnas que realmente cambiaron; la
            // fecha_alta original queda intacta porque no la tocamos.
            Animal? animal = _context.Animales.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            animal.Especie = form.Especie;
            animal.Sexo = form.Sexo[0];
            animal.Reserva = form.Reserva;
            animal.Energia = form.Energia;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Animales/Delete/5 -> pide confirmacion
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Animal? animal = _context.Animales.Find(id);
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
            Animal? animal = _context.Animales.Find(id);
            if (animal != null)
            {
                _context.Animales.Remove(animal);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
