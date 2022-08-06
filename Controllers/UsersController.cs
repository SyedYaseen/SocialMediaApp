using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.DTOs;
using SocialMediaApp.Entities;
using SocialMediaApp.Interfaces;

namespace SocialMediaApp.Controllers;

[Authorize]
public class UsersController : BaseAPIController
{
    private DataContext _context;
    private IUserRepository _userRepository;

    public UsersController(DataContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> Users()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<Memberdto>> GetUser(string username)
    {
        return await _userRepository.GetUserAsync(username);
    }
}