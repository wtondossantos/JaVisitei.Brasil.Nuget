using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Archipelago
    {
        public Archipelago()
        {
            Islands = new HashSet<Island>();
        }

        public string Id { get; set; }
        public string MacroregionId { get; set; }
        public string Name { get; set; }

        public virtual Macroregion Macroregion { get; set; }
        public virtual ICollection<Island> Islands { get; set; }
    }
}
