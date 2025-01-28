namespace EasyApp.UI.Models.Entities
{
    public class BlogSubscription
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // AppUser ile ilişkilendirilir
        public AppUser User { get; set; }

        public int BlogPostId { get; set; }  // BlogPost ile ilişkilendirilir
        public BlogPost BlogPost { get; set; }

        public DateTime SubscribedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
