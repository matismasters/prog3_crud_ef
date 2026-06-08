using System.ComponentModel.DataAnnotations;

namespace CrudEf.Models
{
    // DTO (Data Transfer Object): transporta SOLO los datos que el usuario puede
    // cargar en el formulario. No tiene Id ni FechaAlta, porque esos los pone la
    // base, no el navegante. Las anotaciones son reglas de validacion que MVC
    // chequea automaticamente antes de que toquemos la base.
    public class AnimalFormDto
    {
        [Required(ErrorMessage = "La especie es obligatoria.")]
        [StringLength(100)]
        public string Especie { get; set; } = "";

        [Required]
        [RegularExpression("^[MH]$", ErrorMessage = "El sexo debe ser M o H.")]
        public string Sexo { get; set; } = "";

        [Required(ErrorMessage = "La reserva es obligatoria.")]
        [StringLength(100)]
        public string Reserva { get; set; } = "";

        [Range(0, 100, ErrorMessage = "La energia va de 0 a 100.")]
        public int Energia { get; set; }
    }
}
