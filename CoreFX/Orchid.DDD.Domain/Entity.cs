using System.Collections.Generic;

namespace Orchid.DDD.Domain
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default(TKey));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
            {
                return false;
            }

            // Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Transient objects are not considered as equal
            var other = (Entity<TKey>)obj;
            if (IsTransient() && other.IsTransient())
            {
                return false;
            }

            // Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }

    public abstract class Entity : Entity<int>
    {

    }
}
