using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AutoMapper
{
    public class MapperConfig
    {
        public static Mapper InitializeAutoMapper(params Profile[] profs)
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profs)
                {
                    cfg.AddProfile(profile);
                }
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
