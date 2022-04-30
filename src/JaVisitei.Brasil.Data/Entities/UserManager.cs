using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class UserManager
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ActivationCode { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual User User { get; set; }
    }
}
