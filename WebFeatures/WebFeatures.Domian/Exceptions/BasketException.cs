using System;

namespace WebFeatures.Domian.Exceptions
{
    public class BasketException : Exception
    {
        public Guid BasketId { get; }

        public BasketException(Guid basketId, string message) : base(message)
        {
            BasketId = basketId;
        }
    }
}
