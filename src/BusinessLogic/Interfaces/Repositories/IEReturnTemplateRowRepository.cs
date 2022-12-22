using BusinessLogic.Entities;
using BusinessLogic.Models.EReturnTemplateRow;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IEReturnTemplateRowRepository : IRepository<TemplateRow>
    {
        IEnumerable<TemplateRow> Search(SearchCriteria criteria, out int resultCount);
        void Update(TemplateRow entity);
    }
}