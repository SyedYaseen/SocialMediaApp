using SocialMediaApp.DTOs;

namespace SocialMediaApp.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<Memberdto>> GetUsersAsync();
    public Task<Memberdto> GetUserAsync(string Username);
    public void Update(Memberdto user);
    public Task<bool> SaveChangesAsync();
}

