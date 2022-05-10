using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class EmailTemplate
    {
        public EmailTemplate()
        {
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string Template { get; set; }

        public virtual ICollection<Email> Emails { get; set; }
    }
}
