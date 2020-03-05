using WebFeatures.Domian.Model.Abstractions;

namespace WebFeatures.Domian.Model.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public OrderStatus Status { get; }
        public StreetAddress ShippingAddress { get; }
        public StreetAddress BillingAddress { get; }

        public Order(StreetAddress shippingAddress, StreetAddress billingAddress)
        {
            Status = OrderStatus.Pending;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
        }
    }
}
