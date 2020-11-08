using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ReportStatusEf : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ReportStatusEf()
        {

        }

        public ReportStatusEf(ReportStatusEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.GetDescription();
        }

        public static implicit operator ReportStatusEf(ReportStatusEnum @enum) => new ReportStatusEf(@enum);

        public static implicit operator ReportStatusEnum(ReportStatusEf entity) => (ReportStatusEnum)entity.Id;
    }
}
