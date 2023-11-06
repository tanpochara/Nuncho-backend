
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Nuncho.WebApi.entities;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int OwnerId { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public User Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<Task> Tasks { get; set; }
}