using E_Learning.Areas.Course.Models;
using E_Learning.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace E_Learning.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseView>()
                .ToView("CourseviewModel");
            //builder.Entity<InstructorStatisticsVM>()
            //    .ToView("InstructorStats");
            builder.Entity<CourseReviewView>()
                .HasNoKey()
                .ToView("CourseReviewViewModel");
            base.OnModelCreating(builder);
            /////////////////////////////////////////

            builder.Entity<UserAccount>()
                .HasKey(ua => new {ua.UserID , ua.SocialMediaID});

            /////////////////////////////////////////
            builder.Entity<Cart>().HasKey(
                c=> new {UserId=c.UserId ,CourseId = c.CourseId}
                );
            ///////////////////////////////
            ///  
            
            base.OnModelCreating(builder);

            builder.Entity<ContactUs>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

            });


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
       // public DbSet<Category> Categories { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CoursePreview> CoursePreviews { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<CourseDiscount> CourseDiscounts { get; set; }
        public DbSet<CourseCupons> CourseCupons { get; set; }
        public DbSet<SectionLessons> SectionLessons { get; set; }
        public DbSet<SectionQuiz> SectionQuizzes { get; set; }
        public DbSet<QuizQuestion> quizQuestions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<InstructorWithdraw> InstructorWithdraws { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SocialMedia> socialMedias { get; set; }
        public DbSet<UserAccount> userAccounts { get; set; }
        public DbSet<DataForInstructor> DataForInstructors { get; set; }
       // public DbSet<SuperCategory> SuperCategories { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StudentCourseProgress> Progresses { get; set; }
        //Views
        //public DbSet<DataForInstructor> AdditionalUserData { get; set; }
        //public DbSet<InstructorStatisticsVM> InstructorStatistics { get; set; }
        public DbSet<CourseView> CourseViewModels { get; set; }
        public DbSet<CourseReviewView> courseReviewViewModels { get; set; }
       
    }
}
