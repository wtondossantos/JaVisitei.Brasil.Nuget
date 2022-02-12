using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JaVisitei.Brasil.Business.Service.Base
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> Pesquisar(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Pesquisar();
        void Adicionar(T entity);
        void Alterar(T entity);
        void Excluir(Func<T, bool> predicate);
    }
}
