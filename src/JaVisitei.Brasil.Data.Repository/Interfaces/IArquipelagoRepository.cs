using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IArquipelagoRepository : IBaseRepository<Arquipelago>
    {
        IEnumerable<Arquipelago> PesquisarPorEstado(string id);
    }
}
