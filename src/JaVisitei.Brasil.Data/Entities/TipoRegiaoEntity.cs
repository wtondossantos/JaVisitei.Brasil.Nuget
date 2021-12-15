using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbTipoRegiao")]
    public partial class TipoRegiaoEntity
    {
        public TipoRegiaoEntity()
        {
            Visitas = new HashSet<VisitaEntity>();
        }

        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        internal virtual ICollection<VisitaEntity> Visitas { get; set; }

    }
}