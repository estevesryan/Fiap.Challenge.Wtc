namespace Fiap.Challenge.Wtc.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class BusinessRuleValidationException : DomainException
{
    public BusinessRuleValidationException(string message) : base(message)
    {
    }
}

public class EntityNotFoundException : DomainException
{
    public EntityNotFoundException(string entityName, Guid id) 
        : base($"{entityName} with id {id} was not found.")
    {
    }
}

public class InvalidValueObjectException : DomainException
{
    public InvalidValueObjectException(string message) : base(message)
    {
    }
}