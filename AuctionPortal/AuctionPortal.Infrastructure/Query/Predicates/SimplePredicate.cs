using AuctionPortal.Infrastructure.Query.Predicates.Operators;
using System;

namespace AuctionPortal.Infrastructure.Query.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public SimplePredicate(string targetPropertyName, ValueComparingOperator valueComparingOperator, object comparedValue)
        {
            if (valueComparingOperator == ValueComparingOperator.None)
            {
                throw new ArgumentException("Simple predicate must use some sort of valueComparingOperator");
            }
            TargetPropertyName = !string.IsNullOrWhiteSpace(targetPropertyName) ? targetPropertyName : throw new ArgumentException("Target property name must be defined!");
            ValueComparingOperator = valueComparingOperator;
            ComparedValue = comparedValue;
        }

        public string TargetPropertyName { get; }

        public object ComparedValue { get; }

        public ValueComparingOperator ValueComparingOperator { get; }

        protected bool Equals(SimplePredicate other)
        {
            return string.Equals(TargetPropertyName, other.TargetPropertyName) && Equals(ComparedValue, other.ComparedValue) && ValueComparingOperator == other.ValueComparingOperator;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == this.GetType() && Equals((SimplePredicate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TargetPropertyName != null ? TargetPropertyName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (ComparedValue != null ? ComparedValue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)ValueComparingOperator;
                return hashCode;
            }
        }
    }
}
