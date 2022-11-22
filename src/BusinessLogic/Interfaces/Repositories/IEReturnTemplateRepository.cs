using BusinessLogic.Entities;
using BusinessLogic.Models.EReturnTemplate;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IEReturnTemplateRepository : IRepository<Template>
    {
        IEnumerable<Template> Search(SearchCriteria criteria, out int resultCount);
        Template Get(int id);
        void Update(Template entity);
    }
}