using System;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Entities
{
    public partial class User
    {
        public User()
        {
            UserManagers = new HashSet<UserManager>();
            Visits = new HashSet<Visit>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistryDate { get; set; }
        public DateTime RefreshTokenDate { get; set; }
        public bool Actived { get; set; }
        public int UserRoleId { get; set; }
        public string SecurityStamp { get; set; }
        public string RefreshToken { get; set; }
        public bool Newsletter { get; set; }
        
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<UserManager> UserManagers { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
