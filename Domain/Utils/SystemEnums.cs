using System.ComponentModel;

namespace Domain.Utils
{
    public enum ResidentialStatusEnum
    {
        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        Inactive = 2,

        [Description("Outstanding")]
        Outstanding = 3
    }

    public enum RoleEnum
    {
        [Description(AuthorizationRoles.SuperAdmin)]
        SuperAdmin = 1,
        [Description(AuthorizationRoles.Admin)]
        Admin = 2,
        [Description(AuthorizationRoles.Resident)]
        Resident = 3,
        [Description(AuthorizationRoles.Vigilant)]
        Vigilant = 4,
    }

    public enum AttachmentTypeEnum
    {
        [Description("Document")]
        Document = 1,
        [Description("Image")]
        Image = 2
    }

    public enum PaymentTypeEnum
    {
        [Description("Cash")]
        Cash = 1,
        [Description("Credit Card")]
        CreditCard = 2,
        [Description("cheque")]
        Check = 3,
        [Description("Payment transfer")]
        Transfer = 4
    }

    public enum PaymentStatusEnum
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Paid out")]
        PayOut = 2,
        [Description("Cancelled")]
        Cancelled = 3,
    }

    public enum ReportStatusEnum
    {
        [Description("Opened")]
        Opened = 1,
        [Description("Cancelled")]
        Closed = 2
    }

    public enum ReservationStatusEnum
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Not Approved")]
        NotApproved = 3
    }

}
