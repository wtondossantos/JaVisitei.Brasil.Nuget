using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Service.Interfaces
{
    public interface IIlhaService : IBaseService<Ilha>
    {
        IEnumerable<Ilha> PesquisarPorEstado(string id);
    }
}
