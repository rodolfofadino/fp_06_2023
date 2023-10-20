using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using fiap.core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace fiap.core.Services
{


    public class NoticiaService
    {
        private IMemoryCache _cache;

        public NoticiaService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public List<Noticia> Load(int totalDeNoticias)
        {
            var key = "noticias_";

            if (_cache.TryGetValue(key, out List<Noticia> noticias))
            {
                return noticias;
            }
            

            noticias = new List<Noticia>();
            
            var feed = FeedReader.ReadAsync("https://g1.globo.com/rss/g1/turismo-e-viagem/").Result;

            foreach (var item in feed.Items)
            {
                var feedItem = item.SpecificItem as CodeHollow.FeedReader.Feeds.MediaRssFeedItem;
                var media = feedItem.Media;
                var url = "";
                if (media.Any())
                    url = media.FirstOrDefault().Url;
                noticias.Add(new Noticia() { Id = 1, Titulo = item.Title, Link = item.Link, Imagem = url });
            }

            var cacheEntryOption = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(600));
      
            //var cacheEntryOptionSliding = new MemoryCacheEntryOptions()
            //    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            _cache.Set(key, noticias, cacheEntryOption);



            return noticias.Where(a => a.Imagem != "").ToList();
        }

    }
}