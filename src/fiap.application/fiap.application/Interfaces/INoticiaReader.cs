using fiap.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiap.application.Interfaces
{
    public interface INoticiaReader
    {
        public List<Noticia> Load();
    }
}
