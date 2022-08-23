namespace BusinessLogic.Models
{
    public abstract class BaseSearchCriteria
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool ApplyPaging { get; set; }

        public BaseSearchCriteria()
        {
            Page = 1;
            PageSize = 20;
            ApplyPaging = true;
        }
    }
}
