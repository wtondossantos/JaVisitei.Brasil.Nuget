using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IArquipelagoService : IBaseService<Arquipelago>
    {
        IEnumerable<Arquipelago> PesquisarPorEstado(string id);
    }
}
