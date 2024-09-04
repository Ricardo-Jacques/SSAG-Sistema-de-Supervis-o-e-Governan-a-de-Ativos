using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using System.Data.SqlClient;

namespace SiteMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SolicitacoesController : Controller
    {
        private readonly Contexto _context;

        public SolicitacoesController(Contexto context)
        {
            _context = context;
        }

        // GET: Solicitacoes
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var solicitacoes = await _context.Solicitacoes.OrderByDescending(solicitacao => solicitacao.IdSolicitacao).ToListAsync();
            var paginatedSolicitacoes = solicitacoes.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(solicitacoes.Count / (double)pageSize);

            return View(paginatedSolicitacoes);
        }

        // Aprovar solicitação
        public async Task<IActionResult> Aprovar(int? id)
        {
            using (SqlConnection conexao = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=escoteiros_db;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                await conexao.OpenAsync();

                // Atualiza a solicitação para 'Emprestado'
                var query = "UPDATE solicitacoes SET dataEmprestimo = @data, status = 'Emprestado' WHERE idSolicitacao = @id";
                DateTime data = DateTime.Now;

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@data", data);

                    await comando.ExecuteNonQueryAsync();
                }

                // Atualiza o status do item relacionado à solicitação aprovada
                var query2 = "UPDATE items SET status = 'Emprestado' " +
                             "WHERE id = (SELECT idItem FROM solicitacoes WHERE idSolicitacao = @id)";

                using (SqlCommand comando = new SqlCommand(query2, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    await comando.ExecuteNonQueryAsync();
                }

                // Atualiza o total de empréstimos e empréstimos ativos do usuário relacionado à solicitação
                var query3 = "UPDATE usuarios SET totalEmprestimos = totalEmprestimos + 1, " +
                   "emprestimosAtivos = emprestimosAtivos + 1 " +
                   "WHERE idUsuario = (SELECT idUsuario FROM solicitacoes WHERE idSolicitacao = @id)";

                using (SqlCommand comando = new SqlCommand(query3, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    await comando.ExecuteNonQueryAsync();
                }
            }

            return RedirectToAction("Index");
        }


        //Encerrar solicitação
        public async Task<IActionResult> Encerrar(int? id)
        {
            using (SqlConnection conexao = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=escoteiros_db;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                await conexao.OpenAsync(); // Abre a conexão de forma assíncrona

                var query = "UPDATE solicitacoes SET dataDevolucao = @data, status = 'Encerrado/Devolvido' WHERE idSolicitacao = @id";
                DateTime data = DateTime.Now;

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@data", data);

                    await comando.ExecuteNonQueryAsync(); // Executa a consulta de forma assíncrona
                }

                var query2 = "UPDATE items SET status = 'Disponível' " +
                              "WHERE id = (SELECT idItem FROM solicitacoes WHERE idSolicitacao = @id)";

                using (SqlCommand comando = new SqlCommand(query2, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    await comando.ExecuteNonQueryAsync(); // Executa a consulta de forma assíncrona
                }

                var query3 = "UPDATE usuarios SET emprestimosAtivos = emprestimosAtivos - 1 " +
                   "WHERE idUsuario = (SELECT idUsuario FROM solicitacoes WHERE idSolicitacao = @id)";

                using (SqlCommand comando = new SqlCommand(query3, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    await comando.ExecuteNonQueryAsync(); // Executa a consulta de forma assíncrona
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Solicitacoes/Details/5
        public async Task<IActionResult> Details(int? idSolicitacao)
        {
            if (idSolicitacao == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacoes
                .FirstOrDefaultAsync(m => m.IdSolicitacao == idSolicitacao);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        // GET: Solicitacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacao = await _context.Solicitacoes
                .FirstOrDefaultAsync(m => m.IdSolicitacao == id);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        // POST: Solicitacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idSolicitacao)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(idSolicitacao);
            if (solicitacao != null)
            {
                _context.Solicitacoes.Remove(solicitacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitacaoExists(int id)
        {
            return _context.Solicitacoes.Any(e => e.IdSolicitacao == id);
        }
    }
}
