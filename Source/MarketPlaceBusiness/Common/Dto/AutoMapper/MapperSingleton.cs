using AutoMapper;

namespace MarketPlaceBusiness.Common.Dto.AutoMapper
{
	public class MapperSingleton
	{
		private static IMapper _mapper;


		private MapperSingleton()
		{

		}


		public static IMapper Get()
		{
			if (_mapper != null)
				return _mapper;

			var config = new MapperConfiguration(cfg => {
				cfg.AddProfile<MappingProfile>();
			});

			_mapper = new Mapper(config);

			return _mapper;
		}
	}
}
