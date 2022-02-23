using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CommonServices.Abstraction
{
    public interface IFileService
    {
        public int[] SaveFiles(string [] FileNames, string [] FilePaths);
    }
}
