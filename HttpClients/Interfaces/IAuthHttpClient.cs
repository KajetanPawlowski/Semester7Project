using System.Security.Claims;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface IAuthHttpClient
{
    public Task LoginAsync(string userMail, string password);
    public Task LogoutAsync(string userMail);
    public Task RegisterAsync(string userMail, string password);
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public string? Jwt { get; }
}