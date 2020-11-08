using Domain.Interfaces;
using Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ReportEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public UserEf User { get; set; }

        public ReportStatusEnum ReportStatus { get; set; }
    }
}
