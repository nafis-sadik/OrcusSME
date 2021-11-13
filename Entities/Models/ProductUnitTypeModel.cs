using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ProductUnitTypeModel : BaseModel
    {
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
    }
}
