﻿using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IEmailService
    {
        IResult SendVatReceiptEmail(string recipientEmailAddress, Transaction pspReference);
        IResult SendEReturnDeletedEmail(string eReturnNumber, string deletedBy);
        IResult SendNotificationEmail(TransactionNotification notification);
        IResult SendDuplicateTransactionEmail(List<ProcessedTransaction> transactions);
        IResult SendPasswordResetEmail(IdentityMessage message);
    }
}
