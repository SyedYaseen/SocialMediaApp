using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.DTOs;
using SocialMediaApp.Interfaces;

namespace SocialMediaApp.Repo;


class UserRepository : IUserRepository
{
    private DataContext _context;
    private IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<Memberdto>> GetUsersAsync()
    {
        return await _context.Users
            .ProjectTo<Memberdto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<Memberdto> GetUserAsync(string username)
    { 
        return await _context.Users.Where(item => item.UserName == username)
            .ProjectTo<Memberdto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public void Update(Memberdto user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
