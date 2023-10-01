using JaVisitei.Brasil.Business.ViewModels.Response.State;
using JaVisitei.Brasil.Data.Entities;

namespace JaVisitei.Brasil.Caching.Service.Interfaces
{
    public interface IStateCachingService
    {
        Task<List<StateSearchResponse>> GetNamesAsync(short mapTypeId, List<State> states);
    }
}
