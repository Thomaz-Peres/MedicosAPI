using System.ComponentModel.DataAnnotations;
using DesafioMedicos.Validations;

namespace DesafioMedicos.Models
{
    public class Doctors
    {
        [Key]
        public int MedicoID { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(255, ErrorMessage = "Deve conter no maximo 255 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required]
        public string Crm { get; set; }

        [Required(ErrorMessage = "É necessario ao menos uma especialidade")]
        public string Especialidades { get; set; }
    }
}