using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IIlhaRepository : IBaseRepository<Ilha>
    {
        IEnumerable<Ilha> PesquisarPorEstado(string id);
    }
}
