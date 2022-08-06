using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Entities;
namespace SocialMediaApp.Data;

public class SeedData
{
    public static async Task GetSeedData(DataContext context)
    {
        if(await context.Users.AnyAsync()) return;
        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData)!;
        Console.WriteLine(users);

        foreach (var user in users)
        { 
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();

            user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password"));
            user.PasswordSalt = hmac.Key;
            
            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }
}