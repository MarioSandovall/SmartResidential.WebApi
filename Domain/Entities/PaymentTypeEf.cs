using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PaymentTypeEf : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public PaymentTypeEf()
        {

        }

        public PaymentTypeEf(PaymentTypeEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.GetDescription();
        }

        public static implicit operator PaymentTypeEf(PaymentTypeEnum @enum) => new PaymentTypeEf(@enum);

        public static implicit operator PaymentTypeEnum(PaymentTypeEf entity) => (PaymentTypeEnum)entity.Id;
    }
}

