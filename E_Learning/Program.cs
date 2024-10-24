using E_Learning.Models;
using E_Learning.Repository.IReposatories;
using E_Learning.Repositories.Repository;

using E_Learning.Services.IService;
using E_Learning.Services.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using E_Learning.Areas.Home.Data;
using E_Learning.Areas.Course.Data.Repositories;
using E_Learning.Areas.Course.Data.Services;
using E_Learning.Repositories.IReposatories;
using E_Learning.Areas.Search.Data;
using E_Learning.Areas.Payment.Models;
using E_Learning.Helper;
using E_Learning.Areas.Admin.Data.Services;
using E_Learning.Areas.Student.Data.Services;
//using PayPal.Api;

namespace E_Learning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider().AddRazorRuntimeCompilation();
            // Customer Services
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("local"));
            });

            builder.Services.AddSingleton<IEmailSender>(new EmailService());

            #region Inject Data
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseCardService, CourseCardService>();
            builder.Services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
            builder.Services.AddScoped<ICourseViewRepository, CourseViewRepository>();
            builder.Services.AddScoped<IDataForInstructorRepository, DataForInstructorRepository>();
            builder.Services.AddScoped<IUserDataShortcutService, UserDataShortCutService>();
            builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICourseSectionRepository, CourseSectionRepository>();
            builder.Services.AddScoped<ICourseFullDataViewModelService, CourseFullDataViewModelService>();
            builder.Services.AddScoped<ICourseDiscountRepository, CourseDiscountRepository>();
            builder.Services.AddScoped<ICourseSearchRepository, CourseSearchRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IViewRenderService,ViewRenderService>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            builder.Services.AddScoped<ICourseLevelRepository, CourseLevelRepository>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
            builder.Services.AddScoped<IStatusRepository, StatusRepository>();
            builder.Services.AddScoped<ICourseListServices, CourseListServices>();
            builder.Services.AddScoped<ISectionLessonRepository, SectionLessonRepository>();
            builder.Services.AddScoped<ICoursePreviewRepository , CoursePreviewRepository>();
            builder.Services.AddScoped<ILearningService ,LearningService>();
            builder.Services.AddScoped<IStudentCourseProgressRepository, StudentCourseProgressRepository>();
            #endregion

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("InstructorOrAdmin", policy => policy.RequireRole("Instructor", "Admin"));
            });

            builder.Services.AddIdentity<User, IdentityRole>(op =>
            {
                op.Password.RequireDigit = true;
                op.Password.RequiredLength = 8;
                op.Password.RequireUppercase = true;
                op.Password.RequireLowercase = true;
                op.Password.RequireNonAlphanumeric = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddSession(op =>
            {
                op.IOTimeout = TimeSpan.FromMinutes(5);
            });

            builder.Services.AddScoped<UserManager<User>>();

            //builder.Services.AddIdentity<User, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>()
            //                     .AddDefaultTokenProviders();

            #region Payment

            builder.Services.AddSingleton(x =>
                new PaypalClient(
                    builder.Configuration["PayPalOptions:ClientId"],
                    builder.Configuration["PayPalOptions:ClientSecret"],
                    builder.Configuration["PayPalOptions:Mode"]
                )
            );
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.MapControllerRoute(
                  name: "area",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				  name: "areas",
				  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);
			});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Home",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Home}/{controller=HomeArea}/{action=HomeIndex}");



            app.Run();
        }
    }
}
