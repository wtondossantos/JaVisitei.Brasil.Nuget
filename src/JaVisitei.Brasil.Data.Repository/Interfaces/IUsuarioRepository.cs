﻿using JaVisitei.Brasil.Model.Models;
using JaVisitei.Brasil.Data.Repository.Base;

namespace JaVisitei.Brasil.Data.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario Autenticacao(Usuario usuario);
    }
}
