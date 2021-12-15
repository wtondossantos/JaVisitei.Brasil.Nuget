using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbPais")]
    public partial class PaisEntity
    {
        public PaisEntity()
        {
            Estados = new HashSet<EstadoEntity>();
        }

        [Column("Id")]
        public string Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        internal virtual ICollection<EstadoEntity> Estados { get; set; }
    }
}
