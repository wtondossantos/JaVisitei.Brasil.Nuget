using JaVisitei.Brasil.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JaVisitei.Brasil.Service.Base
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public void Adicionar(T entity)
        {
            _repository.Adicionar(entity);
        }

        public void Alterar(T entity)
        {
            _repository.Alterar(entity);
        }

        public void Excluir(Func<T, bool> predicate)
        {
            _repository.Excluir(predicate);
        }

        public IEnumerable<T> Pesquisar(Expression<Func<T, bool>> predicate)
        {
            return _repository.Pesquisar(predicate);
        }

        public IEnumerable<T> Pesquisar()
        {
            return _repository.Pesquisar();
        }
    }
}
