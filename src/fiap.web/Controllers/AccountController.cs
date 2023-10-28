using fiap.web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace fiap.web.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Musicas");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //procurar no banco
                if (model.UserName == "murilo" && model.Password == "123Mudar")
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, model.UserName));
                    claims.Add(new Claim(ClaimTypes.Hash, model.UserName));

                    claims.Add(new Claim(ClaimTypes.Role, "admin"));

                    var id = new ClaimsIdentity(claims, "password");
                    var principal = new ClaimsPrincipal(id);

                    await HttpContext.SignInAsync("fiap", principal,
                        new AuthenticationProperties() { IsPersistent = model.IsPersistent });

                    return LocalRedirect("/musicas/create");

                }

            }

            return View(model);
        }



        public async Task<IActionResult> LogOff()
        {
            //remover fingerprint do navegador do banco
            await HttpContext.SignOutAsync();

            return LocalRedirect("/");
        }
    }
}
