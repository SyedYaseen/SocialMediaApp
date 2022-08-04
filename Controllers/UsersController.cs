﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Entities;

namespace SocialMediaApp.Controllers;

public class UsersController : BaseAPIController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }
    
    // GET
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> Users()
    {
        return await _context.Users.ToListAsync();
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    } 
}