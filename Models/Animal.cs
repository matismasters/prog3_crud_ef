namespace CrudEf.Models
{
    // La entidad del dominio: representa una fila de la tabla 'animales'.
    // A diferencia del Dictionary<string, object> que devolvia la consola del
    // Safari, un Animal tiene tipos reales, lo cuida el compilador y concentra
    // las reglas del dominio en un solo lugar. Eso es encapsulamiento.
    public class Animal
    {
        public int Id { get; set; }
        public string Especie { get; set; } = "";
        public char Sexo { get; set; }
        public string Reserva { get; set; } = "";
        public int Energia { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
