using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.CheckDigitConfiguration;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ICheckDigitConfigurationService
    {
        IResult Create(CheckDigitConfiguration item);
        SearchResult<CheckDigitConfiguration> Search(SearchCriteria criteria);
        List<CheckDigitConfiguration> GetAll();
        CheckDigitConfiguration Get(int id);
        IResult Update(CheckDigitConfiguration item);
        IResult Delete(int id);
    }
}