﻿using CCR.Domain.Entities;
using CCR.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CCR.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public Task<bool> ExistsAsync(string email)
    {
        return _context.Users.AnyAsync(u => u.Email == email);
    }
}
