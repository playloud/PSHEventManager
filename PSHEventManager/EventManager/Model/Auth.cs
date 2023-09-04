using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PSHEventManager.EventManager.Services;

namespace PSHEventManager.EventManager.Model;

public class Auth : IJwtAuth
{
    private readonly IConfiguration _config;
    private readonly string _key;

    public Auth(IConfiguration _config, string _key)
    {
        this._config = _config;
        this._key = _key;
    }

    public async Task<string> Authenticate(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return null;

        var authenticated = false;

        var dbService = DBServices.GetInstance();
        authenticated = await dbService.Autheticate(username, password);

        if (!authenticated)
            return null;

        // 1. Create Security Token Handler
        var tokenHandler = new JwtSecurityTokenHandler();

        // 2. Create Private Key to Encrypted
        var tokenKey = Encoding.ASCII.GetBytes(_key);

        //3. Create JETdescriptor
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
            Expires = DateTime.Now.AddHours(24),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        //4. Create Token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // 5. Return Token from method
        return tokenHandler.WriteToken(token);
    }
}