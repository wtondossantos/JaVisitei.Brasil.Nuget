using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbMesorregiao")]
    public partial class MesorregiaoEntity
    {
        public MesorregiaoEntity()
        {
            Arquipelagos = new HashSet<ArquipelagoEntity>();
            Microrregiaos = new HashSet<MicrorregiaoEntity>();
        }

        [Column("Id")]
        public string Id { get; set; }

        [Column("IdEstado")]
        public string IdEstado { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Desenho")]
        public string Desenho { get; set; }

        internal virtual EstadoEntity IdEstadoNavigation { get; set; }

        internal virtual ICollection<ArquipelagoEntity> Arquipelagos { get; set; }

        internal virtual ICollection<MicrorregiaoEntity> Microrregiaos { get; set; }
    }
}
