using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserPostPaymentMopCodeRepository : Repository<UserPostPaymentMopCode>, IUserPostPaymentMopCodeRepository
    {
        public UserPostPaymentMopCodeRepository(IncomeDbContext context) : base(context)
        {
        }

        public List<UserPostPaymentMopCode> GetUserPostPaymentMopCodes(int id)
        {
            var results = IncomeDbContext.ImsUserPostPaymentMopCodes
                .AsQueryable()
                .Include(x => x.Mop)
                .Where(x => x.UserId == id)
                .ApplyFilters(Filters)
                .ToList();

            return results;
        }
    }
}