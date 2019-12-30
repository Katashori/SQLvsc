using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLvcs.Models
{
    public class Instance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstanceId { get; set; } // идентификатор инстанса
        [Required]
        public string InstanceName { get; set; } //Имя инстанса
        [Display(Name = "Project")]
        public int ProjectId { get; set; } //Идентификатор проекта
        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
    }
}
