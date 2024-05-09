namespace Framework.Domain;

public abstract class ValueObject<TValue> : IEquatable<TValue>
                where TValue : ValueObject<TValue>
{
    public bool Equals(TValue? other)
    {
        return this==other;
    }

    public abstract bool ObjectIsEqual(TValue? other);
    
    public override bool Equals(object? obj)
    {
        return (obj is TValue otherObject) && ObjectIsEqual(otherObject);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(ValueObject<TValue>? left, ValueObject<TValue>? right)
    {
        if (left is null && right is null)
            return true;
        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }
    public static bool operator !=(ValueObject<TValue> left, ValueObject<TValue> right)
    {
        return !(left == right);
    }
}