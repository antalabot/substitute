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

        public ushort Page { get; set; }
        public ushort PerPage { get; set; }
        public ESortDirection SortDirection { get; set; }
    }
}
