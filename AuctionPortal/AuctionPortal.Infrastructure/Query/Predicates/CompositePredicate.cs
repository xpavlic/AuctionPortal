using AuctionPortal.Infrastructure.Query.Predicates.Operators;
using System.Collections.Generic;
using System.Linq;

namespace AuctionPortal.Infrastructure.Query.Predicates
{
    public class CompositePredicate : IPredicate
    {
        public CompositePredicate(List<IPredicate> predicates, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            Predicates = predicates;
            Operator = logicalOperator;
        }

        public List<IPredicate> Predicates { get; }

        public LogicalOperator Operator { get; }

        protected bool Equals(CompositePredicate other)
        {
            return new HashSet<IPredicate>(Predicates.Where(predicate => predicate is SimplePredicate))
                        .SetEquals(new HashSet<IPredicate>(other.Predicates.Where(predicate => predicate is SimplePredicate)))
                    && Operator == other.Operator;
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
            return obj.GetType() == this.GetType() && Equals((CompositePredicate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Predicates != null ? Predicates.GetHashCode() : 0) * 397) ^ (int)Operator;
            }
        }
    }
}
