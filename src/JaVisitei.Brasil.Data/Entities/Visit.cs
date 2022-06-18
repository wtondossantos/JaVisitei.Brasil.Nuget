using System;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Visit
    {
        public int UserId { get; set; }
        public short RegionTypeId { get; set; }
        public string RegionId { get; set; }
        public string Color { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime RegistryDate { get; set; }

        public virtual RegionType RegionType { get; set; }
        public virtual User User { get; set; }
    }
}
