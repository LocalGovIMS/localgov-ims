using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.EReturnTemplate;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IEReturnTemplateService
    {
        SearchResult<Template> Search(SearchCriteria criteria);
        Template Get(int id);
        List<Template> GetAll();
        IResult Create(Template item);
        IResult Update(Template item);
    }
}