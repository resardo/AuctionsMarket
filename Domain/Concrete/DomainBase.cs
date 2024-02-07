using AutoMapper;
using DAL.UoW;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class DomainBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
       

        public DomainBase(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            
        }
    }
}
