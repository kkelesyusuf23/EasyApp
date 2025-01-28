namespace EasyApp.UI.Models.Entities
{
    public class BlogFollow
    {
        public int Id { get; set; }

        public int FollowedUserId { get; set; }
        public AppUser FollowedUser { get; set; }

        public int FollowerUserId { get; set; }
        public AppUser FollowerUser { get; set; }

        public DateTime FollowDate { get; set; }
    }
}
