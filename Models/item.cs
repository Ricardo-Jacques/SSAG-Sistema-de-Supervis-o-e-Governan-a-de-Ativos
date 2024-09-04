using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiteMVC.Models
{
    public class Item
    {
        [Key]
        [Column("id")]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Column("nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("tipo")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Column("proprietario")]
        [Display(Name = "Proprietário")]
        public string Proprietario { get; set; }

        [Column("valor", TypeName = "decimal(6,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Column("status")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Column("observacoes")]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
        public Item()
        {

        }
    }
}
