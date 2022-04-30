using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class State
    {
        public State()
        {
            Macroregions = new HashSet<Macroregion>();
        }

        public string Id { get; set; }
        public string CountryId { get; set; }
        public string Name { get; set; }
        public string Canvas { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Macroregion> Macroregions { get; set; }
    }
}
