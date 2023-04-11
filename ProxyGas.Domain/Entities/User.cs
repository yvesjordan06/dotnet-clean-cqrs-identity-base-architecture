namespace ProxyGas.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public  string Password { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; } = string.Empty;
    
    public User(string email, string password, string firstName)
    {
        this.Email = email;
        //This is a bad practice, but for the sake of simplicity, we will do it this way
        //In a real world scenario, we would use a hashing algorithm to hash the password
        this.Password = password + "salt";
        this.FirstName = firstName;
    }
}