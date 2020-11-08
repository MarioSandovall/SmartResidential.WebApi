using Domain.Interfaces;
using Domain.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RoleEf : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<UserRoleEf> UserRoles { get; set; }

        public RoleEf()
        {

        }

        public RoleEf(RoleEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.GetDescription();
        }

        public static implicit operator RoleEf(RoleEnum @enum) => new RoleEf(@enum);

        public static implicit operator RoleEnum(RoleEf role) => (RoleEnum)role.Id;
    }
}
