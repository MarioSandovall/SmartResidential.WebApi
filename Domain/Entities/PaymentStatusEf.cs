using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PaymentStatusEf : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public PaymentStatusEf()
        {

        }

        public PaymentStatusEf(PaymentStatusEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.GetDescription();
        }

        public static implicit operator PaymentStatusEf(PaymentStatusEnum @enum) => new PaymentStatusEf(@enum);

        public static implicit operator PaymentStatusEnum(PaymentStatusEf entity) => (PaymentStatusEnum)entity.Id;
    }
}
