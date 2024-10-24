using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repositories.Repository;
using E_Learning.Repository.IReposatories;
using E_Learning.Services.IService;
using E_Learning.Areas.Course.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Learning.Services.Service
{
    public class AdminService:IAdminService
    {
        #region Injections and fields
        private readonly IUserRepository userRepository;
        private readonly ICourseService courseService;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IInstructorWithdrawRepository instructorWithdraw;
        private readonly UserManager<User> userManager;
        public AdminService(UserManager<User> userManager, IUserRepository userRepository,
            ICourseService courseService, IEnrollmentRepository enrollmentRepository , IInstructorWithdrawRepository instructorWithdraw)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.courseService = courseService;
            this.enrollmentRepository = enrollmentRepository;
            this.instructorWithdraw = instructorWithdraw;
        }

        #endregion

        #region Admins(show, add , delete)
        public async Task<List<User>> ShowAdmins()
        {
            var admins = await userManager.GetUsersInRoleAsync("Admin");
            return (List<User>)admins;
        }
        public async Task<ProcessResult> AddAdmin(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                return new ProcessResult { IsSucceded = true };
            }
            return new ProcessResult();
        }
        public async Task<ProcessResult> DeleteAdmin(string AdminId)
        {
            var user = await userManager.FindByIdAsync(AdminId);
            if (user != null)
            {
                await userManager.RemoveFromRoleAsync(user, "Admin");
                return new ProcessResult { IsSucceded = true };
            }
            return new ProcessResult();
        }
        #endregion

        #region Courses (Admit , Reject , BestSeller , top rated)
        //public async Task<List<CourseView>> ShowPendingCourses()
        //{
        //    var courses = await courseService.GetPendingCourses();
        //    return courses;
        //}
        // public async Task AdmitCourse(string CourseId)
        //{
        //    await courseService.UpdateCourseStatus(CourseId, "Admitted");
        //}
        //public async Task RejectCourse(string CourseId)
        //{
        //    await courseService.UpdateCourseStatus(CourseId, "Rejected");
        //}
        //public async Task<List<CourseView>> ShowCourseBestSeller()
        //{
        //    return await courseService.GetBestSeller();
        //}
        //public async Task<List<CourseView>> GetTopRatedCourses()
        //{
        //    return await courseService.GetTopRatedCourses();
        //}
        #endregion

        #region Tracking Enrollments
        public async Task<IEnumerable<Enrollment>> TrackUserEnrollment()
        {
            var enrollments = await enrollmentRepository.GetAllAsync();
            return enrollments;
        }
        public async Task<IEnumerable<Enrollment>> TrackCourseEnrollments(string CourseId)
        {
            var enrollments = await enrollmentRepository.GetEnrollmentsByCourseIdAsync(CourseId);
            return enrollments;
        }
        public async Task<IEnumerable<Enrollment>> TrackUserEnrollments(string UserId)
        {
            var enrollments = await enrollmentRepository.GetEnrollmentsByUserIdAsync(UserId);
            return enrollments;
        }
        #endregion

        #region instructor withdraws
        public async Task<IEnumerable<InstructorWithdraw>> ShowInstructorsWithdraws()
        {
            var withdraws = await instructorWithdraw.GetAllAsync();
            return withdraws;
        }
        public async Task<IEnumerable<InstructorWithdraw>> TrackInstructorWithdrawHistory(string instructorId)
        {
            var withdraws = await instructorWithdraw.GetWithdrawalsByUserIdAsync(instructorId);
            return withdraws;
        }
        #endregion
    }
}
