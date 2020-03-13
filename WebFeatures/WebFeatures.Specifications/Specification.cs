using System;
using System.Linq.Expressions;
using WebFeatures.Specifications.Utils;

namespace WebFeatures.Specifications
{
    public class Specification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }

        public Func<T, bool> Func => _spec ??= Expression.Compile();
        private Func<T, bool> _spec;

        public Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfy(T obj)
            => Func(obj);

        public static implicit operator Specification<T>(Expression<Func<T, bool>> expression)
            => new Specification<T>(expression);

        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
            => spec.Expression;

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
            => new Specification<T>(left.Expression.Or(right.Expression));

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
            => new Specification<T>(left.Expression.And(right.Expression));

        public static Specification<T> operator !(Specification<T> spec)
            => new Specification<T>(spec.Expression.Not());

        public static bool operator true(Specification<T> spec)
            => false;

        public static bool operator false(Specification<T> spec)
            => false;
    }
}
