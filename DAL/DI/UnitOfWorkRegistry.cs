using DAL.UoW;
using Lamar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DI
{
    public class UnitOfWorkRegistry : ServiceRegistry
    {
        public UnitOfWorkRegistry()
        {
            For<IUnitOfWork>().Use<UnitOfWork>();
        }
    }
}
