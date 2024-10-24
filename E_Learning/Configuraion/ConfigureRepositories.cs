using E_Learning.Repositories.IReposatories;
using E_Learning.Repositories.Repository;
using E_Learning.Repository.IReposatories;
namespace E_Learning.Configuraion
{
    public static class ConfigureRepositories
    {

        public static void RegisterRepositories(this IServiceCollection services)
        {
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<ICourseCuponsRepository, CourseCuponsRepository>();
            services.AddScoped<ICourseDiscountRepository, CourseDiscountRepository>();
            services.AddScoped<ICoursePreviewRepository, CoursePreviewRepository>();
            services.AddScoped<ICourseRepository,CourseRepository>();
            services.AddScoped<ICourseSectionRepository,CourseSectionRepository>();
            services.AddScoped<ICourseViewModelRepository, CourseVmRepository>();
            services.AddScoped<IInstructorstatsVMRepository, InstructorVMRepository>();
            services.AddScoped<IDataForInstructor, DatForInstructorRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IInstructorWithdrawRepository, InstructorWithdrawRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IQuizQuestionRepository,QuizQuestionRepository>();
            services.AddScoped<IReviewRepository,ReviewRepository>();
            services.AddScoped<ISectionLessonRepository,SectionLessonRepository>();
            services.AddScoped<ISectionQuizRepository, SectionQuizRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
