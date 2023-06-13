using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class EmailConfig
    {
        public EmailConfig()
        {
            Emails = new HashSet<Email>();
        }

        public int Id { get; set; }
        public string ServerSmtp { get; set; }
        public string FromSmtp { get; set; }
        public int PortSmtp { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public virtual ICollection<Email> Emails { get; set; }
    }
}
