using System;

namespace Admin.Models.Home
{
    public class DiagnosticsViewModel
    {
        public string WebUserName { get; set; }
        public string[] WebClaims { get; set; }
        public string WcfUserName { get; set; }
        public string[] WcfClaims { get; set; }

        public DateTime TestDate { get; set; }
    }
}