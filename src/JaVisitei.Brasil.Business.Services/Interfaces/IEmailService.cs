﻿using JaVisitei.Brasil.Business.Service.Base;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Email;
using JaVisitei.Brasil.Data.Entities;
using System.Threading.Tasks;

namespace JaVisitei.Brasil.Business.Service.Interfaces
{
    public interface IEmailService : IBaseService<Email>
    {
        Task<EmailValidator> SendAsync(SendEmailRequest request);
    }
}
