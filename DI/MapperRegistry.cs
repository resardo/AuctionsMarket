using AutoMapper;
using Domain.Mappings;
using Lamar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public class MapperRegistry : ServiceRegistry
    {
        public MapperRegistry()
        {
            MapperConfiguration config = new MapperConfiguration(c =>
            {
                c.AddProfile<GeneralProfile>();
            });

            var mapper = config.CreateMapper();
            For<IConfigurationProvider>().Use(config);
            For<IMapper>().Use(mapper);
        }
    }
}
