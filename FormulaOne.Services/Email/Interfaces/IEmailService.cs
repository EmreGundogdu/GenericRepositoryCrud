namespace FormulaOne.Services.Email.Interfaces;

public interface IEmailService
{
    void SendEmail(string name);
    void SendGettingStartedEmail(string email,string name);
}