using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class UserManager
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EmailId { get; set; }
        public string ManagerCode { get; set; }
        public bool ConfirmedChange { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Email Email { get; set; }
        public virtual User User { get; set; }
    }
}
