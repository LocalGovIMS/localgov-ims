using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.EReturnTemplateRow;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface IEReturnTemplateRowService
    {
        SearchResult<TemplateRow> Search(SearchCriteria criteria);
        TemplateRow Get(int id);
        IResult Create(TemplateRow item);
        IResult Update(TemplateRow item);
        IResult Delete(int id);
    }
}