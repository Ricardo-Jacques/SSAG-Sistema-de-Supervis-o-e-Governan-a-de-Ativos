using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteMVC.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("idUsuario")]
        [Display(Name = "ID do usuário")]
        public int IdUsuario { get; set; }

        [Column("nomeUsuario")]
        [Display(Name = "Usuário")]
        public string? NomeUsuario { get; set; }

        [Column("senha")]
        [Display(Name = "Senha")]
        public string? Senha { get; set; }

        [Column("totalEmprestimos")]
        [Display(Name = "Total de empréstimos")]
        public int TotalEmprestimos { get; set; }

        [Column("emprestimosAtivos")]
        [Display(Name = "Empréstimos ativos")]
        public int EmprestimosAtivos { get; set; }

        [Column("tipoUsuario")]
        [Display(Name = "Tipo de usuário")]
        public string? TipoUsuario { get; set; } // "Admin" ou "User"

        [Column("email")]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Column("dataNascimento")]
        [Display(Name = "Data de Nascimento")]
        public DateOnly? DataNascimento { get; set; }
    }
}
