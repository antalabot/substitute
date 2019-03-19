using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.DataStructs
{
    public abstract class ResultBase : FilterBase
    {
        public ResultBase(FilterBase filter)
        {
            Page = filter.Page;
            PerPage = filter.PerPage;
            SortDirection = filter.SortDirection;
        }

        public int Pages => Convert.ToInt32(Math.Ceiling((double)Total / PerPage));

        public int Total { get; set; }
    }
}
