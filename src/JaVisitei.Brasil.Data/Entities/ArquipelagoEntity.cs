using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbArquipelago")]
    public partial class ArquipelagoEntity
    {

        public ArquipelagoEntity()
        {
            Ilhas = new HashSet<IlhaEntity>();
        }

        [Column("Id")]
        public string Id { get; set; }
        
        [Column("IdMesorregiao")]
        public string IdMesorregiao { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        internal virtual MesorregiaoEntity IdMesorregiaoNavigation { get; set; }

        internal virtual ICollection<IlhaEntity> Ilhas { get; set; }
    }
}
