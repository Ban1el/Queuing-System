using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace QueueDashboard.Models.DTO
{
    public class JWTAuthentication
    {
        // Validate the token
        public static AuthenticatedUserModel ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"]));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var jti = jwtToken.Claims.First(claim => claim.Type == "jti").Value;
                var userName = jwtToken.Claims.First(sub => sub.Type == "sub").Value;

                var roles = jwtToken.Claims
                                    .Where(c => c.Type == ClaimTypes.Role)
                                    .Select(c => c.Value)
                                    .ToList();

                AuthenticatedUserModel user = new AuthenticatedUserModel
                {
                    Username = userName,
                    Roles = roles,
                };

                // return user id from JWT token if validation successful
                return user;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}