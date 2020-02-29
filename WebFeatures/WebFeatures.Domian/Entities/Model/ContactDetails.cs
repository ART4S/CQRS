using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Domian.Entities.Model.ValueObjects;

namespace WebFeatures.Domian.Entities.Model
{
    /// <summary>
    /// Контактная информация пользователя
    /// </summary>
    [Description("Контактная информация")]
    public class ContactDetails : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
    }
}
