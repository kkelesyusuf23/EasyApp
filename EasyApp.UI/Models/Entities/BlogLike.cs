namespace EasyApp.UI.Models.Entities
{
    public class BlogLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Beğeni yapan kullanıcı
        public AppUser User { get; set; }

        public int BlogPostId { get; set; }  // Beğenilen blog gönderisi
        public BlogPost BlogPost { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
