using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SiteMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SiteMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly Contexto _context;

        public AccountController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult TelaLogin()
        {
            return RedirectToAction("login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Verifica se o usuário existe e se a senha está correta
            var user = _context.Usuario.SingleOrDefault(u => u.NomeUsuario == username && u.Senha == password);

            if (user != null)
            {
                // Cria as claims de autenticação
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.NomeUsuario),
                    new Claim("UserId", user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, user.TipoUsuario)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Faz o login e cria o cookie de autenticação
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Redireciona para a página inicial após o login bem-sucedido
                return RedirectToAction("Index", "Items");
            }

            // Se o login falhar, exibe uma mensagem de erro
            ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Faz o logout e remove o cookie de autenticação
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
