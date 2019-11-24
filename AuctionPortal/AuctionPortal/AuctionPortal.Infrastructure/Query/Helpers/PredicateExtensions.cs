using AuctionPortal.Infrastructure.Query.Predicates;
using AuctionPortal.Infrastructure.Query.Predicates.Operators;
using System;
using System.Collections.Generic;

namespace AuctionPortal.Infrastructure.Query.Helpers
{
    public static class PredicateExtensions
    {
        private static readonly IDictionary<ValueComparingOperator, Func<string, string>> BinaryOperations =
            new Dictionary<ValueComparingOperator, Func<string, string>>
            {
                {ValueComparingOperator.Equal, rightOperand => CheckCommaUsage(rightOperand) ? $" = '{rightOperand}'" : $" = {rightOperand}" },
                {ValueComparingOperator.NotEqual, rightOperand => CheckCommaUsage(rightOperand) ? $" != '{rightOperand}'" : $" != {rightOperand}" },
                {ValueComparingOperator.GreaterThan, rightOperand => $" > {rightOperand}" },
                {ValueComparingOperator.GreaterThanOrEqual, rightOperand => $" >= {rightOperand}" },
                {ValueComparingOperator.LessThan, rightOperand => $" < {rightOperand}" },
                {ValueComparingOperator.LessThanOrEqual, rightOperand => $" <= {rightOperand}" },
                {ValueComparingOperator.StringContains, rightOperand => $" LIKE '%{rightOperand}%'"}
			};

        public static string BuildCompositePredicate(this CompositePredicate compositePredicate)
        {
            if (compositePredicate.Predicates.Count == 0)
            {
                throw new InvalidOperationException("At least one simple predicate must be given");
            }
            var sql = SqlConstants.OpenParenthesis;
            sql += ChoosePredicate(compositePredicate, 0);
            for (var i = 1; i < compositePredicate.Predicates.Count; i++)
            {
                sql += compositePredicate.Operator == LogicalOperator.OR ? SqlConstants.Or : SqlConstants.And;
                sql += ChoosePredicate(compositePredicate, i);
            }
            return sql + SqlConstants.CloseParenthesis;
        }

        public static string BuildSimplePredicate(this IPredicate simplePredicate)
        {
            var predicate = simplePredicate as SimplePredicate;
            if (predicate == null)
            {
                throw new ArgumentException("Expected simple predicate!");
            }
            return GetWhereCondition(predicate);
        }

        private static string GetWhereCondition(SimplePredicate simplePredicate)
        {
            if (!BinaryOperations.ContainsKey(simplePredicate.ValueComparingOperator))
            {
                throw new InvalidOperationException($"Transformation of value comparing operator: {simplePredicate.ValueComparingOperator} to where condition is not supported!");
            }
            if (simplePredicate.ComparedValue == null)
            {
                return GetNullWhereCondition(simplePredicate);
            }
            return GetEscapedWhereCondition(simplePredicate);
        }

        private static string ChoosePredicate(CompositePredicate compositePredicate, int index)
        {
            return compositePredicate.Predicates[index] is CompositePredicate predicate
                ? predicate.BuildCompositePredicate()
                : compositePredicate.Predicates[index].BuildSimplePredicate();
        }

        private static string GetNullWhereCondition(SimplePredicate simplePredicate)
        {
            if (simplePredicate.ValueComparingOperator.Equals(ValueComparingOperator.NotEqual))
            {
                return simplePredicate.TargetPropertyName + " IS NOT NULL";
            }
            return simplePredicate.TargetPropertyName + " IS NULL";
        }

        private static bool CheckCommaUsage(object rightOperand)
        {
            return rightOperand != null && (rightOperand is string || rightOperand is Guid || rightOperand is DateTime);
        }

        private static string GetEscapedWhereCondition(SimplePredicate simplePredicate)
        {
            const string atChar = "@";
            if (simplePredicate.ComparedValue is string value && value.Contains(atChar))
            {
                string escapedValue = value.Insert(value.IndexOf(atChar, StringComparison.Ordinal), atChar);
                return simplePredicate.TargetPropertyName + BinaryOperations[simplePredicate.ValueComparingOperator]
                           .Invoke(escapedValue);
            }
            return simplePredicate.TargetPropertyName + BinaryOperations[simplePredicate.ValueComparingOperator]
                       .Invoke(ConvertOperandToString(simplePredicate.ComparedValue));
        }

        //this method is here because you cannot override ToString() method of enum 
        // and we want to return number instead of name values of enum
        private static string ConvertOperandToString(object operand)
        {
            if (operand is Enum enumOperand)
            {
                return enumOperand.ToString("D");
            }
            return operand.ToString();
        }
    }
}
