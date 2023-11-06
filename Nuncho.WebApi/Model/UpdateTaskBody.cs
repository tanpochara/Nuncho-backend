using TaskStatus = Nuncho.WebApi.constants.TaskStatus;

namespace Nuncho.WebApi.Model;

public class UpdateTaskBody
{
    public TaskStatus Status { get; set; }
}