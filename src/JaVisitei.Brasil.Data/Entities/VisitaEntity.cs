using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbVisita")]
    public partial class VisitaEntity
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("IdTipoRegiao")]
        public int IdTipoRegiao { get; set; }

        [Column("IdRegiao")]
        public string IdRegiao { get; set; }

        [Column("Cor")]
        public string Cor { get; set; }

        [Column("Data")]
        public DateTime Data { get; set; }

        internal virtual UsuarioEntity IdUsuarioNavigation { get; set; }

        internal virtual TipoRegiaoEntity IdTipoRegiaoNavigation { get; set; }
    }
}
