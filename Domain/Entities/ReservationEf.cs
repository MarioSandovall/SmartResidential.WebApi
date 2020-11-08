using Domain.Interfaces;
using Domain.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ReservationEf : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int RequesterId { get; set; }
        public UserEf Requester { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ReservationStatusEnum ReservationStatus { get; set; }

        public int AreaId { get; set; }
        public AreaEf Area { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int? ApproverId { get; set; }
        public UserEf Approver { get; set; }

    }
}
