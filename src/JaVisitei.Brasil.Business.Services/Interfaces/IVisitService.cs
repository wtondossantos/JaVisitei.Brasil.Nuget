using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using JaVisitei.Brasil.Business.Validation.Validators;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IVisitService : IBaseService<Visit>
    {
        Task<VisitValidator> AddAsync(AddVisitRequest request);
    }
}
