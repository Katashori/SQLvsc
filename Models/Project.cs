using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLvcs.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; } // идентификатор проекта
        [Required]
        public string ProjectName { get; set; } //Имя проекта
        [Display(Name = "Client")]
        public int ClientId { get; set; } //Идентификатор клиента
        [ForeignKey("ClientId")]
        public virtual Client Clients { get; set; }
    }
}
