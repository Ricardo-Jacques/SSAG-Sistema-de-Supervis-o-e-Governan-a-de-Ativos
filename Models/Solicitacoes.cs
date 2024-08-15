using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteMVC.Models
{
    [Table("solicitacoes")]
    public class Solicitacoes
    {
        [Key]
        public int IdSolicitacao { get; set; }
        public string Usuario { get; set; }
        public int IdUsuario { get; set; }
        public string Item { get; set; }
        public string TipoItem { get; set; }
        public int IdItem { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string Status { get; set; }
    }
}
