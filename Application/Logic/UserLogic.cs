using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTO;
using Domain.Model;
using MailKit.Security;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO _userDao;
    private const string DEFAULT_PASSWORD = "pass";
    public UserLogic(IUserDAO userDao)
    {
        _userDao = userDao;
    }
    public async Task<User> RegisterUserAsync(RegisterUserDTO dto)
    {
        User? existing = await _userDao.GetByMailAsync(dto.UserMail);
        if (existing != null)
            throw new Exception("UserMail already in used!");
        
        ValidateRegistrationData(dto);
        User toCreate = new User 
        {
            Mail = dto.UserMail,
            Password = DEFAULT_PASSWORD,
            FullName = dto.FullName,
            Role = "woltSupplier"
            
        };
    
        User created = await _userDao.CreateAsync(toCreate);
    
        return created;
    }

    public async Task<User> ValidateUserAsync(UserLoginDTO dto)
    {
        User? existingUser = await _userDao.GetByMailAsync(dto.UserMail);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(dto.Password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }

    public Task<User?> GetByIdAsync(int userId)
    {
        return _userDao.GetByIdAsync(userId);
    }

    public Task<User?> GetByUserMailAsync(string userMail)
    {
        return _userDao.GetByMailAsync(userMail);
    }

    public Task<List<User>> GetUsersAsync()
    {
        return _userDao.GetAllAsync();
    }

    public async Task<User> NotifySupplier(NotifySupplierDTO dto)
    {
        User user = await _userDao.GetByIdAsync(dto.userId);
        // Sender's email address and credentials
        string senderEmail = "woltsusteam7@gmail.com";
        string senderPassword = "wryh rjmb wwbp fone";

        string subject = "Login Information";
        string body = $"Dear {user.FullName},\n\nHere is your login information:\n\n" +
                      $"Username: {user.Mail}\n" +
                      $"Password: {user.Password}\n\n" +
                      $"Click the link below to log in:\n" +
                      "http://localhost:5145/";

        // Create and configure the email message using MimeKit
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Wolt Denmark", senderEmail));
        emailMessage.To.Add(new MailboxAddress(user.FullName, user.Mail));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = body;
        emailMessage.Body = bodyBuilder.ToMessageBody();

        // Create and configure the SMTP client using MimeKit
        using (var client = new SmtpClient())
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            // Disable certificate validation
            
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }

        Console.WriteLine("Email sent now successfully!");

        return user;
    }

    private static void ValidateRegistrationData(RegisterUserDTO userToCreate)
    {
        string userMail = userToCreate.UserMail;

        if (userMail.Length < 3)
            throw new Exception("User mail must be at least 3 characters!");
        
    }
}