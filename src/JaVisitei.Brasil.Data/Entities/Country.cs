using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
