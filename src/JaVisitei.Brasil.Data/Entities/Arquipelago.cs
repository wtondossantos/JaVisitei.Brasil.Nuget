using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbArquipelago")]
    public partial class Arquipelago
    {

        public Arquipelago()
        {
            Ilhas = new HashSet<Ilha>();
        }

        [Column("Id")]
        public string Id { get; set; }
        
        [Column("IdMesorregiao")]
        public string IdMesorregiao { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        internal virtual Mesorregiao IdMesorregiaoNavigation { get; set; }

        internal virtual ICollection<Ilha> Ilhas { get; set; }
    }
}
