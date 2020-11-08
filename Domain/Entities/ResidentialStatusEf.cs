using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ResidentialStatusEf : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ResidentialStatusEf()
        {

        }

        public ResidentialStatusEf(ResidentialStatusEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
        }

        public static implicit operator ResidentialStatusEf(ResidentialStatusEnum @enum) => new ResidentialStatusEf(@enum);

        public static implicit operator ResidentialStatusEnum(ResidentialStatusEf residentialStatus) => (ResidentialStatusEnum)residentialStatus.Id;
    }
}
