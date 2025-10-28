namespace Fiap.Challenge.Wtc.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public List<Guid> MemberIds { get; private set; }

    private Group() { }

    public Group(string name, string? description = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        MemberIds = new List<Guid>();
    }

    public void AddMember(Guid userId)
    {
        if (!MemberIds.Contains(userId))
        {
            MemberIds.Add(userId);
            UpdateTimestamp();
        }
    }

    public void RemoveMember(Guid userId)
    {
        if (MemberIds.Remove(userId))
            UpdateTimestamp();
    }

    public void UpdateName(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        UpdateTimestamp();
    }
}