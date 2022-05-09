using BusinessLogic.Entities;
using BusinessLogic.Models.CheckDigitConfiguration;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ICheckDigitConfigurationRepository : IRepository<CheckDigitConfiguration>
    {
        IEnumerable<CheckDigitConfiguration> Search(SearchCriteria criteria, out int resultCount);

        void Update(CheckDigitConfiguration entity);
    }
}