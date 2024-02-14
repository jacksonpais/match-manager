using MatchManager.Core.Services.Account;
using MatchManager.Core.Services.Account.Interface;
using MatchManager.Core.Services.Token;
using MatchManager.Core.Services.Token.Interface;
using MatchManager.Infrastructure.Repositories.Account;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using MatchManager.Services.Email;
using MatchManager.Services.Email.Interface;
using MatchManager.Services.SecurityService;
using MatchManager.Services.SecurityService.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace MatchManager.Core.Extensions
{
    public static class CoreExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            //Service
            services.AddTransient<IAccountServiceAsync, AccountServiceAsync>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ISecureService, SecureService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddDataProtection();

            //Repository
            services.AddTransient<IAccountRepositoryAsync, AccountRepositoryAsync>();
            return services;
        }
    }
}
