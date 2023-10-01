using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using JaVisitei.Brasil.Business.ViewModels.Response.State;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IStateService : IReadOnlyService<State>
    {
        Task<List<StateSearchResponse>> GetNamesAsync(short mapTypeId);
    }
}
