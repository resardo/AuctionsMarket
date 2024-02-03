using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UoW
{
    public interface IDomainUnitOfWork
    {
        TDomain GetDomain<TDomain>() where TDomain : class;
    }
}
