using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotebookApi.Models
{
    [Table("Notes",Schema ="dbo")]
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("NoteId")]
        [Key]
        public int NoteId { get; set; }
        [Column("NoteTitle")]
        public string NoteTitle { get; set; }
        [Column("NoteContent")]
        public string NoteContent { get; set; }
        [Column("UserName")]
       // public int UserId { get; set; }
        public string UserName { get; set; }
        [Column("ActionDate")]
        public DateTime ActionDate { get; set; }
    }
}
