using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCadastroLocais_Web.Models
{
    //[Table("TB_PontosTuristicos")]
    public class TB_PontosTuristicos
    {
        //[Key]
        public int LocaisId { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}
