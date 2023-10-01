using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public class MapType
    {
        public MapType()
        {
            Countries = new HashSet<Country>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
