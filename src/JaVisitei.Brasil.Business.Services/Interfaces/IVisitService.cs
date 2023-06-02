using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.Validation.Validators;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IVisitService : IService<Visit>
    {
        Task<VisitValidator> InsertAsync(InsertVisitRequest request);
        Task<VisitValidator> UpdateAsync(UpdateVisitRequest request);
        Task<VisitValidator> DeleteAsync(VisitKeyRequest request);
        Task<M> GetByIdAsync<M>(VisitKeyRequest request);
        Task<IEnumerable<M>> GetByUserIdAsync<M>(int userId);
    }
}
