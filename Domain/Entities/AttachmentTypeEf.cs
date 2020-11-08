using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AttachmentTypeEf : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public AttachmentTypeEf()
        {

        }

        public AttachmentTypeEf(AttachmentTypeEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.GetDescription();
        }

        public static implicit operator AttachmentTypeEf(AttachmentTypeEnum @enum) => new AttachmentTypeEf(@enum);

        public static implicit operator AttachmentTypeEnum(AttachmentTypeEf entity) => (AttachmentTypeEnum)entity.Id;
    }
}
