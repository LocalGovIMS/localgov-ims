using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Linq;

namespace DataAccess.Repositories
{
    public class TemplateRowRepository : Repository<TemplateRow>, ITemplateRowRepository
    {
        public TemplateRowRepository(IncomeDbContext context) : base(context)
        {
        }

        public TemplateRow GetTemplateRow(int id)
        {
            var item = IncomeDbContext.TemplateRows
                .AsQueryable()
                .FirstOrDefault(x => x.Id == id);

            return item;
        }

        public void Update(Template template)
        {
            var templateRows = IncomeDbContext.TemplateRows
                .Where(x => x.TemplateId == template.Id)
                .ApplyFilters(Filters)
                .ToList();

            // If we don't have any - delete anything that exists.
            if (template.TemplateRows == null)
            {
                foreach (var item in templateRows)
                {
                    IncomeDbContext.TemplateRows.Remove(item);
                }
            }
            else
            {
                #region Delete TemplateRows

                var toKeep = from a in templateRows
                             join b in template.TemplateRows
                             on new { a.Id } equals new { b.Id }
                             select a;

                var toRemove = templateRows.Except(toKeep);

                foreach (var role in toRemove)
                {
                    IncomeDbContext.TemplateRows.Remove(role);
                }


                #endregion

                #region Add TemplateRows

                var alreadyExist = from a in template.TemplateRows
                                   join b in templateRows
                                   on new { a.Id } equals new { b.Id }
                                   select a;

                var toAdd = template.TemplateRows.Except(alreadyExist);

                foreach (var item in toAdd)
                {
                    item.TemplateId = template.Id;
                    IncomeDbContext.TemplateRows.Add(item);
                }

                foreach (var item in alreadyExist)
                {
                    var row = item;
                    var existingRow = templateRows.FirstOrDefault(x => x.Id == row.Id) ?? row;

                    existingRow.Description = row.Description;
                    existingRow.Reference = row.Reference;
                    existingRow.VatCode = row.VatCode;
                    existingRow.ReferenceOverride = row.ReferenceOverride;
                    existingRow.DescriptionOverride = row.DescriptionOverride;
                    existingRow.VatOverride = row.VatOverride;
                }
            }
            #endregion
        }
    }
}
