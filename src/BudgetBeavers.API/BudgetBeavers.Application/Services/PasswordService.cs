using BudgetBeavers.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BudgetBeavers.Application.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<object> _passwordHasher = new();
    
    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null!, hashedPassword, hashedPassword);
        return result == PasswordVerificationResult.Success;
    }
}