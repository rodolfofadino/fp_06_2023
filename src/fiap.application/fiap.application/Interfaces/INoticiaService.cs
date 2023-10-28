using fiap.domain.Models;

namespace fiap.application.Interfaces
{
    public interface INoticiaService
    {
        public List<Noticia> Load(int totalDeNoticias);
    }
}
