using MatchManager.Core.Services.Account;
using MatchManager.Core.Services.Account.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Core.Extensions
{
    public static class CoreExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            //Service
            services.AddTransient<IAccountServiceAsync, AccountServiceAsync>();

            //Repository
            //services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();
            return services;
        }
    }
}
