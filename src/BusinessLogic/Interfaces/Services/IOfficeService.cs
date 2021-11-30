using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IOfficeService
    {
        List<Office> GetOffices();
    }
}
