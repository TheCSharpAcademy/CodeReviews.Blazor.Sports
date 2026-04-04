using Microsoft.AspNetCore.Identity;
using SportsStatistics.Application.Models;

namespace SportsStatistics.Authorization.Entities;

public class ApplicationUser : IdentityUser
{
    internal ApplicationUserDto ToDto()
    {
        return new(Id, UserName ?? string.Empty, Email ?? string.Empty);
    }
}
