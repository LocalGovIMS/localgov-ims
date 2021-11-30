using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Validators
{
    public interface ITransactionTransferValidator
    {
        IResult Validate(TransferItem sourceItem, IList<TransferItem> transferItems);
        IResult Validate(IList<TransferItem> sourceItems, IList<TransferItem> transferItems);
    }
}
