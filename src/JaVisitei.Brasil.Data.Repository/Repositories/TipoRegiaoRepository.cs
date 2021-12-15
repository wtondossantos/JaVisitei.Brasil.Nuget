﻿using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Data.Base;
using JaVisitei.Brasil.Data.Repository.Base;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Data.Repository.Repositories
{
    public class TipoRegiaoRepository : BaseRepository<TipoRegiao>, ITipoRegiaoRepository
    {
        private new readonly DbJaVisiteiBrasilContext _context;

        public TipoRegiaoRepository(DbJaVisiteiBrasilContext context) : base(context)
        {
            _context = context;
        }
    }
}
