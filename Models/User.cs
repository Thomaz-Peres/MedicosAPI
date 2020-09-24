using System.ComponentModel.DataAnnotations;

namespace DesafioMedicos.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [MinLength(4, ErrorMessage = "Este campo precisa de no minimo 4 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [MinLength(6, ErrorMessage = "Este campo precisa de no minimo 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string Role { get; set; }
    }
}