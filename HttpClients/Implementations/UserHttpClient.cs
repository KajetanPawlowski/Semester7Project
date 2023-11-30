using System.Text.Json;
using Domain.Model;
using HttpClients.Interfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserHttpClient
{
    private readonly HttpClient client;
    private readonly IAuthHttpClient authHttpClient;

    public UserHttpClient(HttpClient client,  IAuthHttpClient authHttpClient)
    {
        this.client = client;
        this.authHttpClient = authHttpClient;
    }
    public async Task<List<string>> GetUsersMailsAsync()
    {
        List<string> mails = new List<string>();
        foreach (User user in await GetUsersAsync())
        {
            mails.Add(user.Mail);
        }

        return mails;
    }
    
    
    //get User Object - pass it the app without Passwords!
    public async Task<List<User>> GetUsersAsync()
    {
        HttpResponseMessage response = await client.GetAsync("/User");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<User> users = JsonSerializer.Deserialize<List<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
}