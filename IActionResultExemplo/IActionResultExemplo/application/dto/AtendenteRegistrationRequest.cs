using System.ComponentModel.DataAnnotations;

namespace IActionResultExemplo.application.dto
{
    public class AtendenteRegistrationRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "A confirmação da senha não corresponde.")]
        public string ConfirmacaoSenha { get; set; }

        [Phone(ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }

}
