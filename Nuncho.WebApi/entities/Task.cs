using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nuncho.WebApi.constants;
using TaskStatus = Nuncho.WebApi.constants.TaskStatus;

namespace Nuncho.WebApi.entities;

public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int BelongToId { get; set; }
    public Project BelongTo { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    
    public string tags { get; set; }
}