using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataServices
{
    public interface IDataServices
    {
        Task<System.IO.Stream> GetImage(string connectionString, string imageName);
    }
}
