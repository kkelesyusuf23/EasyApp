using EasyApp.UI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyApp.UI.Context
{
    public class EasyContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EasyContext(DbContextOptions options) : base(options)
        {
        }
    }
}
