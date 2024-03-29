﻿using BusinessLogic.Classes;

namespace Api.Controllers.PendingTransactions
{
    public class ProcessPaymentModel
    {
        public string AuthResult { get; set; }
        public string PspReference { get; set; }
        public string MerchantReference { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal? Fee { get; set; }
        public string CardPrefix { get; set; }
        public string CardSuffix { get; set; }

        public PaymentResult ToPaymentResult()
        {
            return new PaymentResult
            {
                MerchantReference = this.MerchantReference,
                AuthResult = this.AuthResult,
                PaymentMethod = this.PaymentMethod,
                PspReference = this.PspReference,
                AmountPaid = this.AmountPaid,
                Fee = this.Fee ?? 0,
                CardPrefix = this.CardPrefix,
                CardSuffix = this.CardSuffix
            };
        }
    }
}