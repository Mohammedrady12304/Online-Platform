using E_Learning.Areas.Course.Models;

namespace E_Learning.Areas.Course.Data.Services
{
	public interface IUserDataShortcutService
	{
		Task<UserDataShortcutViewModel> GetDatabyid(string id);
		


	}
}
