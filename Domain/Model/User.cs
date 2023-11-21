namespace Domain.Model;

public class User
{
    public int Id { get; set; }
    //mail used as a unique id
    public string Mail { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
}