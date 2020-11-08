using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ReservationStatusEf : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ReservationStatusEf()
        {

        }

        public ReservationStatusEf(ReservationStatusEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.GetDescription();
        }

        public static implicit operator ReservationStatusEf(ReservationStatusEnum @enum) => new ReservationStatusEf(@enum);

        public static implicit operator ReservationStatusEnum(ReservationStatusEf entity) => (ReservationStatusEnum)entity.Id;
    }
}
