using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HoneyWedding.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // ApplicationUser properties are extended per http://typecastexception.com/post/2014/06/22/ASPNET-Identity-20-Customizing-Users-and-Roles.aspx

        // Add extended properties here:
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public bool AutoPwdReplaced { get; set; }

        // Add extended navigation properties here:

        // calculated properties
        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                if (FirstName != null && LastName != null)
                {
                    return FirstName + " " + LastName;
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "Name")]
        public string FullNameLastFirst
        {
            get
            {
                if (FirstName != null && LastName != null)
                {
                    return string.Format("{0}, {1}", this.LastName, this.FirstName);
                }
                else
                {
                    return null;
                }
            }
        }

    }

    // Custom class for extensible User Role management per http://typecastexception.com/post/2014/06/22/ASPNET-Identity-20-Customizing-Users-and-Roles.aspx
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }

        // Extended properties must also be added to:
        // RoleViewModel (AdminViewModels.cs) and the Create, Edit, Index views (Views => RolesAdmin)
        // Create, Edit methods (RolesAdminController)

        // Add extended properties here:
        public string Description { get; set; }

        // navigation properties

    }

}