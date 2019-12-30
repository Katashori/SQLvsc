using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLvcs.Models
{
    public class Dacpac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DacpacId { get; set; } // Идентификатор файла dacpac
        [Required]
        public string DacpacName { get; set; } //Имя файла
        [Required]
        public string DacpacPath { get; set; } //Путь до файла
        [Required]
        public string Version { get; set; } //Версия
        [Required]
        public DateTime Uploaded { get; set; } // Дата загрузки
        [Display(Name = "User")]
        public int UserId { get; set; } //Логин загрузившего
        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
        [Display(Name = "Database")]
        public int DatabaseId { get; set; } //Идентификатор базы данных
        [ForeignKey("DatabaseId")]
        public virtual Database Databases { get; set; }
    }
}
