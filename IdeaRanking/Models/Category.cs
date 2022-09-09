using System.Text.Json.Serialization;

namespace IdeaRanking.Models;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [JsonIgnore]
    public List<Idea>? Ideas { get; set; }
}