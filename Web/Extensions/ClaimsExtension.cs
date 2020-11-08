using Business.Utils;
using Domain.Utils;
using System;
using System.Linq;
using System.Security.Claims;

namespace Web.Extensions
{
    public static class ClaimsExtension
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return user.Claims.Where(x => x.Type == ClaimsCustomTypes.Id)
                .Select(x => Convert.ToInt32(x.Value)).Single();
        }

        public static bool IsSuperAdmin(this ClaimsPrincipal user)
        {
            return user.Claims.Any(x => x.Type == ClaimsCustomTypes.RoleId &&
                Convert.ToInt32(x.Value) == (int)RoleEnum.SuperAdmin);
        }
    }
}
