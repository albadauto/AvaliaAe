namespace AvaliaAe.Helpers.Interfaces
{
    public interface IEmail
    {
        bool SendMail(string email, string subject, string message);


    }
}
