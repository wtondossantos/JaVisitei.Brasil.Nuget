using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IMicrorregiaoRepository : IBaseRepository<Microrregiao>
    {
        IEnumerable<Microrregiao> PesquisarPorEstado(string id);
    }
}
