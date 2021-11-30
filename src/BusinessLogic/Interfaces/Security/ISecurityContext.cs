﻿namespace BusinessLogic.Interfaces.Security
{
    public interface ISecurityContext
    {
        string UserName { get; }

        int UserId { get; }

        bool IsInRole(string role);

        // I've just added this so that checking this is a little cleaner - we could call IsInRole I suppose...
        bool IsSuperUser { get; }
        bool IsPublicUser { get; }

        string[] AccessibleFundCodes { get; }
        string[] AccessibleTemplates { get; }

        string OfficeCode { get; }
    }
}
