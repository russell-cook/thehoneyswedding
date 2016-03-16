namespace HoneyWedding.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using HoneyWedding.DAL;
    using HoneyWedding.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;


    internal sealed class Configuration : DbMigrationsConfiguration<HoneyWedding.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(ApplicationDbContext context)
        {
            // Initialize Identity data. This routine has been adapted from ApplicationDbInitializer; it was relocated here so that it would run as part of the update-database migration command.

            // These variables use the extensible ApplicationUser class and the custom/extensible ApplicationRole class per the tutorial at: http://typecastexception.com/post/2014/06/22/ASPNET-Identity-20-Customizing-Users-and-Roles.aspx
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
            var roleStore = new RoleStore<ApplicationRole>(context);
            var roleManager = new ApplicationRoleManager(roleStore);

            // Initialize ApplicationRoles in db--these roles are used to control access to AdminApp's various modules via the [Authorize] attribute in the module's controller, as well as for showing/hiding access to these modules in the UI via conditional logic in the "Shared => _Layout.cshtml" View: "@if (Request.IsAuthenticated && User.IsInRole("RoleName"))"
            List<ApplicationRole> initRoles = new List<ApplicationRole> {
                // first role in list is default Administration Role for default user, used by function below when selecting initRoles[0].Name
                new ApplicationRole(){Name = "GlobalAdmin", Description = "Highest level of administration; allows configuration of Global App Settings" },
                new ApplicationRole(){Name = "RolesAdmin",  Description = "Allows for User/Role administration" },
                new ApplicationRole(){Name = "UsersAdmin",  Description = "Allows for User administration, but not Role administration"},
                new ApplicationRole(){Name = "WeddingAdmin",  Description = "Allows for WeddingGuest administration"},
                new ApplicationRole(){Name = "WeddingGuest",  Description = "Wedding Guest"}
            };

            //Create ApplicationRole for each roleNames if it does not exist
            foreach (ApplicationRole initRole in initRoles)
            {
                var role = roleManager.FindByName(initRole.Name);
                if (role == null)
                {
                    var roleresult = roleManager.Create(initRole);
                }
            }

            // set username and password for default admin user
            const string name = "admin@thehoneyswedding.com";
            const string password = "Skoofer_78";

            // Create default admin@example.com user if it does not exist
            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, FirstName = "Russell", LastName = "Cook", IsActive = true, AutoPwdReplaced = true };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to default Administration Role if not already added.
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(initRoles[0].Name))
            {
                // adds default admin user to GlobalAdmin role
                var result = userManager.AddToRole(user.Id, initRoles[0].Name);
            }
            if (!rolesForUser.Contains(initRoles[3].Name))
            {
                // adds default admin user to WeddingAdmin role
                var result = userManager.AddToRole(user.Id, initRoles[3].Name);
            }

        }
    }
}
