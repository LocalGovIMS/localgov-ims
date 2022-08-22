using Admin.Models.EReturnNote;
using BusinessLogic.Interfaces.Services;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;

namespace Admin.Classes.ViewModelBuilders.EReturnNote
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, int>
    {
        private readonly IEReturnNoteService _eReturnNoteService;

        public ListViewModelBuilder(ILog log
            , IEReturnNoteService eReturnNoteService
            )
            : base(log)
        {
            _eReturnNoteService = eReturnNoteService;
        }

        protected override ListViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override ListViewModel OnBuild(int id)
        {
            var result = _eReturnNoteService.GetAll(id);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(result),
                Count = result.Count,
                Pages = 1,
                Page = 1,
                EReturnId = id
            };
        }

        private StaticPagedList<BusinessLogic.Entities.EReturnNote> GetSearchResultAsPagedList(
            List<BusinessLogic.Entities.EReturnNote> result)
        {
            return new StaticPagedList<BusinessLogic.Entities.EReturnNote>(
                result,
                1,
                1,
                result.Count);
        }
    }
}