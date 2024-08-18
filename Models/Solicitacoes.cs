using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteMVC.Models
{
    [Table("solicitacoes")]
    public class Solicitacoes
    {
        [Key]
        [Column("idSolicitacao")]
        [Display(Name = "ID da solicitação")]
        public int IdSolicitacao { get; set; }

        [Column("usuario")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Column("idUsuario")]
        [Display(Name = "ID do usuário")]
        public int IdUsuario { get; set; }

        [Column("item")]
        [Display(Name = "Item")]
        public string Item { get; set; }

        [Column("tipoItem")]
        [Display(Name = "Tipo de item")]
        public string TipoItem { get; set; }

        [Column("idItem")]
        [Display(Name = "ID do item")]
        public int IdItem { get; set; }

        [Column("dataEmprestimo")]
        [Display(Name = "Data de empréstimo")]
        public DateTime? DataEmprestimo { get; set; }

        [Column("dataDevolucao")]
        [Display(Name = "Data de devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Column("status")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        public Solicitacoes()
        {

        }
    }
}
