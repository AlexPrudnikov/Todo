using System;
using System.Security.Claims;

namespace TodoCSharp.Infrastructure
{
    public static class ExtensionMethods
    {
        public static String GetUserId(this ClaimsPrincipal user) => 
            !user.Identity.IsAuthenticated == true ? null : user.FindFirst(ClaimTypes.NameIdentifier).Value;
        // https://www.dxsdata.com/2017/03/asp-net-core-get-user-id/
    }
}
