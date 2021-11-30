using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITemplateRowRepository : IRepository<TemplateRow>
    {
        TemplateRow GetTemplateRow(int id);
        void Update(Template template);
    }
}
