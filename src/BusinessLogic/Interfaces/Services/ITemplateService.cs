using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ITemplateService
    {
        List<Template> GetAllTemplates();
        Template GetTemplate(int id);
        IResult Create(Template item);
        IResult Update(Template item);
    }
}
