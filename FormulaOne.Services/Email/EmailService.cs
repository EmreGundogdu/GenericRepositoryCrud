using FormulaOne.Services.Email.Interfaces;

namespace FormulaOne.Services.Email;

public class EmailService:IEmailService
{
    public void SendEmail(string name)
    {
        Console.WriteLine($"Sending email to {name}");
    }

    public void SendGettingStartedEmail(string email, string name)
    {
        Console.WriteLine("Getting started");
    }
}