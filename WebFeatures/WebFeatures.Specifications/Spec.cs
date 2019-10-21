using System;
using System.Linq.Expressions;
using WebFeatures.Specifications.Utils;

namespace WebFeatures.Specifications
{
    public class Spec<T>
    {
        public Expression<Func<T, bool>> Expression { get; }

        public Func<T, bool> Func => _spec ?? (_spec = Expression.Compile());
        private Func<T, bool> _spec;

        public Spec(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfy(T obj)
        {
            return Func(obj);
        }

        public static implicit operator Spec<T>(Expression<Func<T, bool>> expression)
            => new Spec<T>(expression);

        public static implicit operator Expression<Func<T, bool>>(Spec<T> spec)
            => spec.Expression;

        public static Spec<T> operator |(Spec<T> left, Spec<T> right)
            => new Spec<T>(left.Expression.Or(right.Expression));

        public static Spec<T> operator &(Spec<T> left, Spec<T> right)
            => new Spec<T>(left.Expression.And(right.Expression));

        public static Spec<T> operator !(Spec<T> spec)
            => new Spec<T>(spec.Expression.Not());

        public static bool operator true(Spec<T> spec)
            => false;

        public static bool operator false(Spec<T> spec)
            => false;
    }
}
