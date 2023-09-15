using Microsoft.AspNetCore.Identity;

namespace UserHelpPageTemplate.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser // i would use <Guid> if i had a custom identifier type 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }      
        public byte[]? ProfilePicture { get; set; }
        public bool IsActive { get; set; }
        public bool Cloak { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }
    }
}
