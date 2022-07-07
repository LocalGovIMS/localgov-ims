using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Dependencies;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;

namespace BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailServiceDependencies _dependencies;
        private readonly SmtpClient _smtpClient;

        public EmailService(IEmailServiceDependencies emailServiceDependencies)
        {
            _dependencies = emailServiceDependencies ?? throw new ArgumentNullException("emailServiceDependencies");
            _smtpClient = new SmtpClient(ConfigurationManager.AppSettings["EmailHost"]);
        }

        public IResult SendVatReceiptEmail(string recipientEmailAddress, Transaction transaction)
        {
            try
            {
                var email = _dependencies.EmailFactory.CreateVatReceiptEmail(recipientEmailAddress, transaction);

                _smtpClient.Send(email.Email);
                _dependencies.EmailLogRepository.Add(new EmailLog
                {
                    Date = DateTime.Now,
                    EmailType = email.EmailType.ToString(),
                    TransactionProcessedId = transaction.TransactionLines.FirstOrDefault().Id,
                    RecipientEmailAddress = email.Email.To.ToString(),
                    Subject = email.Email.Subject,
                    Body = email.Email.Body,
                });

                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }

        public IResult SendEReturnDeletedEmail(string eReturnNumber, string deletedBy)
        {
            try
            {
                var email = _dependencies.EmailFactory.CreateEReturnDeletedEmail(eReturnNumber, deletedBy);

                _smtpClient.Send(email.Email);

                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }

        public IResult SendNotificationEmail(TransactionNotification notification)
        {
            try
            {
                var email = _dependencies.EmailFactory.CreateNotificationEmail(notification);

                _smtpClient.Send(email.Email);

                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }

        public IResult SendDuplicateTransactionEmail(List<ProcessedTransaction> transactions)
        {
            try
            {
                var email = _dependencies.EmailFactory.SendDuplicateTransactionEmail(transactions);

                _smtpClient.Send(email.Email);

                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }

        public IResult SendPasswordResetEmail(IdentityMessage message)
        {
            try
            {
                var email = _dependencies.EmailFactory.SendPasswordResetEmail(message);

                _smtpClient.Send(email.Email);

                return new Result();
            }
            catch (Exception e)
            {
                return new Result(e.Message);
            }
        }
    }
}
