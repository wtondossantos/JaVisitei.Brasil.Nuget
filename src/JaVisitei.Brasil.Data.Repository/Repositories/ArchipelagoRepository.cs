﻿using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class ArchipelagoRepository : ReadOnlyRepository<Archipelago>, IArchipelagoRepository
    {   
        public ArchipelagoRepository(DbJaVisiteiBrasilContext context) : base(context) { }

        public async Task<IEnumerable<Archipelago>> GetByStateAsync(string stateId)
        {
            return await GetAsync(x => x.Id.Substring(0, 3).Equals(stateId.Substring(0, 3)));
        }
    }
}
