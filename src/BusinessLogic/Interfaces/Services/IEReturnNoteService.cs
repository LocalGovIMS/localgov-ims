using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IEReturnNoteService
    {
        List<EReturnNote> GetAll(int suspenseId);
        IResult Create(EReturnNote item);
    }
}
