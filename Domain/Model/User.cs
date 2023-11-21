namespace Domain.Model;

public class User
{
    public int Id { get; set; }
    //mail used as a unique id
    public string mail { get; set; }
    public string password { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
}