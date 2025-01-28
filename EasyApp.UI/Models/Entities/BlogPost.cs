namespace EasyApp.UI.Models.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int AuthorId { get; set; }  // AppUser ile ilişkilendirilir
        public AppUser Author { get; set; }

        public ICollection<BlogSubscription> Subscriptions { get; set; }
        public ICollection<BlogComment> Comments { get; set; }
        public ICollection<BlogLike> Likes { get; set; }
    }
}
