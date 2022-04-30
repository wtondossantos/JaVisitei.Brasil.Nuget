using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Municipality
    {
        public string Id { get; set; }
        public string MicroregionId { get; set; }
        public string Name { get; set; }
        public string Canvas { get; set; }

        public virtual Microregion Microregion { get; set; }
    }
}
