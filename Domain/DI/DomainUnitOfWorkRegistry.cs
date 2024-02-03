using Domain.UoW;
using Lamar;

namespace Domain.DI
{
    public class DomainUnitOfWorkRegistry : ServiceRegistry
    {
        public DomainUnitOfWorkRegistry()
        {
            For<IDomainUnitOfWork>().Use<DomainUnitOfWork>();
        }
    }
}
