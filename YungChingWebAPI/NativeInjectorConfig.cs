using LogicService.Interface;
using LogicService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungChingWebAPI.Helper;

namespace YungChingWebAPI {
	public static class NativeInjectorConfig {
        public static void RegisterServices(this IServiceCollection services) {

            //註冊Jwt
            services.AddSingleton<JwtHelper>();
            //TODO
            //services.AddTransient<Func<int, IAnimalService>>(serviceProvider => key =>
            //{
            //    switch (key)
            //    {
            //        case (int)OvertimeLeaveRoleType.Default:
            //            return serviceProvider.GetService<AnimalService>();
            //        case (int)OvertimeLeaveRoleType.Off:
            //            return serviceProvider.GetService<AnimalTypeService>();
            //        default:
            //            throw new KeyNotFoundException();
            //    }

            //});

            ////進行註冊
            services.AddScoped<IUserService, UserService>();
        }
    }
}
