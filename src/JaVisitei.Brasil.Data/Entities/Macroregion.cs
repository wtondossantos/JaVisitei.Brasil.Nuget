using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Macroregion
    {
        public Macroregion()
        {
            Archipelagos = new HashSet<Archipelago>();
            Microregions = new HashSet<Microregion>();
        }

        public string Id { get; set; }
        public string StateId { get; set; }
        public string Name { get; set; }
        public string Canvas { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Archipelago> Archipelagos { get; set; }
        public virtual ICollection<Microregion> Microregions { get; set; }
    }
}
