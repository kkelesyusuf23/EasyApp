namespace EasyApp.UI.Models.Entities
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Titlw { get; set; } //başlık
        public string Content { get; set; }//içerik
        public DateTime CreatedDate { get; set; } // oluşturma tarihi
        public DateTime LastUpdatedDate { get; set; } // son güncelleme tarihi
        public string Status { get; set; } // durum(tamamlandı, bekliyor)
        public string Priority { get; set; } // öncelik(düşük, orta, yüksek)
        public string Categori { get; set; } //iş, kişisel, alışveriş...
        public int UserId { get; set; }  // note alan kullanıcı
        public AppUser User { get; set; }
    }
}
