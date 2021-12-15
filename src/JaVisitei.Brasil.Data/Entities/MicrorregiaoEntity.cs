using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbMicrorregiao")]
    public partial class MicrorregiaoEntity
    {
        public MicrorregiaoEntity()
        {
            Municipios = new HashSet<MunicipioEntity>();
        }

        [Column("Id")]
        public string Id { get; set; }

        [Column("IdMesorregiao")]
        public string IdMesorregiao { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Desenho")]
        public string Desenho { get; set; }

        internal virtual MesorregiaoEntity IdMesorregiaoNavigation { get; set; }

        internal virtual ICollection<MunicipioEntity> Municipios { get; set; }
    }
}