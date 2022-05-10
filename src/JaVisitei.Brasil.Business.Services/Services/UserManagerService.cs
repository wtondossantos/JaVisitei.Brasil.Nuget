using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class UserManagerService : BaseService<UserManager>, IUserManagerService
    {
        public UserManagerService(IUserManagerRepository userManagerRepository) : base(userManagerRepository) { }
    }
}
