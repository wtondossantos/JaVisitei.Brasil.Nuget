using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IMunicipioService : IBaseService<Municipio>
    {
        Task<IEnumerable<Municipio>> PesquisarPorEstadoAsync(string id);
        Task<IEnumerable<Municipio>> PesquisarPorMesorregiaoAsync(string id);
    }
}
