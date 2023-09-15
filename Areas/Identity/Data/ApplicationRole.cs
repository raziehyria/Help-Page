using Microsoft.AspNetCore.Identity;

namespace UserHelpPageTemplate.Areas.Identity.Data
{
    public class ApplicationRole : IdentityRole
    { 
        public string RoleName { get; set; }
        public bool Cloak { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

        public ApplicationRole(string roleName) : base(roleName)
        {
            // Additional initialization if needed
        }
    }
}
