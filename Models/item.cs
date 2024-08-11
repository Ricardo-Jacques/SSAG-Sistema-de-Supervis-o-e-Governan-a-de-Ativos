namespace SiteMVC.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Proprietario { get; set; }
        public double Valor { get; set; }
        public string Estado { get; set; }
        public Item()
        {

        }
    }
}
