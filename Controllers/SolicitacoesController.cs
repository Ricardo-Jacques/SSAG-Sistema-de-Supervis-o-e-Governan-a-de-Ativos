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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Solicitacoes.ToListAsync());
        }

        // Aprovar solicitação
        public async Task<IActionResult> Aprovar(int? id)
        {
            using (SqlConnection conexao = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=escoteiros_db;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                conexao.Open();
                var query = "UPDATE solicitacoes SET dataEmprestimo = @data, status = 'Emprestado' WHERE idSolicitacao = @id";
                DateTime data = DateTime.Now;

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@data", data);

                    comando.ExecuteNonQuery();
                }

                var query2 = "UPDATE items SET estado = 'Emprestado' FROM items " +
                    "JOIN solicitacoes ON solicitacoes.idItem = items.id " +
                    "WHERE items.id = solicitacoes.idItem;";

                using (SqlCommand comando = new SqlCommand(query2, conexao))
                {
                    comando.ExecuteNonQuery();
                }

                var query3 = "UPDATE usuarios SET totalEmprestimos = totalEmprestimos + 1, " +
                   "emprestimosAtivos = emprestimosAtivos + 1 FROM usuarios " +
                   "JOIN solicitacoes ON solicitacoes.idUsuario = usuarios.idUsuario " +
                   "WHERE usuarios.idUsuario = solicitacoes.idUsuario;";

                using (SqlCommand comando = new SqlCommand(query3, conexao))
                {
                    comando.ExecuteNonQuery();
                }

            }
            return RedirectToAction("Index");
        }

        //Encerrar solicitação
        public async Task<IActionResult> Encerrar(int? id)
        {
            using (SqlConnection conexao = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=escoteiros_db;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
            {
                conexao.Open();
                var query = "UPDATE solicitacoes SET dataDevolucao = @data, status = 'Encerrado/Devolvido' WHERE idSolicitacao = @id";
                DateTime data = DateTime.Now;

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@data", data);

                    comando.ExecuteNonQuery();
                }

                var query2 = "UPDATE items SET estado = 'Disponível' FROM items " +
                    "JOIN solicitacoes ON solicitacoes.idItem = items.id " +
                    "WHERE items.id = solicitacoes.idItem;";

                using (SqlCommand comando = new SqlCommand(query2, conexao))
                {
                    comando.ExecuteNonQuery();
                }

                var query3 = "UPDATE usuarios SET emprestimosAtivos = emprestimosAtivos - 1 FROM usuarios " +
                   "JOIN solicitacoes ON solicitacoes.idUsuario = usuarios.idUsuario " +
                   "WHERE usuarios.idUsuario = solicitacoes.idUsuario;";

                using (SqlCommand comando = new SqlCommand(query3, conexao))
                {
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Solicitacoes/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitacao = await _context.Solicitacoes.FindAsync(id);
            if (solicitacao != null)
            {
                _context.Solicitacoes.Remove(solicitacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Solicitacoes.Any(e => e.IdSolicitacao == id);
        }
    }
}
