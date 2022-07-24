using AutoMapper;
using RepositoryModel.Entity;
using RequestResponseModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YungChingWebAPI {
    public class MapperConfig {
        public static void Initialize() {

            Mapper.Initialize(cfg => { 
                cfg.CreateMap<User, UserResponse>();
            });
        }
    }
}
