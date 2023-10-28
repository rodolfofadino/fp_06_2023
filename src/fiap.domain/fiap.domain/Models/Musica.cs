using System.ComponentModel.DataAnnotations;

namespace fiap.domain.Models
{
    public class Musica
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Titulo { get; set; }
        public int? Tempo{ get; set; }
        public string Banda{ get; set; }
        public string Album{ get; set; }
        public string EstiloMusical { get; set; }
    }
}
