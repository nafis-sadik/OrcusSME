using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OutletModel: BaseModel
    {
        public string UserId { get; set; }
        public int OutletId { get; set; }
        public string OutletName { get; set; }
        public string OutletAddresss { get; set; }
    }
}
