using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Microregion
    {
        public Microregion()
        {
            Municipalities = new HashSet<Municipality>();
        }

        public string Id { get; set; }
        public string MacroregionId { get; set; }
        public string Name { get; set; }
        public string Canvas { get; set; }

        public virtual Macroregion Macroregion { get; set; }
        public virtual ICollection<Municipality> Municipalities { get; set; }
    }
}
