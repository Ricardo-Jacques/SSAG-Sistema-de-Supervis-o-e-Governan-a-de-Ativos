using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;

namespace SiteMVC.Controllers
{
    public class RedefinirSenhaController : Controller
    {
        private readonly Contexto _context;

        public RedefinirSenhaController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(string nomeUsuario, string email, string novaSenha, DateOnly dataNascimento)
        {
            var user = _context.Usuario.SingleOrDefault(u => u.NomeUsuario == nomeUsuario && u.Email == email && u.DataNascimento == dataNascimento);

            if (user != null) //Caso o usuário com este E-mail seja encontrado
            {
                user.Senha = novaSenha;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            TempData["MensagemErro"] = "O usuário não foi encontrado. Verifique seus dados e tente novamente!";
            return View ("Index");
        }
    }
}
