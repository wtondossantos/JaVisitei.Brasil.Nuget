using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Visit
    {
        public int UserId { get; set; }
        public short RegionTypeId { get; set; }
        public string RegionId { get; set; }
        public string Color { get; set; }
        public DateOnly? VisitDate { get; set; }
        public DateOnly RegistryDate { get; set; }

        public virtual RegionType RegionType { get; set; }
        public virtual User User { get; set; }
    }
}
