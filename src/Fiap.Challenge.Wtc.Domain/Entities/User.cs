using Fiap.Challenge.Wtc.Domain.Enums;
using Fiap.Challenge.Wtc.Domain.ValueObjects;

namespace Fiap.Challenge.Wtc.Domain.Entities;

public class User : BaseEntity
{
    public Email Email { get; private set; }
    public string Name { get; private set; }
    public string PasswordHash { get; private set; }
    public ProfileType Profile { get; private set; }
    public List<string> Tags { get; private set; }
    public int Score { get; private set; }
    public UserStatus Status { get; private set; }

    private User() { }

    public User(Email email, string name, string passwordHash, ProfileType profile)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Profile = profile;
        Tags = new List<string>();
        Score = 0;
        Status = UserStatus.Active;
    }

    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag))
            Tags.Add(tag);
        
        UpdateTimestamp();
    }

    public void RemoveTag(string tag)
    {
        Tags.Remove(tag);
        UpdateTimestamp();
    }

    public void UpdateScore(int score)
    {
        Score = score;
        UpdateTimestamp();
    }

    public void UpdateStatus(UserStatus status)
    {
        Status = status;
        UpdateTimestamp();
    }

    public void UpdateName(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        UpdateTimestamp();
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        UpdateTimestamp();
    }
}