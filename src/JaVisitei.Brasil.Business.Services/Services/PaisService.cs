﻿using JaVisitei.Brasil.Data.Entities;
using JaVisitei.Brasil.Service.Base;
using JaVisitei.Brasil.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;

namespace JaVisitei.Brasil.Service.Services
{
    public class PaisService : BaseService<Pais>, IPaisService
    {
        private readonly IPaisRepository _repository;

        public PaisService(IPaisRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
