﻿using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IIlhaRepository : IBaseRepository<Ilha>
    {
        IEnumerable<Ilha> PesquisarPorEstado(string id);
    }
}
