using E_Learning.Areas.Course.Models;
using E_Learning.Repositories.IReposatories;
using Microsoft.AspNetCore.Identity;

namespace E_Learning.Areas.Course.Data.Services
{
	public class UserDataShortCutService : IUserDataShortcutService
	{

		private readonly IUserAccountRepository UserAccount;
		private  readonly IUserRepository Userr ;
		private readonly IDataForInstructorRepository dataForInstructor;

		public UserDataShortCutService(IUserAccountRepository userAccountRepository , IUserRepository userRepository ,
											IDataForInstructorRepository dataForInstructor)
		{
			UserAccount = userAccountRepository;
			Userr = userRepository;
			this.dataForInstructor = dataForInstructor;
		}

		public async Task<UserDataShortcutViewModel> GetDatabyid(string id)
		{
			// Use await to asynchronously wait for the tasks to complete
			var u = await Userr.GetByIdAsync(id);

			var Name = u.FName + " " + u.LName;
			var bio = (await dataForInstructor.GetInstructorDataByInstructorIdAsync(id))?.Bio;
			var Image = u.Image;

			var accounts = (await UserAccount.GetUserAccountsByUserIdAsync(id)).ToList();

			return new UserDataShortcutViewModel
			{
				Name = Name,
				Bio = bio,
				Image = Image,
				userAccounts = accounts
			};
		}



	}
}
