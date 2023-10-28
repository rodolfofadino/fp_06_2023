using fiap.application.Interfaces;
using fiap.application.Services;
using fiap.domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace fiap.test
{
    [TestClass]
    public class NoticiaServiceTests
    {
        [TestMethod]
        public void Nome_Metodo_Cenario_Resultado_Experado()
        {
            var datetime = new DateTime(2023, 1, 1);
            var datetimeProvider = new Mock<IDatetimeProvider>();
            datetimeProvider.Setup(x => x.GetNow()).Returns(datetime);

            var memoryCacheMock = new Mock<IMemoryCache>(MockBehavior.Default );

            int expectedNumber = 1;
            object expectedValue = expectedNumber;
            memoryCacheMock.Setup(x => x.TryGetValue("noticias_",out expectedValue)).Returns(false);
            memoryCacheMock.Setup(x => x.Set("noticias_", It.IsAny<List<Noticia>>(), It.IsAny<MemoryCacheEntryOptions>()))
                .Returns((string key, object value, MemoryCacheEntryOptions options) => value);
            
            var noticiasEsperadas = new List<Noticia>() {
                new Noticia(){ Id=1, Titulo="lala", Imagem="https://" }
            };
            var readerMock = new Mock<INoticiaReader>();
            readerMock.Setup(x => x.Load()).Returns(noticiasEsperadas);

            var noticiaService = new NoticiaService(memoryCacheMock.Object, datetimeProvider.Object, readerMock.Object);


            var noticiasFinais = noticiaService.Load(3);

            //Assert.AreEqual(noticiasEsperadas, noticiasFinais);

            //Asserts
            //Verifies

        }
    }
}