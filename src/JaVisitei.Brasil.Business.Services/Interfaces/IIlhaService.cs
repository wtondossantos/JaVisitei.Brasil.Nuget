using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IIlhaService : IBaseService<Ilha>
    {
        IEnumerable<Ilha> PesquisarPorEstado(string id);
        IEnumerable<Ilha> PesquisarPorMesorregiao(string id);
    }
}
