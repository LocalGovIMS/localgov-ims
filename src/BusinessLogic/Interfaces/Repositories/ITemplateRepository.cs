using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Template GetTemplate(int id);
        void Update(Template entity);
    }
}
