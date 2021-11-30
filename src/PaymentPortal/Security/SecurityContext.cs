﻿using BusinessLogic.Interfaces.Security;
using BusinessLogic.Security;
using System.Linq;

namespace PaymentPortal.Security
{
    public class SecurityContext : ISecurityContext
    {
        private readonly IUserStore _userStore;

        public SecurityContext(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public string UserName
        {
            get
            {
                return "0";
            }
        }

        public int UserId { get { return 0; } }

        public bool IsInRole(string role)
        {
            string[] roles = new string[]
            {
                // TODO: Some of these are only needed until the SmartPay service is moved to the business logc
                Role.TransactionList,
                Role.TransactionDetails,
                Role.TransactionCreate,
                Role.TransactionEdit,

                Role.RefundsList,
                Role.RefundsAuthorise,

                Role.NotificationCreate,
                Role.NotificationEdit,

                Role.Payments,
            };

            return roles.Contains(role);
        }

        public bool IsSuperUser
        {
            get
            {
                return true;
            }
        }

        public bool IsPublicUser
        {
            get
            {
                return true;
            }
        }

        public string[] AccessibleFundCodes
        {
            get
            {
                return _userStore.GetFundsForOrigin("basket");
            }
        }

        public string[] AccessibleTemplates
        {
            get
            {
                return new string[] { };
            }
        }

        public string OfficeCode
        {
            get
            {
                return "SP";
            }
        }
    }
}