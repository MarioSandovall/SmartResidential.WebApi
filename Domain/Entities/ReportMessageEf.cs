using Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ReportMessageEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public UserEf User { get; set; }

        public int ReportId { get; set; }
        public ReportEf Report { get; set; }
    }
}
