﻿using System.Threading.Tasks;

namespace SendingMailsAPI.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
