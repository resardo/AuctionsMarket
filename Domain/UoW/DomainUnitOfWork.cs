using AutoMapper;
using System;
using System.Collections.Generic;
using Lamar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UoW
{
    public class DomainUnitOfWork : IDomainUnitOfWork
    {
        private readonly IContainer _container;
        private readonly IMapper _mapper;

        public DomainUnitOfWork(IContainer container, IMapper mapper)
        {
            _container = container;
            _mapper = mapper;
        }
        public TDomain GetDomain<TDomain>() where TDomain : class
        {
            return _container.GetInstance<TDomain>();
        }
    }
}
