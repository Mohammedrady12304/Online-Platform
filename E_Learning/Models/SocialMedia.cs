using Microsoft.EntityFrameworkCore;

namespace E_Learning.Models
{
	[PrimaryKey("Id")]
	public class SocialMedia
	{

		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string Name { get; set; } = null!;

		public string Icon { get; set; } = null!;

		public List<UserAccount>? UserAccountes { get; set;}
	}
}
