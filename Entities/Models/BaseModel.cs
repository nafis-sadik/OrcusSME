using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class BaseModel
    {
        private int _page;
        private int _pageSize;
        public int Page
        {
            get => _page;
            set
            {
                _page = value <= 0 ? 1 : value;
            }
        }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value <= 0 ? 5 : value;
            }
        }
        public string SearchString { get; set; }
        public int Skip => _pageSize * (_page - 1);
        public string UserId { get; set; }
    }
}
