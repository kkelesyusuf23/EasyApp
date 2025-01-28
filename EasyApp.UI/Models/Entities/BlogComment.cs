using System;

namespace EasyApp.UI.Models.Entities
{
    public class BlogComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Yorum yapan kullanıcı
        public AppUser User { get; set; }

        public int BlogPostId { get; set; }  // Yorumun yapıldığı blog gönderisi
        public BlogPost BlogPost { get; set; }

        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
