using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IOfficeService
    {
        List<Office> GetAll();
        Office Get(string officeCode);
        IResult Create(Office item);
        IResult Update(Office item);
    }
}
