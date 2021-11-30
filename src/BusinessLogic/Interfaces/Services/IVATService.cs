using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IVatService
    {
        Vat GetByVatCode(string vatCode);
        Vat GetByFundCode(string fundCode);
        List<Vat> GetAllCodes();
        List<Vat> GetAllCodes(bool includeDisabled);
        IResult Create(Vat item);
        IResult Update(Vat item);
    }
}