using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EReturnChequeRepository : Repository<EReturnCheque>, IEReturnChequeRepository
    {
        public EReturnChequeRepository(IncomeDbContext context) : base(context)
        {
        }

        public void Update(List<EReturnCheque> items, int eReturnId)
        {
            var existingItems = IncomeDbContext.EReturnCheques
                .AsQueryable()
                .Where(x => x.EReturnId == eReturnId)
                .ToList();

            var toUpdate = from a in existingItems
                           join b in items
                           on a.Id equals b.Id
                           select a;

            #region Delete

            var toRemove = existingItems.Except(toUpdate);

            foreach (var item in toRemove)
            {
                IncomeDbContext.EReturnCheques.Remove(item);
            }

            #endregion

            #region Add

            var toAdd = items.Except(toUpdate);

            foreach (var item in toAdd)
            {
                item.EReturnId = eReturnId;
                item.CreatedAt = DateTime.Now;
                IncomeDbContext.EReturnCheques.Add(item);
            }

            #endregion

            #region Update

            foreach (var item in toUpdate)
            {
                var itemToUpdate = existingItems.FirstOrDefault(x => x.Id == item.Id);
                if (itemToUpdate == null) continue;

                itemToUpdate.Amount = item.Amount;
                itemToUpdate.Name = item.Name;
                itemToUpdate.ItemNo = item.ItemNo; // TODO: How is this managed?

                IncomeDbContext.EReturnCheques.Attach(itemToUpdate);
            }

            #endregion
        }
    }
}