using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IIlhaService : IBaseService<Ilha>
    {
        Task<IEnumerable<Ilha>> PesquisarPorEstadoAsync(string id);
        Task<IEnumerable<Ilha>> PesquisarPorMesorregiaoAsync(string id);
    }
}
