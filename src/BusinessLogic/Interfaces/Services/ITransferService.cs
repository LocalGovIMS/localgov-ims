using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ITransferService
    {
        IResult Transfer(List<TransferItem> transferItems, TransferItem sourceItem);
        IResult Transfer(List<TransferItem> transferItems, List<TransferItem> sourceItems);
    }
}
