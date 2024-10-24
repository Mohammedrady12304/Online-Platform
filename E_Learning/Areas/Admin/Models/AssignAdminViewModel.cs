using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Learning.Areas.Admin.Models
{
    public class AssignAdminViewModel
    {
        public string Username { get; set; } = null!;

        // You may also want a list of users to select from
        public IEnumerable<SelectListItem>? Users { get; set; }
    }

}
