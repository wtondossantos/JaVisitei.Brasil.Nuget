using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class EmailFooter
    {
        public EmailFooter()
        {
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string Footer { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}
