
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rakais_EL3lag.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rakais_EL3lag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> user;

        public AccountController(UserManager<IdentityUser> user)
        {
            this.user = user;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto UserLogin)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = await user.FindByNameAsync(UserLogin.Username);
                if (applicationUser != null)
                {
                    bool Isfound = await user.CheckPasswordAsync(applicationUser, UserLogin.Password);
                    if (Isfound == true)
                    {
                        List<Claim> UserClaims = new List<Claim>();
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id));
                        UserClaims.Add(new Claim(ClaimTypes.Name, applicationUser.UserName ));
                        var Roles = await user.GetRolesAsync(applicationUser);
                        foreach (var item in Roles)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, item));
                        }
                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mohamedfouad@mohamed12345Ibra$2463187"));

                        SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken Token = new JwtSecurityToken(
                            issuer: "http://localhost:5298/",
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: signing
                               );
                        return Ok(
                            new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(Token),
                                expiration = DateTime.Now.AddHours(1)
                            }
                            );
                    }
                    ModelState.AddModelError("UserName", "UserName or Password In Valid");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
