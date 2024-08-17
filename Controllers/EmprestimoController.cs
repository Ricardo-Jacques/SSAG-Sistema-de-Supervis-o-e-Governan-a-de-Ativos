using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using System.Data.SqlClient;

namespace SiteMVC.Controllers
{
    [Authorize]
    public class EmprestimoController : Controller
    {
        private readonly Contexto _context;

        public EmprestimoController(Contexto context)
        {
            _context = context;
        }

        // Método para exibir o formulário
        public IActionResult Index()
        {
            var itens = _context.Items.ToList(); // Busque os itens do banco de dados ou qualquer outra fonte de dados.
            return View(itens);

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

                // Cria uma nova solicitação
                var query = "INSERT INTO solicitacoes (usuario, idUsuario, item, tipoItem, idItem, status) " +
                            "VALUES (@usuario, @idUsuario, @item, @tipoItem, @idItem, 'Aguardando aprovação')";

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // Definindo os parâmetros
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@tipoItem", tipoItem);
                    comando.Parameters.AddWithValue("@idItem", idItem);

                    comando.ExecuteNonQuery();
                }

                return RedirectToAction("Index"); 
            }
        }
    }
}
