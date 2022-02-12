using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Service.Interfaces
{
    public interface IArquipelagoService : IBaseService<Arquipelago>
    {
        IEnumerable<Arquipelago> PesquisarPorEstado(string id);
    }
}
