﻿using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbMunicipio")]
    public partial class MunicipioEntity
    {
        [Column("Id")]
        public string Id { get; set; }

        [Column("IdMicrorregiao")]
        public string IdMicrorregiao { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Desenho")]
        public string Desenho { get; set; }

        internal virtual MicrorregiaoEntity IdMicrorregiaoNavigation { get; set; }
    }
}
