using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;
using System.Data.SqlClient;

namespace SiteMVC.Controllers
{
    [Authorize]
    public class EmprestimoController : Controller
    {
        // Método para exibir o formulário
        public IActionResult Index()
        {
            return View();
        }

        // Método POST para processar o envio do formulário
        [HttpPost]
        public IActionResult CriarEmprestimo(Emprestimo emprestimo)
        {
            using (SqlConnection conexao = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=escoteiros_db;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                // Abre a conexão
                conexao.Open();

                // Obtém o nome e o ID do usuário logado
                string usuario = User.Identity.Name;
                string idUsuario = User.FindFirst("UserId")?.Value;

                // Captura os dados do formulário
                string item = emprestimo.Item;
                string tipoItem = emprestimo.TipoItem;
                int idItem = emprestimo.IdItem;
                DateTime dataEmprestimo = DateTime.Now;

                // Query SQL com parâmetros
                var query = "INSERT INTO solicitacoes (usuario, idUsuario, item, tipoItem, idItem, dataEmprestimo, status) " +
                            "VALUES (@usuario, @idUsuario, @item, @tipoItem, @idItem, @dataEmprestimo, 'Emprestado')";

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // Definindo os parâmetros
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@tipoItem", tipoItem);
                    comando.Parameters.AddWithValue("@idItem", idItem);
                    comando.Parameters.AddWithValue("@dataEmprestimo", dataEmprestimo);

                    // Executa o comando SQL
                    comando.ExecuteNonQuery();
                }

                return RedirectToAction("Index"); // Redireciona para a página inicial após o envio
            }
        }
    }
}
