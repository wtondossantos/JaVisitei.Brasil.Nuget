using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Email
    {
        public Email()
        {
            UserManagers = new HashSet<UserManager>();
        }

        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int FooterId { get; set; }
        public int TemplateId { get; set; }
        public int EmailConfigId { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }

        public virtual EmailConfig EmailConfig { get; set; }
        public virtual EmailFooter Footer { get; set; }
        public virtual EmailHeader Header { get; set; }
        public virtual EmailTemplate Template { get; set; }
        public virtual ICollection<UserManager> UserManagers { get; set; }
    }
}
