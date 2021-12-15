﻿using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Service.Base;
using System.Collections.Generic;

namespace JaVisitei.Brasil.Service.Interfaces
{
    public interface IMunicipioService : IBaseService<Municipio>
    {
        IEnumerable<Municipio> PesquisarPorEstado(string id);
    }
}
