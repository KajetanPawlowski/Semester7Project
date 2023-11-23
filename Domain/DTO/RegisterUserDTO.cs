namespace Domain.DTO;

public class RegisterUserDTO
{
    public string UserMail { get; set; }
    public string? Password { get; set; }
    public string FullName { get; set; }
}