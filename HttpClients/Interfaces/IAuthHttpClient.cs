using System.Security.Claims;
using Domain.DTO;
using Domain.Model;

namespace HttpClients.Interfaces;

public interface IAuthHttpClient
{
    //Using User Logic
    public Task LoginAsync(string userMail, string password);
    public Task LogoutAsync(string userMail);
    public Task<User> RegisterAsync(RegisterUserDTO dto);
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public string? Jwt { get; }
}