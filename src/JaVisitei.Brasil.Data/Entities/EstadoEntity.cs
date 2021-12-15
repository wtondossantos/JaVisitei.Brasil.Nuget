﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbEstado")]
    public partial class EstadoEntity
    {
        public EstadoEntity()
        {
            Mesorregiaos = new HashSet<MesorregiaoEntity>();
        }

        [Column("Id")]
        public string Id { get; set; }

        [Column("IdPais")]
        public string IdPais { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Desenho")]
        public string Desenho { get; set; }

        internal virtual PaisEntity IdPaisNavigation { get; set; }

        internal virtual ICollection<MesorregiaoEntity> Mesorregiaos { get; set; }
    }
}
