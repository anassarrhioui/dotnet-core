using FirstApp.Entity;

namespace FirstApp.Service;

public interface AccountService
{
    public Account AddNewAccount(Account account);
    public List<Account> GetAllAccounts();
    public Account GetAccountById(int id);
    public List<Account> GetDebitedAccounts();
    public double GetBalanceAVG();
}

