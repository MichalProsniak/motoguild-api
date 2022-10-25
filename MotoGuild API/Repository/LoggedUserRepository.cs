using System.Security.Claims;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Repository;

public class LoggedUserRepository : ILoggedUserRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private bool disposed;

    public LoggedUserRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public string GetLoggedUserName()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext != null)
        {
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        return result;
    }
}