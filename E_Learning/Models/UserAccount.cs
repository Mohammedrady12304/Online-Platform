using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
	[PrimaryKey("Id")]
	public class UserAccount
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[ForeignKey("User")]
        public string UserID { get; set; } = null!;
		public User? User { get; set; }

		[ForeignKey("SocialMedia")]
		public string SocialMediaID { get; set; } = null!;
		public SocialMedia? SocialMedia { get; set; }

		[Url(ErrorMessage = "unvalid URL...")]
		public string url { get; set; } = null!;



	}
}
