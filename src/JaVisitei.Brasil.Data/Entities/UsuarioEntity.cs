using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JaVisitei.Brasil.Data.Entities
{
    [Table("TbUsuario")]
    public partial class UsuarioEntity 
    {
        public UsuarioEntity()
        {
            Visitas = new HashSet<VisitaEntity>();
        }

        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Sobrenome")]
        public string Sobrenome { get; set; }

        [Column("NomeUsuario")]
        public string NomeUsuario { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Senha")]
        public string Senha { get; set; }

        internal virtual ICollection<VisitaEntity> Visitas { get; set; }

    }
}

