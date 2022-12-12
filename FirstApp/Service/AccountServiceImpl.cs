using FirstApp.Entity;

namespace FirstApp.Service;

public class AccountServiceImpl : AccountService
{
    private readonly Dictionary<int, Account> _accounts = new Dictionary<int, Account>();
    public Account AddNewAccount(Account account)
    {
        _accounts.Add(account.Id, account);
        return account;
    }

    public List<Account> GetAllAccounts()
    {
        return _accounts.Values.ToList();
    }

    public Account GetAccountById(int id)
    {
        return _accounts[id];
    }

    public List<Account> GetDebitedAccounts()
    {
        return _accounts.Values.Where(acc => acc.Balance < 0).ToList();
    }

    public double GetBalanceAVG()
    {
        return _accounts.Values.Average(acc => acc.Balance);
    }
}