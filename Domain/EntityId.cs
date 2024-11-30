namespace Sisusa.Common.Domain;

/// <summary>
/// A unique identifier used to uniquely identify an entity in the domain
/// </summary>
/// <param name="idValue">The unique value identifying this entity - a scalar type.</param>
/// <typeparam name="TType">The primitive type used by this identifier</typeparam>
public abstract class EntityId<TType>(TType idValue) : IEquatable<EntityId<TType>> where TType : IComparable
{
    public TType Value { get; init; } = idValue;

    public bool Equals(EntityId<TType>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) ||
               EqualityComparer<TType>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;

        if (obj is EntityId<TType> other)
        {
            return Value.CompareTo(other.Value) == 0;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(EntityId<TType>? left, EntityId<TType>? right)
    {
        if (ReferenceEquals(null, left)) return false;
        return ReferenceEquals(left, right) || left.Equals(right);
    }

    public static bool operator !=(EntityId<TType>? left, EntityId<TType>? right)
    {
        return !(left == right);
    }
}