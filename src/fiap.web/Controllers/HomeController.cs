using fiap.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace fiap.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["aluno"] = $"Ana Lucia {DateTime.Now.ToString()}";

            ViewBag.TipoDoProduto = "Tenis";

            //origem do banco
            ViewBag.Tentativa = "<script>alert('teste');</script>";

            //return View("Produto");

            //abstraindo o acesso ao banco
            var homeModel = new HomeViewModel();
            homeModel.CategoriaDoProduto = "Calcados";
            homeModel.NomeDoAluno = "Caio";

         
            return View(homeModel);
        }

        public IActionResult Detalhe(string categoria, int idDoProduto, string tamanho)
        {
            
            return View("Produto");
        }
        
        
        [HttpPost]
        public IActionResult Produto(HomeViewModel model)
        {
            return View();
        }


        public IActionResult Login(string returnUrl)
        {
            if (!Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect("/");

            }
            else
            {

                return LocalRedirect(returnUrl);
            }

            //return LocalRedirect(returnUrl);
        }

    }
}
