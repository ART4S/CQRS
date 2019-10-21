using System.ComponentModel;
using WebFeatures.Domian.Entities.Abstractions;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Domian.Entities.Model
{
    /// <summary>
    /// Контактная информация пользователя
    /// </summary>
    [Description("Контактная информация")]
    public class ContactDetails : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public virtual Address Address { get; set; }
    }
}
