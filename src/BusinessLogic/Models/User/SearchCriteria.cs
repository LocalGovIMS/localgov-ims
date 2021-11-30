namespace BusinessLogic.Models.User
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}