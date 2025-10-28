using System.Text.RegularExpressions;
using Fiap.Challenge.Wtc.Domain.Exceptions;

namespace Fiap.Challenge.Wtc.Domain.ValueObjects;

public class Email : ValueObject
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidValueObjectException("Email cannot be empty");

        email = email.Trim().ToLowerInvariant();

        if (!EmailRegex.IsMatch(email))
            throw new InvalidValueObjectException($"Invalid email format: {email}");

        return new Email(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
}