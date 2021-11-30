using BusinessLogic.Classes;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IRefundService
    {
        RefundStatus RefundTransaction(RefundRequest refundRequest, string reason);
        RefundStatus RefundTransactions(List<RefundRequest> refundRequests, string reason);
    }
}
