using System.Linq;
using Microsoft.AspNetCore.Mvc;
using demo.DTOModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using demo.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace demo.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private tokenParameter _tokenParameter = new tokenParameter();
        public AuthenticationController()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _tokenParameter = config.GetSection("tokenParameter").Get<tokenParameter>();
        }

        [HttpPost, Route("requestToken")]
        public ActionResult RequestToken([FromBody] LoginRequestDTO request)
        {
            //这儿在做用户的帐号密码校验。我这儿略过了。
            if (request.username == null && request.password == null)
                return BadRequest("Invalid Request");

            var token = GenUserToken(request.username, "testUser");

            var refreshToken = "123456";

            return Ok(new[] { token, refreshToken });
        }
        [HttpPost, Route("refreshToken")]
        public ActionResult RefreshToken([FromBody]RefreshTokenDTO request)
        {
            if(request.Token == null && request.refreshToken == null)
                return BadRequest("Invalid Request");

            var handler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal claim = handler.ValidateToken(request.Token, new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenParameter.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                }, out SecurityToken securityToken);

                var username = claim.Identity.Name;
                var token = GenUserToken(username, "testUser");

                var refreshToken = "654321";
                
                return Ok(new[] { token, refreshToken });
            }
            catch(Exception)
            {
                return BadRequest("Invalid Request");
            }
        }
        private string GenUserToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenParameter.Issuer, null, claims, expires: DateTime.UtcNow.AddMinutes(_tokenParameter.AccessExpiration), signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }
    }
}