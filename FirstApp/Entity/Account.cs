namespace FirstApp.Entity;

public class Account
{
    public int Id { get; set; }
    public int Balance { get; set; }
    public string Currency { get; set; }

    public Account()
    {
    }

    public Account(int id, int balance, string currency)
    {
        Id = id;
        Balance = balance;
        Currency = currency;
    }
}