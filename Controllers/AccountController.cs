using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.DTOs;
using SocialMediaApp.Entities;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Services;

namespace SocialMediaApp.Controllers;

public class AccountController : BaseAPIController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<Userdto>> Register(Registerdto registerdto)
    {
        if (await UserExists(registerdto.UserName)) return BadRequest("Sorry username exists");
        
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
          UserName  = registerdto.UserName.ToLower(),
          Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerdto.Password)),
          PasswordSalt = hmac.Key
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new Userdto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(item => item.UserName == username.ToLower());
    }


    [HttpPost("login")]
    public async Task<ActionResult<Userdto>> Login(Logindto logindto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(item => item.UserName == logindto.Username.ToLower());
        if (user == null) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.Password[i]) return Unauthorized("Incorrect password");
        }
        
        return new Userdto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

}