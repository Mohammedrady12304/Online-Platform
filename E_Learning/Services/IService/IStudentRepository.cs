namespace E_Learning.Services.IService
{
    public interface IStudentRepository
    {
        Task AddToWhichlist(string courseId, string UserId);
        Task RemoveFromWhichlist(string courseId, string UserId);
        // To be completed
    }
}
