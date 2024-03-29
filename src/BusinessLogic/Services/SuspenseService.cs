using BusinessLogic.Classes.Result;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.Suspense;
using BusinessLogic.Suspense.JournalAllocation;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BusinessLogic.Services
{
    public class SuspenseService : BaseService, ISuspenseService
    {
        private readonly IJournalAllocationStrategy _journalAllocationStrategy;

        public SuspenseService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , IJournalAllocationStrategy journalAllocationStrategy)
            : base(logger, unitOfWork, securityContext)
        {
            _journalAllocationStrategy = journalAllocationStrategy;
        }

        public IResult Create(Entities.Suspense suspense)
        {
            try
            {
                UnitOfWork.Suspenses.Add(suspense);
                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result() { Data = suspense };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to save Suspense");
            }
        }

        public IResult Create(CreateSuspenseArgs args)
        {
            try
            {
                UnitOfWork.Suspenses.Add(args.Suspense);

                if (args.SaveChanges)
                {
                    UnitOfWork.Complete(SecurityContext.UserId);
                }

                return new Result() { Data = args.Suspense };
            }
            catch (DbUpdateException ex)
            {
                Logger.Warn(null, ex);

                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(null, ex);

                return new Result(ex.Message);
            }
        }

        public SearchResult<SuspenseWrapper> Search(SearchCriteria criteria)
        {
            try
            {
                var results = UnitOfWork.Suspenses.Search(criteria.TrimStringProperties(), out int eReturnsCount)
                    .Select(x => new SuspenseWrapper(x))
                    .ToList();

                return new SearchResult<SuspenseWrapper>()
                {
                    Count = eReturnsCount,
                    Items = results,
                    Page = criteria.Page,
                    PageSize = criteria.PageSize
                };
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public SuspenseWrapper GetSuspense(int id)
        {
            try
            {
                return new SuspenseWrapper(UnitOfWork.Suspenses.GetSuspense(id));
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return null;
            }
        }

        public IResult Journal(List<int> suspenseItems, List<Journal> journalItems, List<CreditNote> creditNotes)
        {
            return _journalAllocationStrategy.Execute(suspenseItems, journalItems, creditNotes);
        }

        public IResult SaveNotes(int id, string notes)
        {
            if (!SecurityContext.IsInRole(Security.Role.Finance))
                return new Result("You do not have permission to update the notes for this suspense item");

            try
            {
                var suspense = GetSuspense(id);

                if (suspense == null || suspense.Item == null)
                    throw new NullReferenceException("Unable to load suspense record");

                suspense.Item.Notes = notes;

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("Unable to update the suspense notes");
            }
        }
    }

    public class CreateSuspenseArgs
    {
        public Entities.Suspense Suspense { get; set; }
        public bool SaveChanges { get; set; } = true;
    }
}