namespace Sisusa.Common.Domain;

public abstract class Entity<TId>(TId theId)
{
    public TId Id { get; init; } = theId ?? throw new ArgumentNullException(message: "Entity must have a valid unique identifier.", paramName: nameof(theId));

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is Entity<TId> other)
        {
            return Id != null && Id.Equals(other.Id);
        }
        return false;
    }

    public abstract override int GetHashCode();

    public static bool operator ==(Entity<TId>? a, Entity<TId>? b)
    {
        if (ReferenceEquals(a, b)) return true;
        return a is not null && a.Equals(b);
    }

    public static bool operator !=(Entity<TId>? a, Entity<TId>? b)
    {
        return !(a == b);
    }
}