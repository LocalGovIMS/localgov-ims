using BusinessLogic.Classes.Caching;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Interfaces.Security;
using System.Linq;

namespace BusinessLogic.Security
{
    public class UserStore : IUserStore
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFundGroupFundRepository _fundGroupFundRepository;
        private readonly IUserTemplateRepository _userTemplateRepository;
        private readonly IFundRepository _fundRepository;

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

        public User GetUser(string username)
        {
            var cacheKey = $"{nameof(UserStore)}::{nameof(GetUser)}::{username}";

            var output = MemoryCache.GetCachedData(
                cacheKey,
                () =>
                {
                    return _userRepository.GetUser(username);
                }, 10);

            return output;
        }

        public string[] GetUserRoles(string username)
        {
            return _userRoleRepository.GetByUsername(username, false).ToArray();
        }

        public string[] GetUserFunds(string username)
        {
            try
            {
                var cacheKey = $"{nameof(UserStore)}::{nameof(GetUserFunds)}::{username}";

                var output = MemoryCache.GetCachedData(
                    cacheKey,
                    () =>
                    {
                        return _userRepository.GetUserAccessibleFunds(username).ToArray();
                    });

                return output;
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
                    var cacheKey = $"{nameof(UserStore)}::{nameof(GetFundsForOrigin)}::basket";

                    var output = MemoryCache.GetCachedData(
                        cacheKey,
                        () =>
                        {
                            return _fundRepository.GetAll(true)
                                .Where(x => x.IsABasketFund())
                                .Select(x => x.FundCode)
                                .ToArray();
                        });

                    return output;
                }

                return new string[] { };
            }
            catch
            {
                return new string[] { };
            }
        }

        public int[] GetUserTemplates(string username)
        {
            var cacheKey = $"{nameof(UserStore)}::{nameof(GetUserTemplates)}::{username}";

            var output = MemoryCache.GetCachedData(
                cacheKey,
                () =>
                {
                    return _userTemplateRepository.GetByUsername(username).ToArray();
                });

            return output;
        }
    }
}
