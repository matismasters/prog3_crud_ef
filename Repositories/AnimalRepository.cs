using CrudEf.Data;
using CrudEf.Models;

namespace CrudEf.Repositories
{
    // Mismo repositorio que el proyecto a mano (crud_ado), MISMOS metodos
    // publicos... pero por dentro casi no hay codigo. Compara cada metodo con
    // su equivalente en crud_ado y vas a ver lo que Entity Framework te saca de
    // encima: no hay conexion que abrir, ni comando, ni parametros uno por uno,
    // ni mapeo fila-objeto. EF traduce entre los Animal y la tabla por vos.
    public class AnimalRepository
    {
        private readonly SafariContext _context;

        public AnimalRepository(SafariContext context)
        {
            _context = context;
        }

        public List<Animal> ObtenerTodos()
        {
            return _context.Animales.OrderBy(a => a.Id).ToList();
        }

        public Animal? ObtenerPorId(int id)
        {
            return _context.Animales.Find(id);
        }

        public void Crear(Animal animal)
        {
            _context.Animales.Add(animal);
            _context.SaveChanges();
        }

        public void Actualizar(Animal animal)
        {
            _context.Animales.Update(animal);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            Animal? animal = _context.Animales.Find(id);
            if (animal != null)
            {
                _context.Animales.Remove(animal);
                _context.SaveChanges();
            }
        }
    }
}
