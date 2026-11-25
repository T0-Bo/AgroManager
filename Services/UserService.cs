using AgroManager.Data;
using AgroManager.Models;
using Microsoft.AspNetCore.Identity;
using AgroManager.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AgroManager.Services;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        AppDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }


    public async Task<User?> CreateEmployeeAsync(
    string name,
    string password)
    {
        name = name.Trim();

        bool nameExists = await _context.Users
            .AnyAsync(u => u.Name == name);

        if (nameExists)
            return null;

        var user = new User
        {
            Name = name,
            Role = UserRole.Employee,
            Activo = true
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            password
        );

        _context.Users.Add(user);

        try
        {
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is Microsoft.Data.Sqlite.SqliteException sqliteException
                && sqliteException.SqliteExtendedErrorCode == 2067)
        {
            return null;
        }
    }


    public async Task<User?> InitializeAdminAsync(
    string name,
    string password)
    {

        name = name.Trim();
        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable
            );

        bool usersExist = await _context.Users.AnyAsync();

        if (usersExist)
        {
            await transaction.CommitAsync();
            return null;
        }

        var admin = new User
        {
            Name = name,
            Role = UserRole.Admin
        };

        admin.PasswordHash = _passwordHasher.HashPassword(
            admin,
            password
        );

        _context.Users.Add(admin);

        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        return admin;
    }


    public async Task<User?> AuthenticateAsync(
        string name,
        string password)
    {

        name = name.Trim();
        
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Name == name);

        if (user is null || !user.Activo)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password
        );

        if (result == PasswordVerificationResult.Failed)
            return null;

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                password
            );

            await _context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> DeactivateEmployeeAsync(int userId)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Id == userId);

        if (user is null || user.Role != UserRole.Employee)
            return false;

        user.Activo = false;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userId);
    }
}