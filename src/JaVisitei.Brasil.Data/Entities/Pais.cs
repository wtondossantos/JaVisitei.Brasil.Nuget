using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbPais")]
    public partial class Pais
    {
        public Pais()
        {
            Estados = new HashSet<Estado>();
        }

        [Column("Id")]
        public string Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        internal virtual ICollection<Estado> Estados { get; set; }
    }
}
