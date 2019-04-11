using Microsoft.AspNet.Identity.EntityFramework;

namespace PN.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("EntityConnection", throwIfV1Schema: false) { }

        public static ApplicationDbContext Create() => new ApplicationDbContext();
    }
}
