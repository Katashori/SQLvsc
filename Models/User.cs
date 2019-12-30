using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLvcs.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } // Идентификатор пользователя
        [Required]
        public string UserName { get; set; } // Имя пользователя
        [Required]
        public string Password { get; set; } // Пароль пользователя
    }
}
