using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeperStar.Data.References;

namespace ShoeperStar.Data.Custom.ViewComponents
{
    public class RolesDropDown : ViewComponent
    {
        private AppDbContext _dbContext;

        public RolesDropDown(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var roleStore = new RoleStore<IdentityRole>(_dbContext);

            var roles = roleStore.Roles.Select(x => x.Name).Where(x => x != UserRoles.Admin).ToList();
            roles.Insert(0, "Select");

            return View(roles);
        }
    }
}
