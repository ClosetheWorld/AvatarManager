namespace AvatarManager.Core.Models;

public class Folder
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<string> ContainAvatarIds { get; set; }
}
