using System.Text;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password)); 
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        return hashedPassword == HashPassword(providedPassword);
    }
}
