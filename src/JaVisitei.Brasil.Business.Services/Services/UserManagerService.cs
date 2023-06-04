using JaVisitei.Brasil.Business.ViewModels.Request.UserManager;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Data.Entities;
using AutoMapper;
using System.Threading.Tasks;
using System;

namespace JaVisitei.Brasil.Business.Service.Services
{
    public class UserManagerService : Service<UserManager>, IUserManagerService
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IMapper _mapper;

        public UserManagerService(IUserManagerRepository userManagerRepository,
            IMapper mapper) : base(userManagerRepository)
        {
            _userManagerRepository = userManagerRepository;
            _mapper = mapper;
        }

        public async Task<UserManager> CreateAsync(InsertEmailConfirmationUserManagerRequest request)
        {
            return await _userManagerRepository.CreateAsync(_mapper.Map<UserManager>(request));
        }

        public async Task<UserManager> CreateEmailConfirmationAsync(string userId)
        {
            var request = new InsertEmailConfirmationUserManagerRequest { 
                UserId = userId
            };
            return await _userManagerRepository.CreateAsync(_mapper.Map<UserManager>(request));
        }

        public async Task<UserManager> CreateAsync(InsertPasswordResetUserManagerRequest request)
        {
            return await _userManagerRepository.CreateAsync(_mapper.Map<UserManager>(request));
        }
        public async Task<UserManager> CreatePasswordResetAsync(string userId)
        {
            var request = new InsertPasswordResetUserManagerRequest
            {
                UserId = userId
            };
            return await _userManagerRepository.CreateAsync(_mapper.Map<UserManager>(request));
        }
        
        public async Task<UserManager> GetByManagerCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            return await _userManagerRepository
                   .GetFirstOrDefaultAsync(x => x.ManagerCode.Equals(code.Substring(0, 8)) &&
                       x.Id.Equals(Convert.ToInt32(code.Substring(8, code.Length - 8))));
        }

        public async Task<bool> ConfirmedChangeAsync(UserManager userManager)
        {
            if (userManager is null || userManager.Id.Equals(0))
                return false;

            userManager.ConfirmedChange = true;
            return await _userManagerRepository.UpdateAsync(userManager);
        }
    }
}
