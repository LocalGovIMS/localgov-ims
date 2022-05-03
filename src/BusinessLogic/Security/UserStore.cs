using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Security;
using System.Linq;

namespace BusinessLogic.Security
{
    public class UserStore : IUserStore
    {
        private IUserRoleRepository _userRoleRepository;
        private IUserRepository _userRepository;
        private IFundGroupFundRepository _fundGroupFundRepository;
        private IUserTemplateRepository _userTemplateRepository;
        private IFundRepository _fundRepository;

        public UserStore(IUserRoleRepository userRoleRepository
            , IUserRepository userRepository
            , IFundGroupFundRepository fundGroupFundRepository
            , IUserTemplateRepository userTemplateRepository
            , IFundRepository fundRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _fundGroupFundRepository = fundGroupFundRepository;
            _userTemplateRepository = userTemplateRepository;
            _fundRepository = fundRepository;
        }

        public User GetUser(string userName)
        {
            return _userRepository.GetUser(userName);
        }

        public string[] GetUserRoles(string userName)
        {
            return _userRoleRepository.GetByUsername(userName, false).ToArray();
        }

        public string[] GetUserFunds(string userName)
        {
            try
            {
                var userId = _userRepository.GetUser(userName).UserId;
                var userAccessibleFunds = _fundGroupFundRepository.GetAllExtended()
                    .Where(x => x.FundGroup.UserFundGroups.Any(y => y.UserId == userId))
                    .Select(f => f.Fund.FundCode);

                return userAccessibleFunds.ToArray();
            }
            catch
            {
                return new string[] { };
            }
        }

        public string[] GetFundsForOrigin(string origin)
        {
            try
            {
                if (origin.Equals("basket"))
                {
                    return _fundRepository.GetAll(true)
                        .Where(x => x.IsABasketFund())
                        .Select(x => x.FundCode)
                        .ToArray();
                }

                return new string[] { };
            }
            catch
            {
                return new string[] { };
            }
        }

        public string[] GetUserTemplates(string userName)
        {
            return _userTemplateRepository.GetByUsername(userName).ToArray();
        }
    }
}
