namespace EasyApp.UI.Models.Entities
{
    public class ToDo
    {
        public int ToDoId { get; set; }
        public string ToDoName { get; set; } // başlık
        public string ToDoDescription { get; set; } // açıklama
        public string Category { get; set; } // kategori
        public string Priority { get; set; } // önemi
        public string Status { get; set; } // durum
        public DateTime CreatedDate { get; set; } // oluşturma tarihi
        public DateTime DeadLine { get; set; } // teslim tarihi
        public DateTime CompletionDate { get; set; } // tamamlama tarihi

        public bool IsReminder { get; set; } // hatırlatıcısı olsun mu
        public DateTime ReminderDate { get; set; } // hatırlatma tarihi(teslim tarihinden önceki gün)
        public int UserId { get; set; }  // note alan kullanıcı
        public AppUser User { get; set; }

    }
}
