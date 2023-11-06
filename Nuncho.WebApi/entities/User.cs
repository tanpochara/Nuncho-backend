using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Nuncho.WebApi.entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<Project> Projects { get; set; }
}
