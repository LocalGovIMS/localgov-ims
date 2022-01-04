using Admin.Models.SuspenseNote;
using BusinessLogic.Interfaces.Services;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;

namespace Admin.Classes.ViewModelBuilders.SuspenseNote
{
    public class ListViewModelBuilder : BaseViewModelBuilder<ListViewModel, int>
    {
        private readonly ISuspenseNoteService _suspenseNoteService;

        public ListViewModelBuilder(ILog log
            , ISuspenseNoteService suspenseNoteService
            )
            : base(log)
        {
            _suspenseNoteService = suspenseNoteService;
        }

        protected override ListViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override ListViewModel OnBuild(int id)
        {
            var result = _suspenseNoteService.GetAll(id);

            return new ListViewModel()
            {
                Items = GetSearchResultAsPagedList(result),
                Count = result.Count,
                Pages = 1,
                Page = 1,
                SuspenseId = id
            };
        }

        private StaticPagedList<BusinessLogic.Entities.SuspenseNote> GetSearchResultAsPagedList(
            List<BusinessLogic.Entities.SuspenseNote> result)
        {
            return new StaticPagedList<BusinessLogic.Entities.SuspenseNote>(
                result,
                1,
                1,
                result.Count);
        }
    }
}