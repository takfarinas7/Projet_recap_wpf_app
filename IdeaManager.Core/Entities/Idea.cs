namespace IdeaManager.Core.Entities;

public class Idea
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Votes { get; set; } = 0;
    public bool IsApproved { get; set; } = false;
}
