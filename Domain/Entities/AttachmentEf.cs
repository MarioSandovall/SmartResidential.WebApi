using Domain.Interfaces;
using Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class AttachmentEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public AttachmentTypeEnum AttachmentType { get; set; }

        [Required]
        [StringLength(300)]
        public string Path { get; set; }

        public int? PaymentId { get; set; }
        public PaymentEf Payment { get; set; }

        public int? ExpenseId { get; set; }
        public ExpenseEf Expense { get; set; }

        public int? ReportId { get; set; }
        public ReportEf Report { get; set; }

        public int? ActionId { get; set; }
        public ActionEf Action { get; set; }

        public int? AnnouncementId { get; set; }
        public AnnouncementEf Announcement { get; set; }

    }
}
