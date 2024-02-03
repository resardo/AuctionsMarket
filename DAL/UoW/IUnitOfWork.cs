using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>() where TRepository : class;
        int Save();
    }
}
