namespace Sisusa.Common.Domain
{
    /// <summary>
    /// Represents an object that is part of the domain but does not have a unique identifier
    /// </summary>
    public abstract class ValueObject
    {
        public abstract override bool Equals(object? other);

        public abstract override int GetHashCode();

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return left?.Equals(right) ?? right is null;
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }
    }
}