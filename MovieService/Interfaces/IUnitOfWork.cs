using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
