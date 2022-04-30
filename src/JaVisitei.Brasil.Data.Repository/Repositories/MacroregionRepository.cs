﻿using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class MacroregionRepository : BaseRepository<Macroregion>, IMacroregionRepository
    {
        public MacroregionRepository(DbJaVisiteiBrasilContext context) : base(context) { }
    }
}