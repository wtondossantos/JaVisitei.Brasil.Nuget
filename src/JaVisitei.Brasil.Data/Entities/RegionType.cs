using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class RegionType
    {
        public RegionType()
        {
            Visits = new HashSet<Visit>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
