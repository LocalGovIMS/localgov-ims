using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IEReturnTypeService
    {
        List<EReturnType> GetAllEReturnTypes();
    }
}