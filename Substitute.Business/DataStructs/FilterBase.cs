using Substitute.Business.DataStructs.Enum;

namespace Substitute.Business.DataStructs
{
    public abstract class FilterBase
    {
        public FilterBase()
        {
            Page = 1;
            PerPage = 25;
            SortDirection = ESortDirection.Ascending;
        }

        public int Page { get; set; }
        public int PerPage { get; set; }
        public ESortDirection SortDirection { get; set; }
    }
}
