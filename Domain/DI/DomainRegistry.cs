using DAL.DI;
using Domain.Concrete;
using Domain.Contracts;
using Lamar;
using Microsoft.AspNetCore.Http;


namespace Domain.DI
{
    public class DomainRegistry : ServiceRegistry
    {
        public DomainRegistry()
        {
            IncludeRegistry<DomainUnitOfWorkRegistry>();

            For<IUserDomain>().Use<UserDomain>();
            For<IAuctionDomain>().Use<AuctionDomain>();
            For<IBidDomain>().Use<BidDomain>();
            For<ILoginDomain>().Use<LoginDomain>();
            For<IRoleDomain>().Use<RoleDomain>();

            AddRepositoryRegistries();
            AddHttpContextRegistries();
        }

        private void AddRepositoryRegistries()
        {
            IncludeRegistry<RepositoryRegistry>();
        }

        private void AddHttpContextRegistries()
        {
            For<IHttpContextAccessor>().Use<HttpContextAccessor>();
        }
    }
}
