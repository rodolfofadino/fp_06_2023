using Microsoft.AspNetCore.Mvc;
using fiap.application.Interfaces;

namespace fiap.web.ViewComponents
{
    public class NoticiasViewComponent : ViewComponent
    {
        private INoticiaService _noticiaService;

        public NoticiasViewComponent(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;

        }
        public async Task<IViewComponentResult> InvokeAsync(int total, bool noticiasUrgentes)
        {
            var noticias = _noticiaService.Load(total);
            
            var viewName = "Noticias";

            if (noticiasUrgentes)
            {
                viewName = "NoticiasUrgentes";
            }

            return View(viewName, noticias);
            
        }
    }
}
