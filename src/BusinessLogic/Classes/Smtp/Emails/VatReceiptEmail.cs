using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using BusinessLogic.Models;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace BusinessLogic.Classes.Smtp.Emails
{
    public class VatReceiptEmail : BaseEmail<VatReceiptArgs>
    {
        public override EmailTypeEnum EmailType { get { return EmailTypeEnum.VatReceipt; } }

        public VatReceiptEmail(VatReceiptArgs args) : base(args)
        {
            Email.To.Add(new MailAddress(args.RecipientEmailAddress));

            if (args.Transaction.Total < 0)
            {
                Email.Subject = string.Format("{0}Refund confirmation {1}"
                    , args.Transaction.ReceiptIssued ? "Copy " : string.Empty
                    , args.Transaction.PspReference);
            }
            else
            {
                Email.Subject = string.Format("{0}Payment confirmation {1}"
                    , args.Transaction.ReceiptIssued ? "Copy " : string.Empty
                    , args.Transaction.PspReference);
            }
            Email.IsBodyHtml = true;
            Email.Body = BuildBody();
        }

        private string BuildBody()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("<P>Hi,</p>");

            if (Args.Transaction.Total < 0)
            {
                sb.AppendFormat("<p>Your payment has been refunded on reference number: {0}</p>", Args.Transaction.PspReference);
                sb.AppendFormat("<p>The refund details are:</p>");
            }
            else
            {
                sb.AppendFormat("<p>Thanks for your payment, your reference number is: {0}</p>", Args.Transaction.PspReference);
                sb.AppendFormat("<p>Your payment details are:</p>");
            }

            if (Args.Transaction.FormattedAddress != ",")
            {
                sb.AppendFormat("Address : {0}<br />", Args.Transaction.FormattedAddress);
            }

            //For each item:
            var description = new StringBuilder();
            foreach (var line in Args.Transaction.TransactionLines)
            {
                description.AppendFormat("&nbsp; &nbsp; {0} - {1}<br>", line.Fund.FundName, line.AccountReference);
            }

            sb.AppendFormat("<div><div style=\"display:block; float:left;\">Description :</div><div style=\"display:block; float:left;\">{0}</div></div>", description.ToString());
            sb.AppendFormat("Quantity of goods : {0}<br />", Args.Transaction.TransactionLines.Count);
            sb.AppendFormat("Price : £{0} excluding VAT<br />", NumberFormatExtension.SignTrailing(Args.Transaction.Total - Args.Transaction.TotalVat.ToTwoDecimalPlaces()));

            // For each VAT rate:
            foreach (var vatRate in Args.Transaction.TransactionLines.GroupBy(x => x.VatCode, (key, g) => new { VatCode = key, Total = g.Sum(x => x.VatAmount), Percentage = g.Sum(x => x.Vat.Percentage) }))
            {
                sb.AppendFormat("VAT : £{0} at {1} %<br />", NumberFormatExtension.SignTrailing(vatRate.Total.ToTwoDecimalPlaces()), vatRate.Percentage.ToTwoDecimalPlaces());
            }

            sb.AppendFormat("Total price paid : £{0} including VAT<br />", NumberFormatExtension.SignTrailing(Args.Transaction.Total));
            sb.AppendFormat("Date : {0}<br />", Args.Transaction.EntryDate);
            sb.AppendFormat($"VAT Number : {ConfigurationManager.AppSettings["Organisation.VatNumber"]}<br />");
            sb.AppendFormat($"VAT Registered Address : {GetVatRegisteredAddress()}<br />");
            sb.AppendFormat("<p />");
            sb.AppendFormat("<p>Regards</p>");
            sb.AppendFormat($"<p>{ConfigurationManager.AppSettings["Organisation.ShortName"]}</p>");

            return sb.ToString();
        }

        private string GetVatRegisteredAddress()
        {
            var address = new StringBuilder();

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.AddressLine1"]))
                address.Append($"{ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.AddressLine1"]}, ");

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.AddressLine2"]))
                address.Append($"{ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.AddressLine2"]}, ");

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.AddressLine3"]))
                address.Append($"{ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.AddressLine3"]}, ");

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.PostCode"]))
                address.Append($"{ConfigurationManager.AppSettings["Organisation.VatRegisteredAddress.PostCode"]}, ");

            return address.ToString()
                .Trim(',')
                .Trim();
        }
    }

    public class VatReceiptArgs
    {
        public string RecipientEmailAddress { get; private set; }
        public Transaction Transaction { get; private set; }

        public VatReceiptArgs(
            string recipientEmailAddress,
            Transaction transaction)
        {
            RecipientEmailAddress = recipientEmailAddress;
            Transaction = transaction;
        }
    }
}
