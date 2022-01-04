using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface ISuspenseNoteService
    {
        List<SuspenseNote> GetAll(int suspenseId);
        IResult Create(SuspenseNote item);
    }
}
