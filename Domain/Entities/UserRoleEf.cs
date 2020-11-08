namespace Domain.Entities
{
    public class UserRoleEf
    {
        public int UserId { get; set; }
        public UserEf User { get; set; }

        public int RoleId { get; set; }
        public RoleEf Role { get; set; }
    }
}
