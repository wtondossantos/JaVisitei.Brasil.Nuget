﻿using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Repository.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IArchipelagoRepository : IReadOnlyRepository<Archipelago>
    {
        Task<IEnumerable<Archipelago>> GetByStateAsync(string stateId);
    }
}
