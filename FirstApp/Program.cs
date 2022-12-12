using FirstApp.Entity;
using FirstApp.Service;

namespace FirstApp;

public class Program
{
    public static void Main(string[] args)
    {
        AccountService accountService = new AccountServiceImpl();

        accountService.AddNewAccount(new Account(1, 1000, "DH"));
        accountService.AddNewAccount(new Account(2, 2000, "DH"));
        accountService.AddNewAccount(new Account(3, 3000, "DH"));
        accountService.AddNewAccount(new Account(4, 4000, "DH"));
        
        accountService.GetAllAccounts().ForEach(Console.WriteLine);
    }
}