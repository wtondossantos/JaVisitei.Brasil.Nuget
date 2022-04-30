using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Response.Visit;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IVisitService : IBaseService<Visit>
    {
        Task<AddVisitResponse> AddAsync(AddVisitRequest request);
    }
}
