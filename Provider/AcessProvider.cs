using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Student_Api.Provider
{
    public static class AcessProvider
    {
        public enum SystemUserType { Admin,User}
        public static bool RetrieveUserFromAccesToken(ref string data)
        {
            if (!string.IsNullOrWhiteSpace(data))
            {
                byte[] AuthKey = Encoding.ASCII.GetBytes(ConfigProvider.EncryptionKey);
                JwtSecurityTokenHandler tokenhandler = new();
                TokenValidationParameters tokenvalidationparameter = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(AuthKey),
                    ValidateIssuer = false,
                    ValidateAudience=false
                };
                ClaimsPrincipal claimprincipal = tokenhandler.ValidateToken(data, tokenvalidationparameter,out SecurityToken tokenSecure);
                Claim claim = claimprincipal.Claims.Where(d => d.Type == ClaimTypes.UserData).FirstOrDefault();
                if (claim != null && !string.IsNullOrWhiteSpace(claim.Value)){ data = claim.Value; return true; } else { data = string.Empty; }
            }
            return false;
        }
        public static string GetUserAccessToken(object users,DateTime validityTill,Claim additionalclaim = null)
        {
            byte[] AuthKey = Encoding.ASCII.GetBytes(ConfigProvider.EncryptionKey);
            JwtSecurityTokenHandler tokenHandler = new();
            List<Claim> claims = new() { new Claim(ClaimTypes.UserData, users.ToJson()) };
            if (additionalclaim != null) { claims.Add(additionalclaim); }
            return tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = validityTill,
                SigningCredentials = new SigningCredentials
                     (new SymmetricSecurityKey(AuthKey),
                     SecurityAlgorithms.HmacSha256Signature)
            }));               
        }
    }
}
