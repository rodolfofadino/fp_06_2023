using fiap.application.Interfaces;
using fiap.domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace fiap.application.Services
{
    public class NoticiaService : INoticiaService
    {
        private IMemoryCache _cache;
        private IDatetimeProvider _dateTimeProvider;
        private INoticiaReader _noticiaReader;

        public NoticiaService(IMemoryCache memoryCache, IDatetimeProvider dateTimeProvider, INoticiaReader noticiaReader)
        {
            _cache = memoryCache;
            _dateTimeProvider = dateTimeProvider;
            _noticiaReader = noticiaReader;
        }

        public List<Noticia> Load(int totalDeNoticias)
        {
            var horario = _dateTimeProvider.GetNow();

            if (horario.Hour >= 0 && horario.Hour <= 6)
            {
                //listar noticias do rss de ontem
                //return new List<Noticia>();
            }



            var key = "noticias_";

            if (_cache.TryGetValue(key, out List<Noticia> noticias))
            {
                return noticias;
            }


            noticias = _noticiaReader.Load();

          

            var cacheEntryOption = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(600));

            //var cacheEntryOptionSliding = new MemoryCacheEntryOptions()
            //    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            _cache.Set(key, noticias, cacheEntryOption);



            return noticias.Where(a => a.Imagem != "").ToList();
        }

    }
}