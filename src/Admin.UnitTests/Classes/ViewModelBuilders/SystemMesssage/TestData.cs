using System;
using System.Collections.Generic;

namespace Admin.UnitTests.Classes.ViewModelBuilders.SystemMessage
{
    internal static class TestData
    {
        internal static BusinessLogic.Entities.SystemMessage GetASystemMesssage()
        {
            return new BusinessLogic.Entities.SystemMessage()
            {
                Id = 1,
                EndDate = DateTime.Now,
                Message = "Test",
                Severity = "info",
                StartDate = DateTime.Now
            };
        }

        internal static List<KeyValuePair<string, string>> GetSeverities()
        {
            return new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("info", "Informational"),
                    new KeyValuePair<string, string>("warning", "Warning"),
                    new KeyValuePair<string, string>("error", "Error")
                };
        }
    }
}
