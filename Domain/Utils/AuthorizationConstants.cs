namespace Domain.Utils
{
    public class AuthorizationRoles
    {
        public const string PolicyName = "SmartResidential";

        public const string Admin = "Admin";
        public const string Vigilant = "Vigilant";
        public const string Resident = "Resident";
        public const string SuperAdmin = "SuperAdmin";

        public const string SuperAdminOrAdmin = SuperAdmin + "," + Admin;

        public static string[] AllRoles = new[] { SuperAdmin, Admin, Resident, Vigilant };
    }
}