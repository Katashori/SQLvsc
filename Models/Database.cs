using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLvcs.Models
{
    public class Database
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DatabaseId { get; set; } // идентификатор базы данных
        [Required]
        public string DatabaseName { get; set; } //Имя базы данных
        [Display(Name = "Instance")]
        public int InstanceId { get; set; } //Идентификатор инстанса
        [ForeignKey("InstanceId")]
        public virtual Instance Instances { get; set; }
    }
}
