using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.SeedWork.DDD.Domain
{
    public class ValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {

        public bool Equals(TValueObject other)
        {
            if (other == null) return false;

            if (Object.ReferenceEquals(this, other)) return true;

            var publicProperties = this.GetType().GetProperties();

            if (publicProperties != null
                &&
                publicProperties.Any())
            {
                return publicProperties.All(p =>
                    {
                        var left = p.GetValue(this, null);
                        var right = p.GetValue(other, null);

                        if (typeof(TValueObject).IsAssignableFrom(left.GetType()))
                            //TODO: need to change its logical
                            return Object.ReferenceEquals(left, right);
                        else
                            return left.Equals(right);
                    });
            }
            else
                return true;
        }

        public bool Equals(object other)
        {
            if (other == null) return false;

            if (Object.ReferenceEquals(this, other)) return true;

            var valueObject = other as ValueObject<TValueObject>;

            if (valueObject != null) return Equals(valueObject);
            else return false;
        }

    }
}
