﻿using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Business.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IMicrorregiaoService : IBaseService<Microrregiao>
    {
        Task<IEnumerable<Microrregiao>> PesquisarPorEstadoAsync(string id);
    }
}
