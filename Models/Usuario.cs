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
        [Display(Name = "Id do usuário")]
        public int idUsuario { get; set; }

        [Column("nomeUsuario")]
        [Display(Name = "Usuario")]
        public string NomeUsuario { get; set; }

        [Column("senha")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Column("totalEmprestimos")]
        [Display(Name = "Total de empréstimos")]
        public int TotalEmprestimos { get; set; }

        [Column("atrasos")]
        [Display(Name = "Atrasos")]
        public int Atrasos { get; set; }

        [Column("emprestimosAtivos")]
        [Display(Name = "Empréstimos ativos")]
        public int EmprestimosAtivos { get; set; }

        public Usuario()
        {

        }
    }
}
