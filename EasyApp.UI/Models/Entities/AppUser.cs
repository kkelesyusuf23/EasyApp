using Microsoft.AspNetCore.Identity;

namespace EasyApp.UI.Models.Entities
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public string? EmailVerificationCode { get; set; } 
        public DateTime? VerificationCodeExpires { get; set; }

        public ICollection<BlogSubscription> Subscriptions { get; set; }
        public ICollection<BlogFollow> Follows { get; set; }
        public ICollection<BlogLike> Likes { get; set; }
        public ICollection<BlogComment> Comments { get; set; }
    }
}
