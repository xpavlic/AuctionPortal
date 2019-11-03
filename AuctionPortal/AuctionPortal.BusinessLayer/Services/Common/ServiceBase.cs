using AutoMapper;

namespace AuctionPortal.BusinessLayer.Services.Common
{
	public abstract class ServiceBase
	{
		protected readonly IMapper Mapper;

		protected ServiceBase(IMapper mapper)
		{
			this.Mapper = mapper;
		}
	}
}
