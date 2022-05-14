using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
namespace Api.Providers
{
    public static class RetrieveTokken
    {

        public static int GetIdUser(this HttpContext context)
        {
            return int.Parse(context.User?.Identity?.Name);
        }

        public static int GetRoleUser(this HttpContext context)
        {
            return int.Parse(context.User.Claims.SingleOrDefault(e => e.Type == ClaimTypes.Role)?.Value);
        }

        private static string GetClaim(string type, HttpContext context)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // var token0 = await context.GetTokenAsync("name");
            // var me = context.User.Claims;
            string tokenString = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            JwtSecurityToken  token = tokenHandler.ReadJwtToken(tokenString);
            Claim claim = token.Claims.SingleOrDefault(e => e.Type == type);

            return claim?.Value;
        }

        public static string GetClaimfromToken(this HttpContext context, string type,string tokenStr)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
         
            JwtSecurityToken  token = tokenHandler.ReadJwtToken(tokenStr);
            Claim claim = token.Claims.SingleOrDefault(e => e.Type == type);

            return claim?.Value;
        }



    }
}