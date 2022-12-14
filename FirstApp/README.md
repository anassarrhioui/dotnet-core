
## Class Account
```csharp
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
```

## Interface AccountService
```csharp
public interface AccountService
{
    public Account AddNewAccount(Account account);
    public List<Account> GetAllAccounts();
    public Account GetAccountById(int id);
    public List<Account> GetDebitedAccounts();
    public double GetBalanceAVG();
}

```

## Class AccountServiceImpl
```csharp
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

```