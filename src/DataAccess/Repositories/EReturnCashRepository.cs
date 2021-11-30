using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;
using System;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EReturnCashRepository : Repository<EReturnCash>, IEReturnCashRepository
    {
        public EReturnCashRepository(IncomeDbContext context) : base(context)
        {
        }

        public void Update(EReturnCash item, int eReturnId)
        {
            var cash = IncomeDbContext.EReturnCashes
                .AsQueryable()
                .FirstOrDefault(x => x.EReturnId == eReturnId);

            if (cash == null)
            {
                item.CreatedAt = DateTime.Now;
                IncomeDbContext.EReturnCashes.Add(item);
            }
            else
            {
                cash.BagNumber = item.BagNumber;
                cash.Pounds50 = item.Pounds50;
                cash.Pounds20 = item.Pounds20;
                cash.Pounds10 = item.Pounds10;
                cash.Pounds5 = item.Pounds5;
                cash.Pounds2 = item.Pounds2;
                cash.Pounds1 = item.Pounds1;
                cash.Pence50 = item.Pence50;
                cash.Pence20 = item.Pence20;
                cash.Pence10 = item.Pence10;
                cash.Pence5 = item.Pence5;
                cash.Pence2 = item.Pence2;
                cash.Pence1 = item.Pence1;
                cash.Total = item.Total;
            }
        }
    }
}