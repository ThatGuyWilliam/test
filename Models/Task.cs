using System.ComponentModel.DataAnnotations.Schema;

namespace SkillTest.Models
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey("AssigneeID")]
        public virtual User Assignee { get; set; }
        public DateTime DueDate { get; set; }
        public bool Active { get; set; }
    }
}
