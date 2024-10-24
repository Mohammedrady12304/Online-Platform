using E_Learning.Models;

namespace E_Learning.Areas.Course.Models
{
	public class UserDataShortcutViewModel
	{
        public string Name { get; set; }

		public string Bio { get; set; }

		public string? Image { get; set; }

		public List<UserAccount>? userAccounts { get; set; }


	}
}
