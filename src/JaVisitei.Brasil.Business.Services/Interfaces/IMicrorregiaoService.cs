using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IMicrorregiaoService : IBaseService<Microrregiao>
    {
        IEnumerable<Microrregiao> PesquisarPorEstado(string id);
    }
}
