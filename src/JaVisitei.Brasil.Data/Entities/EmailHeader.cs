using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class EmailHeader
    {
        public EmailHeader()
        {
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string Header { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}
