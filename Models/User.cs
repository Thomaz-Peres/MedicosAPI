using System.ComponentModel.DataAnnotations;

namespace DesafioMedicos.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string Role { get; set; }
    }
}