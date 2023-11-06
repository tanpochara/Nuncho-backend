using Nuncho.WebApi.constants;
using TaskStatus = Nuncho.WebApi.constants.TaskStatus;

namespace Nuncho.WebApi.Model;

public class CreateTaskBody
{
    public string title { get; set; }
    public string description { get; set; }
    public TaskStatus? status { get; set; }
    public TaskPriority? priority { get; set; }
}