using E_Learning.Services.IService;
using E_Learning.Services.Service;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace E_Learning.Configuraion
{
    public static class ConfigureServices
    {

        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IInstructorService, InstructorServise>();
            services.AddSingleton<IEmailSender>(new EmailService());
        }
    }
}
