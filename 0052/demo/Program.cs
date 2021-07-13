using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using demo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Issuer"],
        ValidAudience = builder.Configuration["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"]))
    };
});

var app = builder.Build();

//app.UseSwagger();

app.UseAuthorization();
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", (Func<string>)(() => "Hello World!"));

app.MapPost("/token", GetToken);

async Task GetToken(HttpContext http)
{
    var inputUser = await http.Request.ReadFromJsonAsync<Client>();
    if (!string.IsNullOrEmpty(inputUser.name) &&
        !string.IsNullOrEmpty(inputUser.password))
    {
        // 检查用户是否合法, 代码省略

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, inputUser.id.ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, inputUser.name)
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Issuer"],
            audience: builder.Configuration["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"])),
                SecurityAlgorithms.HmacSha256)
        );

        await http.Response.WriteAsJsonAsync(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        return;
    }

    http.Response.StatusCode = 400;
}

app.MapGet("/todoitems1", GetTodoItems1);

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
async Task GetTodoItems1(HttpContext http)
{
    await http.Response.WriteAsJsonAsync($"Done !!");
}


app.Run();
