using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Pagination
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Skip { get { return PageNo * PageSize; } private set { } }
    }
}
